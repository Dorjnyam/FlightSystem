using Microsoft.AspNetCore.SignalR;
using FlightSystem.Shared.Constants;
using FlightSystem.Shared.DTOs.Response;
using Microsoft.Extensions.Logging;

namespace FlightSystem.Server.Hubs;

public class FlightHub : Hub
{
    private readonly ILogger<FlightHub> _logger;

    public FlightHub(ILogger<FlightHub> logger)
    {
        _logger = logger;
    }

    public async Task JoinFlightGroup(int flightId)
    {
        var groupName = GetFlightGroupName(flightId);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        _logger.LogInformation("Connection {ConnectionId} joined flight group {FlightId}", Context.ConnectionId, flightId);
    }

    public async Task LeaveFlightGroup(int flightId)
    {
        var groupName = GetFlightGroupName(flightId);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        _logger.LogInformation("Connection {ConnectionId} left flight group {FlightId}", Context.ConnectionId, flightId);
    }

    public async Task JoinEmployeeGroup(int employeeId)
    {
        var groupName = GetEmployeeGroupName(employeeId);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        _logger.LogInformation("Connection {ConnectionId} joined employee group {EmployeeId}", Context.ConnectionId, employeeId);
    }

    public async Task RequestFlightUpdate(int flightId)
    {
        await Clients.Group(GetFlightGroupName(flightId)).SendAsync(SignalREvents.RequestFlightUpdate, flightId);
    }

    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("Client disconnected: {ConnectionId}, Exception: {Exception}", 
            Context.ConnectionId, exception?.Message);
        await base.OnDisconnectedAsync(exception);
    }

    private static string GetFlightGroupName(int flightId) => $"Flight_{flightId}";
    private static string GetEmployeeGroupName(int employeeId) => $"Employee_{employeeId}";
}

