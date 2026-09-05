using Ampeco.Sdk.Internal;
using Ampeco.Sdk.Models;

namespace Ampeco.Sdk;

/// <summary>
/// Reservation operations (reservations v1.0) and the reservation cancel action.
/// </summary>
public sealed class ReservationsClient : ResourceClient
{
    internal ReservationsClient(ApiConnection connection) : base(connection)
    {
    }

    private const string ResourceBase = "resources/reservations/v1.0";
    private const string ActionBase = "actions/reservation/v1.0";

    /// <summary>Fetches a reservation by id.</summary>
    public Task<Reservation> GetAsync(long reservationId, CancellationToken cancellationToken = default)
        => GetAsync<Reservation>($"{ResourceBase}/{reservationId}", cancellationToken: cancellationToken);

    /// <summary>Fetches a single page of reservations.</summary>
    /// <param name="filter">Optional filters (serialized as deepObject query parameters).</param>
    /// <param name="pageRequest">Paging options; defaults to cursor pagination from the first page.</param>
    public Task<Page<Reservation>> GetPageAsync(ReservationFilter? filter = null, PageRequest? pageRequest = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return GetPageAsync<Reservation>(ResourceBase, query, pageRequest, cancellationToken);
    }

    /// <summary>Iterates over all reservations matching the filter, transparently following cursor pagination.</summary>
    public IAsyncEnumerable<Reservation> StreamAsync(ReservationFilter? filter = null, int? perPage = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return StreamAsync<Reservation>(ResourceBase, query, new PageRequest { PerPage = perPage }, cancellationToken);
    }

    /// <summary>Cancels a reservation.</summary>
    /// <param name="reservationId">Reservation id.</param>
    /// <param name="request">Optional force flag and cancellation reason.</param>
    public Task CancelAsync(long reservationId, CancelReservationRequest? request = null, CancellationToken cancellationToken = default)
        => SendNoContentAsync(HttpMethod.Post, $"{ActionBase}/{reservationId}/cancel", request, cancellationToken);
}
