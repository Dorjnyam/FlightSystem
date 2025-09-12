using FlightSystem.Core.Models;

namespace FlightSystem.Shared.Interfaces.Repositories
{
    public interface ISeatAssignmentRepository : IRepository<SeatAssignment>
    {
        Task<SeatAssignment?> GetBySeatAndFlightAsync(int seatId, int flightId);
        Task<SeatAssignment?> GetByFlightPassengerAsync(int flightPassengerId);
        Task<IEnumerable<SeatAssignment>> GetAssignmentsForFlightAsync(int flightId);
        Task<bool> IsSeatAssignedAsync(int seatId, int flightId);
        Task<SeatAssignment?> AssignSeatAsync(SeatAssignment assignment);
        Task<bool> ReleaseSeatAsync(int seatAssignmentId);
        Task<IEnumerable<SeatAssignment>> GetAssignmentsByEmployeeAsync(int employeeId);
    }
}
