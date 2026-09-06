namespace Ampeco.Sdk;

/// <summary>
/// Options used to configure an <see cref="AmpecoClient"/>.
/// </summary>
public sealed class AmpecoClientOptions
{
    // TenantUrl and ApiKey are deliberately not `required`: a type with required members
    // cannot satisfy the new() constraint the Options pattern needs. They are validated
    // at runtime instead - by AddAmpeco() and by the AmpecoClient constructor.

    /// <summary>
    /// Your AMPECO tenant URL, e.g. <c>https://mytenant.ampeco.com</c>.
    /// The SDK appends the <c>/public-api/</c> base path automatically.
    /// </summary>
    public string TenantUrl { get; set; } = string.Empty;

    /// <summary>
    /// API token generated in the CHARGE back office (Back Office → API Access Tokens).
    /// Sent as an <c>Authorization: Bearer</c> header on every request.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// The <see cref="System.Net.Http.HttpClient"/> to use. When null (default) the client
    /// creates and owns its own instance which is disposed together with the <see cref="AmpecoClient"/>.
    /// When you provide one, the caller owns its lifetime and it is not disposed.
    /// </summary>
    public HttpClient? HttpClient { get; set; }

    /// <summary>Per-request timeout applied to API calls. Defaults to 100 seconds.</summary>
    public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromSeconds(100);

    /// <summary>
    /// Default number of items requested per page for listing endpoints (1–100, API maximum is 100).
    /// </summary>
    public int DefaultPerPage { get; set; } = 100;

    /// <summary>
    /// Normalizes the tenant URL and returns the API base address (with trailing slash),
    /// e.g. <c>https://mytenant.ampeco.com/public-api/</c>.
    /// </summary>
    internal Uri GetBaseAddress()
    {
        var url = TenantUrl.Trim().TrimEnd('/');
        if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
            !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            url = "https://" + url;
        }

        return new Uri(url + "/public-api/", UriKind.Absolute);
    }
}
