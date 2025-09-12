using FlightSystem.Core.Models;
using FlightSystem.Core.Enums;

namespace FlightSystem.Core.Interfaces.Repositories
{
    public interface ISeatRepository : IRepository<Seat>
    {
        Task<IEnumerable<Seat>> GetSeatsByAircraftAsync(int aircraftId);
        Task<IEnumerable<Seat>> GetSeatsByClassAsync(int aircraftId, SeatClass seatClass);
        Task<Seat?> GetSeatByNumberAsync(int aircraftId, string seatNumber);
        Task<IEnumerable<Seat>> GetAvailableSeatsForFlightAsync(int flightId);
        Task<IEnumerable<Seat>> GetWindowSeatsAsync(int aircraftId);
        Task<IEnumerable<Seat>> GetAisleSeatsAsync(int aircraftId);
    }
}
