namespace Ampeco.Sdk.Models;

/// <summary>
/// Well-known values used across the AMPECO Public API. Enum-like fields are typed as
/// <see cref="string"/> so that new values introduced by AMPECO never break deserialization;
/// these constants cover the values documented in the current API spec.
/// </summary>
public static class ValueSets
{
    /// <summary>Charge point type.</summary>
    public static class ChargePointType
    {
        public const string Private = "private";
        public const string Public = "public";
        public const string Personal = "personal";
    }

    /// <summary>Charge point administrative status.</summary>
    public static class ChargePointStatus
    {
        public const string Enabled = "enabled";
        public const string Disabled = "disabled";
        public const string OutOfOrder = "out of order";
        public const string Demo = "demo";
    }

    /// <summary>Charge point network connectivity type.</summary>
    public static class ChargePointNetworkType
    {
        public const string Cellular = "cellular";
        public const string Ethernet = "ethernet";
        public const string Wlan = "wlan";
    }

    /// <summary>Charge point OCPP connectivity status.</summary>
    public static class ChargePointNetworkStatus
    {
        public const string NeverConnected = "never_connected";
        public const string Available = "available";
        public const string TemporarilyUnavailable = "temporarily_unavailable";
        public const string LongTermUnavailable = "long-term_unavailable";
    }

    /// <summary>Charge point hardware status.</summary>
    public static class ChargePointHardwareStatus
    {
        public const string Available = "available";
        public const string Unavailable = "unavailable";
        public const string Faulted = "faulted";
    }

    /// <summary>Charge point capabilities.</summary>
    public static class ChargePointCapability
    {
        public const string RemoteStartStopCapable = "remote_start_stop_capable";
        public const string MeterValues = "meter_values";
        public const string StopTransactionOnEvDisconnect = "stop_transaction_on_ev_disconnect";
        public const string DisregardTheHeartbeats = "disregard_the_heartbeats";
        public const string DisplayMessages = "display_messages";
    }

    /// <summary>Charge point sharing billing rule.</summary>
    public static class SharingBillingRule
    {
        public const string OwnerCoversGuests = "owner_covers_guests";
        public const string GuestsPayDirectly = "guests_pay_directly";
        public const string EnforceSubscription = "enforce_subscription";
    }

    /// <summary>EVSE administrative status.</summary>
    public static class EvseStatus
    {
        public const string Enabled = "enabled";
        public const string Disabled = "disabled";
        public const string OutOfOrder = "out of order";
    }

    /// <summary>EVSE live hardware status.</summary>
    public static class EvseHardwareStatus
    {
        public const string Available = "available";
        public const string Preparing = "preparing";
        public const string Charging = "charging";
        public const string SuspendedEv = "suspendedEV";
        public const string SuspendedEvse = "suspendedEVSE";
        public const string Finishing = "finishing";
        public const string Reserved = "reserved";
        public const string Unavailable = "unavailable";
        public const string Faulted = "faulted";
        public const string Occupied = "occupied";
    }

    /// <summary>Charging current type of an EVSE.</summary>
    public static class CurrentType
    {
        public const string Ac = "ac";
        public const string Dc = "dc";
    }

    /// <summary>Connector format.</summary>
    public static class ConnectorFormat
    {
        public const string Socket = "socket";
        public const string Cable = "cable";
    }

    /// <summary>Connector administrative status.</summary>
    public static class ConnectorStatus
    {
        public const string Enabled = "enabled";
        public const string Disabled = "disabled";
    }

    /// <summary>Location / EVSE administrative status.</summary>
    public static class Status
    {
        public const string Enabled = "enabled";
        public const string Disabled = "disabled";
    }

    /// <summary>Location parking type.</summary>
    public static class ParkingType
    {
        public const string AlongMotorway = "ALONG_MOTORWAY";
        public const string ParkingGarage = "PARKING_GARAGE";
        public const string ParkingLot = "PARKING_LOT";
        public const string OnDriveway = "ON_DRIVEWAY";
        public const string OnStreet = "ON_STREET";
        public const string UndergroundGarage = "UNDERGROUND_GARAGE";
        public const string IsTent = "IS_TENT";
        public const string Secure = "SECURE";
    }

    /// <summary>Location access methods.</summary>
    public static class AccessMethod
    {
        public const string Open = "OPEN";
        public const string Token = "TOKEN";
        public const string LicensePlate = "LICENSE_PLATE";
        public const string AccessCode = "ACCESS_CODE";
        public const string Intercom = "INTERCOM";
        public const string ParkingTicket = "PARKING_TICKET";
    }

    /// <summary>User status.</summary>
    public static class UserStatus
    {
        public const string Enabled = "enabled";
        public const string Disabled = "disabled";
    }

    /// <summary>Source of a user's most recent activity.</summary>
    public static class UserActivitySource
    {
        public const string DeviceActivity = "device_activity";
        public const string DeviceLogout = "device_logout";
        public const string IdTagAuthorization = "id_tag_authorization";
        public const string Transaction = "transaction";
        public const string Registration = "registration";
    }

    /// <summary>Charging session status.</summary>
    public static class SessionStatus
    {
        public const string Unknown = "unknown";
        public const string Pending = "pending";
        public const string Active = "active";
        public const string Finished = "finished";
        public const string Failed = "failed";
        public const string Expired = "expired";
    }

    /// <summary>Reason a charging session ended unsuccessfully.</summary>
    public static class SessionEndReason
    {
        public const string WorkingHoursExceeded = "working_hours_exceeded";
        public const string Timeout = "timeout";
        public const string AuthorizationAmountReached = "authorization_amount_reached";
        public const string NewStartTransactionReceived = "new_start_transaction_received";
        public const string ForceStop = "force_stop";
        public const string StandardStop = "standard_stop";
        public const string EnergyExceeded = "energy_exceeded";
        public const string ScheduleCompleted = "schedule_completed";
        public const string PreAuthorizationFailed = "pre_authorization_failed";
        public const string SystemForceStop = "system_force_stop";
        public const string TimeLimit = "time_limit";
        public const string BalanceExceeded = "balance_exceeded";
        public const string Deauthorized = "deauthorized";
        public const string EmergencyStop = "emergencystop";
        public const string EnergyLimitReached = "energylimitreached";
        public const string EvDisconnected = "evdisconnected";
        public const string GroundFault = "groundfault";
        public const string ImmediateReset = "immediatereset";
        public const string HardReset = "hardreset";
        public const string Local = "local";
        public const string LocalOutOfCredit = "localoutofcredit";
        public const string Other = "other";
        public const string OverCurrentFault = "overcurrentfault";
        public const string PowerLoss = "powerloss";
    }

    /// <summary>How a session is paid for.</summary>
    public static class SessionPaymentType
    {
        public const string Tokenized = "tokenized";
        public const string Subscription = "subscription";
        public const string Balance = "balance";
        public const string Corporate = "corporate";
        public const string Terminal = "terminal";
        public const string BankTransfer = "bank_transfer";
    }

    /// <summary>Payment status of a session.</summary>
    public static class PaymentStatus
    {
        public const string Pending = "pending";
        public const string Paid = "paid";
        public const string Partially = "partially";
        public const string Failed = "failed";
    }

    /// <summary>Billing status of a session.</summary>
    public static class SessionBillingStatus
    {
        public const string Pending = "pending";
        public const string Suspended = "suspended";
        public const string Completed = "completed";
    }

    /// <summary>Reservation status.</summary>
    public static class ReservationStatus
    {
        public const string Active = "active";
        public const string Expired = "expired";
        public const string Canceled = "canceled";
        public const string Done = "done";
    }

    /// <summary>Transaction status.</summary>
    public static class TransactionStatus
    {
        public const string Pending = "pending";
        public const string Finalized = "finalized";
        public const string Failed = "failed";
        public const string Reversed = "reversed";
        public const string Refunded = "refunded";
        public const string Authorized = "authorized";
        public const string Initialized = "initialized";
    }

    /// <summary>What a transaction paid for.</summary>
    public static class TransactionPurchaseType
    {
        public const string SubscriptionPlan = "subscription_plan";
        public const string TopUpPackage = "topup_package";
        public const string Session = "session";
        public const string PaymentAuthorisation = "payment_authorisation";
        public const string PaymentTerminalAuthorisation = "payment_terminal_authorisation";
        public const string CustomFee = "custom_fee";
        public const string CardRegistration = "card_registration";
    }

    /// <summary>Whether billing is handled internally by AMPECO or externally.</summary>
    public static class BillingType
    {
        public const string Internal = "internal";
        public const string External = "external";
    }

    /// <summary>Subscription status.</summary>
    public static class SubscriptionStatus
    {
        public const string Active = "active";
        public const string Canceled = "canceled";
        public const string Expired = "expired";
        public const string Suspended = "suspended";
        public const string Pending = "pending";
    }

    /// <summary>Tariff type.</summary>
    public static class TariffType
    {
        public const string Free = "free";
        public const string FlatRate = "flat rate";
        public const string DurationEnergy = "duration+energy";
        public const string DurationEnergyTimeOfDay = "duration+energy time of day";
        public const string EnergyTimeOfDay = "energy tou";
        public const string Standard = "standard";
        public const string ChargingNotAllowed = "charging not allowed";
        public const string AveragePowerLevels = "average power levels";
        public const string PeakPowerLevels = "peak power levels";
        public const string StandardTimeOfDay = "standard_tod";
        public const string OptimisedDynamicPricing = "optimised dynamic pricing";
        public const string DiscountBased = "discount based";
    }

    /// <summary>Invoice type.</summary>
    public static class InvoiceType
    {
        public const string Invoice = "invoice";
        public const string ProForma = "pro_forma";
        public const string CreditNote = "credit_note";
    }

    /// <summary>Invoice/receipt payment status.</summary>
    public static class InvoicePaymentStatus
    {
        public const string Paid = "paid";
        public const string PartiallyPaid = "partially_paid";
        public const string Pending = "pending";
    }

    /// <summary>Invoice fiscalization status.</summary>
    public static class FiscalizationStatus
    {
        public const string Pending = "pending";
        public const string Certified = "certified";
        public const string Failed = "failed";
        public const string Canceled = "canceled";
    }

    /// <summary>CDR protocol type.</summary>
    public static class CdrProtocolType
    {
        public const string Ocpi = "OCPI";
        public const string Hubject = "Hubject";
    }

    /// <summary>Roaming connection protocol.</summary>
    public static class RoamingProtocol
    {
        public const string Ocpi = "OCPI";
        public const string OcpiEClearing211 = "ocpi_e_clearing_2_1_1";
        public const string OcpiEClearing221 = "ocpi_e_clearing_2_2_1";
        public const string OcpiGireve = "OCPI Gireve";
        public const string GireveOcpi211 = "Gireve OCPI 2.1.1";
        public const string HubjectOicp22 = "Hubject OICP 2.2";
        public const string HubjectOicp23 = "Hubject OICP 2.3";
        public const string HubjectOcpi = "hubject_ocpi";
    }

    /// <summary>Roaming EVSE status as reported by the roaming platform (OCPI statuses).</summary>
    public static class RoamingEvseStatus
    {
        public const string Available = "available";
        public const string Blocked = "blocked";
        public const string Charging = "charging";
        public const string Inoperative = "inoperative";
        public const string OutOfOrder = "outoforder";
        public const string Planned = "planned";
        public const string Removed = "removed";
        public const string Reserved = "reserved";
        public const string Unknown = "unknown";
    }

    /// <summary>Reset types for the charge point reset action (OCPP).</summary>
    public static class ResetType
    {
        public const string Soft = "Soft";
        public const string Hard = "Hard";
    }

    /// <summary>Availability change types for the change-availability action (OCPP).</summary>
    public static class AvailabilityType
    {
        /// <summary>Charge point is not available for charging.</summary>
        public const string Inoperative = "Inoperative";

        /// <summary>Charge point is available for charging.</summary>
        public const string Operative = "Operative";
    }

    /// <summary>OCPP charging profile purpose.</summary>
    public static class ChargingProfilePurpose
    {
        public const string ChargePointMaxProfile = "ChargePointMaxProfile";
        public const string TxDefaultProfile = "TxDefaultProfile";
        public const string TxProfile = "TxProfile";
    }

    /// <summary>OCPP charging profile kind.</summary>
    public static class ChargingProfileKind
    {
        public const string Absolute = "Absolute";
        public const string Recurring = "Recurring";
        public const string Relative = "Relative";
    }

    /// <summary>OCPP charging rate unit.</summary>
    public static class ChargingRateUnit
    {
        public const string Amperes = "A";
        public const string Watts = "W";
    }

    /// <summary>OCPP charging profile recurrency kind.</summary>
    public static class RecurrencyKind
    {
        public const string Daily = "Daily";
        public const string Weekly = "Weekly";
    }

    /// <summary>Payment processors supported by the pre-authorization action.</summary>
    public static class PreAuthorizationProcessor
    {
        public const string Worldline = "worldline";
        public const string Stripe = "stripe";
    }
}
