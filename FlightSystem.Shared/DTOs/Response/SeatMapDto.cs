namespace FlightSystem.Shared.DTOs.Response
{
    public class SeatMapDto
    {
        public int FlightId { get; set; }
        public string FlightNumber { get; set; } = string.Empty;
        public string AircraftType { get; set; } = string.Empty;
        public List<SeatRowDto> SeatRows { get; set; } = [];
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public Dictionary<string, int> SeatClassCounts { get; set; } = [];
    }
}
