namespace FlightSystem.Shared.DTOs.Request;

public class CreateFlightPassengerDto
{
    public int FlightId { get; set; }
    public int PassengerId { get; set; }
    public string BookingReference { get; set; } = string.Empty;
    public string? SpecialRequests { get; set; }
    public string? BaggageInfo { get; set; }
    public int CreatedByEmployeeId { get; set; }
}
