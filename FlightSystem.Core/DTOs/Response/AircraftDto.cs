namespace FlightSystem.Core.DTOs.Response
{
    public class AircraftDto
    {
        public int Id { get; set; }
        public string AircraftCode { get; set; } = string.Empty;
        public string AircraftType { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
