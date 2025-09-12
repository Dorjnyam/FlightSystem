using FlightSystem.Shared.Interfaces.Services;
using FlightSystem.Shared.Interfaces.Repositories;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Core.Models;
using FlightSystem.Core.Enums;
using FlightSystem.Core.Exceptions;
using FlightSystem.Core.Extensions;
using Microsoft.Extensions.Logging;
using System.Text;

namespace FlightSystem.Shared.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createDto)
        {
            var existingEmployee = await _employeeRepository.GetByEmployeeCodeAsync(createDto.EmployeeCode);
            if (existingEmployee != null)
                throw new FlightSystemException($"Ажилтны код {createDto.EmployeeCode} аль хэдийн хэрэглэгдсэн байна", "EMPLOYEE_CODE_EXISTS");

            var employee = new Employee
            {
                EmployeeCode = createDto.EmployeeCode,
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                Email = createDto.Email,
                Password = createDto.Password,
                Role = createDto.Role,
                WorkStationId = createDto.WorkStationId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            employee = await _employeeRepository.AddAsync(employee);
            _logger.LogInformation("Ажилтан {EmployeeCode} амжилттай үүслээ, ID {EmployeeId}", employee.EmployeeCode, employee.Id);

            return MapToEmployeeDto(employee);
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            return employee != null ? MapToEmployeeDto(employee) : null;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees.Select(MapToEmployeeDto);
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(int employeeId, UpdateEmployeeDto updateDto)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
                throw new FlightSystemException("Ажилтан олдсонгүй", "EMPLOYEE_NOT_FOUND");

            if (updateDto.FirstName != null)
                employee.FirstName = updateDto.FirstName;
            if (updateDto.LastName != null)
                employee.LastName = updateDto.LastName;
            if (updateDto.Email != null)
                employee.Email = updateDto.Email;
            if (updateDto.Role.HasValue)
                employee.Role = updateDto.Role.Value;
            if (updateDto.WorkStationId != null)
                employee.WorkStationId = updateDto.WorkStationId;
            if (updateDto.IsActive.HasValue)
                employee.IsActive = updateDto.IsActive.Value;

            employee.UpdatedAt = DateTime.UtcNow;
            employee = await _employeeRepository.UpdateAsync(employee);

            _logger.LogInformation("Ажилтан {EmployeeCode} амжилттай шинэчлэгдлээ, ID {EmployeeId}", employee.EmployeeCode, employeeId);
            return MapToEmployeeDto(employee);
        }

        public async Task<bool> DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
                return false;

            employee.IsActive = false;
            employee.UpdatedAt = DateTime.UtcNow;
            await _employeeRepository.UpdateAsync(employee);

            _logger.LogInformation("Ажилтан {EmployeeCode} идэвхгүй боллоо, ID {EmployeeId}", employee.EmployeeCode, employeeId);
            return true;
        }

        public async Task<AuthResultDto?> AuthenticateAsync(string employeeCode, string password)
        {
            var employee = await _employeeRepository.AuthenticateAsync(employeeCode, password);
            if (employee == null)
                return new AuthResultDto { IsSuccess = false, Message = "Нэвтрэх мэдээлэл буруу байна" };

            if (!employee.IsActive)
                return new AuthResultDto { IsSuccess = false, Message = "Ажилтны акаунт идэвхгүй байна" };

            var token = await GenerateTokenAsync(employee);
            _logger.LogInformation("Ажилтан {EmployeeCode} амжилттай нэвтэрлээ", employeeCode);

            return new AuthResultDto
            {
                IsSuccess = true,
                Token = token,
                Employee = MapToEmployeeDto(employee),
                Message = "Амжилттай нэвтэрлээ",
                ExpiresAt = DateTime.UtcNow.AddHours(8)
            };
        }

        public async Task<bool> HasPermissionAsync(int employeeId, string permission)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null || !employee.IsActive)
                return false;

            return permission switch
            {
                "checkin" => employee.Role == EmployeeRole.CheckinAgent || employee.Role == EmployeeRole.Supervisor || employee.Role == EmployeeRole.Admin,
                "flight_status" => employee.Role == EmployeeRole.Supervisor || employee.Role == EmployeeRole.Admin,
                "admin" => employee.Role == EmployeeRole.Admin,
                _ => false
            };
        }

        public async Task<string> GenerateTokenAsync(Employee employee)
        {
            var tokenData = $"{employee.Id}:{employee.EmployeeCode}:{DateTime.UtcNow.Ticks}";
            var tokenBytes = Encoding.UTF8.GetBytes(tokenData);
            return Convert.ToBase64String(tokenBytes);
        }

        public async Task<bool> ChangePasswordAsync(int employeeId, string oldPassword, string newPassword)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
                return false;

            if (employee.Password != oldPassword)
                return false;

            await _employeeRepository.UpdatePasswordAsync(employeeId, newPassword);
            _logger.LogInformation("Ажилтан {EmployeeCode}-ийн нууц үг амжилттай солигдлоо", employee.EmployeeCode);
            return true;
        }

        public async Task<EmployeeDto?> GetByEmployeeCodeAsync(string employeeCode)
        {
            var employee = await _employeeRepository.GetByEmployeeCodeAsync(employeeCode);
            return employee != null ? MapToEmployeeDto(employee) : null;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByRoleAsync(EmployeeRole role)
        {
            var employees = await _employeeRepository.GetEmployeesByRoleAsync(role);
            return employees.Select(MapToEmployeeDto);
        }

        private EmployeeDto MapToEmployeeDto(Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                EmployeeCode = employee.EmployeeCode,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Role = employee.Role.GetDisplayName(),
                WorkStationId = employee.WorkStationId,
                IsActive = employee.IsActive,
                CreatedAt = employee.CreatedAt
            };
        }
    }
}
