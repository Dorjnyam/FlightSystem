namespace FlightSystem.Core.DTOs.Response
{
    public class SeatRowDto
    {
        public string Row { get; set; } = string.Empty;
        public List<SeatInfoDto> Seats { get; set; } = [];
    }
}
