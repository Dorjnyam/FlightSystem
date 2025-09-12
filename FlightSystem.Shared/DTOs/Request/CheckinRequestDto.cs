using FlightSystem.Shared.Enums;

namespace FlightSystem.Shared.DTOs.Request
{
    public class CheckinRequestDto
    {
        public string FlightNumber { get; set; } = string.Empty;
        public string PassportNumber { get; set; } = string.Empty;
        public int EmployeeId { get; set; }
        public int? PreferredSeatId { get; set; }
        public string? SpecialRequests { get; set; }
        public string? BaggageInfo { get; set; }
    }
}
