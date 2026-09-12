using Ampeco.Sdk.Internal;
using Microsoft.Extensions.Options;

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
public sealed class AmpecoClient : IAmpecoClient, IDisposable
{
    private readonly ApiConnection _connection;
    private readonly bool _ownsConnection;

    /// <summary>Creates a new AMPECO Public API client.</summary>
    /// <param name="options">Tenant URL, API key and other settings.</param>
    public AmpecoClient(AmpecoClientOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        if (options.Validate() is { } problem)
        {
            throw new ArgumentException(problem, nameof(options));
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

    /// <summary>
    /// Constructor used by <c>services.AddAmpeco(...)</c>. The <paramref name="httpClient"/> is
    /// supplied and owned by <c>IHttpClientFactory</c>, so this client never disposes it.
    /// </summary>
    /// <param name="httpClient">The typed client's <see cref="HttpClient"/>.</param>
    /// <param name="options">The configured client options.</param>
    public AmpecoClient(HttpClient httpClient, IOptions<AmpecoClientOptions> options)
        : this(ForHttpClient(httpClient, options))
    {
    }

    private static AmpecoClientOptions ForHttpClient(HttpClient httpClient, IOptions<AmpecoClientOptions> options)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        ArgumentNullException.ThrowIfNull(options);

        var value = options.Value;
        return new AmpecoClientOptions
        {
            TenantUrl = value.TenantUrl,
            ApiKey = value.ApiKey,
            RequestTimeout = value.RequestTimeout,
            DefaultPerPage = value.DefaultPerPage,
            HttpClient = httpClient,
        };
    }

    /// <inheritdoc />
    public ChargePointsClient ChargePoints { get; }

    /// <inheritdoc />
    public EvsesClient Evses { get; }

    /// <inheritdoc />
    public LocationsClient Locations { get; }

    /// <inheritdoc />
    public UsersClient Users { get; }

    /// <inheritdoc />
    public SessionsClient Sessions { get; }

    /// <inheritdoc />
    public TransactionsClient Transactions { get; }

    /// <inheritdoc />
    public TariffsClient Tariffs { get; }

    /// <inheritdoc />
    public ReservationsClient Reservations { get; }

    /// <inheritdoc />
    public PartnersClient Partners { get; }

    /// <inheritdoc />
    public CdrsClient Cdrs { get; }

    /// <inheritdoc />
    public InvoicesClient Invoices { get; }

    /// <inheritdoc />
    public ReceiptsClient Receipts { get; }

    /// <inheritdoc />
    public SubscriptionsClient Subscriptions { get; }

    /// <inheritdoc />
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
