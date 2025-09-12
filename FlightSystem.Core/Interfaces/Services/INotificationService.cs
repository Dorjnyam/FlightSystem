using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Shared.Enums;

namespace FlightSystem.Core.Interfaces.Services
{
    public interface INotificationService
    {
        /// <summary>
        /// Нислэгийн төлөв өөрчлөгдсөн тухай мэдэгдэл илгээх
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task NotifyFlightStatusChangeAsync(int flightId, FlightStatus status);
        /// <summary>
        /// Суудал хуваарилагдсан тухай мэдэгдэл илгээх
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="seatNumber"></param>
        /// <param name="passengerName"></param>
        /// <returns></returns>
        Task NotifySeatAssignmentAsync(int flightId, string seatNumber, string passengerName);
        /// <summary>
        /// Чекийн үйл явц дууссан тухай мэдэгдэл илгээх
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="passengerName"></param>
        /// <returns></returns>
        Task NotifyCheckinCompleteAsync(int flightId, string passengerName);
        /// <summary>
        /// Нислэгийн хаалганы өөрчлөлтийн тухай мэдэгдэл илгээх
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="passengerName"></param>
        /// <returns></returns>
        Task NotifyBoardingPassIssuedAsync(int flightId, string passengerName);
        /// <summary>
        /// Системийн бүх хэрэглэгчдэд мэдэгдэл илгээх
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task BroadcastSystemMessageAsync(string message);
        /// <summary>
        /// Тодорхой хэрэглэгчид мэдэгдэл илгээх
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendToUserAsync(int employeeId, string message);
    }
}
