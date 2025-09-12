using Microsoft.EntityFrameworkCore;
using FlightSystem.Shared.Interfaces.Repositories;
using FlightSystem.Core.Models;
using FlightSystem.Core.Enums;
using FlightSystem.Data.Context;

namespace FlightSystem.Data.Repositories;

public class FlightRepository(FlightSystemDbContext context) : Repository<Flight>(context), IFlightRepository
{
    public async Task<Flight?> GetByFlightNumberAsync(string flightNumber)
    {
        return await _dbSet
            .Include(f => f.Aircraft)
            .Include(f => f.CreatedByEmployee)
            .FirstOrDefaultAsync(f => f.FlightNumber == flightNumber);
    }

    public async Task<IEnumerable<Flight>> GetFlightsByDateAsync(DateTime date)
    {
        var startDate = date.Date;
        var endDate = startDate.AddDays(1);

        return await _dbSet
            .Include(f => f.Aircraft)
            .Where(f => f.ScheduledDeparture >= startDate && f.ScheduledDeparture < endDate)
            .OrderBy(f => f.ScheduledDeparture)
            .ToListAsync();
    }

    public async Task<IEnumerable<Flight>> GetActiveFlightsAsync()
    {
        return await _dbSet
            .Include(f => f.Aircraft)
            .Where(f => f.Status != FlightStatus.Cancelled && f.Status != FlightStatus.Departed)
            .OrderBy(f => f.ScheduledDeparture)
            .ToListAsync();
    }

    public async Task<IEnumerable<Flight>> GetFlightsByAircraftAsync(int aircraftId)
    {
        return await _dbSet
            .Include(f => f.Aircraft)
            .Where(f => f.AircraftId == aircraftId)
            .OrderBy(f => f.ScheduledDeparture)
            .ToListAsync();
    }

    public async Task<IEnumerable<Flight>> GetFlightsByStatusAsync(FlightStatus status)
    {
        return await _dbSet
            .Include(f => f.Aircraft)
            .Where(f => f.Status == status)
            .OrderBy(f => f.ScheduledDeparture)
            .ToListAsync();
    }

    public async Task<Flight?> GetFlightWithDetailsAsync(int flightId)
    {
        return await _dbSet
            .Include(f => f.Aircraft)
            .Include(f => f.CreatedByEmployee)
            .Include(f => f.FlightPassengers)
                .ThenInclude(fp => fp.Passenger)
            .FirstOrDefaultAsync(f => f.Id == flightId);
    }

    public async Task<bool> UpdateFlightStatusAsync(int flightId, FlightStatus status)
    {
        var flight = await GetByIdAsync(flightId);
        if (flight == null) return false;

        flight.Status = status;
        flight.UpdatedAt = DateTime.UtcNow;
        await SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Flight>> GetFlightsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Include(f => f.Aircraft)
            .Where(f => f.ScheduledDeparture >= startDate && f.ScheduledDeparture <= endDate)
            .OrderBy(f => f.ScheduledDeparture)
            .ToListAsync();
    }
}
