using FlightSystem.Core.Enums;

namespace FlightSystem.Shared.DTOs.Response;

public class EmployeeLoginResultDto
{
    public bool IsSuccess { get; set; }
    public string? Token { get; set; }
    public EmployeeDto? Employee { get; set; }
    public List<string> Errors { get; set; } = [];
}
