namespace FlightSystem.Core.DTOs.Request
{
    public class GenerateBoardingPassRequestDto
    {
        public int FlightPassengerId { get; set; }
        public int SeatAssignmentId { get; set; }
        public int EmployeeId { get; set; }
        public string? Gate { get; set; }
    }
}
