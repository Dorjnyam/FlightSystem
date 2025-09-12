using FlightSystem.Shared.DTOs.Response;

namespace FlightSystem.Core.Interfaces.Services
{
    public interface IRealtimeService
    {
        /// <summary>
        /// realtime ��������� ���������� ���������
        /// </summary>
        /// <param name="flightInfo"></param>
        /// <returns></returns>
        Task SendFlightUpdateAsync(FlightInfoDto flightInfo);
        /// <summary>
        /// realtime ������� ���������� ���������
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="seatInfo"></param>
        /// <returns></returns>
        Task SendSeatUpdateAsync(int flightId, SeatInfoDto seatInfo);
        /// <summary>
        /// realtime check-in ���������� ���������
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task SendCheckinUpdateAsync(int flightId, CheckinStatusDto status);
        /// <summary>
        /// realtime ��������� �������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendSystemMessageAsync(string message);
        /// <summary>
        /// connectionId-� flight group-� �������
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task JoinFlightGroupAsync(string connectionId, int flightId);
        /// <summary>
        /// connectionId-� flight group-� �����
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task LeaveFlightGroupAsync(string connectionId, int flightId);
    }
}
