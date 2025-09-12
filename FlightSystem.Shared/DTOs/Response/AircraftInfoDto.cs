namespace FlightSystem.Shared.DTOs.Response;

public class AircraftInfoDto
{
    public int Id { get; set; }
    public string AircraftCode { get; set; } = string.Empty;
    public string AircraftType { get; set; } = string.Empty;
    public int TotalSeats { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
