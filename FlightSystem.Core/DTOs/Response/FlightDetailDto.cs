namespace FlightSystem.Core.DTOs.Response
{
    public class FlightDetailDto : FlightInfoDto
    {
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public int CheckedInPassengers { get; set; }
        public int BoardedPassengers { get; set; }
        public string CreatedByEmployee { get; set; } = string.Empty;
    }
}
