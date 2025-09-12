using FlightSystem.Shared.Enums;

namespace FlightSystem.Core.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public EmployeeRole Role { get; set; }
        public string WorkStationId { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<Flight> CreatedFlights { get; set; } = [];
        public virtual ICollection<FlightPassenger> CheckedInPassengers { get; set; } = [];
        public virtual ICollection<SeatAssignment> SeatAssignments { get; set; } = [];
        public virtual ICollection<BoardingPass> IssuedBoardingPasses { get; set; } = [];
    }
}
