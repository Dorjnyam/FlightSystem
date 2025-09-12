using FlightSystem.Shared.Enums;

namespace FlightSystem.Shared.DTOs.Request
{
    public class CreateFlightDto
    {
        public string FlightNumber { get; set; } = string.Empty;
        public int AircraftId { get; set; }
        public string DepartureAirport { get; set; } = string.Empty;
        public string ArrivalAirport { get; set; } = string.Empty;
        public DateTime ScheduledDeparture { get; set; }
        public DateTime ScheduledArrival { get; set; }
        public string? GateNumber { get; set; }
        public DateTime CheckinOpenTime { get; set; }
        public DateTime CheckinCloseTime { get; set; }
        public DateTime BoardingTime { get; set; }
        public int CreatedByEmployeeId { get; set; }
    }
}
