namespace FlightSystem.Shared.DTOs.Response
{
    public class AuthResultDto
    {
        public bool IsSuccess { get; set; }
        public string? Token { get; set; }
        public EmployeeDto? Employee { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime? ExpiresAt { get; set; }
    }
}
