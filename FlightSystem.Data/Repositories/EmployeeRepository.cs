using Microsoft.EntityFrameworkCore;
using FlightSystem.Core.Interfaces.Repositories;
using FlightSystem.Core.Models;
using FlightSystem.Core.Enums;
using FlightSystem.Data.Context;

namespace FlightSystem.Data.Repositories;

public class EmployeeRepository(FlightSystemDbContext context) : Repository<Employee>(context), IEmployeeRepository
{
    public async Task<Employee?> GetByEmployeeCodeAsync(string employeeCode)
    {
        return await _dbSet
            .FirstOrDefaultAsync(e => e.EmployeeCode == employeeCode);
    }

    public async Task<Employee?> AuthenticateAsync(string employeeCode, string password)
    {
        return await _dbSet
            .FirstOrDefaultAsync(e => e.EmployeeCode == employeeCode && 
                                    e.Password == password && 
                                    e.IsActive);
    }

    public async Task<IEnumerable<Employee>> GetEmployeesByRoleAsync(EmployeeRole role)
    {
        return await _dbSet
            .Where(e => e.Role == role && e.IsActive)
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetActiveEmployeesAsync()
    {
        return await _dbSet
            .Where(e => e.IsActive)
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .ToListAsync();
    }

    public async Task<bool> UpdatePasswordAsync(int employeeId, string newPassword)
    {
        var employee = await GetByIdAsync(employeeId);
        if (employee == null) return false;

        employee.Password = newPassword;
        employee.UpdatedAt = DateTime.UtcNow;
        await SaveChangesAsync();

        return true;
    }

    public async Task<bool> IsEmployeeCodeUniqueAsync(string employeeCode, int? excludeId = null)
    {
        var query = _dbSet.Where(e => e.EmployeeCode == employeeCode);
        if (excludeId.HasValue)
            query = query.Where(e => e.Id != excludeId.Value);

        return !await query.AnyAsync();
    }
}
