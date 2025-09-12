namespace FlightSystem.Core.Models
{
    public class FlightPassenger
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public int PassengerId { get; set; }
        public string BookingReference { get; set; } = string.Empty;
        public bool IsCheckedIn { get; set; }
        public DateTime? CheckinTime { get; set; }
        public int? CheckinByEmployeeId { get; set; }
        public string? SpecialRequests { get; set; }
        public string? BaggageInfo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual Flight Flight { get; set; } = null!;
        public virtual Passenger Passenger { get; set; } = null!;
        public virtual Employee? CheckinByEmployee { get; set; }
        public virtual ICollection<SeatAssignment> SeatAssignments { get; set; } = [];
        public virtual ICollection<BoardingPass> BoardingPasses { get; set; } = [];
    }
}
