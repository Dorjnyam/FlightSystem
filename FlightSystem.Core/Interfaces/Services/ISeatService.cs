using FlightSystem.Core.DTOs.Request;
using FlightSystem.Core.DTOs.Response;
using FlightSystem.Core.Enums;

namespace FlightSystem.Core.Interfaces.Services
{
    public interface ISeatService
    {
        /// <summary>
        /// Шинэ суудал үүсгэх
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        Task<SeatDto> CreateSeatAsync(CreateSeatDto createDto);
        /// <summary>
        /// Суудлын дэлгэрэнгүй мэдээлэл авах
        /// </summary>
        /// <param name="seatId"></param>
        /// <returns></returns>
        Task<SeatDto?> GetSeatByIdAsync(int seatId);
        /// <summary>
        /// Бүх суудлын мэдээлэл авах
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SeatDto>> GetAllSeatsAsync();
        /// <summary>
        /// Суудлын мэдээлэл шинэчлэх
        /// </summary>
        /// <param name="seatId"></param>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        Task<SeatDto> UpdateSeatAsync(int seatId, UpdateSeatDto updateDto);
        /// <summary>
        /// Суудал устгах
        /// </summary>
        /// <param name="seatId"></param>
        /// <returns></returns>
        Task<bool> DeleteSeatAsync(int seatId);

        /// <summary>
        /// Онгоцны нислэгийн суудлын мэдээлэл авах
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task<IEnumerable<SeatInfoDto>> GetSeatsByFlightAsync(int flightId);
        /// <summary>
        /// Онгоцны нислэгийн чөлөөт суудлын мэдээлэл авах
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="seatClass"></param>
        /// <returns></returns>
        Task<IEnumerable<SeatInfoDto>> GetAvailableSeatsAsync(int flightId, SeatClass? seatClass = null);
        /// <summary>
        /// Онгоцны суудлын дугаараар мэдээлэл авах
        /// </summary>
        /// <param name="aircraftId"></param>
        /// <param name="seatNumber"></param>
        /// <returns></returns>
        Task<SeatInfoDto?> GetSeatInfoAsync(int aircraftId, string seatNumber);
        /// <summary>
        /// Суудал захиалах
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<SeatAssignmentDto?> ReserveSeatAsync(ReserveSeatRequestDto request);
        /// <summary>
        /// Суудал чөлөөлөх
        /// </summary>
        /// <param name="seatAssignmentId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<bool> ReleaseSeatAsync(int seatAssignmentId, int employeeId);
        /// <summary>
        /// Онгоцны нислэгийн суудлын зураглал үүсгэх
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task<SeatMapDto> GenerateSeatMapAsync(int flightId);
    }
}
