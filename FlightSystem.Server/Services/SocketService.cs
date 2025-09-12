using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using FlightSystem.Shared.DTOs.Response;

namespace FlightSystem.Server.Services;

public class SocketService : ISocketService
{
    private readonly ILogger<SocketService> _logger;
    private TcpListener? _tcpListener;
    private readonly List<TcpClient> _clients = [];
    private bool _isRunning;
    private readonly int _port = 8888;

    public SocketService(ILogger<SocketService> logger)
    {
        _logger = logger;
    }

    public async Task StartAsync()
    {
        try
        {
            _tcpListener = new TcpListener(IPAddress.Any, _port);
            _tcpListener.Start();
            _isRunning = true;

            _logger.LogInformation("Socket server started on port {Port}", _port);

            while (_isRunning)
            {
                var tcpClient = await _tcpListener.AcceptTcpClientAsync();
                _clients.Add(tcpClient);
                
                _logger.LogInformation("Client connected: {Client}", tcpClient.Client.RemoteEndPoint);

                // Клиентыг тусад нь боловсруулах
                _ = Task.Run(() => HandleClientAsync(tcpClient));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Socket server error");
        }
    }

    public Task StopAsync()
    {
        _isRunning = false;
        _tcpListener?.Stop();
        
        // Бүх клиентуудыг хаах
        foreach (var client in _clients.ToList())
        {
            try
            {
                client.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error closing client connection");
            }
        }
        
        _clients.Clear();
        _logger.LogInformation("Socket server stopped");
        return Task.CompletedTask;
    }

    public async Task SendMessageAsync(string message)
    {
        var data = Encoding.UTF8.GetBytes(message + "\n");
        
        var clientsToRemove = new List<TcpClient>();
        
        foreach (var client in _clients.ToList())
        {
            try
            {
                if (client.Connected)
                {
                    await client.GetStream().WriteAsync(data);
                }
                else
                {
                    clientsToRemove.Add(client);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message to client");
                clientsToRemove.Add(client);
            }
        }

        // Disconnected клиентуудыг устгах
        foreach (var client in clientsToRemove)
        {
            _clients.Remove(client);
            client.Close();
        }
    }

    public Task<bool> IsRunningAsync()
    {
        return Task.FromResult(_isRunning);
    }

    private async Task HandleClientAsync(TcpClient tcpClient)
    {
        var buffer = new byte[4096];
        var stream = tcpClient.GetStream();

        try
        {
            while (tcpClient.Connected)
            {
                var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                
                if (bytesRead == 0)
                {
                    break;
                }

                var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                _logger.LogInformation("Received from client: {Message}", message);

                // Echo back to client
                var response = $"Server received: {message}";
                var responseData = Encoding.UTF8.GetBytes(response);
                await stream.WriteAsync(responseData, 0, responseData.Length);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling client");
        }
        finally
        {
            _clients.Remove(tcpClient);
            tcpClient.Close();
            _logger.LogInformation("Client disconnected: {Client}", tcpClient.Client.RemoteEndPoint);
        }
    }
}
