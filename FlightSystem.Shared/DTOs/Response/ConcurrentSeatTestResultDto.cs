namespace FlightSystem.Shared.DTOs.Response
{
    /// <summary>
    /// Result of concurrent seat assignment test
    /// </summary>
    public class ConcurrentSeatTestResultDto
    {
        /// <summary>
        /// Unique test identifier
        /// </summary>
        public string TestId { get; set; } = string.Empty;

        /// <summary>
        /// Test start time
        /// </summary>
        public DateTime TestStartTime { get; set; }

        /// <summary>
        /// Test end time
        /// </summary>
        public DateTime TestEndTime { get; set; }

        /// <summary>
        /// First passenger result
        /// </summary>
        public ConcurrentPassengerResultDto Passenger1Result { get; set; } = new();

        /// <summary>
        /// Second passenger result
        /// </summary>
        public ConcurrentPassengerResultDto Passenger2Result { get; set; } = new();

        /// <summary>
        /// Winner passenger passport number
        /// </summary>
        public string WinnerPassenger { get; set; } = string.Empty;

        /// <summary>
        /// Test summary
        /// </summary>
        public string Summary { get; set; } = string.Empty;
    }
}