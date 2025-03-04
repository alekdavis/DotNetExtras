// Ignore Spelling: Json
using Flurl.Http;
using Flurl.Http.Configuration;
using System.Net;

namespace DotNetExtras.Web.Client;

/// <summary>
/// Encapsulates error information from errors returned by SCIM services, Apigee, Azure, and OAuth.
/// </summary>
/// <seealso cref="ScimError" />
public static class RestClient
{
    /// <summary>
    /// Returns a rest client.
    /// </summary>
    /// <param name="url">
    /// Base or endpoint URL of a REST API.
    /// </param>
    /// <param name="proxy">
    /// Optional URL of a proxy server.
    /// </param>
    /// <returns>
    /// Initialized REST client.
    /// </returns>
    public static IFlurlClient Create
    (
        string url,
        string? proxy = null
    )
    {
        return string.IsNullOrEmpty(proxy)
            ? new FlurlClient(url)
            : new FlurlClientBuilder(url)
               .ConfigureInnerHandler(h => {
                   h.Proxy = new WebProxy(proxy);
                   h.UseProxy = true; })
               .Build();
    }
}
