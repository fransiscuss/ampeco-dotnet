using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Models;

/// <summary>A reservation of an EVSE, as returned by the reservations v1.0 endpoints.</summary>
public sealed record Reservation
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("operatorId")]
    public long? OperatorId { get; init; }

    [JsonPropertyName("chargePointId")]
    public long ChargePointId { get; init; }

    [JsonPropertyName("evseId")]
    public long EvseId { get; init; }

    /// <summary><see cref="ValueSets.ReservationStatus"/>.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary>User the reservation was made for.</summary>
    [JsonPropertyName("userId")]
    public long UserId { get; init; }

    [JsonPropertyName("reservedAt")]
    public DateTimeOffset ReservedAt { get; init; }

    [JsonPropertyName("canceledAt")]
    public DateTimeOffset? CanceledAt { get; init; }

    /// <summary>Reservation duration in minutes.</summary>
    [JsonPropertyName("duration")]
    public int Duration { get; init; }

    [JsonPropertyName("lastUpdatedAt")]
    public DateTimeOffset? LastUpdatedAt { get; init; }
}

/// <summary>Request to cancel a reservation.</summary>
public sealed record CancelReservationRequest
{
    /// <summary>
    /// Use <c>true</c> to end the reservation regardless of the charge point's response.
    /// </summary>
    [JsonPropertyName("force")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Force { get; init; }

    /// <summary>
    /// Cancellation reason. When empty, "Activated via API" is recorded automatically.
    /// </summary>
    [JsonPropertyName("reason")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Reason { get; init; }
}

/// <summary>Request to reserve an EVSE on a charge point.</summary>
public sealed record ReserveEvseRequest
{
    /// <summary>User the reservation is made for.</summary>
    [JsonPropertyName("userId")]
    public long UserId { get; init; }

    /// <summary>Reservation duration in minutes.</summary>
    [JsonPropertyName("duration")]
    public int? Duration { get; init; }
}
