namespace FlightSystem.Shared.DTOs.Request;

public class UpdateFlightPassengerDto
{
    public string? SpecialRequests { get; set; }
    public string? BaggageInfo { get; set; }
    public int UpdatedByEmployeeId { get; set; }
}
