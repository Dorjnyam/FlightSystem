namespace FlightSystem.Core.Exceptions
{
    public class CheckinTimeExpiredException : FlightSystemException
    {
        public CheckinTimeExpiredException()
            : base("Бүртгэлийн хугацаа дууссан байна", "CHECKIN_TIME_EXPIRED")
        {
        }
    }
}
