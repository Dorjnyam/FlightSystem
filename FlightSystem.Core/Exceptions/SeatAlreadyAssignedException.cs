namespace FlightSystem.Core.Exceptions
{
    public class SeatAlreadyAssignedException : FlightSystemException
    {
        public SeatAlreadyAssignedException(string seatNumber, int flightId)
            : base($"������ {seatNumber} ��� ������ ������ {flightId}-� ��������� �����", "SEAT_ALREADY_ASSIGNED")
        {
        }
    }
}
