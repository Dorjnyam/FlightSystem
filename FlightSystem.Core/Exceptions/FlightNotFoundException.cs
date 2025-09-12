namespace FlightSystem.Core.Exceptions
{
    public class FlightNotFoundException : FlightSystemException
    {
        public FlightNotFoundException(string flightNumber)
            : base($"Нислэг {flightNumber} олдсонгүй", "FLIGHT_NOT_FOUND")
        {
        }
    }
}
