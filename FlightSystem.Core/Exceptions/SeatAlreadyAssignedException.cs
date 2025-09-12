namespace FlightSystem.Core.Exceptions
{
    public class SeatAlreadyAssignedException : FlightSystemException
    {
        public SeatAlreadyAssignedException(string seatNumber, int flightId)
            : base($"Суудал {seatNumber} аль хэдийн нислэг {flightId}-д оноогдсон байна", "SEAT_ALREADY_ASSIGNED")
        {
        }
    }
}
