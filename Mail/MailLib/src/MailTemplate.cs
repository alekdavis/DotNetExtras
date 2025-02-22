using DotNetExtras.Common.Extensions;
using HtmlAgilityPack;
using RazorLight;
using RazorLight.Caching;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace DotNetExtras.Mail;

/// <summary>
/// Creates email message body and subject by merging a translated Razor template file with the provided data structure.
/// </summary>
/// <remarks>
/// <para>
/// The merge process makes these assumptions and conforms to the following conventions:
/// 
/// - A template file is named as follows: TEMPLATEID_LANGUAGE-CODE.EXTENSION.
/// - The template file is located in the specified folder.
/// - The template file is in HTML format.
/// - The template file contains a title element that will be used as the email subject.
/// - The template file contains a body element that will be used as the email body.
/// - The template file can contain Razor syntax for data binding.
/// - The template file can contain special characters that need to be escaped.
/// - The template file can contain special elements that need to be handled.
/// - The template file can contain localized versions mapped to different languages.
/// - The template file can contain a default language version.
/// - The specified language code must match the language code suffix in the file name.
/// - The specified language code can be a fallback language if the preferred language is not found.
/// - If a more specific language code (e.g. 'es-mx') is not implemented for the specified template, a more generic language code (e.g. 'es') will be tried.
/// - A language map can be used to map non-standard language codes to standard language codes.
/// - The constructor parameters can be used to set the default language, default template file extension, language separator, sub-language separator, and language map.
/// </para>
/// </remarks>
/// <example>
/// Zodiac_en-us.html file:
/// <code language="html">
/// &lt;!DOCTYPE html&gt;
/// &lt;html lang="en"&gt;
/// &lt;head&gt;
/// &lt;title&gt;Welcome @Raw(Model.Zodiac)!&lt;/title&gt;
/// &lt;meta charset="utf-8"&gt;
/// &lt;/head&gt;
/// &lt;body&gt;
/// &lt;p&gt;Hello @Raw(Model.Name),&lt;/p&gt;
/// &lt;p&gt;
/// Your Zodiac sign is: @Raw(Model.Zodiac).
/// &lt;/p&gt;
/// &lt;p&gt;
/// &amp;copy; @Model.Year | &lt;a href="#"&gt;Terms&lt;/a&gt; | &lt;a href="#"&gt;Privacy&lt;/a&gt; | &lt;a href="#"&gt;Unsubscribe&lt;/a&gt;
/// &lt;/p&gt;
/// &lt;/body&gt;
/// &lt;/html&gt;
/// </code>
/// 
/// C# code:
/// <code>
/// MailTemplate template = new();
///
/// Data data = new()
/// {
///     Zodiac = "Leo",
///     Name = "John",
///     Year = 2025
/// };
/// 
/// MailTemplate message = template.Merge("Zodiac", "Samples/Zodiac", "en-US", data, ".html");
/// 
/// // Subject will hold the merged value of the title element.
/// string subject = message.Subject;
/// 
/// // Body will hold the merged value of the body element.
/// string body = message.Body;
/// </code>
/// </example>
public partial class MailTemplate: IMailMessage
{
    #region Private properties
    // Default language if localized version is not supported.
    private readonly string? _defaultLanguage = null;

    // Default template file extension. 
    private readonly string? _defaultTemplateFileExtension = null;

    // Separates template ID from language code in template file name, such as "NewAccountActivation-en".
    private readonly string? _languageSeparator = null;

    // Separates language code parts, such as in "en_US".
    private readonly string? _subLanguageSeparator = null;

    // Map of non-standard language fallbacks.
    private readonly Dictionary<string, string>? _languageMap = null;

    // Localization language.
    private string? _language = null;

    // Path to the template file.
    private string? _path = null;

    // Cache key for the localized template.
    private string? _key = null;

    // Cache localized template keys.
    private static readonly ConcurrentDictionary<string, string> _cachedKeys = new();

    // Cache localized template file paths.
    private static readonly ConcurrentDictionary<string, string> _cachedPaths = new();

    // Cache languages.
    private static readonly ConcurrentDictionary<string, string> _cachedLanguages = new();

    // Cache localized template text values.
    private static readonly ConcurrentDictionary<string, string> _cachedTemplates = new();

    // Used to multi-threaded synchronization and Razor engine initialization.
    private static readonly object _razorLock = new();

    // Need semaphore for Razor operation because lock cannot be used in async call.
    private static readonly SemaphoreSlim _razorSemaphore = new(1, 1);

    // Handles merges (needs to be static so it can use in-memory caching).
    private static RazorLightEngine? _razorEngine = null;
    #endregion

    #region Public properties
    /// <summary>
    /// Returns the localized HTML email template.
    /// </summary>
    public string? Template { get; private set; } = null;

    /// <summary>
    /// Returns the email HTML message body (after data transformation).
    /// </summary>
    public string? Body { get; private set; } = null;

    /// <summary>
    /// Returns the email HTML message subject from the title element (after data transformation).
    /// </summary>
    public string? Subject { get; private set; } = null;

    /// <summary>
    /// Returns HTML template language in a pretty format, such as "xx-YY-ZZ".
    /// </summary>
    /// <remarks>
    /// This property is only needed for returning the applied language code.
    /// </remarks>
    public string? Language
    {
        get
        {
            if (!string.IsNullOrEmpty(_language))
            {
                string language = _language.Replace('_', '-');

                int index = language.IndexOf('-');

                if (index > 0 && index < language.Length)
                {
                    string left = language[..index];
                    string right = language[index..];

                    language = left.ToLower() + right.ToUpper();
                }

                return language.TrimStart('-').TrimEnd('-');
            }

            return _language;
        }
    }

    /// <summary>
    /// Indicates whether the pre-compiled template was loaded from the Razor engine's memory cache.
    /// </summary>
    public bool Cached
    {
        get; private set;
    }
    #endregion

    #region Constructors    
    /// <summary>
    /// Initializes a new instance of the <see cref="MailTemplate"/> class.
    /// </summary>
    /// <param name="defaultLanguage">
    /// Default language code.
    /// </param>
    /// <param name="defaultTemplateFileExtension">
    /// Default template file extension.
    /// </param>
    /// <param name="languageMap">
    /// Non-standard mapping of language code fallbacks.
    /// </param>
    /// <param name="languageSeparator">
    /// Separates template ID from language code in template file name, such as "NewAccountActivation-en".
    /// </param>
    /// <param name="subLanguageSeparator">
    /// Separates language code parts, such as in "en_US".
    /// </param>
    public MailTemplate
    (
        string? defaultLanguage = "en-US",
        string? defaultTemplateFileExtension = ".html",
        string? languageSeparator = "_",
        string? subLanguageSeparator = "-",
        Dictionary<string, string>? languageMap = null
    )
    {
        _defaultLanguage = defaultLanguage;
        _defaultTemplateFileExtension = defaultTemplateFileExtension;
        _languageMap = languageMap;
        _languageSeparator = languageSeparator;
        _subLanguageSeparator = subLanguageSeparator;

        Cached = false;

        InitializeRazor();
    }
    #endregion

    #region Public methods
    /// <summary>
    /// Merges the email template with the specified data for the 
    /// preferred language or the fallback language.
    /// </summary>
    /// <param name="templateId">
    /// Template identifier that will be used as a file name.
    /// </param>
    /// <param name="templateFolderPath">
    /// Template folder path (can be relative or absolute).
    /// </param>
    /// <param name="language">
    /// Preferred template language code.
    /// </param>
    /// <param name="data">
    /// Notification data that will be merged with template to generate message.
    /// </param>
    /// <param name="templateFileExtension">
    /// Extension of the template file. 
    /// If not specified, the default value set by the constructor will be used. 
    /// </param>
    /// <returns>
    /// Template merged with data in the specified or fallback language.
    /// </returns>
    public MailTemplate Merge
    (
        string templateId,
        string templateFolderPath,
        string language,
        object data,
        string? templateFileExtension = null
    )
    {
        // If we have the language map,
        // translate the preferred language code to the mapped value.
        if (!string.IsNullOrEmpty(language) && _languageMap != null && _languageMap.ContainsKey(language))
        {
            language = _languageMap[language];
        }

        List<string> languages = GetCompatibleLanguages(NormalizeLanguage(language));

        // Cache key for the preferred language.
        string? originalKey = FormatKey(templateId, language);

        string key;

        foreach (string superLanguage in languages)
        {
            // Generate cache key for the template and language.
            key = FormatKey(templateId, superLanguage);

            // See if this language key is already mapped in cache.
            if (_cachedKeys.ContainsKey(key))
            {
                key = _cachedKeys[key];
            }

            // Try getting template file path from the cache.
            if (_cachedPaths.ContainsKey(key))
            {
                _key = key;
                _language = _cachedLanguages[key];
                _path = _cachedPaths[key];

                // If the preferred language is not mapped in cache,
                // map it to the found language.
                if (!_cachedKeys.ContainsKey(originalKey))
                {
                    _cachedKeys[originalKey] = key;
                }

                break;
            }
            // Template file path is not in the cache...
            else
            {
                string? extension = templateFileExtension ?? _defaultTemplateFileExtension;

                extension ??= "";

                // Get file path for this language.
                string path = FormatPath(
                    templateFolderPath,
                    templateId,
                    superLanguage,
                    extension);

                if (File.Exists(path))
                {
                    _key = key;
                    _language = superLanguage;
                    _path = path;

                    _cachedPaths[key] = _path;
                    _cachedLanguages[key] = _language;

                    // If the preferred language is not mapped in cache,
                    // map it to the found language.
                    if (!_cachedKeys.ContainsKey(originalKey))
                    {
                        _cachedKeys[originalKey] = key;
                    }

                    break;
                }
            }
        }

        if (_path == null)
        {
            throw new Exception(
                $"Cannot find HTML mail template '{templateId}' for language '{language}'.");
        }

        if (_key == null)
        {
            throw new Exception(
                $"Cannot determine key to identify mail template '{templateId}' for language '{language}'.");
        }

        // Try getting template from cache.
        if (_cachedTemplates.ContainsKey(_key))
        {
            Template = _cachedTemplates[_key];
        }
        // Template has not been cached, yet...
        else
        {
            string text;

            try
            {
                text = File.ReadAllText(_path);
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot read text from the mail template file '{_path}'.", ex);
            }

            // Get template from file and handle non-Razor @ characters.
            Template = NormalizeTemplate(text);

            _cachedTemplates[_key] = Template;
        }

        try
        {
            // If we have notification data, merge them with the template.
            if (data != null)
            {
                Body = Merge(_key, Template, data);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Cannot merge mail template with supplied data '{data.ToJson()}'.", ex);
        }

        // Retrieve email notification subject from the title tag.
        if (!string.IsNullOrEmpty(Body))
        {
            HtmlDocument htmlDoc = new();

            try
            {
                // See: https://html-agility-pack.net/from-string
                htmlDoc.LoadHtml(Body);
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot load HTML content from the mail template in '{_path}'.", ex);
            }

            HtmlNode? htmlNode = null;

            try
            {
                // Get the HTML title node.
                htmlNode = htmlDoc.DocumentNode.SelectSingleNode("//title");
            }
            catch
            {
                // Not a catastrophic error (maybe the title was not intended).
            }

            if (htmlNode != null)
            {
                string title = htmlNode.InnerText;

                if (title != null)
                {
                    Subject = RegexRepeatedSpaceChars().Replace(title, " ").Trim();
                }
            }
        }

        return this;
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Merges the template with data.
    /// </summary>
    /// <param name="key">
    /// Key identifying localized template.
    /// </param>
    /// <param name="template">
    /// Template text.
    /// </param>
    /// <param name="data">
    /// Notification data.
    /// </param>
    /// <returns>
    /// Notification message body.
    /// </returns>
    private string Merge
    (
        string key,
        string template,
        object data
    )
    {
        Task<string> task = MergeAsync(
            key,
            template,
            data);

        try
        {
            task.Wait();
        }
        catch
        {
            throw;
        }

        return task.Result;
    }

    /// <inheritdoc cref="MergeAsync(string, string, object)" path="param|returns"/>
    /// <summary>
    /// Asynchronous method merging template with data.
    /// </summary>
    private async Task<string> MergeAsync
    (
        string key,
        string template,
        object data
    )
    {
        InitializeRazor();

        TemplateCacheLookupResult? cacheResult = null;

        lock (_razorLock)
        {
#pragma warning disable CS8602
            cacheResult = _razorEngine.Handler.Cache.RetrieveTemplate(key);
#pragma warning restore CS8602
        }

        string result;

        await _razorSemaphore.WaitAsync();
        try
        {
            if (cacheResult != null && cacheResult.Success)
            {
                Cached = true;
                ITemplatePage templatePage = cacheResult.Template.TemplatePageFactory();

                result = await _razorEngine.RenderTemplateAsync(templatePage, data);
            }
            else
            {
                Cached = false;
                result = await _razorEngine.CompileRenderStringAsync(key, template, data);
            }
        }
        finally
        {
            _ = _razorSemaphore.Release();
        }

        return result;
    }

    /// <summary>
    /// Implements special handling of certain template elements.
    /// </summary>
    /// <param name="template">
    /// Template text.
    /// </param>
    /// <returns>
    /// Normalized template.
    /// </returns>
    private static string NormalizeTemplate
    (
        string template
    )
    {
        // If @media element is already escaped, nothing else to do.
        if (template.Contains("@@media", StringComparison.InvariantCultureIgnoreCase))
        {
            return template;
        }

        // Escape @media element.
        return template.Replace("@media", "@@media", StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// Takes a language code and returns a list of all relevant languages 
    /// including the original, the fallbacks (if any) and the default.
    /// </summary>
    /// <param name="language">
    /// The preferred language code.
    /// </param>
    /// <returns>
    /// List of possible alternatives.
    /// </returns>
    /// <example>
    /// en-AU: en-AU, en, en-US.
    /// en-EN: en-EN, en, en-US.
    /// es-MX: ex-MX, es, en-US.
    /// </example>
    private List<string> GetCompatibleLanguages
    (
        string language
    )
    {
        language = NormalizeLanguage(language);

        List<string> locales = [language];

        if (!string.IsNullOrEmpty(_subLanguageSeparator))
        {
            while (language.Contains(_subLanguageSeparator))
            {
                int idx = language.LastIndexOf(_subLanguageSeparator);

                if (idx != -1)
                {
                    // Get part of the string before last underscore.
                    language = language[..idx];

                    locales.Add(language);
                }
            }
        }

        string defaultLanguage = NormalizeLanguage(_defaultLanguage);

        if (!locales.Contains(defaultLanguage))
        {
            locales.Add(NormalizeLanguage(defaultLanguage));
        }

        return locales;
    }

    /// <summary>
    /// Removes dashes and underscores from the language code.
    /// </summary>
    /// <param name="value">
    /// The original language code.
    /// </param>
    /// <returns>
    /// The compacted language code value.
    /// </returns>
    private static string Compact
    (
        string value
    )
    {
        return value
            .Replace("-", "")
            .Replace("_", "");
    }

    /// <summary>
    /// Generates the cache key for the specified language code and email template..
    /// </summary>
    /// <param name="templateId">
    /// The template identifier.
    /// </param>
    /// <param name="language">
    /// The language code.
    /// </param>
    /// <returns>
    /// The cache key.
    /// </returns>
    private string FormatKey
    (
        string templateId,
        string language
    )
    {
        language = Compact(NormalizeLanguage(language)).ToUpper();

        string idValue = Compact(templateId).ToUpper();

        return $"{idValue}{language}";
    }

    /// <summary>
    /// Generates the path to the email template file.
    /// </summary>
    /// <param name="templateFolderPath">
    /// The template folder path.
    /// </param>
    /// <param name="templateId">
    /// The template identifier.
    /// </param>
    /// <param name="language">
    /// The language code.
    /// </param>
    /// <param name="extension">
    /// The file extension.
    /// </param>
    /// <returns>
    /// The file path.
    /// </returns>
    private string FormatPath
    (
        string templateFolderPath,
        string templateId,
        string language,
        string extension
    )
    {
        string fileName = FormatFileNameWithExtension(templateId, language, extension);

        while (templateFolderPath.EndsWith('/'))
        {
            templateFolderPath = templateFolderPath.TrimEnd('/');
        }

        while (templateFolderPath.EndsWith('\\'))
        {
            templateFolderPath = templateFolderPath.TrimEnd('\\');
        }

        return Path.GetFullPath(Path.Combine(templateFolderPath, fileName));
    }

    /// <summary>
    /// Formats the name of the template file without the extension.
    /// </summary>
    /// <param name="templateId">
    /// The template identifier.
    /// </param>
    /// <param name="language">
    /// The language code.
    /// </param>
    /// <returns>
    /// The file name in the format: TEMPLATEID-LANGUAGE_CODE
    /// </returns>
    private string FormatFileName
    (
        string templateId,
        string language
    )
    {
        return string.IsNullOrEmpty(language) 
            ? templateId 
            : $"{templateId}{_languageSeparator}{language.ToLower()}";
    }

    /// <summary>
    /// Formats the name of the template file.
    /// </summary>
    /// <param name="templateId">
    /// The template identifier.
    /// </param>
    /// <param name="language">
    /// The language code.
    /// </param>
    /// <param name="extension">
    /// The file extension.
    /// </param>
    /// <returns>
    /// The file name in the format: TEMPLATEID-LANGUAGE_CODE.EXTENSION
    /// </returns>
    private string FormatFileNameWithExtension
    (
        string templateId,
        string language,
        string extension
    )
    {
        string? fileName = string.IsNullOrEmpty(language) ? templateId : FormatFileName(templateId, language);

#pragma warning disable IDE0011 // Add braces
        if (string.IsNullOrEmpty(extension))
            return fileName ?? "";
#pragma warning restore IDE0011 // Add braces

        return $"{fileName}{extension}";
    }

    /// <summary>
    /// Converts the language code with dashes to the language file name suffix 
    /// with underscores and in lower case.
    /// </summary>
    /// <param name="language">
    /// The language code, such as 'es-MX'.
    /// </param>
    /// <returns>
    /// The modified language code, such as 'es_mx'.
    /// </returns>
    /// <remarks>
    /// Converting the language code to lower case is important
    /// for case sensitive file systems, such as in Linux.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "<Pending>")]
    private string NormalizeLanguage
    (
        string? language = null
    )
    {
        if (string.IsNullOrWhiteSpace(language))
        {
            language = _defaultLanguage;
        }

        if (language == null)
        {
            return "";
        }

        return !string.IsNullOrEmpty(_languageSeparator) &&
               !string.IsNullOrEmpty(_subLanguageSeparator) &&
               !string.IsNullOrEmpty(language)
            ? language.ToLower().Replace(_languageSeparator, _subLanguageSeparator)
            : language.ToLower();
    }

    /// <summary>
    /// Initializes the Razor engine.
    /// </summary>
    private static void InitializeRazor()
    {
        lock (_razorLock)
        {
            if (_razorEngine == null)
            {
                try
                {
                    _razorEngine = new RazorLightEngineBuilder()
                        .UseNoProject()
                        .UseMemoryCachingProvider()
                        .Build();
                }
                catch (Exception ex)
                {
                    throw new Exception("Cannot initialize Razor engine: RazorLightEngineBuilder.UseNoProject().UseMemoryCachingProvider().Build() failed.", ex);
                }
            }
        }
    }

    [GeneratedRegex(@"\s+")]
    private static partial Regex RegexRepeatedSpaceChars();
    #endregion
}

