using FlightSystem.Core.DTOs.Request;
using FlightSystem.Core.DTOs.Response;
using FlightSystem.Core.Enums;
using FlightSystem.Core.Models;

namespace FlightSystem.Core.Interfaces.Services
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Шинээр ажилтан үүсгэх
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createDto);
        /// <summary>
        /// ID-аар ажилтан хайх
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<EmployeeDto?> GetEmployeeByIdAsync(int employeeId);
        /// <summary>
        /// Бүх ажилтныг авах
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        /// <summary>
        /// Ажилтны мэдээллийг шинэчлэх
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        Task<EmployeeDto> UpdateEmployeeAsync(int employeeId, UpdateEmployeeDto updateDto);
        /// <summary>
        /// Ажилтан устгах
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<bool> DeleteEmployeeAsync(int employeeId);

        /// <summary>
        /// Ажилтан нэвтрэх
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<AuthResultDto?> AuthenticateAsync(string employeeCode, string password);
        /// <summary>
        /// Ажилтан нэвтрэх эрхтэй эсэхийг шалгах
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        Task<bool> HasPermissionAsync(int employeeId, string permission);
        /// <summary>
        /// JWT Token үүсгэх
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<string> GenerateTokenAsync(Employee employee);
        /// <summary>
        /// Нууц үг солих
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<bool> ChangePasswordAsync(int employeeId, string oldPassword, string newPassword);

        /// <summary>
        /// Ажилтныг ажилтан кодоор хайх
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        Task<EmployeeDto?> GetByEmployeeCodeAsync(string employeeCode);
        /// <summary>
        /// Ажилтныг үүргээр нь хайх
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<IEnumerable<EmployeeDto>> GetEmployeesByRoleAsync(EmployeeRole role);
    }
}
