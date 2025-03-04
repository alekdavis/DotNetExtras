namespace DotNetExtras.Web.Client;

/// <summary>
/// Encapsulates error information specific to REST API calls including strongly typed data returned.
/// </summary>
public class GenericRestErrorDetails<T>: RestErrorDetails
{
    #region Public properties
    /// <summary>
    /// Returns error object received from the endpoint.
    /// </summary>
    public T? ContentData { get; set; }
    #endregion
}
