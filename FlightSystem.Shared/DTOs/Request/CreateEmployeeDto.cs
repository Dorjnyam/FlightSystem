using FlightSystem.Core.Enums;

namespace FlightSystem.Shared.DTOs.Request
{
    public class CreateEmployeeDto
    {
        public string EmployeeCode { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public EmployeeRole Role { get; set; }
        public string WorkStationId { get; set; } = string.Empty;
    }
}
