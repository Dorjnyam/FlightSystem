using FlightSystem.Core.Models;

namespace FlightSystem.Core.Interfaces.Repositories
{
    public interface IFlightPassengerRepository : IRepository<FlightPassenger>
    {
        Task<FlightPassenger?> GetFlightPassengerAsync(int flightId, int passengerId);
        Task<FlightPassenger?> GetByBookingReferenceAsync(string bookingReference);
        Task<IEnumerable<FlightPassenger>> GetPassengersForFlightAsync(int flightId);
        Task<IEnumerable<FlightPassenger>> GetFlightsForPassengerAsync(int passengerId);
        Task<IEnumerable<FlightPassenger>> GetCheckedInPassengersAsync(int flightId);
        Task<FlightPassenger?> CheckinPassengerAsync(int flightPassengerId, int employeeId);
        Task<bool> IsPassengerCheckedInAsync(int flightId, int passengerId);
    }
}
