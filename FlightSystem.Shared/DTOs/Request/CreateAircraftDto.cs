namespace FlightSystem.Shared.DTOs.Request
{
    public class CreateAircraftDto
    {
        public string AircraftCode { get; set; } = string.Empty;
        public string AircraftType { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
    }
}
