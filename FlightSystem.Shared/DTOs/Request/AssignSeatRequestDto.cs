namespace FlightSystem.Shared.DTOs.Request
{
    public class AssignSeatRequestDto
    {
        public int FlightPassengerId { get; set; }
        public int SeatId { get; set; }
        public int EmployeeId { get; set; }
        public string? Notes { get; set; }
    }
}
