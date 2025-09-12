using FlightSystem.Core.DTOs.Request;
using FlightSystem.Core.DTOs.Response;
using FlightSystem.Core.Enums;

namespace FlightSystem.Core.Interfaces.Services
{
    public interface IFlightService
    {
        /// <summary>
        /// Нислэгийн мэдээллийг ID-аар авах
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task<FlightInfoDto?> GetFlightByIdAsync(int flightId);
        /// <summary>
        /// Бүх нислэгийн мэдээллийг авах
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<FlightInfoDto>> GetAllFlightsAsync();
        /// <summary>
        /// Шинэ нислэг үүсгэх
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        Task<FlightInfoDto> CreateFlightAsync(CreateFlightDto createDto);
        /// <summary>
        /// Нислэгийн мэдээллийг шинэчлэх
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        Task<FlightInfoDto> UpdateFlightAsync(int flightId, UpdateFlightDto updateDto);
        /// <summary>
        /// Нислэгийг устгах
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task<bool> DeleteFlightAsync(int flightId);

        /// <summary>
        /// Нислэгийн мэдээллийг нислэгийн дугаараар авах
        /// </summary>
        /// <param name="flightNumber"></param>
        /// <returns></returns>
        Task<FlightInfoDto?> GetFlightByNumberAsync(string flightNumber);
        /// <summary>
        /// Идэвхтэй нислэгүүдийг авах
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<FlightInfoDto>> GetActiveFlightsAsync();
        /// <summary>
        /// Нислэгийн төлөвийг шинэчлэх
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<FlightInfoDto> UpdateFlightStatusAsync(int flightId, FlightStatus status);
        /// <summary>
        /// Одоогийн нислэгүүдийг авах
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<IEnumerable<FlightInfoDto>> GetFlightsByDateAsync(DateTime date);
        /// <summary>
        /// Нислэгийн төлөвийг шинэчлэх боломжтой эсэхийг шалгах
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        Task<bool> CanUpdateFlightStatusAsync(int flightId, FlightStatus newStatus);
        /// <summary>
        /// Нислэгийн дэлгэрэнгүй мэдээллийг авах
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task<FlightDetailDto?> GetFlightDetailsAsync(int flightId);
        /// <summary>
        /// Нислэгийн бүртгэл нээлттэй эсэхийг шалгах
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task<bool> IsCheckinOpenAsync(int flightId);
        /// <summary>
        /// Нислэгийн суух үйл ажиллагаа нээлттэй эсэхийг шалгах
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task<bool> IsBoardingOpenAsync(int flightId);
    }
}
