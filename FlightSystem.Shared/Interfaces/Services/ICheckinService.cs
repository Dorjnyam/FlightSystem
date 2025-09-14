using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Core.Enums;

namespace FlightSystem.Shared.Interfaces.Services
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
        
        /// <summary>
        /// Пасспорт дугаараар зорчигч хайх
        /// </summary>
        /// <param name="passportNumber"></param>
        /// <returns></returns>
        Task<PassengerDto?> GetPassengerByPassportAsync(string passportNumber);
        
        /// <summary>
        /// Check-in хийгдсэн зорчигчдын жагсаалт авах
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task<IEnumerable<PassengerDto>> GetCheckedInPassengersAsync(int flightId);
        
        /// <summary>
        /// Check-in боломжийн мэдээлэл авах
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="passportNumber"></param>
        /// <returns></returns>
        Task<CheckinEligibilityDto> GetCheckinEligibilityAsync(int flightId, string passportNumber);
        
        /// <summary>
        /// Check-in цуцлах (flightId болон passengerId ашиглан)
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="passengerId"></param>
        /// <returns></returns>
        Task<bool> CancelCheckinByFlightAndPassengerAsync(int flightId, int passengerId);

        /// <summary>
        /// Зорчигч нислэгт бүртгэлтэй эсэхийг шалгах
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="passportNumber"></param>
        /// <returns></returns>
        Task<FlightPassengerValidationDto> ValidateFlightPassengerAsync(int flightId, string passportNumber);

        /// <summary>
        /// Нислэгийн бүртгэлтэй зорчигчдын жагсаалт авах
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task<IEnumerable<FlightPassengerDto>> GetFlightPassengersAsync(int flightId);

        /// <summary>
        /// Зорчигчийн нислэгийн бүртгэлүүд авах
        /// </summary>
        /// <param name="passportNumber"></param>
        /// <returns></returns>
        Task<IEnumerable<FlightPassengerDto>> GetPassengerFlightsAsync(string passportNumber);

        /// <summary>
        /// Booking reference-ээр нислэгийн бүртгэл авах
        /// </summary>
        /// <param name="bookingReference"></param>
        /// <returns></returns>
        Task<FlightPassengerDto?> GetFlightPassengerByBookingReferenceAsync(string bookingReference);

        /// <summary>
        /// Concurrent seat assignment test
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ConcurrentSeatTestResultDto> TestConcurrentSeatAssignmentAsync(ConcurrentSeatTestRequestDto request);
    }
}
