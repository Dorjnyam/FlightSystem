using FlightSystem.Core.Models;

namespace FlightSystem.Shared.Interfaces.Repositories
{
    public interface IAircraftRepository : IRepository<Aircraft>
    {
        Task<Aircraft?> GetByCodeAsync(string aircraftCode);
        Task<IEnumerable<Aircraft>> GetActiveAircraftAsync();
        Task<Aircraft?> GetAircraftWithSeatsAsync(int aircraftId);
        Task<IEnumerable<Aircraft>> GetAircraftByTypeAsync(string aircraftType);
    }
}
