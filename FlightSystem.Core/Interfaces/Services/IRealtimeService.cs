using FlightSystem.Shared.DTOs.Response;

namespace FlightSystem.Core.Interfaces.Services
{
    public interface IRealtimeService
    {
        /// <summary>
        /// realtime нислэгийн мэдээллийг шинэчилэх
        /// </summary>
        /// <param name="flightInfo"></param>
        /// <returns></returns>
        Task SendFlightUpdateAsync(FlightInfoDto flightInfo);
        /// <summary>
        /// realtime суудлын мэдээллийг шинэчилэх
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="seatInfo"></param>
        /// <returns></returns>
        Task SendSeatUpdateAsync(int flightId, SeatInfoDto seatInfo);
        /// <summary>
        /// realtime check-in мэдээллийг шинэчилэх
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task SendCheckinUpdateAsync(int flightId, CheckinStatusDto status);
        /// <summary>
        /// realtime системийн мэдэгдэл илгээх
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendSystemMessageAsync(string message);
        /// <summary>
        /// connectionId-г flight group-д оруулах
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task JoinFlightGroupAsync(string connectionId, int flightId);
        /// <summary>
        /// connectionId-г flight group-с хасах
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task LeaveFlightGroupAsync(string connectionId, int flightId);
    }
}
