using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Models;

/// <summary>A roaming Charge Data Record (CDR), as returned by the cdrs v2.0 endpoints.</summary>
public sealed record Cdr
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("operatorId")]
    public long OperatorId { get; init; }

    /// <summary>Roaming platform id the CDR was received through.</summary>
    [JsonPropertyName("platformId")]
    public long PlatformId { get; init; }

    /// <summary><see cref="ValueSets.CdrProtocolType"/>.</summary>
    [JsonPropertyName("protocolType")]
    public string ProtocolType { get; init; } = string.Empty;

    /// <summary>AMPECO session id linked to the CDR, when matched.</summary>
    [JsonPropertyName("sessionId")]
    public long? SessionId { get; init; }

    /// <summary>Session id reported by the roaming platform.</summary>
    [JsonPropertyName("roamingSessionId")]
    public string? RoamingSessionId { get; init; }

    /// <summary>Original roaming id of the CDR.</summary>
    [JsonPropertyName("roamingId")]
    public string? RoamingId { get; init; }

    [JsonPropertyName("receivedAt")]
    public DateTimeOffset ReceivedAt { get; init; }

    [JsonPropertyName("startTime")]
    public DateTimeOffset StartTime { get; init; }

    [JsonPropertyName("endTime")]
    public DateTimeOffset EndTime { get; init; }

    /// <summary>True when the CDR is a credit (correction) record.</summary>
    [JsonPropertyName("credit")]
    public bool? Credit { get; init; }

    [JsonPropertyName("creditReferenceId")]
    public string? CreditReferenceId { get; init; }

    /// <summary>True when the CDR belongs to a local (non-roaming) session.</summary>
    [JsonPropertyName("isLocal")]
    public bool IsLocal { get; init; }

    /// <summary>Raw OCPI payload of the CDR, when received via OCPI.</summary>
    [JsonPropertyName("ocpiData")]
    public JsonElement? OcpiData { get; init; }

    /// <summary>Raw OICP payload of the CDR, when received via Hubject OICP.</summary>
    [JsonPropertyName("oicpData")]
    public JsonElement? OicpData { get; init; }

    [JsonPropertyName("sentAt")]
    public DateTimeOffset? SentAt { get; init; }

    /// <summary>Delivery response: <c>success</c> or <c>fail</c>.</summary>
    [JsonPropertyName("deliveryResponse")]
    public string? DeliveryResponse { get; init; }

    [JsonPropertyName("externalId")]
    public string? ExternalId { get; init; }

    [JsonPropertyName("lastUpdatedAt")]
    public DateTimeOffset? LastUpdatedAt { get; init; }
}
