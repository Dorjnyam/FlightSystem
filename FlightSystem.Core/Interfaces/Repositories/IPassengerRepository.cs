using FlightSystem.Core.Models;
using FlightSystem.Shared.Enums;

namespace FlightSystem.Core.Interfaces.Repositories
{
    public interface IPassengerRepository : IRepository<Passenger>
    {
        Task<Passenger?> GetByPassportNumberAsync(string passportNumber);
        Task<IEnumerable<Passenger>> SearchPassengersAsync(string searchTerm);
        Task<IEnumerable<Passenger>> GetPassengersByTypeAsync(PassengerType type);
        Task<IEnumerable<Passenger>> GetPassengersByNationalityAsync(string nationality);
    }
}
