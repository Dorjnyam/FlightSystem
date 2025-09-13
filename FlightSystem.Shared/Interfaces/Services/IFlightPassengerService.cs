using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;

namespace FlightSystem.Shared.Interfaces.Services
{
    public interface IFlightPassengerService
    {
        /// <summary>
        /// Create a new flight passenger booking
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        Task<FlightPassengerDto> CreateFlightPassengerAsync(CreateFlightPassengerDto createDto);

        /// <summary>
        /// Create a new flight passenger booking by passport number
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        Task<FlightPassengerDto> CreateFlightPassengerByPassportAsync(CreateFlightPassengerByPassportDto createDto);

        /// <summary>
        /// Update flight passenger booking
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        Task<FlightPassengerDto> UpdateFlightPassengerAsync(int id, UpdateFlightPassengerDto updateDto);

        /// <summary>
        /// Delete flight passenger booking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteFlightPassengerAsync(int id);

        /// <summary>
        /// Get flight passenger by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<FlightPassengerDto?> GetFlightPassengerByIdAsync(int id);

        /// <summary>
        /// Get all flight passengers
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<FlightPassengerDto>> GetAllFlightPassengersAsync();

        /// <summary>
        /// Get flight passengers by flight ID
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task<IEnumerable<FlightPassengerDto>> GetFlightPassengersByFlightAsync(int flightId);

        /// <summary>
        /// Get flight passengers by passenger ID
        /// </summary>
        /// <param name="passengerId"></param>
        /// <returns></returns>
        Task<IEnumerable<FlightPassengerDto>> GetFlightPassengersByPassengerAsync(int passengerId);

        /// <summary>
        /// Get flight passenger by booking reference
        /// </summary>
        /// <param name="bookingReference"></param>
        /// <returns></returns>
        Task<FlightPassengerDto?> GetFlightPassengerByBookingReferenceAsync(string bookingReference);

        /// <summary>
        /// Cancel flight passenger booking (soft cancel - keeps record but cancels check-in)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<bool> CancelFlightPassengerAsync(int id, int employeeId);
    }
}
