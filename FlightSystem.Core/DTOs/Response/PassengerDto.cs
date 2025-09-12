namespace FlightSystem.Core.DTOs.Response
{
    public class PassengerDto
    {
        public int Id { get; set; }
        public string PassportNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Nationality { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PassengerType { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
