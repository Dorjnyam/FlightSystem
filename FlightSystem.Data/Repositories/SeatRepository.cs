using Microsoft.EntityFrameworkCore;
using FlightSystem.Core.Interfaces.Repositories;
using FlightSystem.Core.Models;
using FlightSystem.Core.Enums;
using FlightSystem.Data.Context;

namespace FlightSystem.Data.Repositories;

public class SeatRepository(FlightSystemDbContext context) : Repository<Seat>(context), ISeatRepository
{
    public async Task<Seat?> GetSeatByNumberAsync(int aircraftId, string seatNumber)
    {
        return await _dbSet
            .Include(s => s.Aircraft)
            .FirstOrDefaultAsync(s => s.AircraftId == aircraftId && s.SeatNumber == seatNumber);
    }

    public async Task<IEnumerable<Seat>> GetSeatsByAircraftAsync(int aircraftId)
    {
        return await _dbSet
            .Include(s => s.Aircraft)
            .Where(s => s.AircraftId == aircraftId && s.IsActive)
            .OrderBy(s => s.Row)
            .ThenBy(s => s.Column)
            .ToListAsync();
    }

    public async Task<IEnumerable<Seat>> GetAvailableSeatsForFlightAsync(int flightId)
    {
         var flight = await _context.Flights
            .Include(f => f.Aircraft)
            .FirstOrDefaultAsync(f => f.Id == flightId);

        if (flight == null) return [];

        var seats = await _dbSet
            .Where(s => s.AircraftId == flight.AircraftId && s.IsActive)
            .OrderBy(s => s.Row)
            .ThenBy(s => s.Column)
            .ToListAsync();

                var assignedSeatIds = await _context.SeatAssignments
            .Where(sa => sa.FlightPassenger!.FlightId == flightId && sa.IsActive)
            .Select(sa => sa.SeatId)
            .ToListAsync();

                return seats.Where(s => !assignedSeatIds.Contains(s.Id));
    }

    public async Task<IEnumerable<Seat>> GetSeatsByClassAsync(int aircraftId, SeatClass seatClass)
    {
        return await _dbSet
            .Include(s => s.Aircraft)
            .Where(s => s.AircraftId == aircraftId && s.Class == seatClass && s.IsActive)
            .OrderBy(s => s.Row)
            .ThenBy(s => s.Column)
            .ToListAsync();
    }

    public async Task<IEnumerable<Seat>> GetWindowSeatsAsync(int aircraftId)
    {
        return await _dbSet
            .Include(s => s.Aircraft)
            .Where(s => s.AircraftId == aircraftId && s.IsWindowSeat && s.IsActive)
            .OrderBy(s => s.Row)
            .ThenBy(s => s.Column)
            .ToListAsync();
    }

    public async Task<IEnumerable<Seat>> GetAisleSeatsAsync(int aircraftId)
    {
        return await _dbSet
            .Include(s => s.Aircraft)
            .Where(s => s.AircraftId == aircraftId && s.IsAisleSeat && s.IsActive)
            .OrderBy(s => s.Row)
            .ThenBy(s => s.Column)
            .ToListAsync();
    }
}
