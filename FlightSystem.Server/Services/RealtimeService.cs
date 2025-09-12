using Microsoft.AspNetCore.SignalR;
using FlightSystem.Server.Hubs;
using FlightSystem.Shared.Constants;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Shared.Interfaces.Services;

namespace FlightSystem.Server.Services;

public class RealtimeService : IRealtimeService
{
    private readonly IHubContext<FlightHub> _hubContext;
    private readonly ISocketService _socketService;
    private readonly ILogger<RealtimeService> _logger;

    public RealtimeService(
        IHubContext<FlightHub> hubContext,
        ISocketService socketService,
        ILogger<RealtimeService> logger)
    {
        _hubContext = hubContext;
        _socketService = socketService;
        _logger = logger;
    }

    public async Task SendFlightUpdateAsync(FlightInfoDto flightInfo)
    {
        try
        {
            await _hubContext.Clients.Group($"Flight_{flightInfo.Id}")
                .SendAsync(SignalREvents.FlightUpdated, flightInfo);

            await _socketService.SendMessageAsync($"FLIGHT_UPDATE:{flightInfo.Id}:{flightInfo.Status}");
            
            _logger.LogInformation("Flight update sent for flight {FlightId}", flightInfo.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending flight update");
        }
    }

    public async Task SendSeatUpdateAsync(int flightId, SeatInfoDto seatInfo)
    {
        try
        {
            await _hubContext.Clients.Group($"Flight_{flightId}")
                .SendAsync(SignalREvents.SeatAssigned, seatInfo);

            await _socketService.SendMessageAsync($"SEAT_UPDATE:{flightId}:{seatInfo.SeatNumber}:{seatInfo.IsAvailable}");
            
            _logger.LogInformation("Seat update sent for flight {FlightId}, seat {SeatNumber}", 
                flightId, seatInfo.SeatNumber);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending seat update");
        }
    }

    public async Task SendCheckinUpdateAsync(int flightId, CheckinStatusDto status)
    {
        try
        {
            await _hubContext.Clients.Group($"Flight_{flightId}")
                .SendAsync(SignalREvents.PassengerCheckedIn, status);

            await _socketService.SendMessageAsync($"CHECKIN_UPDATE:{flightId}:{status.IsCheckedIn}");
            
            _logger.LogInformation("Checkin update sent for flight {FlightId}", flightId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending checkin update");
        }
    }

    public async Task SendSystemMessageAsync(string message)
    {
        try
        {
            await _hubContext.Clients.All.SendAsync(SignalREvents.SystemMessage, message);
            await _socketService.SendMessageAsync($"SYSTEM:{message}");
            
            _logger.LogInformation("System message sent: {Message}", message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending system message");
        }
    }

    public async Task JoinFlightGroupAsync(string connectionId, int flightId)
    {
        await _hubContext.Groups.AddToGroupAsync(connectionId, $"Flight_{flightId}");
    }

    public async Task LeaveFlightGroupAsync(string connectionId, int flightId)
    {
        await _hubContext.Groups.RemoveFromGroupAsync(connectionId, $"Flight_{flightId}");
    }
}
