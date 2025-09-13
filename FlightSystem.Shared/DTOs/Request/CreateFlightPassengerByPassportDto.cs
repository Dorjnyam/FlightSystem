namespace FlightSystem.Shared.DTOs.Request;

public class CreateFlightPassengerByPassportDto
{
    public int FlightId { get; set; }
    public string PassportNumber { get; set; } = string.Empty;
    public string BookingReference { get; set; } = string.Empty;
    public string? SpecialRequests { get; set; }
    public string? BaggageInfo { get; set; }
    public int CreatedByEmployeeId { get; set; }
}
