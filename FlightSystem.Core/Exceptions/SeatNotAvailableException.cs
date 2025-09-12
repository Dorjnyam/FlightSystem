namespace FlightSystem.Core.Exceptions
{
    public class SeatNotAvailableException : FlightSystemException
    {
        public SeatNotAvailableException(string seatNumber)
            : base($"Суудал {seatNumber} боломжгүй байна", "SEAT_NOT_AVAILABLE")
        {
        }
    }
}
