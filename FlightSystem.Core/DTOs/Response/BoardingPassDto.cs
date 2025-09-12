namespace FlightSystem.Core.DTOs.Response
{
    public class BoardingPassDto
    {
        public int Id { get; set; }
        public string BoardingPassCode { get; set; } = string.Empty;
        public string QRCode { get; set; } = string.Empty;
        public string BarcodeData { get; set; } = string.Empty;
        public DateTime IssuedAt { get; set; }
        public string IssuedByEmployee { get; set; } = string.Empty;
        public DateTime? BoardingTime { get; set; }
        public bool IsBoardingComplete { get; set; }
        public string? Gate { get; set; }
        public FlightInfoDto Flight { get; set; } = new();
        public PassengerDto Passenger { get; set; } = new();
        public SeatInfoDto Seat { get; set; } = new();
    }
}
