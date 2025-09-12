using FlightSystem.Core.Models;
using FlightSystem.Shared.Enums;

namespace FlightSystem.Core.Interfaces.Repositories
{
    public interface IFlightRepository : IRepository<Flight>
    {
        Task<Flight?> GetByFlightNumberAsync(string flightNumber);
        Task<IEnumerable<Flight>> GetFlightsByDateAsync(DateTime date);
        Task<IEnumerable<Flight>> GetActiveFlightsAsync();
        Task<IEnumerable<Flight>> GetFlightsByAircraftAsync(int aircraftId);
        Task<IEnumerable<Flight>> GetFlightsByStatusAsync(FlightStatus status);
        Task<Flight?> GetFlightWithDetailsAsync(int flightId);
        Task<bool> UpdateFlightStatusAsync(int flightId, FlightStatus status);
        Task<IEnumerable<Flight>> GetFlightsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
