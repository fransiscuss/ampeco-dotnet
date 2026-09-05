using System.Text.Json;

namespace Ampeco.Sdk;

/// <summary>
/// Thrown when the AMPECO Public API returns an error response.
/// </summary>
public class AmpecoApiException : Exception
{
    /// <summary>HTTP status code returned by the API.</summary>
    public int StatusCode { get; }

    /// <summary>Per-field validation errors (present on 422 responses).</summary>
    public IReadOnlyDictionary<string, IReadOnlyList<string>>? Errors { get; }

    /// <summary>The raw response body, when available.</summary>
    public string? ResponseBody { get; }

    public AmpecoApiException(int statusCode, string message, string? responseBody = null,
        IReadOnlyDictionary<string, IReadOnlyList<string>>? errors = null)
        : base(BuildMessage(statusCode, message))
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
        Errors = errors;
    }

    private static string BuildMessage(int statusCode, string message) =>
        string.IsNullOrWhiteSpace(message)
            ? $"AMPECO API returned status {(int)statusCode} ({statusCode})."
            : $"AMPECO API returned status {(int)statusCode} ({statusCode}): {message}";
}

/// <summary>
/// Exception used when the API returns a malformed/unexpected success payload.
/// </summary>
public sealed class AmpecoProtocolException : AmpecoApiException
{
    public AmpecoProtocolException(string message, Exception? inner = null)
        : base(0, message, inner?.Message)
    {
    }
}
