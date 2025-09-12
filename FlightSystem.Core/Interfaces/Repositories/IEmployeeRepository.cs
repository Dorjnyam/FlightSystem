using FlightSystem.Core.Models;
using FlightSystem.Core.Enums;

namespace FlightSystem.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee?> GetByEmployeeCodeAsync(string employeeCode);
        Task<Employee?> AuthenticateAsync(string employeeCode, string password);
        Task<IEnumerable<Employee>> GetEmployeesByRoleAsync(EmployeeRole role);
        Task<IEnumerable<Employee>> GetActiveEmployeesAsync();
        Task<bool> IsEmployeeCodeUniqueAsync(string employeeCode, int? excludeId = null);
        Task<bool> UpdatePasswordAsync(int employeeId, string hashedPassword);
    }
}
