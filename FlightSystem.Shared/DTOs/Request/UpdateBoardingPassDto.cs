namespace FlightSystem.Shared.DTOs.Request
{
    public class UpdateBoardingPassDto
    {
        public DateTime? BoardingTime { get; set; }
        public bool? IsBoardingComplete { get; set; }
        public string? Gate { get; set; }
    }
}
