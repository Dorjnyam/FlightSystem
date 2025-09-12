namespace FlightSystem.Server.Services;

public interface ISocketService
{
    Task StartAsync();
    Task StopAsync();
    Task SendMessageAsync(string message);
    Task<bool> IsRunningAsync();
}
