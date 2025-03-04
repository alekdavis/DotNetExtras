// Ignore Spelling: Json

using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using DotNetExtras.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace DotNetExtras.Web.Client;

/// <summary>
/// Encapsulates error information from errors returned by SCIM services, Apigee, Azure, and OAuth.
/// </summary>
/// <seealso cref="ScimError" />
public class RestError: ScimError
{
    // Errors that are returned before endpoint is invoked can look like:
    //
    // {
    //  "errors": {
    //      "SubscriptionUrl": [
    //          "The SubscriptionUrl field is required."
    //      ]
    //  },
    //  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    //  "title": "One or more validation errors occurred.",
    //  "status": 400,
    //  "traceId": "00-56a1bd30f1c706363d1a8819b0c9d9dd-345e5e58de7987aa-00"
    // }

    #region Public properties    
    /// <summary>
    /// Returns raw content string.
    /// </summary>
    public string Content
    {
        get;
    }

    /// <summary>
    /// Indicates whether the content was successfully parsed into the error info.
    /// </summary>
    public bool IsParsed { get; private set; }
    #endregion

    #region Constructors    
    /// <summary>
    /// Initializes a new instance of the <see cref="RestError"/> class.
    /// </summary>
    /// <param name="content">
    /// JSON string or HTML of the response error.
    /// </param>
    public RestError
    (
        string content
    )
    {
        Content = content;

        OAuthError? oauthError = null;
        ScimErrors? scimErrors = null;
        ProblemDetails? problemDetails = null;

        try
        {
            problemDetails = content.FromJson<ProblemDetails>();
        }
        catch
        {
        }
        
        if (problemDetails != null)
        {
            Message = problemDetails.Detail ?? problemDetails.Title;

            if (problemDetails.Status.HasValue)
            {
                Code = problemDetails.Status.Value;
            }

            string key = "ServiceCode";
            if (problemDetails.Extensions.ContainsKey(key))
            {
                ServiceCode = problemDetails.Extensions[key]?.ToString();
            }

            key = "serviceCode";
            if (string.IsNullOrEmpty(ServiceCode) && problemDetails.Extensions.ContainsKey(key))
            {
                ServiceCode = problemDetails.Extensions[key]?.ToString();
            }

            key = "SourceSystemCode";
            if (problemDetails.Extensions.ContainsKey(key))
            {
                SourceSystemCode = problemDetails.Extensions[key]?.ToString();
            }

            key = "sourceSystemCode";
            if (string.IsNullOrEmpty(SourceSystemCode) && problemDetails.Extensions.ContainsKey(key))
            {
                SourceSystemCode = problemDetails.Extensions[key]?.ToString();
            }

            key = "SourceSystemMessage";
            if (problemDetails.Extensions.ContainsKey(key))
            {
                SourceSystemMessage = problemDetails.Extensions[key]?.ToString();
            }

            key = "sourceSystemMessage";
            if (string.IsNullOrEmpty(SourceSystemMessage) && problemDetails.Extensions.ContainsKey(key))
            {
                SourceSystemMessage = problemDetails.Extensions[key]?.ToString();
            }

            key = "RequestId";
            if (problemDetails.Extensions.ContainsKey(key))
            {
                RequestId = problemDetails.Extensions[key]?.ToString();
            }

            key = "requestId";
            if (string.IsNullOrEmpty(RequestId) && problemDetails.Extensions.ContainsKey(key))
            {
                RequestId = problemDetails.Extensions[key]?.ToString();
            }

            if (string.IsNullOrEmpty(Message) && !string.IsNullOrEmpty(SourceSystemMessage))
            {
                Message             = SourceSystemMessage;
                SourceSystemMessage = null;
            }
        }
        
        if (string.IsNullOrEmpty(Message))
        {
            try
            {
                scimErrors = content.FromJson<ScimErrors>();
            }
            catch
            {
            }

            if (scimErrors != null && scimErrors.Errors != null && scimErrors.Errors.Count > 0)
            {
                Code                = scimErrors.Errors[0].Code;
                Message             = scimErrors.Errors[0].Message;
                ServiceCode         = scimErrors.Errors[0].ServiceCode;
                SourceSystemCode    = scimErrors.Errors[0].SourceSystemCode;
                SourceSystemMessage = scimErrors.Errors[0].SourceSystemMessage;
                RequestId           = scimErrors.Errors[0].RequestId;
            }
            else
            {
                try
                {
                    oauthError = new OAuthError(content);
                }
                catch
                {
                }

                if (oauthError != null)
                {
                    if (oauthError.Code.HasValue)
                    {
                        Code = oauthError.Code.Value;
                    }

                    ServiceCode = oauthError.Error;

                    Message = oauthError.Description;
                }
            }
        }

        if (string.IsNullOrEmpty(Message))
        {
            // Finally, see if we can get error from HTML error page returned by the server.
            try
            {
                Message = SanitizeHtmlText(Content);
            }
            catch
            {
            }
        }

        if (string.IsNullOrEmpty(Message))
        {
            // If nothing worked, set message to content.
            Message = Content;
            IsParsed = false;
        }
        else
        {
            IsParsed = true;
        }
    }
    #endregion

    #region Private methods
    // 
    /// <summary>
    /// Strips HTML tags from text containing HTML leaving just text of the body element
    /// with no repeating spaces and trimmed on both ends.
    /// </summary>
    /// <param name="htmlText">
    /// HTML string.
    /// </param>
    /// <returns>
    /// Sanitized text.
    /// </returns>
    /// <remarks>
    /// https://stackoverflow.com/questions/70480208/convert-html-to-plain-text-using-c-sharp
    /// </remarks>
    public virtual string? SanitizeHtmlText
    (
        string? htmlText
    )
    {
        if (string.IsNullOrWhiteSpace(htmlText))
        {
            return null;
        }

        string? plainText = null;

        try
        {
            IConfiguration config;
            IBrowsingContext context;
            IHtmlParser? parser;

            // Use the default configuration for AngleSharp.
            config = AngleSharp.Configuration.Default;

            // Create a new context for evaluating webpages with the given config.
            context = BrowsingContext.New(config);

            // Create a parser to specify the document to load (here from our fixed string)
            parser = context.GetService<IHtmlParser>();

            if (parser != null)
            {
                htmlText = htmlText.Replace("><", "> <");
                IHtmlDocument document = parser.ParseDocument(htmlText);

                if (document != null) 
                {
                    plainText = Regex.Replace(
                        Regex.Replace(document?.Body?.TextContent ?? "", @"\r\n?|\n", " "), "\\s+", " ")
                            .Trim();
                }
            }
        }
        catch
        {
        }

        return string.IsNullOrWhiteSpace(plainText) ? null : plainText;
    }
    #endregion
}
