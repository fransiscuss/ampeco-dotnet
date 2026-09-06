
namespace Ampeco.Sdk;

/// <summary>
/// Client for the AMPECO EV Charging Platform Public API, grouped by resource.
/// </summary>
/// <remarks>
/// Resolve this from dependency injection after calling
/// <c>services.AddAmpeco(...)</c>, or construct <see cref="AmpecoClient"/> directly.
/// All API errors are surfaced as <see cref="AmpecoApiException"/>.
/// </remarks>
public interface IAmpecoClient
{
    /// <summary>Charge point CRUD and charging actions.</summary>
    ChargePointsClient ChargePoints { get; }

    /// <summary>EVSE CRUD and charging actions.</summary>
    EvsesClient Evses { get; }

    /// <summary>Location CRUD.</summary>
    LocationsClient Locations { get; }

    /// <summary>User CRUD.</summary>
    UsersClient Users { get; }

    /// <summary>Charging session reads and session actions.</summary>
    SessionsClient Sessions { get; }

    /// <summary>Payment transaction reads and writes.</summary>
    TransactionsClient Transactions { get; }

    /// <summary>Tariff CRUD.</summary>
    TariffsClient Tariffs { get; }

    /// <summary>Reservation reads and the cancel action.</summary>
    ReservationsClient Reservations { get; }

    /// <summary>Partner CRUD.</summary>
    PartnersClient Partners { get; }

    /// <summary>Read-only roaming CDR access.</summary>
    CdrsClient Cdrs { get; }

    /// <summary>Read-only invoice access.</summary>
    InvoicesClient Invoices { get; }

    /// <summary>Read-only receipt access.</summary>
    ReceiptsClient Receipts { get; }

    /// <summary>Read-only subscription access.</summary>
    SubscriptionsClient Subscriptions { get; }

    /// <summary>Roaming operators and connections.</summary>
    RoamingClient Roaming { get; }
}
