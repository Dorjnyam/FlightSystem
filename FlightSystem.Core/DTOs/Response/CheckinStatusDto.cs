namespace FlightSystem.Core.DTOs.Response
{
    public class CheckinStatusDto
    {
        public bool IsCheckedIn { get; set; }
        public DateTime? CheckinTime { get; set; }
        public string? EmployeeName { get; set; }
        public bool HasSeatAssignment { get; set; }
        public SeatInfoDto? AssignedSeat { get; set; }
        public bool HasBoardingPass { get; set; }
        public BoardingPassDto? BoardingPass { get; set; }
    }
}
