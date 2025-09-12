using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSystem.Core.Enums;

namespace FlightSystem.Core.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public string PassportNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public PassengerType Type { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<FlightPassenger> FlightPassengers { get; set; } = [];
    }
}
