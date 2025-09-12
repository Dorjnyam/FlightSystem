using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystem.Core.Models
{
    public class BoardingPass
    {
        public int Id { get; set; }
        public int FlightPassengerId { get; set; }
        public int SeatAssignmentId { get; set; }
        public string BoardingPassCode { get; set; } = string.Empty;
        public string QRCode { get; set; } = string.Empty;
        public string BarcodeData { get; set; } = string.Empty;
        public DateTime IssuedAt { get; set; }
        public int IssuedByEmployeeId { get; set; }
        public DateTime? BoardingTime { get; set; }
        public bool IsBoardingComplete { get; set; }
        public string? Gate { get; set; }
        public virtual FlightPassenger FlightPassenger { get; set; } = null!;
        public virtual SeatAssignment SeatAssignment { get; set; } = null!;
        public virtual Employee IssuedByEmployee { get; set; } = null!;
    }
}
