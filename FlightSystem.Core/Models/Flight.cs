using FlightSystem.Core.Enums;

namespace FlightSystem.Core.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; } = string.Empty;
        public int AircraftId { get; set; }
        public string DepartureAirport { get; set; } = string.Empty;
        public string ArrivalAirport { get; set; } = string.Empty;
        public DateTime ScheduledDeparture { get; set; }
        public DateTime ScheduledArrival { get; set; }
        public DateTime? ActualDeparture { get; set; }
        public DateTime? ActualArrival { get; set; }
        public FlightStatus Status { get; set; }
        public string? GateNumber { get; set; }
        public DateTime CheckinOpenTime { get; set; }
        public DateTime CheckinCloseTime { get; set; }
        public DateTime BoardingTime { get; set; }
        public int CreatedByEmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual Aircraft Aircraft { get; set; } = null!;
        public virtual Employee CreatedByEmployee { get; set; } = null!;
        public virtual ICollection<FlightPassenger> FlightPassengers { get; set; } = [];
    }
}
