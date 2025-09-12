using FlightSystem.Core.Enums;

namespace FlightSystem.Core.DTOs.Request
{
    public class UpdateSeatDto
    {
        public SeatClass? Class { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsEmergencyExit { get; set; }
    }
}
