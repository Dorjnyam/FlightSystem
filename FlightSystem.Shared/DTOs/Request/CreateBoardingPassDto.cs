namespace FlightSystem.Shared.DTOs.Request
{
    public class CreateBoardingPassDto
    {
        public int FlightPassengerId { get; set; }
        public int SeatAssignmentId { get; set; }
        public int IssuedByEmployeeId { get; set; }
        public string? Gate { get; set; }
    }
}
