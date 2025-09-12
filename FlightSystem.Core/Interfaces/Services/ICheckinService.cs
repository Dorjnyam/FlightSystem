using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Shared.Enums;

namespace FlightSystem.Core.Interfaces.Services
{
    public interface ICheckinService
    {
        /// <summary>
        /// Main Check-in үйлдлүүд
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CheckinResultDto> CheckinPassengerAsync(CheckinRequestDto request);
        /// <summary>
        /// Суудал хуваарилах
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<SeatAssignmentDto> AssignSeatAsync(AssignSeatRequestDto request);
        /// <summary>
        /// Бордны карт үүсгэх
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BoardingPassDto> GenerateBoardingPassAsync(GenerateBoardingPassRequestDto request);

        /// <summary>
        /// Суудлын менежмент
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="seatClass"></param>
        /// <returns></returns>
        Task<IEnumerable<SeatInfoDto>> GetAvailableSeatsAsync(int flightId, SeatClass? seatClass = null);
        /// <summary>
        /// Суудлын боломжтой эсэхийг шалгах
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="seatId"></param>
        /// <returns></returns>
        Task<bool> IsSeatAvailableAsync(int flightId, int seatId);
        /// <summary>
        /// Суудлын мэдээлэл авах
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task<SeatMapDto> GetSeatMapAsync(int flightId);
        
        /// <summary>
        /// Validation
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task<bool> CanCheckinAsync(int flightId);
        /// <summary>
        /// Check-in хийх эрхтэй эсэхийг шалгах
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="passportNumber"></param>
        /// <returns></returns>
        Task<CheckinEligibilityDto> ValidateCheckinEligibilityAsync(int flightId, string passportNumber);

        /// <summary>
        /// Check-in төлөвийг авах
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="passportNumber"></param>
        /// <returns></returns>
        Task<CheckinStatusDto> GetCheckinStatusAsync(int flightId, string passportNumber);
        /// <summary>
        /// Check-in цуцлах
        /// </summary>
        /// <param name="flightPassengerId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<bool> CancelCheckinAsync(int flightPassengerId, int employeeId);
    }
}
