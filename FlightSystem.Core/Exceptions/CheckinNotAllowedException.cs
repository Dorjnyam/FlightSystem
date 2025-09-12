namespace FlightSystem.Core.Exceptions
{
    public class CheckinNotAllowedException : FlightSystemException
    {
        public CheckinNotAllowedException(string reason)
            : base($"Бүртгэл хийх боломжгүй: {reason}", "CHECKIN_NOT_ALLOWED")
        {
        }
    }
}
