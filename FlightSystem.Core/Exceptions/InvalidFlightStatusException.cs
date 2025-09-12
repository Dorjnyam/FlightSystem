using FlightSystem.Core.Enums;

namespace FlightSystem.Core.Exceptions
{
    public class InvalidFlightStatusException : FlightSystemException
    {
        public InvalidFlightStatusException(FlightStatus currentStatus, FlightStatus newStatus)
            : base($"Нислэгийн төлөвийг {currentStatus}-с {newStatus} руу өөрчилж болохгүй", "INVALID_FLIGHT_STATUS")
        {
        }
    }
}
