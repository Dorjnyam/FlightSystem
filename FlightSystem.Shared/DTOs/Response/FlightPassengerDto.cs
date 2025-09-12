namespace FlightSystem.Shared.DTOs.Response
{
    public class FlightPassengerDto
    {
        public int Id { get; set; }
        public string BookingReference { get; set; } = string.Empty;
        public bool IsCheckedIn { get; set; }
        public DateTime? CheckinTime { get; set; }
        public string? CheckinByEmployee { get; set; }
        public string? SpecialRequests { get; set; }
        public string? BaggageInfo { get; set; }
        public PassengerDto Passenger { get; set; } = new();
        public FlightInfoDto Flight { get; set; } = new();
        public SeatInfoDto? AssignedSeat { get; set; }
    }
}
