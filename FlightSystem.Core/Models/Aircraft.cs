using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystem.Core.Models
{
    public class Aircraft
    {
        public int Id { get; set; }
        public string AircraftCode { get; set; } = string.Empty;
        public string AircraftType { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<Flight> Flights { get; set; } = [];
        public virtual ICollection<Seat> Seats { get; set; } = [];
    }
}
