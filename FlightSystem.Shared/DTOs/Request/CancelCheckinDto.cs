namespace FlightSystem.Shared.DTOs.Request
{
    /// <summary>
    /// check-in хүсэлт цуцлах DTO
    /// </summary>
    public class CancelCheckinDto
    {
        public int EmployeeId { get; set; }
        public string? Reason { get; set; }
    }
}