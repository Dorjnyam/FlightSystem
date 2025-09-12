namespace FlightSystem.Shared.DTOs.Request
{
    public class ReserveSeatRequestDto
    {
        public int FlightId { get; set; }
        public int SeatId { get; set; }
        public int PassengerId { get; set; }
        public int EmployeeId { get; set; }
    }
}
