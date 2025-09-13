namespace FlightSystem.Shared.DTOs.Response;

public class FlightPassengerValidationDto
{
    public bool IsValid { get; set; }
    public bool IsBooked { get; set; }
    public bool IsCheckedIn { get; set; }
    public string Message { get; set; } = string.Empty;
    public FlightPassengerDto? FlightPassenger { get; set; }
    public PassengerDto? Passenger { get; set; }
    public FlightInfoDto? Flight { get; set; }
    public string BookingReference { get; set; } = string.Empty;
    public DateTime? CheckinTime { get; set; }
    public bool HasSeatAssignment { get; set; }
    public bool HasBoardingPass { get; set; }
}
