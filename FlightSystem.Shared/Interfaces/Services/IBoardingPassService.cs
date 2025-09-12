using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;

namespace FlightSystem.Shared.Interfaces.Services
{
    public interface IBoardingPassService
    {
        /// <summary>
        /// CRUD үйлдлүүд
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        Task<BoardingPassDto> CreateBoardingPassAsync(CreateBoardingPassDto createDto);
        /// <summary>
        /// ID-аар нэг нэвтрэх эрх үүсгэх
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BoardingPassDto?> GetBoardingPassByIdAsync(int id);
        /// <summary>
        /// Бүх нэвтрэх эрхүүдийг авах
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BoardingPassDto>> GetAllBoardingPassesAsync();
        /// <summary>
        /// ID-аар нэг нэвтрэх эрхийг шинэчлэх
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        Task<BoardingPassDto> UpdateBoardingPassAsync(int id, UpdateBoardingPassDto updateDto);
        /// <summary>
        /// ID-аар нэг нэвтрэх эрхийг устгах
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteBoardingPassAsync(int id);

        /// <summary>
        /// Business үйлдлүүд
        /// </summary>
        /// <param name="boardingPassCode"></param>
        /// <returns></returns>
        Task<BoardingPassDto?> GetByCodeAsync(string boardingPassCode);
        /// <summary>
        /// Нэвтрэх эрх үүсгэх
        /// </summary>
        /// <param name="flightPassengerId"></param>
        /// <param name="seatAssignmentId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<BoardingPassDto> GenerateBoardingPassAsync(int flightPassengerId, int seatAssignmentId, int employeeId);
        /// <summary>
        /// Нэвтрэх эрхийн кодыг шалгах
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> ValidateBoardingPassAsync(string code);
        /// <summary>
        /// Нэвтрэх үйл явцыг боловсруулах
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<BoardingPassDto> ProcessBoardingAsync(string code);
        /// <summary>
        /// Нислэгийн бүх нэвтрэх эрхүүдийг авах
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task<IEnumerable<BoardingPassDto>> GetBoardingPassesForFlightAsync(int flightId);
    }
}
