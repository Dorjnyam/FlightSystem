using FlightSystem.Shared.Interfaces.Services;
using FlightSystem.Shared.DTOs.Response;
using Microsoft.Extensions.Logging;

namespace FlightSystem.Shared.Services
{
    public class RealtimeService : IRealtimeService
    {
        private readonly ILogger<RealtimeService> _logger;

        public RealtimeService(ILogger<RealtimeService> logger)
        {
            _logger = logger;
        }

        public async Task SendFlightUpdateAsync(FlightInfoDto flightInfo)
        {
            try
            {
                _logger.LogInformation("Нислэгийн мэдээлэл шинэчлэгдлээ. ID: {FlightId}", flightInfo.Id);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Нислэгийн мэдээлэл шинэчлэхэд алдаа гарлаа. ID: {FlightId}", flightInfo.Id);
            }
        }

        public async Task SendSeatUpdateAsync(int flightId, SeatInfoDto seatInfo)
        {
            try
            {
                _logger.LogInformation("Суудлын мэдээлэл шинэчлэгдлээ. Нислэг: {FlightId}, Суудал: {SeatNumber}",
                    flightId, seatInfo.SeatNumber);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Суудлын мэдээлэл шинэчлэхэд алдаа гарлаа. Нислэг: {FlightId}", flightId);
            }
        }

        public async Task SendCheckinUpdateAsync(int flightId, CheckinStatusDto status)
        {
            try
            {
                _logger.LogInformation("Чек-ин бүртгэл шинэчлэгдлээ. Нислэг: {FlightId}", flightId);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Чек-ин шинэчлэхэд алдаа гарлаа. Нислэг: {FlightId}", flightId);
            }
        }

        public async Task SendSystemMessageAsync(string message)
        {
            try
            {
                _logger.LogInformation("Системийн мэдэгдэл илгээгдлээ: {Message}", message);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Системийн мэдэгдэл илгээхэд алдаа гарлаа: {Message}", message);
            }
        }

        public async Task JoinFlightGroupAsync(string connectionId, int flightId)
        {
            try
            {
                _logger.LogInformation("Холболт {ConnectionId} нислэгийн бүлэгт нэгдлээ. Нислэг: {FlightId}",
                    connectionId, flightId);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Холболт {ConnectionId} нислэгийн бүлэгт нэгдэхэд алдаа гарлаа. Нислэг: {FlightId}",
                    connectionId, flightId);
            }
        }

        public async Task LeaveFlightGroupAsync(string connectionId, int flightId)
        {
            try
            {
                _logger.LogInformation("Холболт {ConnectionId} нислэгийн бүлгээс гарлаа. Нислэг: {FlightId}",
                    connectionId, flightId);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Холболт {ConnectionId} нислэгийн бүлгээс гарахад алдаа гарлаа. Нислэг: {FlightId}",
                    connectionId, flightId);
            }
        }
    }
}
