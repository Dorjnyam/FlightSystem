using FlightSystem.Core.Enums;

namespace FlightSystem.Shared.DTOs.Response
{
    public class CheckinEligibilityDto
    {
        public bool IsEligible { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime? CheckinOpenTime { get; set; }
        public DateTime? CheckinCloseTime { get; set; }
        public bool IsAlreadyCheckedIn { get; set; }
        public FlightStatus FlightStatus { get; set; }
    }
}
