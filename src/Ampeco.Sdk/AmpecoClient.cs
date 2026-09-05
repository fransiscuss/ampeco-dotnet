using Ampeco.Sdk.Internal;

namespace Ampeco.Sdk;

/// <summary>
/// Client for the AMPECO EV Charging Platform Public API.
/// Create one long-lived instance per tenant/API key; it is thread-safe.
/// </summary>
/// <remarks>
/// Operations are grouped by resource: <see cref="ChargePoints"/>, <see cref="Evses"/>, <see cref="Locations"/>,
/// <see cref="Users"/>, <see cref="Sessions"/>, <see cref="Transactions"/>, <see cref="Tariffs"/>,
/// <see cref="Reservations"/>, <see cref="Partners"/>, <see cref="Cdrs"/>, <see cref="Invoices"/>,
/// <see cref="Receipts"/>, <see cref="Subscriptions"/> and <see cref="Roaming"/>.
/// All API errors are surfaced as <see cref="AmpecoApiException"/>.
/// </remarks>
public sealed class AmpecoClient : IDisposable
{
    private readonly ApiConnection _connection;
    private readonly bool _ownsConnection;

    /// <summary>Creates a new AMPECO Public API client.</summary>
    /// <param name="options">Tenant URL, API key and other settings.</param>
    public AmpecoClient(AmpecoClientOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        if (string.IsNullOrWhiteSpace(options.TenantUrl))
        {
            throw new ArgumentException("TenantUrl is required.", nameof(options));
        }

        if (string.IsNullOrWhiteSpace(options.ApiKey))
        {
            throw new ArgumentException("ApiKey is required.", nameof(options));
        }

        _ownsConnection = options.HttpClient is null;
        _connection = new ApiConnection(options);

        ChargePoints = new ChargePointsClient(_connection);
        Evses = new EvsesClient(_connection);
        Locations = new LocationsClient(_connection);
        Users = new UsersClient(_connection);
        Sessions = new SessionsClient(_connection);
        Transactions = new TransactionsClient(_connection);
        Tariffs = new TariffsClient(_connection);
        Reservations = new ReservationsClient(_connection);
        Partners = new PartnersClient(_connection);
        Cdrs = new CdrsClient(_connection);
        Invoices = new InvoicesClient(_connection);
        Receipts = new ReceiptsClient(_connection);
        Subscriptions = new SubscriptionsClient(_connection);
        Roaming = new RoamingClient(_connection);
    }

    /// <summary>Charge point CRUD and charging actions.</summary>
    public ChargePointsClient ChargePoints { get; }

    /// <summary>EVSE CRUD and charging actions.</summary>
    public EvsesClient Evses { get; }

    /// <summary>Location CRUD.</summary>
    public LocationsClient Locations { get; }

    /// <summary>User CRUD.</summary>
    public UsersClient Users { get; }

    /// <summary>Charging session reads and session actions.</summary>
    public SessionsClient Sessions { get; }

    /// <summary>Payment transaction reads and writes.</summary>
    public TransactionsClient Transactions { get; }

    /// <summary>Tariff CRUD.</summary>
    public TariffsClient Tariffs { get; }

    /// <summary>Reservation reads and the cancel action.</summary>
    public ReservationsClient Reservations { get; }

    /// <summary>Partner CRUD.</summary>
    public PartnersClient Partners { get; }

    /// <summary>Read-only roaming CDR access.</summary>
    public CdrsClient Cdrs { get; }

    /// <summary>Read-only invoice access.</summary>
    public InvoicesClient Invoices { get; }

    /// <summary>Read-only receipt access.</summary>
    public ReceiptsClient Receipts { get; }

    /// <summary>Read-only subscription access.</summary>
    public SubscriptionsClient Subscriptions { get; }

    /// <summary>Roaming operators and connections.</summary>
    public RoamingClient Roaming { get; }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_ownsConnection)
        {
            _connection.Dispose();
        }
    }
}
