using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Ampeco.Sdk.Tests;

public class ClientRegistrationTests
{
    [Fact]
    public void AddAmpeco_ResolvesClientWithConfiguredBaseAddress()
    {
        var services = new ServiceCollection();
        services.AddAmpeco(options =>
        {
            options.TenantUrl = "https://mytenant.ampeco.com";
            options.ApiKey = "test-token";
        });

        using var provider = services.BuildServiceProvider();
        var client = provider.GetRequiredService<IAmpecoClient>();

        Assert.NotNull(client.ChargePoints);
        Assert.NotNull(client.Roaming);
    }

    [Fact]
    public void AddAmpeco_NormalizesTenantUrlIntoThePublicApiBaseAddress()
    {
        var services = new ServiceCollection();
        services.AddAmpeco(options =>
        {
            options.TenantUrl = "mytenant.ampeco.com/";
            options.ApiKey = "test-token";
        });

        using var provider = services.BuildServiceProvider();
        var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(IAmpecoClient));

        Assert.Equal("https://mytenant.ampeco.com/public-api/", httpClient.BaseAddress!.ToString());
    }

    [Fact]
    public void AddAmpeco_ThrowsWhenApiKeyIsMissing()
    {
        var services = new ServiceCollection();
        services.AddAmpeco(options => options.TenantUrl = "https://mytenant.ampeco.com");

        using var provider = services.BuildServiceProvider();

        var ex = Assert.Throws<OptionsValidationException>(() => provider.GetRequiredService<IAmpecoClient>());
        Assert.Contains(nameof(AmpecoClientOptions.ApiKey), ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void AddAmpeco_ThrowsWhenTenantUrlIsMissing()
    {
        var services = new ServiceCollection();
        services.AddAmpeco(options => options.ApiKey = "test-token");

        using var provider = services.BuildServiceProvider();

        var ex = Assert.Throws<OptionsValidationException>(() => provider.GetRequiredService<IAmpecoClient>());
        Assert.Contains(nameof(AmpecoClientOptions.TenantUrl), ex.Message, StringComparison.Ordinal);
    }
}
