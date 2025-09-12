namespace FlightSystem.Shared.DTOs.Request
{
    public class UpdateAircraftDto
    {
        public string? AircraftType { get; set; }
        public int? TotalSeats { get; set; }
        public bool? IsActive { get; set; }
    }
}
