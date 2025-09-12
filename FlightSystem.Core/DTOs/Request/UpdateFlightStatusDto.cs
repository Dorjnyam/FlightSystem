using FlightSystem.Core.Enums;

namespace FlightSystem.Core.DTOs.Request
{
    public class UpdateFlightStatusDto
    {
        public FlightStatus Status { get; set; }
        public int UpdatedByEmployeeId { get; set; }
        public string? Reason { get; set; }
    }
}
