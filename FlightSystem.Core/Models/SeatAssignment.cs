using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystem.Core.Models
{
    public class SeatAssignment
    {
        public int Id { get; set; }
        public int FlightPassengerId { get; set; }
        public int SeatId { get; set; }
        public DateTime AssignedAt { get; set; }
        public int AssignedByEmployeeId { get; set; }
        public bool IsActive { get; set; }
        public string? Notes { get; set; }
        public virtual FlightPassenger FlightPassenger { get; set; } = null!;
        public virtual Seat Seat { get; set; } = null!;
        public virtual Employee AssignedByEmployee { get; set; } = null!;
        public virtual ICollection<BoardingPass> BoardingPasses { get; set; } = [];
    }
}
