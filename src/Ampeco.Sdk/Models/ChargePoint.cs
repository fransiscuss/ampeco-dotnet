using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Models;

/// <summary>A charge point, as returned by the charge-points v2.0 endpoints.</summary>
public sealed record ChargePoint
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    /// <summary>Owning operator id (null for charge points shared from roaming).</summary>
    [JsonPropertyName("operatorId")]
    public long? OperatorId { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary><see cref="ValueSets.ChargePointType"/>.</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("pin")]
    public string? Pin { get; init; }

    /// <summary>Location this charge point belongs to.</summary>
    [JsonPropertyName("locationId")]
    public long? LocationId { get; init; }

    [JsonPropertyName("chargingZoneId")]
    public long? ChargingZoneId { get; init; }

    /// <summary>Electricity rate used for cost calculations.</summary>
    [JsonPropertyName("electricityRateId")]
    public long? ElectricityRateId { get; init; }

    /// <summary>Subscription configuration for personal (home) charge points.</summary>
    [JsonPropertyName("subscription")]
    public ChargePointSubscription? Subscription { get; init; }

    /// <summary><see cref="ValueSets.ChargePointNetworkType"/>.</summary>
    [JsonPropertyName("networkType")]
    public string? NetworkType { get; init; }

    /// <summary><see cref="ValueSets.ChargePointStatus"/>.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("managedByOperator")]
    public bool? ManagedByOperator { get; init; }

    /// <summary>External identifier supplied by the operator.</summary>
    [JsonPropertyName("externalId")]
    public string? ExternalId { get; init; }

    /// <summary>OCPP connectivity details.</summary>
    [JsonPropertyName("network")]
    public ChargePointNetwork? Network { get; init; }

    /// <summary><see cref="ValueSets.ChargePointCapability"/> values.</summary>
    [JsonPropertyName("capabilities")]
    public IReadOnlyList<string>? Capabilities { get; init; }

    /// <summary>Start charging automatically when a vehicle is plugged in without authorization.</summary>
    [JsonPropertyName("autoStartWithoutAuthorization")]
    public bool? AutoStartWithoutAuthorization { get; init; }

    [JsonPropertyName("disableAutoStartEmulation")]
    public bool? DisableAutoStartEmulation { get; init; }

    /// <summary>OCPP security settings.</summary>
    [JsonPropertyName("security")]
    public ChargePointSecurity? Security { get; init; }

    [JsonPropertyName("modelId")]
    public long? ModelId { get; init; }

    [JsonPropertyName("monitoringEnabled")]
    public bool? MonitoringEnabled { get; init; }

    [JsonPropertyName("autoRecoveryEnabled")]
    public bool? AutoRecoveryEnabled { get; init; }

    [JsonPropertyName("enableAutoFaultRecovery")]
    public bool? EnableAutoFaultRecovery { get; init; }

    /// <summary>Owner of the charge point when it is a personal (home) charger.</summary>
    [JsonPropertyName("user")]
    public ChargePointOwner? User { get; init; }

    /// <summary>Partner the charge point is shared with / owned by.</summary>
    [JsonPropertyName("partner")]
    public ChargePointPartner? Partner { get; init; }

    [JsonPropertyName("utilityId")]
    public long? UtilityId { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; init; }

    [JsonPropertyName("firstContactAt")]
    public DateOnly? FirstContactAt { get; init; }

    /// <summary>Roaming operator id when the charge point is accessed via roaming.</summary>
    [JsonPropertyName("roamingOperatorId")]
    public long? RoamingOperatorId { get; init; }

    [JsonPropertyName("displayTariffAndCosts")]
    public bool? DisplayTariffAndCosts { get; init; }

    [JsonPropertyName("tariffDisplayMessages")]
    public ChargePointTariffDisplayMessages? TariffDisplayMessages { get; init; }

    [JsonPropertyName("lastBootNotification")]
    public ChargePointBootNotification? LastBootNotification { get; init; }

    /// <summary><see cref="ValueSets.ChargePointNetworkStatus"/>.</summary>
    [JsonPropertyName("networkStatus")]
    public string? NetworkStatus { get; init; }

    [JsonPropertyName("lastNetworkStatusUpdateAt")]
    public DateTimeOffset? LastNetworkStatusUpdateAt { get; init; }

    /// <summary><see cref="ValueSets.ChargePointHardwareStatus"/>.</summary>
    [JsonPropertyName("hardwareStatus")]
    public string? HardwareStatus { get; init; }

    [JsonPropertyName("vendorErrorCode")]
    public string? VendorErrorCode { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyDictionary<string, string>? Tags { get; init; }

    [JsonPropertyName("uptimeTrackingEnabled")]
    public bool? UptimeTrackingEnabled { get; init; }

    [JsonPropertyName("uptimeTrackingActivatedAt")]
    public DateTimeOffset? UptimeTrackingActivatedAt { get; init; }

    /// <summary>Sharing code for personal charger sharing.</summary>
    [JsonPropertyName("sharingCode")]
    public string? SharingCode { get; init; }

    /// <summary><see cref="ValueSets.SharingBillingRule"/>.</summary>
    [JsonPropertyName("sharingBillingRule")]
    public string? SharingBillingRule { get; init; }

    [JsonPropertyName("enabledRandomisedDelay")]
    public bool? EnabledRandomisedDelay { get; init; }

    [JsonPropertyName("usesRenewableEnergy")]
    public bool? UsesRenewableEnergy { get; init; }

    [JsonPropertyName("integratedAt")]
    public DateTimeOffset? IntegratedAt { get; init; }

    [JsonPropertyName("manufacturedAt")]
    public DateTimeOffset? ManufacturedAt { get; init; }

    [JsonPropertyName("lastUpdatedAt")]
    public DateTimeOffset? LastUpdatedAt { get; init; }

    [JsonPropertyName("ocppConnectedChargePointId")]
    public long? OcppConnectedChargePointId { get; init; }

    [JsonPropertyName("notes")]
    public IReadOnlyList<Note>? Notes { get; init; }

    [JsonPropertyName("countryStationId")]
    public string? CountryStationId { get; init; }
}

/// <summary>OCPP connectivity details of a charge point.</summary>
public sealed record ChargePointNetwork
{
    [JsonPropertyName("ocppVersion")]
    public string? OcppVersion { get; init; }

    [JsonPropertyName("ocppId")]
    public string? OcppId { get; init; }

    [JsonPropertyName("securityProfile")]
    public int? SecurityProfile { get; init; }
}

/// <summary>OCPP security settings of a charge point.</summary>
public sealed record ChargePointSecurity
{
    /// <summary>Desired OCPP security profile (0–3).</summary>
    [JsonPropertyName("desiredLevel")]
    public int? DesiredLevel { get; init; }

    /// <summary><see cref="ValueSets.ChargePointNetworkStatus"/>-like applied status: <c>applied</c>, <c>pending</c> or <c>rejected</c>.</summary>
    [JsonPropertyName("desiredProfileStatus")]
    public string? DesiredProfileStatus { get; init; }

    [JsonPropertyName("currentLevel")]
    public int? CurrentLevel { get; init; }
}

/// <summary>Subscription configuration of a personal (home) charge point.</summary>
public sealed record ChargePointSubscription
{
    [JsonPropertyName("subscriptionId")]
    public long? SubscriptionId { get; init; }

    [JsonPropertyName("isActive")]
    public bool? IsActive { get; init; }
}

/// <summary>Owner of a personal (home) charge point.</summary>
public sealed record ChargePointOwner
{
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }
}

/// <summary>Partner associated with a charge point.</summary>
public sealed record ChargePointPartner
{
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>Tariff display message configuration.</summary>
public sealed record ChargePointTariffDisplayMessages
{
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    [JsonPropertyName("language")]
    public string? Language { get; init; }
}

/// <summary>Last OCPP boot notification received from a charge point.</summary>
public sealed record ChargePointBootNotification
{
    [JsonPropertyName("serialNumber")]
    public string? SerialNumber { get; init; }

    [JsonPropertyName("vendor")]
    public string? Vendor { get; init; }

    [JsonPropertyName("model")]
    public string? Model { get; init; }

    [JsonPropertyName("firmwareVersion")]
    public string? FirmwareVersion { get; init; }

    [JsonPropertyName("chargePointSerialNumber")]
    public string? ChargePointSerialNumber { get; init; }

    [JsonPropertyName("chargePointModel")]
    public string? ChargePointModel { get; init; }

    [JsonPropertyName("chargePointVendor")]
    public string? ChargePointVendor { get; init; }

    [JsonPropertyName("receivedAt")]
    public DateTimeOffset? ReceivedAt { get; init; }
}

/// <summary>
/// Payload for creating or updating a charge point (charge-points v2.0).
/// Only set properties are sent, so the same class works for both create and update.
/// </summary>
public sealed record ChargePointWrite
{
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; init; }

    /// <summary><see cref="ValueSets.ChargePointType"/>.</summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; init; }

    [JsonPropertyName("pin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Pin { get; init; }

    [JsonPropertyName("locationId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? LocationId { get; init; }

    [JsonPropertyName("chargingZoneId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? ChargingZoneId { get; init; }

    [JsonPropertyName("electricityRateId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? ElectricityRateId { get; init; }

    [JsonPropertyName("networkType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? NetworkType { get; init; }

    /// <summary><see cref="ValueSets.ChargePointStatus"/>.</summary>
    [JsonPropertyName("status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Status { get; init; }

    [JsonPropertyName("managedByOperator")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ManagedByOperator { get; init; }

    [JsonPropertyName("externalId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ExternalId { get; init; }

    /// <summary><see cref="ValueSets.ChargePointCapability"/> values.</summary>
    [JsonPropertyName("capabilities")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<string>? Capabilities { get; init; }

    [JsonPropertyName("autoStartWithoutAuthorization")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? AutoStartWithoutAuthorization { get; init; }

    [JsonPropertyName("disableAutoStartEmulation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? DisableAutoStartEmulation { get; init; }

    [JsonPropertyName("security")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ChargePointWriteSecurity? Security { get; init; }

    [JsonPropertyName("modelId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? ModelId { get; init; }

    [JsonPropertyName("monitoringEnabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? MonitoringEnabled { get; init; }

    [JsonPropertyName("autoRecoveryEnabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? AutoRecoveryEnabled { get; init; }

    [JsonPropertyName("enableAutoFaultRecovery")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? EnableAutoFaultRecovery { get; init; }

    /// <summary>Owner of the charge point when it is a personal (home) charger.</summary>
    [JsonPropertyName("user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ChargePointWriteOwner? User { get; init; }

    [JsonPropertyName("utilityId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? UtilityId { get; init; }

    [JsonPropertyName("tags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyDictionary<string, string>? Tags { get; init; }

    [JsonPropertyName("uptimeTrackingEnabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? UptimeTrackingEnabled { get; init; }

    [JsonPropertyName("sharingCode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SharingCode { get; init; }

    [JsonPropertyName("enabledRandomisedDelay")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? EnabledRandomisedDelay { get; init; }

    [JsonPropertyName("usesRenewableEnergy")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? UsesRenewableEnergy { get; init; }

    [JsonPropertyName("integratedAt")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTimeOffset? IntegratedAt { get; init; }

    [JsonPropertyName("manufacturedAt")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTimeOffset? ManufacturedAt { get; init; }

    [JsonPropertyName("ocppConnectedChargePointId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? OcppConnectedChargePointId { get; init; }

    [JsonPropertyName("countryStationId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CountryStationId { get; init; }
}

/// <summary>Security settings sent when writing a charge point.</summary>
public sealed record ChargePointWriteSecurity
{
    /// <summary>Desired OCPP security profile (0–3).</summary>
    [JsonPropertyName("desiredLevel")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? DesiredLevel { get; init; }
}

/// <summary>Owner assignment when writing a personal (home) charge point.</summary>
public sealed record ChargePointWriteOwner
{
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? Id { get; init; }

    [JsonPropertyName("email")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Email { get; init; }
}

/// <summary>Live status of a charge point (status endpoint).</summary>
public sealed record ChargePointStatusInfo
{
    /// <summary><see cref="ValueSets.ChargePointNetworkStatus"/>.</summary>
    [JsonPropertyName("networkStatus")]
    public string? NetworkStatus { get; init; }

    /// <summary><see cref="ValueSets.ChargePointHardwareStatus"/>.</summary>
    [JsonPropertyName("hardwareStatus")]
    public string? HardwareStatus { get; init; }

    [JsonPropertyName("evses")]
    public IReadOnlyList<EvseStatusInfo>? Evses { get; init; }

    [JsonPropertyName("lastUpdatedAt")]
    public DateTimeOffset? LastUpdatedAt { get; init; }
}

/// <summary>Live status of a single EVSE, as reported in the charge point status.</summary>
public sealed record EvseStatusInfo
{
    [JsonPropertyName("networkId")]
    public string? NetworkId { get; init; }

    [JsonPropertyName("hardwareStatus")]
    public string? HardwareStatus { get; init; }

    [JsonPropertyName("connectors")]
    public IReadOnlyList<ConnectorStatusInfo>? Connectors { get; init; }
}

/// <summary>Live status of a single connector.</summary>
public sealed record ConnectorStatusInfo
{
    [JsonPropertyName("networkId")]
    public string? NetworkId { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }
}
