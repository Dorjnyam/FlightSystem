using FlightSystem.Core.Interfaces.Services;
using FlightSystem.Core.DTOs.Response;
using FlightSystem.Core.Enums;
using Microsoft.Extensions.Logging;
using FlightSystem.Core.Extensions;

namespace FlightSystem.Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRealtimeService _realtimeService;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            IRealtimeService realtimeService,
            ILogger<NotificationService> logger)
        {
            _realtimeService = realtimeService;
            _logger = logger;
        }

        public async Task NotifyFlightStatusChangeAsync(int flightId, FlightStatus status)
        {
            try
            {
                var flightInfo = new FlightInfoDto
                {
                    Id = flightId,
                    Status = status.GetDisplayName()
                };

                await _realtimeService.SendFlightUpdateAsync(flightInfo);
                _logger.LogInformation("Нислэгийн {FlightId} статус {Status}-д өөрчлөгдсөн мэдэгдэл амжилттай илгээгдлээ", flightId, status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Нислэгийн {FlightId} статусын мэдэгдлийг илгээж чадсангүй", flightId);
            }
        }

        public async Task NotifySeatAssignmentAsync(int flightId, string seatNumber, string passengerName)
        {
            try
            {
                var seatInfo = new SeatInfoDto
                {
                    SeatNumber = seatNumber,
                    PassengerName = passengerName,
                    IsAvailable = false
                };

                await _realtimeService.SendSeatUpdateAsync(flightId, seatInfo);
                _logger.LogInformation("Нислэг {FlightId}, суудал {SeatNumber}, зорчигч {PassengerName}-д суудлын мэдэгдэл амжилттай илгээгдлээ",
                    flightId, seatNumber, passengerName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Нислэгийн {FlightId}-д суудлын мэдэгдлийг илгээж чадсангүй", flightId);
            }
        }

        public async Task NotifyCheckinCompleteAsync(int flightId, string passengerName)
        {
            try
            {
                var checkinStatus = new CheckinStatusDto
                {
                    IsCheckedIn = true,
                    CheckinTime = DateTime.UtcNow
                };

                await _realtimeService.SendCheckinUpdateAsync(flightId, checkinStatus);
                _logger.LogInformation("Нислэг {FlightId}-д зорчигч {PassengerName} бүртгэлээ амжилттай хийлээ", flightId, passengerName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Нислэгийн {FlightId}-д зорчигч бүртгэл амжилтгүй боллоо", flightId);
            }
        }

        public async Task NotifyBoardingPassIssuedAsync(int flightId, string passengerName)
        {
            try
            {
                var message = $"Зорчигч {passengerName}-д нислэг {flightId} boarding pass олгогдлоо";
                await _realtimeService.SendSystemMessageAsync(message);
                _logger.LogInformation("Нислэг {FlightId}-д зорчигч {PassengerName}-ийн boarding pass олгогдсон мэдэгдэл илгээгдлээ",
                    flightId, passengerName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Нислэгийн {FlightId}-д boarding pass мэдэгдэл илгээгдсэнгүй", flightId);
            }
        }

        public async Task BroadcastSystemMessageAsync(string message)
        {
            try
            {
                await _realtimeService.SendSystemMessageAsync(message);
                _logger.LogInformation("Системийн мэдэгдэл амжилттай цацагдлаа: {Message}", message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Системийн мэдэгдэл цацахад алдаа гарлаа: {Message}", message);
            }
        }

        public async Task SendToUserAsync(int employeeId, string message)
        {
            try
            {
                _logger.LogInformation("Ажилтан {EmployeeId}-д мэдэгдэл илгээгдлээ: {Message}", employeeId, message);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ажилтан {EmployeeId}-д мэдэгдэл илгээхэд алдаа гарлаа", employeeId);
            }
        }
    }
}
