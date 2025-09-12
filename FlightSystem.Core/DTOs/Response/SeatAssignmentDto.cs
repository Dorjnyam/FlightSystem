namespace FlightSystem.Core.DTOs.Response
{
    public class SeatAssignmentDto
    {
        public int Id { get; set; }
        public DateTime AssignedAt { get; set; }
        public string AssignedByEmployee { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string? Notes { get; set; }
        public SeatInfoDto Seat { get; set; } = new();
        public PassengerDto Passenger { get; set; } = new();
    }
}
