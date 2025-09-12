using Microsoft.EntityFrameworkCore;
using FlightSystem.Core.Interfaces.Repositories;
using FlightSystem.Core.Models;
using FlightSystem.Data.Context;

namespace FlightSystem.Data.Repositories;

public class AircraftRepository(FlightSystemDbContext context) : Repository<Aircraft>(context), IAircraftRepository
{
    public async Task<Aircraft?> GetByAircraftCodeAsync(string aircraftCode)
    {
        return await _dbSet
            .Include(a => a.Seats)
            .FirstOrDefaultAsync(a => a.AircraftCode == aircraftCode);
    }

    public async Task<IEnumerable<Aircraft>> GetActiveAircraftAsync()
    {
        return await _dbSet
            .Include(a => a.Seats)
            .Where(a => a.IsActive)
            .OrderBy(a => a.AircraftCode)
            .ToListAsync();
    }

    public async Task<IEnumerable<Aircraft>> GetAircraftByTypeAsync(string aircraftType)
    {
        return await _dbSet
            .Include(a => a.Seats)
            .Where(a => a.AircraftType == aircraftType && a.IsActive)
            .OrderBy(a => a.AircraftCode)
            .ToListAsync();
    }

    public async Task<Aircraft?> GetAircraftWithSeatsAsync(int aircraftId)
    {
        return await _dbSet
            .Include(a => a.Seats)
            .FirstOrDefaultAsync(a => a.Id == aircraftId);
    }

    public async Task<Aircraft?> GetByCodeAsync(string aircraftCode)
    {
        return await _dbSet
            .Include(a => a.Seats)
            .FirstOrDefaultAsync(a => a.AircraftCode == aircraftCode);
    }
}
