using Microsoft.EntityFrameworkCore;
using FlightSystem.Shared.Interfaces.Repositories;
using FlightSystem.Core.Models;
using FlightSystem.Data.Context;

namespace FlightSystem.Data.Repositories;

public class SeatAssignmentRepository(FlightSystemDbContext context) : Repository<SeatAssignment>(context), ISeatAssignmentRepository
{
    public async Task<bool> IsSeatAssignedAsync(int seatId, int flightId)
    {
        return await _dbSet
            .AnyAsync(sa => sa.SeatId == seatId && 
                          sa.FlightPassenger!.FlightId == flightId && 
                          sa.IsActive);
    }

    public async Task<SeatAssignment?> AssignSeatAsync(SeatAssignment seatAssignment)
    {
        var entry = await _dbSet.AddAsync(seatAssignment);
        await SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<bool> ReleaseSeatAsync(int seatAssignmentId)
    {
        var seatAssignment = await GetByIdAsync(seatAssignmentId);
        if (seatAssignment == null) return false;

        seatAssignment.IsActive = false;
        seatAssignment.UpdatedAt = DateTime.UtcNow;

        await SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<SeatAssignment>> GetAssignmentsForFlightAsync(int flightId)
    {
        return await _dbSet
            .Include(sa => sa.Seat)
            .Include(sa => sa.FlightPassenger)
                .ThenInclude(fp => fp!.Passenger)
            .Include(sa => sa.AssignedByEmployee)
            .Where(sa => sa.FlightPassenger!.FlightId == flightId && sa.IsActive)
            .OrderBy(sa => sa.Seat!.Row)
            .ThenBy(sa => sa.Seat!.Column)
            .ToListAsync();
    }

    public async Task<SeatAssignment?> GetAssignmentBySeatAsync(int seatId, int flightId)
    {
        return await _dbSet
            .Include(sa => sa.Seat)
            .Include(sa => sa.FlightPassenger)
                .ThenInclude(fp => fp!.Passenger)
            .Include(sa => sa.AssignedByEmployee)
            .FirstOrDefaultAsync(sa => sa.SeatId == seatId && 
                                     sa.FlightPassenger!.FlightId == flightId && 
                                     sa.IsActive);
    }

    public async Task<IEnumerable<SeatAssignment>> GetAssignmentsForPassengerAsync(int flightPassengerId)
    {
        return await _dbSet
            .Include(sa => sa.Seat)
            .Include(sa => sa.AssignedByEmployee)
            .Where(sa => sa.FlightPassengerId == flightPassengerId && sa.IsActive)
            .ToListAsync();
    }

    public async Task<SeatAssignment?> GetBySeatAndFlightAsync(int seatId, int flightId)
    {
        return await _dbSet
            .Include(sa => sa.Seat)
            .Include(sa => sa.FlightPassenger)
            .Include(sa => sa.AssignedByEmployee)
            .FirstOrDefaultAsync(sa => sa.SeatId == seatId && 
                                     sa.FlightPassenger!.FlightId == flightId && 
                                     sa.IsActive);
    }

    public async Task<SeatAssignment?> GetByFlightPassengerAsync(int flightPassengerId)
    {
        return await _dbSet
            .Include(sa => sa.Seat)
            .Include(sa => sa.AssignedByEmployee)
            .FirstOrDefaultAsync(sa => sa.FlightPassengerId == flightPassengerId && sa.IsActive);
    }

    public async Task<IEnumerable<SeatAssignment>> GetAssignmentsByEmployeeAsync(int employeeId)
    {
        return await _dbSet
            .Include(sa => sa.Seat)
            .Include(sa => sa.FlightPassenger)
                .ThenInclude(fp => fp!.Passenger)
            .Where(sa => sa.AssignedByEmployeeId == employeeId && sa.IsActive)
            .OrderBy(sa => sa.AssignedAt)
            .ToListAsync();
    }
}
