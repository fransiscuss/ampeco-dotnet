using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Models;

/// <summary>An EVSE (Electric Vehicle Supply Equipment), as returned by the evses v2.1 endpoints.</summary>
public sealed record Evse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("chargePointId")]
    public long ChargePointId { get; init; }

    [JsonPropertyName("operatorId")]
    public long? OperatorId { get; init; }

    /// <summary>Operator-defined physical reference (e.g. "1" for the first EVSE).</summary>
    [JsonPropertyName("physicalReference")]
    public string PhysicalReference { get; init; } = string.Empty;

    /// <summary>OCPI-style eMI3 identifier, when published to roaming.</summary>
    [JsonPropertyName("emi3Id")]
    public string? Emi3Id { get; init; }

    /// <summary>Human-readable label.</summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary><see cref="ValueSets.CurrentType"/>.</summary>
    [JsonPropertyName("currentType")]
    public string CurrentType { get; init; } = ValueSets.CurrentType.Ac;

    /// <summary>OCPP network identifier of the EVSE.</summary>
    [JsonPropertyName("networkId")]
    public string NetworkId { get; init; } = string.Empty;

    /// <summary><see cref="ValueSets.EvseStatus"/>.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = ValueSets.Status.Enabled;

    /// <summary>Year until which the meter is MID certified.</summary>
    [JsonPropertyName("midMeterCertificationEndYear")]
    public int? MidMeterCertificationEndYear { get; init; }

    /// <summary>Tariff group applied to this EVSE.</summary>
    [JsonPropertyName("tariffGroupId")]
    public long? TariffGroupId { get; init; }

    [JsonPropertyName("allowsReservation")]
    public bool? AllowsReservation { get; init; }

    [JsonPropertyName("bookingEnabled")]
    public bool? BookingEnabled { get; init; }

    [JsonPropertyName("monitoringEnabled")]
    public bool? MonitoringEnabled { get; init; }

    [JsonPropertyName("powerOptions")]
    public PowerOptions? PowerOptions { get; init; }

    [JsonPropertyName("externalId")]
    public string? ExternalId { get; init; }

    [JsonPropertyName("capabilityOverrides")]
    public EvseCapabilityOverrides? CapabilityOverrides { get; init; }

    /// <summary>Roaming operator id.</summary>
    [JsonPropertyName("roamingOperatorId")]
    public long RoamingOperatorId { get; init; }

    /// <summary>Roaming publication details.</summary>
    [JsonPropertyName("roaming")]
    public EvseRoaming? Roaming { get; init; }

    /// <summary><see cref="ValueSets.EvseHardwareStatus"/>.</summary>
    [JsonPropertyName("hardwareStatus")]
    public string? HardwareStatus { get; init; }

    [JsonPropertyName("chargingProfile")]
    public ChargingProfile? ChargingProfile { get; init; }

    /// <summary>Connectors of this EVSE.</summary>
    [JsonPropertyName("connectors")]
    public IReadOnlyList<Connector>? Connectors { get; init; }

    [JsonPropertyName("notes")]
    public IReadOnlyList<Note>? Notes { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; init; }

    [JsonPropertyName("lastUpdatedAt")]
    public DateTimeOffset? LastUpdatedAt { get; init; }
}

/// <summary>Roaming publication details of an EVSE.</summary>
public sealed record EvseRoaming
{
    /// <summary>EvseId published to the roaming platform (eMI3 format).</summary>
    [JsonPropertyName("evseId")]
    public string? EvseId { get; init; }

    /// <summary>Tariff ids published to the roaming platform.</summary>
    [JsonPropertyName("tariffIds")]
    public IReadOnlyList<string>? TariffIds { get; init; }

    /// <summary>Capabilities published to the roaming platform.</summary>
    [JsonPropertyName("capabilities")]
    public IReadOnlyList<string>? Capabilities { get; init; }

    [JsonPropertyName("physicalReference")]
    public string? PhysicalReference { get; init; }

    /// <summary><see cref="ValueSets.RoamingEvseStatus"/>.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }
}

/// <summary>An active smart-charging profile applied to an EVSE or charge point (OCPP).</summary>
public sealed record ChargingProfile
{
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    [JsonPropertyName("minChargingRate")]
    public decimal? MinChargingRate { get; init; }

    [JsonPropertyName("schedulePeriods")]
    public IReadOnlyList<ChargingSchedulePeriod>? SchedulePeriods { get; init; }

    [JsonPropertyName("scheduleStart")]
    public DateTimeOffset? ScheduleStart { get; init; }

    /// <summary><see cref="ValueSets.ChargingRateUnit"/>.</summary>
    [JsonPropertyName("chargingRateUnit")]
    public string? ChargingRateUnit { get; init; }

    [JsonPropertyName("stackLevel")]
    public int? StackLevel { get; init; }

    /// <summary><see cref="ValueSets.ChargingProfileKind"/>.</summary>
    [JsonPropertyName("chargingProfileKind")]
    public string? ChargingProfileKind { get; init; }

    /// <summary><see cref="ValueSets.RecurrencyKind"/>.</summary>
    [JsonPropertyName("recurrencyKind")]
    public string? RecurrencyKind { get; init; }

    [JsonPropertyName("chargingCompleteAt")]
    public DateTimeOffset? ChargingCompleteAt { get; init; }

    /// <summary><see cref="ValueSets.ChargingProfilePurpose"/>.</summary>
    [JsonPropertyName("purpose")]
    public string? Purpose { get; init; }

    [JsonPropertyName("validTo")]
    public DateTimeOffset? ValidTo { get; init; }

    [JsonPropertyName("validFrom")]
    public DateTimeOffset? ValidFrom { get; init; }

    [JsonPropertyName("duration")]
    public int? Duration { get; init; }
}

/// <summary>A charging schedule period of a smart-charging profile.</summary>
public sealed record ChargingSchedulePeriod
{
    /// <summary>Start of the period, in seconds from the schedule start.</summary>
    [JsonPropertyName("startPeriod")]
    public int StartPeriod { get; init; }

    /// <summary>Power or current limit for the period.</summary>
    [JsonPropertyName("limit")]
    public decimal Limit { get; init; }

    [JsonPropertyName("numberPhases")]
    public int? NumberPhases { get; init; }
}

/// <summary>A connector of an EVSE.</summary>
public sealed record Connector
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    /// <summary>OCPP network identifier of the connector.</summary>
    [JsonPropertyName("networkId")]
    public string NetworkId { get; init; } = string.Empty;

    /// <summary>Connector type, e.g. <c>Type2</c>, <c>CCS</c>, <c>CHAdeMO</c> (see the API docs for the full list).</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary><see cref="ValueSets.ConnectorFormat"/>.</summary>
    [JsonPropertyName("format")]
    public string? Format { get; init; }

    /// <summary><see cref="ValueSets.ConnectorStatus"/>.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("ocpiMaxVoltage")]
    public int? OcpiMaxVoltage { get; init; }

    [JsonPropertyName("ocpiMaxAmperage")]
    public int? OcpiMaxAmperage { get; init; }

    [JsonPropertyName("ocpiMaxElectricPower")]
    public int? OcpiMaxElectricPower { get; init; }

    [JsonPropertyName("externalId")]
    public string? ExternalId { get; init; }
}

/// <summary>Payload for creating or updating an EVSE.</summary>
public sealed record EvseWrite
{
    /// <summary>Operator-defined physical reference.</summary>
    [JsonPropertyName("physicalReference")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PhysicalReference { get; init; }

    /// <summary><see cref="ValueSets.CurrentType"/>.</summary>
    [JsonPropertyName("currentType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CurrentType { get; init; }

    [JsonPropertyName("label")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Label { get; init; }

    /// <summary>OCPP network identifier of the EVSE.</summary>
    [JsonPropertyName("networkId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? NetworkId { get; init; }

    /// <summary><see cref="ValueSets.EvseStatus"/>.</summary>
    [JsonPropertyName("status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Status { get; init; }

    [JsonPropertyName("midMeterCertificationEndYear")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MidMeterCertificationEndYear { get; init; }

    [JsonPropertyName("tariffGroupId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? TariffGroupId { get; init; }

    [JsonPropertyName("allowsReservation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? AllowsReservation { get; init; }

    [JsonPropertyName("bookingEnabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? BookingEnabled { get; init; }

    [JsonPropertyName("monitoringEnabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? MonitoringEnabled { get; init; }

    [JsonPropertyName("powerOptions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PowerOptions? PowerOptions { get; init; }

    [JsonPropertyName("externalId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ExternalId { get; init; }

    [JsonPropertyName("capabilityOverrides")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public EvseCapabilityOverrides? CapabilityOverrides { get; init; }

    /// <summary>Charge point to attach the EVSE to. Required when creating an EVSE; ignored on update.</summary>
    [JsonPropertyName("chargePointId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? ChargePointId { get; init; }
}
