using Ampeco.Sdk;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>Dependency-injection registration for the AMPECO API client.</summary>
public static class AmpecoServiceCollectionExtensions
{
    /// <summary>
    /// Registers <see cref="IAmpecoClient"/> as a typed <c>HttpClient</c>, so the
    /// connection pool is managed by <c>IHttpClientFactory</c> rather than the SDK.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configure">Configures the tenant URL, API key and other options.</param>
    /// <returns>
    /// The <see cref="IHttpClientBuilder"/> for the registration, so callers can add
    /// their own handlers (retry, logging, Polly) on top.
    /// </returns>
    public static IHttpClientBuilder AddAmpeco(
        this IServiceCollection services,
        Action<AmpecoClientOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);

        services.AddOptions<AmpecoClientOptions>()
            .Configure(configure)
            .Validate(
                static options => !string.IsNullOrWhiteSpace(options.TenantUrl),
                $"{nameof(AmpecoClientOptions)}.{nameof(AmpecoClientOptions.TenantUrl)} is required.")
            .Validate(
                static options => !string.IsNullOrWhiteSpace(options.ApiKey),
                $"{nameof(AmpecoClientOptions)}.{nameof(AmpecoClientOptions.ApiKey)} is required.");

        return services.AddHttpClient<IAmpecoClient, AmpecoClient>(static (provider, client) =>
        {
            var options = provider.GetRequiredService<IOptions<AmpecoClientOptions>>().Value;
            client.BaseAddress = options.GetBaseAddress();
            client.Timeout = options.RequestTimeout;
        });
    }
}
