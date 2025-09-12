using FlightSystem.Core.Enums;

namespace FlightSystem.Core.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int AircraftId { get; set; }
        public string SeatNumber { get; set; } = string.Empty; 
        public string Row { get; set; } = string.Empty; 
        public string Column { get; set; } = string.Empty; 
        public SeatClass Class { get; set; }
        public bool IsWindowSeat { get; set; }
        public bool IsAisleSeat { get; set; }
        public bool IsEmergencyExit { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual Aircraft Aircraft { get; set; } = null!;
        public virtual ICollection<SeatAssignment> SeatAssignments { get; set; } = [];
    }
}
