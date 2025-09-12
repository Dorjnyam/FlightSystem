using FlightSystem.Shared.DTOs.Response;

namespace FlightSystem.Core.Interfaces.External
{
    public interface IEmailService
    {
        Task SendBoardingPassEmailAsync(string toEmail, BoardingPassDto boardingPass);
        Task SendFlightUpdateEmailAsync(string toEmail, FlightInfoDto flight);
        Task SendCheckinConfirmationAsync(string toEmail, CheckinResultDto checkinResult);
    }
}
