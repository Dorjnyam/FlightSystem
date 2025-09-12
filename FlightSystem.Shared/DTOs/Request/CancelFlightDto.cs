namespace FlightSystem.Shared.DTOs.Request
{
    /// <summary>
    /// нислэг цуцлах DTO
    /// </summary>
    public class CancelFlightDto
    {
        public int EmployeeId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public bool NotifyPassengers { get; set; } = true;
    }
}