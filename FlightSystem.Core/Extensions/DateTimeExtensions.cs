namespace FlightSystem.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsWithinCheckinWindow(this DateTime dateTime, DateTime checkinOpen, DateTime checkinClose)
        {
            return dateTime >= checkinOpen && dateTime <= checkinClose;
        }

        public static string ToDisplayString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm");
        }

        public static string ToTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm");
        }
    }
}
