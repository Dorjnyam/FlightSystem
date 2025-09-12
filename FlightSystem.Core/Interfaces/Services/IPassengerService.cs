using FlightSystem.Core.DTOs.Request;
using FlightSystem.Core.DTOs.Response;

namespace FlightSystem.Core.Interfaces.Services
{
    public interface IPassengerService
    {
        /// <summary>
        /// Шинэ зорчигч үүсгэх
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        Task<PassengerDto> CreatePassengerAsync(CreatePassengerDto createDto);
        /// <summary>
        /// ID-аар зорчигч хайх
        /// </summary>
        /// <param name="passengerId"></param>
        /// <returns></returns>
        Task<PassengerDto?> GetPassengerByIdAsync(int passengerId);
        /// <summary>
        /// Бүх зорчигчийн мэдээллийг авах
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PassengerDto>> GetAllPassengersAsync();
        /// <summary>
        /// Зорчигчийн мэдээллийг шинэчлэх
        /// </summary>
        /// <param name="passengerId"></param>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        Task<PassengerDto> UpdatePassengerAsync(int passengerId, UpdatePassengerDto updateDto);
        /// <summary>
        /// Зорчигчийн мэдээллийг устгах
        /// </summary>
        /// <param name="passengerId"></param>
        /// <returns></returns>
        Task<bool> DeletePassengerAsync(int passengerId);

        /// <summary>
        /// Пасспорт дугаараар зорчигч хайх
        /// </summary>
        /// <param name="passportNumber"></param>
        /// <returns></returns>
        Task<PassengerDto?> GetByPassportNumberAsync(string passportNumber);
        /// <summary>
        /// Онгоцны нислэг дээрх зорчигчийн мэдээллийг авах
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="passportNumber"></param>
        /// <returns></returns>
        Task<FlightPassengerDto?> GetFlightPassengerAsync(int flightId, string passportNumber);
        /// <summary>
        /// Онгоцны нислэг дээрх бүх зорчигчийн мэдээллийг авах
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task<IEnumerable<FlightPassengerDto>> GetPassengersForFlightAsync(int flightId);
        /// <summary>
        /// Онгоцны нислэг дээр тухайн зорчигч захиалсан эсэхийг шалгах
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="passportNumber"></param>
        /// <returns></returns>
        Task<bool> IsPassengerBookedForFlightAsync(int flightId, string passportNumber);
        /// <summary>
        /// Зорчигчийн мэдээллийг хайх
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        Task<IEnumerable<PassengerDto>> SearchPassengersAsync(string searchTerm);
    }
}
