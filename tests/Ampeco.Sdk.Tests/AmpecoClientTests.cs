using System.Net;
using Ampeco.Sdk.Models;
using Xunit;

namespace Ampeco.Sdk.Tests;

public class AmpecoClientTests
{
    [Fact]
    public async Task GetAsync_AddsBearerTokenAndCallsExpectedPath()
    {
        var (client, handler) = TestClient.Create();
        handler.EnqueueJson(HttpStatusCode.OK, new { data = new { id = 42, name = "CP-1", status = "enabled" } });

        var chargePoint = await client.ChargePoints.GetAsync(42);

        Assert.Equal(42, chargePoint.Id);
        Assert.Equal("CP-1", chargePoint.Name);
        Assert.Equal(ValueSets.ChargePointStatus.Enabled, chargePoint.Status);

        var request = handler.Requests.Single();
        Assert.Equal("https://mytenant.ampeco.com/public-api/resources/charge-points/v2.0/42", request.RequestUri!.ToString());
        Assert.Equal("Bearer", request.Headers.Authorization!.Scheme);
        Assert.Equal("test-token", request.Headers.Authorization.Parameter);
    }

    [Fact]
    public async Task Listing_SendsDeepObjectFiltersAndCursorPaging()
    {
        var (client, handler) = TestClient.Create();
        handler.EnqueueJson(HttpStatusCode.OK, new
        {
            data = Array.Empty<object>(),
            links = new { next = (string?)null },
            meta = new { next_cursor = (string?)null, per_page = 2 },
        });

        await client.Sessions.GetPageAsync(
            filter: new SessionFilter { UserId = 123, StartedAfter = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero) },
            pageRequest: new PageRequest { PerPage = 25 });

        var request = handler.Requests.Single();
        var query = request.RequestUri!.Query;
        Assert.Contains("filter%5BuserId%5D=123", query);
        Assert.Contains("filter%5BstartedAfter%5D=2024-01-01T00%3A00%3A00Z", query);
        Assert.Contains("cursor=", query);
        Assert.Contains("per_page=25", query);
    }

    [Fact]
    public async Task StreamAsync_FollowsCursorPagination()
    {
        var (client, handler) = TestClient.Create();

        handler.EnqueueJson(HttpStatusCode.OK, new
        {
            data = new[] { new { id = "s1", status = "active" } },
            meta = new { next_cursor = "CUR2", per_page = 2 },
        });
        handler.EnqueueJson(HttpStatusCode.OK, new
        {
            data = new[] { new { id = "s2", status = "finished" } },
            meta = new { next_cursor = (string?)null, per_page = 2 },
        });

        var sessions = new List<Session>();
        await foreach (var session in client.Sessions.StreamAsync())
        {
            sessions.Add(session);
        }

        Assert.Equal(2, sessions.Count);
        Assert.Equal("s1", sessions[0].Id);
        Assert.Equal("s2", sessions[1].Id);

        Assert.Equal(2, handler.Requests.Count);
        // First request opts into cursor pagination with an empty cursor param.
        Assert.Contains("cursor=", handler.Requests[0].RequestUri!.Query);
        Assert.Contains("cursor=CUR2", handler.Requests[1].RequestUri!.Query);
    }

    [Fact]
    public async Task ErrorResponses_ThrowAmpecoApiExceptionWithValidationErrors()
    {
        var (client, handler) = TestClient.Create();
        handler.Enqueue(HttpStatusCode.UnprocessableEntity,
            """{"message":"The given data was invalid.","errors":{"name":["The name field is required."]}}""");

        var exception = await Assert.ThrowsAsync<AmpecoApiException>(
            () => client.ChargePoints.CreateAsync(new ChargePointWrite { Type = ValueSets.ChargePointType.Public }));

        Assert.Equal(422, exception.StatusCode);
        Assert.Contains("The given data was invalid.", exception.Message);
        Assert.NotNull(exception.Errors);
        Assert.Equal("The name field is required.", Assert.Single(exception.Errors["name"]));
    }

    [Fact]
    public async Task StartCharging_PostsBodyAndReturnsOn202()
    {
        var (client, handler) = TestClient.Create();
        handler.Enqueue(HttpStatusCode.Accepted, "");

        await client.ChargePoints.StartChargingAsync(5, 2, new StartSessionRequest
        {
            UserId = 77,
            StopConditions = new SessionStopConditions { MaxEnergyKwh = 20 },
        });

        var request = handler.Requests.Single();
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.EndsWith("/actions/charge-point/v1.0/5/start/2", request.RequestUri!.ToString());
        var body = handler.RequestBodies.Single();
        Assert.Contains("\"userId\":77", body);
        Assert.Contains("\"maxEnergyKwh\":20", body);
    }

    [Fact]
    public async Task CreateAsync_OmitsNullWriteProperties()
    {
        var (client, handler) = TestClient.Create();
        handler.EnqueueJson(HttpStatusCode.OK, new { data = new { id = 9 } });

        await client.Users.CreateAsync(new UserWrite
        {
            Email = "user@example.com",
            Password = "secret",
            FirstName = "Ivan",
        });

        var body = handler.RequestBodies.Single();
        Assert.Contains("\"email\":\"user@example.com\"", body);
        Assert.Contains("\"password\":\"secret\"", body);
        Assert.Contains("\"first_name\":\"Ivan\"", body);
        Assert.DoesNotContain("lastName", body);
        Assert.DoesNotContain("phone", body);
    }

    [Fact]
    public async Task ResetAsync_ReturnsSuccessFlag()
    {
        var (client, handler) = TestClient.Create();
        handler.EnqueueJson(HttpStatusCode.OK, new { success = true });

        var success = await client.ChargePoints.ResetAsync(5, ValueSets.ResetType.Soft);

        Assert.True(success);
        Assert.EndsWith("/actions/charge-point/v1.0/5/reset/Soft", handler.Requests.Single().RequestUri!.ToString());
    }

    [Fact]
    public async Task UpdateAsync_UsesPatchVerb()
    {
        var (client, handler) = TestClient.Create();
        handler.EnqueueJson(HttpStatusCode.OK, new { data = new { id = 5, name = "renamed" } });

        var updated = await client.ChargePoints.UpdateAsync(5, new ChargePointWrite { Name = "renamed" });

        Assert.Equal("renamed", updated.Name);
        Assert.Equal(HttpMethod.Patch, handler.Requests.Single().Method);
    }
}
