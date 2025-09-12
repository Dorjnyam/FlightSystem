using FlightSystem.Core.Enums;

namespace FlightSystem.Shared.DTOs.Request
{
    public class UpdateEmployeeDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public EmployeeRole? Role { get; set; }
        public string? WorkStationId { get; set; }
        public bool? IsActive { get; set; }
    }
}
