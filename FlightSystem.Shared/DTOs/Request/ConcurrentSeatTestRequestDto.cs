namespace FlightSystem.Shared.DTOs.Request
{
    /// <summary>
    /// Concurrent seat assignment test request
    /// </summary>
    public class ConcurrentSeatTestRequestDto
    {
        /// <summary>
        /// Flight ID for the test
        /// </summary>
        public int FlightId { get; set; }

        /// <summary>
        /// Seat number to test concurrent assignment
        /// </summary>
        public string SeatNumber { get; set; } = string.Empty;

        /// <summary>
        /// First passenger passport number
        /// </summary>
        public string Passenger1Passport { get; set; } = string.Empty;

        /// <summary>
        /// Second passenger passport number
        /// </summary>
        public string Passenger2Passport { get; set; } = string.Empty;
    }
}