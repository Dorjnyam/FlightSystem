namespace FlightSystem.Shared.DTOs.Response
{
    public class FlightInfoDto
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; } = string.Empty;
        public string DepartureAirport { get; set; } = string.Empty;
        public string ArrivalAirport { get; set; } = string.Empty;
        public DateTime ScheduledDeparture { get; set; }
        public DateTime ScheduledArrival { get; set; }
        public DateTime? ActualDeparture { get; set; }
        public DateTime? ActualArrival { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? GateNumber { get; set; }
        public string AircraftType { get; set; } = string.Empty;
        public DateTime CheckinOpenTime { get; set; }
        public DateTime CheckinCloseTime { get; set; }
        public DateTime BoardingTime { get; set; }
    }
}
