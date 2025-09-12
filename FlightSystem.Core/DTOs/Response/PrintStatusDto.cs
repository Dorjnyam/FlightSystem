namespace FlightSystem.Core.DTOs.Response
{
    public class PrintStatusDto
    {
        public bool IsAvailable { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
        public DateTime LastChecked { get; set; }
    }
}
