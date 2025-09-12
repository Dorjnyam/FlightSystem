using FlightSystem.Shared.Enums;

namespace FlightSystem.Shared.DTOs.Request
{
    public class CreatePassengerDto
    {
        public string PassportNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public PassengerType Type { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
