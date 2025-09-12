namespace FlightSystem.Shared.Constants;

public static class UIConstants
{
    public static class Colors
    {
        public const string Success = "#28a745";
        public const string Warning = "#ffc107";
        public const string Danger = "#dc3545";
        public const string Info = "#17a2b8";
        public const string Primary = "#007bff";
        public const string Secondary = "#6c757d";
    }

    public static class FlightStatusColors
    {
        public const string Scheduled = "#6c757d";
        public const string CheckinOpen = "#28a745";
        public const string CheckinClosed = "#ffc107";
        public const string Boarding = "#17a2b8";
        public const string LastCall = "#fd7e14";
        public const string GateClosed = "#dc3545";
        public const string Departed = "#343a40";
        public const string Delayed = "#e83e8c";
        public const string Cancelled = "#dc3545";
    }

    public static class SeatClasses
    {
        public const string Economy = "#f8f9fa";
        public const string PremiumEconomy = "#e9ecef";
        public const string Business = "#dee2e6";
        public const string First = "#adb5bd";
    }

    public static class Spacing
    {
        public const int Small = 8;
        public const int Medium = 16;
        public const int Large = 24;
        public const int ExtraLarge = 32;
    }
}
