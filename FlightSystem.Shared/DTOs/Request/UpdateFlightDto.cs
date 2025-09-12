using FlightSystem.Shared.Enums;

namespace FlightSystem.Shared.DTOs.Request
{
    public class UpdateFlightDto
    {
        public string? GateNumber { get; set; }
        public DateTime? ActualDeparture { get; set; }
        public DateTime? ActualArrival { get; set; }
        public FlightStatus? Status { get; set; }
        public DateTime? CheckinOpenTime { get; set; }
        public DateTime? CheckinCloseTime { get; set; }
        public DateTime? BoardingTime { get; set; }
    }
}
