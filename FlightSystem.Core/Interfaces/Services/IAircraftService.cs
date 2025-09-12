using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;

namespace FlightSystem.Core.Interfaces.Services
{
    public interface IAircraftService
    {
        /// <summary>
        /// CRUD үйлдлүүд
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        Task<AircraftDto> CreateAircraftAsync(CreateAircraftDto createDto);
        /// <summary>
        /// ID-аар нисэх онгоцын мэдээлэл авах
        /// </summary>
        /// <param name="aircraftId"></param>
        /// <returns></returns>
        Task<AircraftDto?> GetAircraftByIdAsync(int aircraftId);
        /// <summary>
        /// Бүх нисэх онгоцны мэдээлэл авах
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AircraftDto>> GetAllAircraftAsync();
        /// <summary>
        /// Нисэх онгоцны мэдээлэл шинэчлэх
        /// </summary>
        /// <param name="aircraftId"></param>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        Task<AircraftDto> UpdateAircraftAsync(int aircraftId, UpdateAircraftDto updateDto);
        /// <summary>
        /// Нисэх онгоцны мэдээлэл устгах
        /// </summary>
        /// <param name="aircraftId"></param>
        /// <returns></returns>
        Task<bool> DeleteAircraftAsync(int aircraftId);

        /// <summary>
        /// Business үйлдлүүд
        /// </summary>
        /// <param name="aircraftCode"></param>
        /// <returns></returns>
        Task<AircraftDto?> GetByCodeAsync(string aircraftCode);
        /// <summary>
        /// Идэвхтэй нисэх онгоцны мэдээлэл авах
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AircraftDto>> GetActiveAircraftAsync();
        /// <summary>
        /// Нисэх онгоцны суудлын мэдээлэлтэй авах
        /// </summary>
        /// <param name="aircraftId"></param>
        /// <returns></returns>
        Task<AircraftDetailDto?> GetAircraftWithSeatsAsync(int aircraftId);
    }
}
