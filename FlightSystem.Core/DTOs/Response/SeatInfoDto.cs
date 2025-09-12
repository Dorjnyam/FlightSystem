namespace FlightSystem.Core.DTOs.Response
{
    public class SeatInfoDto
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public string Row { get; set; } = string.Empty;
        public string Column { get; set; } = string.Empty;
        public string SeatClass { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public bool IsWindowSeat { get; set; }
        public bool IsAisleSeat { get; set; }
        public bool IsEmergencyExit { get; set; }
        public string? PassengerName { get; set; }
        public string? PassengerPassport { get; set; }
    }
}
