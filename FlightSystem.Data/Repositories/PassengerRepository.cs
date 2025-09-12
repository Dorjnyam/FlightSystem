using Microsoft.EntityFrameworkCore;
using FlightSystem.Core.Interfaces.Repositories;
using FlightSystem.Core.Models;
using FlightSystem.Core.Enums;
using FlightSystem.Data.Context;

namespace FlightSystem.Data.Repositories;

public class PassengerRepository(FlightSystemDbContext context) : Repository<Passenger>(context), IPassengerRepository
{
    public async Task<Passenger?> GetByPassportNumberAsync(string passportNumber)
    {
        return await _dbSet
            .FirstOrDefaultAsync(p => p.PassportNumber == passportNumber);
    }

    public async Task<IEnumerable<Passenger>> SearchPassengersAsync(string searchTerm)
    {
        return await _dbSet
            .Where(p => p.FirstName.Contains(searchTerm) ||
                       p.LastName.Contains(searchTerm) ||
                       p.PassportNumber.Contains(searchTerm) ||
                       p.Email.Contains(searchTerm))
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .ToListAsync();
    }

    public async Task<IEnumerable<Passenger>> GetPassengersByTypeAsync(PassengerType type)
    {
        return await _dbSet
            .Where(p => p.Type == type)
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .ToListAsync();
    }

    public async Task<IEnumerable<Passenger>> GetPassengersByNationalityAsync(string nationality)
    {
        return await _dbSet
            .Where(p => p.Nationality == nationality)
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .ToListAsync();
    }
}
