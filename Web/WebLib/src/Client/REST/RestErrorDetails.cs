using Flurl.Http;
using System.Net;

namespace DotNetExtras.Web.Client;

/// <summary>
/// Encapsulates error information specific to REST API calls.
/// </summary>
public class RestErrorDetails
{
    #region Public properties
    /// <summary>
    /// Returns error object received from the endpoint as a string.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Returns HTTP status code received from the endpoint.
    /// </summary>
    public HttpStatusCode? StatusCode { get; set; }

    /// <summary>
    /// Returns the original <see cref="FlurlHttpException"/>.
    /// </summary>
    public FlurlHttpException? SourceException { get; set; }

    /// <summary>
    /// Rest error info.
    /// </summary>
    public RestError? Error { get; set; }
    #endregion
}
