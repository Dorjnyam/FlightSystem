namespace FlightSystem.Shared.DTOs.Response
{
    public class SeatDto
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public string Row { get; set; } = string.Empty;
        public string Column { get; set; } = string.Empty;
        public string SeatClass { get; set; } = string.Empty;
        public bool IsWindowSeat { get; set; }
        public bool IsAisleSeat { get; set; }
        public bool IsEmergencyExit { get; set; }
        public bool IsActive { get; set; }
        public string AircraftCode { get; set; } = string.Empty;
    }
}
