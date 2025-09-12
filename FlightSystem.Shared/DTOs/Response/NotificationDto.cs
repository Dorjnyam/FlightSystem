namespace FlightSystem.Shared.DTOs.Response;

public class NotificationDto
{
    public NotificationType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public int? FlightId { get; set; }
    public DateTime Timestamp { get; set; }
}

public enum NotificationType
{
    FlightStatusUpdate,
    SeatAssignment,
    CheckinUpdate,
    Boarding,
    Delay,
    Cancellation,
    System
}
