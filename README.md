# Ampeco.Sdk

A hand-written .NET client SDK for the [AMPECO EV Charging Platform Public API](https://developers.ampeco.com) — concrete classes only, no code generation.

- **Targets:** .NET 8.0 and .NET 9.0
- **Dependencies:** none (only `System.Text.Json`, part of the BCL)
- **API version used for modeling:** Public API spec v3.244.0 (September 2026)

## Installation

```sh
dotnet add package Ampeco.Sdk
```

Or build from source: `dotnet pack src/Ampeco.Sdk -o artifacts`.

## Getting an API key

Generate a token in the CHARGE back office (**Back Office → API Access Tokens**), or contact
your AMPECO Customer Success Manager. Every call is executed as the token's owning admin;
permissions and audit logs follow that admin's access.

## Getting started

```csharp
using Ampeco.Sdk;
using Ampeco.Sdk.Models;

var client = new AmpecoClient(new AmpecoClientOptions
{
    TenantUrl = "https://your-tenant.ampeco.com", // your tenant URL
    ApiKey = "your-api-token",                    // Back Office → API Access Tokens
});

// List charge points (cursor pagination is handled transparently)
await foreach (var cp in client.ChargePoints.StreamAsync(
    filter: new ChargePointFilter { Type = ValueSets.ChargePointType.Public }))
{
    Console.WriteLine($"{cp.Id}: {cp.Name} ({cp.NetworkStatus})");
}

// Start charging
await client.ChargePoints.StartChargingAsync(chargePointId, evseId, new StartSessionRequest
{
    UserId = 123,
    StopConditions = new SessionStopConditions { MaxEnergyKwh = 20 },
});

// Stop charging
await client.ChargePoints.StopChargingAsync(chargePointId, sessionId);
```

Dispose the client when done if it owns its `HttpClient`:

```csharp
client.Dispose();
```

## API surface

Operations are grouped by resource on the `AmpecoClient`:

| Client | Covers |
|--------|--------|
| `ChargePoints` | CRUD (v2.0), status, and actions: start/stop charging, reset, change availability, unlock, reserve |
| `Evses` | CRUD (v2.1), start charging by EVSE |
| `Locations` | CRUD (v2.0) |
| `Users` | CRUD (v1.1) |
| `Sessions` | Listing with filters/expansions, custom fields, change tariff, assign user, retry payment |
| `Transactions` | CRUD (v1.0), create pre-authorization (Stripe / Worldline) |
| `Tariffs` | CRUD (v1.0) |
| `Reservations` | Listing, cancel action |
| `Partners` | CRUD (v2.0) |
| `Cdrs` | Read-only roaming CDRs (v2.0) |
| `Invoices` | Read-only (v1.0) |
| `Receipts` | Read-only (v2.0) |
| `Subscriptions` | Read-only (v1.0) |
| `Roaming` | Roaming operators (read/update) and connections (read-only) |

### Listing patterns

Every listing endpoint supports paging by hand or streaming:

```csharp
// One page at a time
Page<Session> page = await client.Sessions.GetPageAsync(
    filter: new SessionFilter { Status = ValueSets.SessionStatus.Active },
    pageRequest: new PageRequest { PerPage = 50 });

string? next = page.NextCursor; // pass into the next PageRequest

// Or iterate everything
await foreach (var session in client.Sessions.StreamAsync(filter, perPage: 50))
{
    // ...
}
```

### Errors

Non-success responses throw `AmpecoApiException` with the status code, the API's message
and (for HTTP 422) the per-field validation errors:

```csharp
try
{
    await client.Users.CreateAsync(new UserWrite { Email = "user@example.com", Password = "..." });
}
catch (AmpecoApiException ex) when (ex.StatusCode == 422)
{
    foreach (var (field, messages) in ex.Errors ?? [])
        Console.WriteLine($"{field}: {string.Join("; ", messages)}");
}
```

### Expansions (includes)

Session reads accept a `SessionQuery` to include authorizations, price breakdowns,
charging periods and energy-consumption samples; they map to the API's
`with*`/`include[]` query parameters.

## Design notes

- **Hand-written concrete classes** — no NSwag/OpenAPI code generation. Models are
  plain C# records annotated with `System.Text.Json` attributes.
- **String value-sets instead of enums** — enum-like fields (`status`, `type`, …) are
  typed as `string` so that values AMPECO adds in the future never break deserialization.
  Documented values are provided as constants in `Ampeco.Sdk.Models.ValueSets`
  (e.g. `ValueSets.ChargePointStatus.Available`).
- **Cursor pagination** — the SDK always sends the `cursor` parameter (empty on the first
  request, as the API requires to opt into cursor pagination) and transparently falls back
  to page-based iteration for legacy endpoints. Note the API sunsets `?page=` on 2026-06-01.
- **DeepObject filters** — filter classes serialize to the API's `filter[name]=value` style;
  lists become repeated `filter[name][]` entries.
- **Write models omit nulls** — create/update records only serialize properties you set,
  so the same class works for both create and PATCH update.

## Not yet covered

The full API exposes ~449 endpoints. This SDK targets the core charging, billing and
roaming surfaces. Natural extensions (all following the same patterns) include:
circuits, electricity rates/meters, tariff groups, RFID/id tags, partner invites,
sub-operators, charge point configurations, downtime periods, notifications and more.
Open an issue or add a sub-client modeled on the existing ones.

## Live API documentation

<https://developers.ampeco.com>

## License

MIT
