using FlightSystem.Shared.Interfaces.Services;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Shared.Constants;
using FlightSystem.Core.Enums;

namespace FlightSystem.Server.Services;

public class NotificationService : INotificationService
{
    private readonly IRealtimeService _realtimeService;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(IRealtimeService realtimeService, ILogger<NotificationService> logger)
    {
        _realtimeService = realtimeService;
        _logger = logger;
    }

    public async Task SendFlightStatusNotificationAsync(int flightId, string newStatus, string message)
    {
        try
        {
            var notification = new NotificationDto
            {
                Type = NotificationType.FlightStatusUpdate,
                Title = "Нислэгийн төлөв өөрчлөгдлөө",
                Message = message,
                FlightId = flightId,
                Timestamp = DateTime.UtcNow
            };

            await _realtimeService.SendSystemMessageAsync($"FLIGHT_STATUS:{flightId}:{newStatus}:{message}");
            _logger.LogInformation("Flight status notification sent for flight {FlightId}: {Status}", flightId, newStatus);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending flight status notification");
        }
    }

    public async Task SendSeatAssignmentNotificationAsync(int flightId, string seatNumber, string passengerName)
    {
        try
        {
            var message = $"Суудал {seatNumber} зорчигч {passengerName}-д хуваарилагдлаа";
            await _realtimeService.SendSystemMessageAsync($"SEAT_ASSIGNED:{flightId}:{seatNumber}:{passengerName}");
            _logger.LogInformation("Seat assignment notification sent for flight {FlightId}, seat {SeatNumber}", flightId, seatNumber);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending seat assignment notification");
        }
    }

    public async Task SendCheckinNotificationAsync(int flightId, string passengerName, bool isCheckedIn)
    {
        try
        {
            var status = isCheckedIn ? "бүртгэгдлээ" : "бүртгэл цуцлагдлаа";
            var message = $"Зорчигч {passengerName} {status}";
            await _realtimeService.SendSystemMessageAsync($"CHECKIN:{flightId}:{passengerName}:{isCheckedIn}");
            _logger.LogInformation("Checkin notification sent for flight {FlightId}, passenger {PassengerName}: {Status}", 
                flightId, passengerName, isCheckedIn);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending checkin notification");
        }
    }

    public async Task SendBoardingNotificationAsync(int flightId, string gateNumber)
    {
        try
        {
            var message = $"Нислэг {flightId} {gateNumber} хаалгаар суух ажиллагаа эхэлж байна";
            await _realtimeService.SendSystemMessageAsync($"BOARDING:{flightId}:{gateNumber}");
            _logger.LogInformation("Boarding notification sent for flight {FlightId}, gate {GateNumber}", flightId, gateNumber);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending boarding notification");
        }
    }

    public async Task SendDelayNotificationAsync(int flightId, TimeSpan delay)
    {
        try
        {
            var message = $"Нислэг {flightId} {delay.TotalMinutes} минут хойшилж байна";
            await _realtimeService.SendSystemMessageAsync($"DELAY:{flightId}:{delay.TotalMinutes}");
            _logger.LogInformation("Delay notification sent for flight {FlightId}, delay {Delay} minutes", 
                flightId, delay.TotalMinutes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending delay notification");
        }
    }

    public async Task SendCancellationNotificationAsync(int flightId, string reason)
    {
        try
        {
            var message = $"Нислэг {flightId} цуцаллагдлаа. Шалтгаан: {reason}";
            await _realtimeService.SendSystemMessageAsync($"CANCELLATION:{flightId}:{reason}");
            _logger.LogInformation("Cancellation notification sent for flight {FlightId}, reason: {Reason}", flightId, reason);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending cancellation notification");
        }
    }

    public async Task NotifyFlightStatusChangeAsync(int flightId, FlightStatus status)
    {
        try
        {
            var message = $"Нислэг {flightId} төлөв өөрчлөгдлөө: {status}";
            await _realtimeService.SendSystemMessageAsync($"FLIGHT_STATUS_CHANGE:{flightId}:{status}");
            _logger.LogInformation("Flight status change notification sent for flight {FlightId}: {Status}", flightId, status);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending flight status change notification");
        }
    }

    public async Task NotifySeatAssignmentAsync(int flightId, string seatNumber, string passengerName)
    {
        try
        {
            var message = $"Суудал {seatNumber} зорчигч {passengerName}-д хуваарилагдлаа";
            await _realtimeService.SendSystemMessageAsync($"SEAT_ASSIGNMENT:{flightId}:{seatNumber}:{passengerName}");
            _logger.LogInformation("Seat assignment notification sent for flight {FlightId}, seat {SeatNumber}, passenger {PassengerName}", 
                flightId, seatNumber, passengerName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending seat assignment notification");
        }
    }

    public async Task NotifyCheckinCompleteAsync(int flightId, string passengerName)
    {
        try
        {
            var message = $"Зорчигч {passengerName} check-in хийж дууслаа";
            await _realtimeService.SendSystemMessageAsync($"CHECKIN_COMPLETE:{flightId}:{passengerName}");
            _logger.LogInformation("Checkin complete notification sent for flight {FlightId}, passenger {PassengerName}", 
                flightId, passengerName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending checkin complete notification");
        }
    }

    public async Task NotifyBoardingPassIssuedAsync(int flightId, string passengerName)
    {
        try
        {
            var message = $"Зорчигч {passengerName}-д boarding pass олгогдлоо";
            await _realtimeService.SendSystemMessageAsync($"BOARDING_PASS_ISSUED:{flightId}:{passengerName}");
            _logger.LogInformation("Boarding pass issued notification sent for flight {FlightId}, passenger {PassengerName}", 
                flightId, passengerName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending boarding pass issued notification");
        }
    }

    public async Task BroadcastSystemMessageAsync(string message)
    {
        try
        {
            await _realtimeService.SendSystemMessageAsync(message);
            _logger.LogInformation("System broadcast message sent: {Message}", message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending system broadcast message");
        }
    }

    public async Task SendToUserAsync(int employeeId, string message)
    {
        try
        {
            await _realtimeService.SendSystemMessageAsync($"USER_MESSAGE:{employeeId}:{message}");
            _logger.LogInformation("User message sent to employee {EmployeeId}: {Message}", employeeId, message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending user message");
        }
    }
}
