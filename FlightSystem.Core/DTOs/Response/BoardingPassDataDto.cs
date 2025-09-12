namespace FlightSystem.Core.DTOs.Response
{
    public class BoardingPassDataDto
    {
        public string PassengerName { get; set; } = string.Empty;
        public string FlightNumber { get; set; } = string.Empty;
        public string SeatNumber { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public string Gate { get; set; } = string.Empty;
        public string BoardingPassCode { get; set; } = string.Empty;
    }
}
