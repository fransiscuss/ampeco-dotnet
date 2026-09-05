using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Models;

/// <summary>A charging location, as returned by the locations v2.0 endpoints.</summary>
public sealed record Location
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("operatorId")]
    public long? OperatorId { get; init; }

    [JsonPropertyName("externalId")]
    public string? ExternalId { get; init; }

    /// <summary>Localized name.</summary>
    [JsonPropertyName("name")]
    public TranslatedTextList? Name { get; init; }

    /// <summary>Localized description.</summary>
    [JsonPropertyName("description")]
    public TranslatedTextList? Description { get; init; }

    [JsonPropertyName("shortDescription")]
    public TranslatedTextList? ShortDescription { get; init; }

    [JsonPropertyName("additionalDescription")]
    public TranslatedTextList? AdditionalDescription { get; init; }

    /// <summary><see cref="ValueSets.Status"/>.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("geoposition")]
    public GeoPosition? Geoposition { get; init; }

    [JsonPropertyName("streetAddress")]
    public TranslatedTextList? StreetAddress { get; init; }

    [JsonPropertyName("city")]
    public string? City { get; init; }

    [JsonPropertyName("region")]
    public string? Region { get; init; }

    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>ISO 3166-1 alpha-2 country code.</summary>
    [JsonPropertyName("country")]
    public string? Country { get; init; }

    [JsonPropertyName("postCode")]
    public string? PostCode { get; init; }

    /// <summary>IANA timezone identifier.</summary>
    [JsonPropertyName("timezone")]
    public string? Timezone { get; init; }

    /// <summary><see cref="ValueSets.ParkingType"/>.</summary>
    [JsonPropertyName("parkingType")]
    public string? ParkingType { get; init; }

    /// <summary>Accessibility of the location, e.g. <c>free_publicly_accessible</c>.</summary>
    [JsonPropertyName("accessibilityType")]
    public string? AccessibilityType { get; init; }

    /// <summary><see cref="ValueSets.AccessMethod"/> values.</summary>
    [JsonPropertyName("accessMethods")]
    public IReadOnlyList<string>? AccessMethods { get; init; }

    /// <summary>Amenities near the location, e.g. <c>CAFE</c>, <c>MALL</c>.</summary>
    [JsonPropertyName("facilities")]
    public IReadOnlyList<string>? Facilities { get; init; }

    [JsonPropertyName("workingHours")]
    public WorkingHours? WorkingHours { get; init; }

    /// <summary>Charging zones within the location.</summary>
    [JsonPropertyName("chargingZones")]
    public IReadOnlyList<ChargingZone>? ChargingZones { get; init; }

    [JsonPropertyName("roamingOperatorId")]
    public long? RoamingOperatorId { get; init; }

    /// <summary>True when the location is published via roaming.</summary>
    [JsonPropertyName("isRoaming")]
    public bool? IsRoaming { get; init; }

    /// <summary>Partners that have access to the location.</summary>
    [JsonPropertyName("partnerIds")]
    public IReadOnlyList<long>? PartnerIds { get; init; }

    [JsonPropertyName("roaming")]
    public LocationRoaming? Roaming { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<string>? Tags { get; init; }

    [JsonPropertyName("notes")]
    public IReadOnlyList<Note>? Notes { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; init; }

    [JsonPropertyName("lastUpdatedAt")]
    public DateTimeOffset? LastUpdatedAt { get; init; }
}

/// <summary>A zone within a location that groups EVSEs (e.g. by floor or area).</summary>
public sealed record ChargingZone
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("floorLevel")]
    public string? FloorLevel { get; init; }

    [JsonPropertyName("locationId")]
    public long? LocationId { get; init; }

    /// <summary><see cref="ValueSets.Status"/>.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("additionalInfo")]
    public string? AdditionalInfo { get; init; }
}

/// <summary>Roaming identity of a location.</summary>
public sealed record LocationRoaming
{
    [JsonPropertyName("owner")]
    public RoamingEntityName? Owner { get; init; }

    [JsonPropertyName("operator")]
    public RoamingEntityName? Operator { get; init; }

    [JsonPropertyName("suboperator")]
    public RoamingEntityName? Suboperator { get; init; }
}

/// <summary>Name of an entity as published via roaming.</summary>
public sealed record RoamingEntityName
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Payload for creating or updating a location (locations v2.0).
/// Only set properties are sent.
/// </summary>
public sealed record LocationWrite
{
    [JsonPropertyName("externalId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ExternalId { get; init; }

    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TranslatedTextList? Name { get; init; }

    /// <summary><see cref="ValueSets.Status"/>.</summary>
    [JsonPropertyName("status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Status { get; init; }

    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TranslatedTextList? Description { get; init; }

    [JsonPropertyName("shortDescription")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TranslatedTextList? ShortDescription { get; init; }

    [JsonPropertyName("additionalDescription")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TranslatedTextList? AdditionalDescription { get; init; }

    [JsonPropertyName("geoposition")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public GeoPosition? Geoposition { get; init; }

    [JsonPropertyName("streetAddress")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TranslatedTextList? StreetAddress { get; init; }

    [JsonPropertyName("city")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? City { get; init; }

    [JsonPropertyName("region")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Region { get; init; }

    [JsonPropertyName("state")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? State { get; init; }

    /// <summary>ISO 3166-1 alpha-2 country code.</summary>
    [JsonPropertyName("country")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Country { get; init; }

    [JsonPropertyName("postCode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PostCode { get; init; }

    [JsonPropertyName("timezone")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Timezone { get; init; }

    /// <summary><see cref="ValueSets.ParkingType"/>.</summary>
    [JsonPropertyName("parkingType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ParkingType { get; init; }

    /// <summary><see cref="ValueSets.AccessMethod"/> values.</summary>
    [JsonPropertyName("accessMethods")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<string>? AccessMethods { get; init; }

    /// <summary>Amenities near the location, e.g. <c>CAFE</c>, <c>MALL</c>.</summary>
    [JsonPropertyName("facilities")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<string>? Facilities { get; init; }

    [JsonPropertyName("workingHours")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public WorkingHours? WorkingHours { get; init; }

    [JsonPropertyName("tags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<string>? Tags { get; init; }
}
