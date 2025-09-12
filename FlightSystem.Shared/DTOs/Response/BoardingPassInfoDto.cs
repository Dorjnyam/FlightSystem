namespace FlightSystem.Shared.DTOs.Response;

public class BoardingPassInfoDto
{
    public int Id { get; set; }
    public string BoardingPassNumber { get; set; } = string.Empty;
    public string QRCode { get; set; } = string.Empty;
    public bool IsUsed { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UsedAt { get; set; }
    public PassengerInfoDto Passenger { get; set; } = new();
    public FlightInfoDto Flight { get; set; } = new();
    public SeatInfoDto? AssignedSeat { get; set; }
    public string? GateNumber { get; set; }
    public string? BoardingGroup { get; set; }
}
