using FlightSystem.Shared.DTOs.Response;

namespace FlightSystem.Shared.Interfaces.External
{
    public interface IEmailService
    {
        Task SendBoardingPassEmailAsync(string toEmail, BoardingPassDto boardingPass);
        Task SendFlightUpdateEmailAsync(string toEmail, FlightInfoDto flight);
        Task SendCheckinConfirmationAsync(string toEmail, CheckinResultDto checkinResult);
    }
}
