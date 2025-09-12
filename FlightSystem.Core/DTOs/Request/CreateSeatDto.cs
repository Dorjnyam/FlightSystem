using FlightSystem.Core.Enums;

namespace FlightSystem.Core.DTOs.Request
{
    public class CreateSeatDto
    {
        public int AircraftId { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public string Row { get; set; } = string.Empty;
        public string Column { get; set; } = string.Empty;
        public SeatClass Class { get; set; }
        public bool IsWindowSeat { get; set; }
        public bool IsAisleSeat { get; set; }
        public bool IsEmergencyExit { get; set; }
    }
}
