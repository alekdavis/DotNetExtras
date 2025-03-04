using Microsoft.Extensions.Configuration;

namespace DotNetExtras.Testing;
/// <summary>
/// Implements common helper methods for unit tests.
/// </summary>
public class BaseTests
{
    /// <summary>
    /// Hard codes the configuration settings necessary for completing unit tests.
    /// Pass 'new Dictionary&lt;string,string?&gt;{...}' into the method
    /// with the appropriate dictionary items.
    /// </summary>
    /// <param name="settings">
    /// The Dictionary&lt;string,string?&gt; object holding configuration settings.
    /// </param>
    /// <returns>
    /// Configuration object.
    /// </returns>
    /// <remarks>
    /// You can pass array items by appending zero-based index to the key as illustrated
    /// in the example (e.g."ServiceA:ArraySetting1:0", "ServiceA:ArraySetting1:1", etc.).
    /// See
    /// https://stackoverflow.com/questions/37825107/net-core-use-configuration-to-bind-to-options-with-array
    /// for details.
    /// </remarks>
    /// <example>
    /// <![CDATA[
    /// IConfiguration config = SetConfiguration(
    ///     new Dictionary<string,string?>
    ///     {
    ///         {"ServiceA:ValueSettingX", "ValueX"},
    ///         {"ServiceA:ValueSettingY", "ValueY"},
    ///         {"ServiceA:ValueSettingZ", "ValueZ"},
    ///         {"ServiceA:ArraySetting1:0", "Value0"},
    ///         {"ServiceA:ArraySetting1:1", "Value1"},
    ///         {"ServiceA:ArraySetting1:2", "Value2"},
    ///     }
    /// );
    /// ]]>
    /// </example>
    public static IConfiguration MockConfiguration
    (
        IEnumerable<KeyValuePair<string, string?>> settings
    )
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();

        return config;
    }
}

