using System.Net;

namespace Ampeco.Sdk;

/// <summary>
/// Thrown when the AMPECO Public API returns an error response.
/// </summary>
public class AmpecoApiException : Exception
{
    /// <summary>HTTP status code returned by the API, or 0 when the failure was not an HTTP status.</summary>
    public int StatusCode { get; }

    /// <summary>Per-field validation errors (present on 422 responses).</summary>
    public IReadOnlyDictionary<string, IReadOnlyList<string>>? Errors { get; }

    /// <summary>The raw response body, when available.</summary>
    public string? ResponseBody { get; }

    /// <summary>Creates an exception describing an API error response.</summary>
    public AmpecoApiException(int statusCode, string message, string? responseBody = null,
        IReadOnlyDictionary<string, IReadOnlyList<string>>? errors = null)
        : this(statusCode, message, responseBody, errors, innerException: null)
    {
    }

    /// <summary>Creates an exception describing an API error response, preserving the underlying cause.</summary>
    public AmpecoApiException(int statusCode, string message, string? responseBody,
        IReadOnlyDictionary<string, IReadOnlyList<string>>? errors, Exception? innerException)
        : base(BuildMessage(statusCode, message), innerException)
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
        Errors = errors;
    }

    private static string BuildMessage(int statusCode, string message)
    {
        // StatusCode 0 means the request never produced an HTTP status (see AmpecoProtocolException).
        if (statusCode <= 0)
        {
            return string.IsNullOrWhiteSpace(message) ? "The AMPECO API response could not be processed." : message;
        }

        var reason = Enum.IsDefined(typeof(HttpStatusCode), statusCode)
            ? ((HttpStatusCode)statusCode).ToString()
            : "Unknown";

        return string.IsNullOrWhiteSpace(message)
            ? $"AMPECO API returned status {statusCode} ({reason})."
            : $"AMPECO API returned status {statusCode} ({reason}): {message}";
    }
}

/// <summary>
/// Exception used when the API returns a malformed/unexpected success payload.
/// </summary>
public sealed class AmpecoProtocolException : AmpecoApiException
{
    /// <summary>Creates an exception describing a response that did not match the documented shape.</summary>
    public AmpecoProtocolException(string message, Exception? inner = null)
        : base(0, message, responseBody: null, errors: null, innerException: inner)
    {
    }
}
