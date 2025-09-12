using FlightSystem.Core.Models;

namespace FlightSystem.Shared.Interfaces.Repositories
{
    public interface IBoardingPassRepository : IRepository<BoardingPass>
    {
        Task<BoardingPass?> GetByCodeAsync(string boardingPassCode);
        Task<BoardingPass?> GetByFlightPassengerAsync(int flightPassengerId);
        Task<IEnumerable<BoardingPass>> GetBoardingPassesForFlightAsync(int flightId);
        Task<BoardingPass?> UpdateBoardingStatusAsync(int id, bool isBoarded, DateTime? boardingTime);
        Task<IEnumerable<BoardingPass>> GetBoardingPassesByEmployeeAsync(int employeeId);
        Task<int> GetBoardedCountAsync(int flightId);
    }
}
