using System.Net;
using Ampeco.Sdk.Models;
using Xunit;

namespace Ampeco.Sdk.Tests;

/// <summary>Regression coverage for transport behaviour that is easy to break silently.</summary>
public class TransportRegressionTests
{
    [Fact]
    public async Task StreamAsync_WalksEveryLegacyPageInsteadOfJumpingPastTheLast()
    {
        var (client, handler) = TestClient.Create();
        for (var page = 1; page <= 3; page++)
        {
            handler.EnqueueJson(HttpStatusCode.OK, new
            {
                data = new[] { new { id = page, name = $"CP-{page}" } },
                meta = new { current_page = page, last_page = 3, per_page = 1 },
            });
        }

        var ids = new List<long?>();
        await foreach (var chargePoint in client.ChargePoints.StreamAsync())
        {
            ids.Add(chargePoint.Id);
        }

        Assert.Equal([1L, 2L, 3L], ids);

        var pages = handler.Requests
            .Select(request => System.Web.HttpUtility.ParseQueryString(request.RequestUri!.Query)["page"] ?? "(none)")
            .ToArray();
        Assert.Equal(["(none)", "2", "3"], pages);
    }

    [Fact]
    public async Task Client_DoesNotMutateACallerSuppliedHttpClient()
    {
        var handler = new FakeHttpHandler();
        handler.EnqueueJson(HttpStatusCode.OK, new { data = new { id = 0 } }); // consumed by the warm-up call below
        handler.EnqueueJson(HttpStatusCode.OK, new { data = new { id = 1 } });

        using var shared = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://shared.example/public-api/"),
            Timeout = TimeSpan.FromSeconds(7),
        };

        // A single request freezes HttpClient.BaseAddress and .Timeout: assigning either
        // afterwards throws, so the SDK must leave a client it does not own alone.
        _ = await shared.GetAsync(new Uri("https://shared.example/public-api/ping"));

        using var client = new AmpecoClient(new AmpecoClientOptions
        {
            TenantUrl = "https://ignored.example",
            ApiKey = "test-token",
            HttpClient = shared,
            RequestTimeout = TimeSpan.FromSeconds(30),
        });

        var chargePoint = await client.ChargePoints.GetAsync(1);

        Assert.Equal(1, chargePoint.Id);
        Assert.Equal(TimeSpan.FromSeconds(7), shared.Timeout);
        Assert.Equal("https://shared.example/public-api/resources/charge-points/v2.0/1", handler.Requests[^1].RequestUri!.ToString());
    }

    [Fact]
    public void ApiException_NamesTheStatusInsteadOfRepeatingTheNumber()
    {
        var exception = new AmpecoApiException(404, "Not found.");

        Assert.Contains("404 (NotFound)", exception.Message, StringComparison.Ordinal);
        Assert.DoesNotContain("404 (404)", exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void ProtocolException_KeepsTheUnderlyingCauseAndDropsTheFakeStatus()
    {
        var inner = new InvalidOperationException("boom");
        var exception = new AmpecoProtocolException("Response could not be parsed.", inner);

        Assert.Same(inner, exception.InnerException);
        Assert.Equal("Response could not be parsed.", exception.Message);
        Assert.DoesNotContain("status 0", exception.Message, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData("", "token", "TenantUrl")]
    [InlineData("https://tenant.example", "", "ApiKey")]
    [InlineData("https://tenant.example", "bad\ntoken", "ApiKey")]
    public void Constructor_RejectsUnusableOptions(string tenantUrl, string apiKey, string expected)
    {
        var exception = Assert.Throws<ArgumentException>(() => new AmpecoClient(new AmpecoClientOptions
        {
            TenantUrl = tenantUrl,
            ApiKey = apiKey,
        }));

        Assert.Contains(expected, exception.Message, StringComparison.Ordinal);
    }
}
