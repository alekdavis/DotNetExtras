// Ignore Spelling: Auth json timestamp

using DotNetExtras.Common.Extensions;
using System.Text.Json.Serialization;

namespace DotNetExtras.Web.Client;

/// <summary>
/// Represents error object returned from a failed authorization request.
/// </summary>
public class OAuthError
{
    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="OAuthError"/> class
    /// from the JSON response sent by the authorization server.
    /// </summary>
    /// <param name="content">
    /// JSON response.
    /// </param>
    /// <remarks>
    /// <para>
    /// The trick here is to be able to interpret the returned JSON and map it to the proper properties.
    /// It is not simple because providers can rely on custom properties or not conform to the 
    /// RFC 6749 at all (I'm looking at you Apigee).
    /// So we need to add some smart fallback logic when looking for error properties.
    /// </para>
    /// <para>
    /// The following is an error returned by Azure:
    /// </para>
    /// <code language="JavaScript">
    /// <![CDATA[
    /// {
    ///   "error":"invalid_request",
    ///   "error_description":"AADSTS90014: The required field 'Scope' is missing from the credential. Ensure that you have all the necessary parameters for the login request.\r\nTrace ID: 589d109d-9088-4fc2-9285-b611cb088900\r\nCorrelation ID: 81839674-4732-4fd6-ab8a-49e884576106\r\nTimestamp: 2022-09-28 23:58:57Z",
    ///   "error_codes":[90014],
    ///   "timestamp":"2022-09-28 23:58:57Z",
    ///   "trace_id":"589d109d-9088-4fc2-9285-b611cb088900",
    ///   "correlation_id":"81839674-4732-4fd6-ab8a-49e884576106",
    ///   "error_uri":"https://login.microsoftonline.com/error?code=90014"
    /// }
    /// ]]>
    /// </code>
    /// <para>
    /// The following is an error returned by Apigee (and, yes, the response includes unescaped new lines, which should not be allowed):
    /// </para>
    /// <code language="JavaScript">
    /// <![CDATA[
    /// {
    ///   "errorMessage": "invalid_grant",
    ///   "errorReason":"AADSTS501051: Application '8502f0e8-0773-4e8e-b4b6-f5a8d9db179f'(IAM-Microservices-Azure-Group.All-TestClient_IAP35777) is not assigned to a role for the application 'api://8502f0e8-0773-4e8e-b4b6-f5a8d9db179f'(IAM-Microservices-Azure-Group.All-TestClient_IAP35777).
    ///    Trace ID: c47bf852-88a9-4111-9168-8573cf027400
    ///    Correlation ID: 0a02992c-2841-47ef-b239-0b86f6fbbe2f
    ///    Timestamp: 2022-09-28 22:58:25Z",
    ///    "errorStatus":401
    /// }
    /// ]]>
    /// </code>
    /// </remarks>
    public OAuthError
    (
        string content
    )
    {
        if (content == null)
        {
            return;
        }

        Content = content.Trim();
        Raw? raw = null;
        
        try
        {
            if (Content.StartsWith('{') || Content.StartsWith('['))
            {
                raw = content.FromJson<Raw>();
            }
        }
        catch
        {
        }

        if (raw != null)
        {
            Error = raw.error ?? raw.errorMessage;

            Description = raw.error_description ?? raw.errorReason ?? raw.message;

            Code = raw.code ?? raw.errorStatus ?? raw.status;

            if (Code == null && raw.error_codes != null && raw.error_codes.Length > 0)
            {
                Code = raw.error_codes[0];
            }
        }
    }
    #endregion

    #region Public properties
    /// <summary>
    /// HTTP response content string.
    /// </summary>
    [JsonPropertyName("content")]
    public string? Content { get; private set; }

    /// <summary>
    /// Returns the string OAuth error code value.
    /// </summary>
    /// <value>
    /// The OAuth error code.
    /// </value>
    [JsonPropertyName("error")]
    public string? Error { get; private set; }

    /// <summary>
    /// Returns the OAuth error description.
    /// </summary>
    /// <value>
    /// The OAuth error description.
    /// </value>
    [JsonPropertyName("description")]
    public string? Description { get; private set; }

    /// <summary>
    /// Returns the numeric OAuth error code value.
    /// </summary>
    /// <value>
    /// The OAuth error code (if the error contains multiple codes, the first one will be returned).
    /// </value>
    [JsonPropertyName("code")]
    public int? Code { get; private set; }
    #endregion

    #region Private class    
    /// <summary>
    /// This class combines JSON properties of error objects returned by common authorization servers,
    /// some of with comply with RFC 6749 and some are custom properties.
    /// </summary>
    #pragma warning disable IDE1006 // Naming Styles
    private class Raw
    {
        #region Azure error properties
        public string? error { get; set; }

        public string? error_description { get; set; }

        public int? code { get; set; }

        public int[]? error_codes { get; set; }

        public DateTime? timestamp { get; set; }

        public string? trace_id { get; set; }

        public string? correlation_id { get; set; }

        public string? error_uri { get; set; }
        #endregion

        #region Apigee error properties
        public string? errorMessage { get; set; }

        public string? errorReason { get; set; }

        public int? errorStatus { get; set; }
        #endregion

        #region Other possible properties
        public string? message { get; set; }

        public int? status { get; set; }
        #endregion
    }
    #pragma warning restore IDE1006 // Naming Styles
    #endregion
}
