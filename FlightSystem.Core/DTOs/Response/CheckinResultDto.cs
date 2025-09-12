namespace FlightSystem.Core.DTOs.Response
{
    public class CheckinResultDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public FlightPassengerDto? FlightPassenger { get; set; }
        public SeatAssignmentDto? SeatAssignment { get; set; }
        public BoardingPassDto? BoardingPass { get; set; }
        public List<string> Errors { get; set; } = [];
    }
}
