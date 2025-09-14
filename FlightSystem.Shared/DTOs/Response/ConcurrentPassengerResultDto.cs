namespace FlightSystem.Shared.DTOs.Response
{
    /// <summary>
    /// Individual passenger result for concurrent seat assignment test
    /// </summary>
    public class ConcurrentPassengerResultDto
    {
        /// <summary>
        /// Passenger passport number
        /// </summary>
        public string PassportNumber { get; set; } = string.Empty;

        /// <summary>
        /// Whether the seat assignment was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Seat assignment ID if successful
        /// </summary>
        public string? SeatAssignmentId { get; set; }

        /// <summary>
        /// Error message if failed
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Processing time in milliseconds
        /// </summary>
        public long ProcessingTimeMs { get; set; }

        /// <summary>
        /// Request start time
        /// </summary>
        public DateTime RequestStartTime { get; set; }

        /// <summary>
        /// Request end time
        /// </summary>
        public DateTime RequestEndTime { get; set; }
    }
}
