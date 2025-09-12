namespace FlightSystem.Core.DTOs.Response
{
    public class AircraftDetailDto : AircraftDto
    {
        public List<SeatDto> Seats { get; set; } = [];
        public Dictionary<string, int> SeatClassDistribution { get; set; } = [];
        public int ActiveFlights { get; set; }
    }
}
