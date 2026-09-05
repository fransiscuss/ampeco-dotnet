using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Models;

/// <summary>A roaming operator (CPO or EMSP), as returned by the roaming-operators v2.0 endpoints.</summary>
public sealed record RoamingOperator
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("operatorId")]
    public long? OperatorId { get; init; }

    /// <summary>Role of the operator: <c>cpo</c> or <c>emsp</c>.</summary>
    [JsonPropertyName("role")]
    public string? Role { get; init; }

    /// <summary>Hubject operator id, when connected via Hubject.</summary>
    [JsonPropertyName("hubjectId")]
    public string? HubjectId { get; init; }

    [JsonPropertyName("businessName")]
    public string? BusinessName { get; init; }

    /// <summary>Roaming platform the operator is connected through.</summary>
    [JsonPropertyName("platformId")]
    public long PlatformId { get; init; }

    /// <summary>Partner linked to the roaming operator, when applicable.</summary>
    [JsonPropertyName("partnerId")]
    public long? PartnerId { get; init; }

    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    /// <summary>ISO 3166-1 alpha-2 country code.</summary>
    [JsonPropertyName("countryCode")]
    public string? CountryCode { get; init; }

    /// <summary>OCPI party id.</summary>
    [JsonPropertyName("partyId")]
    public string? PartyId { get; init; }

    /// <summary>CPO-specific settings, when the operator acts as a CPO.</summary>
    [JsonPropertyName("cpoSettings")]
    public RoamingOperatorSettings? CpoSettings { get; init; }

    [JsonPropertyName("lastUpdatedAt")]
    public DateTimeOffset? LastUpdatedAt { get; init; }
}

/// <summary>CPO settings of a roaming operator.</summary>
public sealed record RoamingOperatorSettings
{
    [JsonPropertyName("countryCode")]
    public string? CountryCode { get; init; }

    [JsonPropertyName("partyId")]
    public string? PartyId { get; init; }

    [JsonPropertyName("publishTariffs")]
    public bool? PublishTariffs { get; init; }
}

/// <summary>Payload for updating a roaming operator.</summary>
public sealed record RoamingOperatorWrite
{
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Enabled { get; init; }

    [JsonPropertyName("cpoSettings")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RoamingOperatorSettings? CpoSettings { get; init; }
}

/// <summary>A roaming platform connection, as returned by the roaming-connections v2.0 endpoints.</summary>
public sealed record RoamingConnection
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    /// <summary><see cref="ValueSets.RoamingProtocol"/>.</summary>
    [JsonPropertyName("protocol")]
    public string Protocol { get; init; } = string.Empty;

    /// <summary>Platform endpoint url.</summary>
    [JsonPropertyName("endpoint")]
    public string Endpoint { get; init; } = string.Empty;

    /// <summary>OCPI credentials token, when the protocol is OCPI-based.</summary>
    [JsonPropertyName("ocpi")]
    public RoamingOcpiCredentials? Ocpi { get; init; }

    /// <summary>Length of the dispute period, in days.</summary>
    [JsonPropertyName("disputePeriodLength")]
    public int DisputePeriodLength { get; init; }

    /// <summary>Name of the connection issuer.</summary>
    [JsonPropertyName("issuer")]
    public string? Issuer { get; init; }

    [JsonPropertyName("preferRealtimeAuthorization")]
    public bool? PreferRealtimeAuthorization { get; init; }

    [JsonPropertyName("lastUpdatedAt")]
    public DateTimeOffset? LastUpdatedAt { get; init; }
}

/// <summary>OCPI credentials of a roaming connection.</summary>
public sealed record RoamingOcpiCredentials
{
    [JsonPropertyName("token")]
    public string? Token { get; init; }
}
