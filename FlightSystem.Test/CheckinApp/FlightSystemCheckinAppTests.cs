using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;

namespace FlightSystem.Test.CheckinApp;

public class FlightSystemCheckinAppTests
{
    [Fact]
    public void CheckinRequestDto_Properties_ShouldBeSettable()
    {
        // Arrange
        var request = new CheckinRequestDto
        {
            FlightNumber = "PM001",
            PassportNumber = "TEST123456",
            EmployeeId = 1,
            PreferredSeatId = 12,
            SpecialRequests = "Window seat preferred",
            BaggageInfo = "1 checked bag"
        };

        // Assert
        Assert.Equal("PM001", request.FlightNumber);
        Assert.Equal("TEST123456", request.PassportNumber);
        Assert.Equal(1, request.EmployeeId);
        Assert.Equal(12, request.PreferredSeatId);
        Assert.Equal("Window seat preferred", request.SpecialRequests);
        Assert.Equal("1 checked bag", request.BaggageInfo);
    }

    [Fact]
    public void CheckinResultDto_Properties_ShouldBeSettable()
    {
        // Arrange
        var result = new CheckinResultDto
        {
            IsSuccess = true,
            Message = "Check-in successful",
            FlightPassenger = new FlightPassengerDto(),
            SeatAssignment = new SeatAssignmentDto(),
            BoardingPass = new BoardingPassDto(),
            Errors = new List<string>()
        };

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Check-in successful", result.Message);
        Assert.NotNull(result.FlightPassenger);
        Assert.NotNull(result.SeatAssignment);
        Assert.NotNull(result.BoardingPass);
        Assert.NotNull(result.Errors);
    }

    [Fact]
    public void BoardingPassDto_Properties_ShouldBeSettable()
    {
        // Arrange
        var boardingPass = new BoardingPassDto
        {
            Id = 1,
            BoardingPassCode = "BP123456",
            QRCode = "QR123456789",
            BarcodeData = "BARCODE123456789",
            IssuedAt = DateTime.UtcNow,
            IssuedByEmployee = "EMP001",
            BoardingTime = DateTime.UtcNow.AddMinutes(30),
            IsBoardingComplete = false,
            Gate = "G1",
            Flight = new FlightInfoDto(),
            Passenger = new PassengerDto(),
            Seat = new SeatInfoDto()
        };

        // Assert
        Assert.Equal(1, boardingPass.Id);
        Assert.Equal("BP123456", boardingPass.BoardingPassCode);
        Assert.Equal("QR123456789", boardingPass.QRCode);
        Assert.Equal("BARCODE123456789", boardingPass.BarcodeData);
        Assert.Equal("EMP001", boardingPass.IssuedByEmployee);
        Assert.Equal("G1", boardingPass.Gate);
        Assert.NotNull(boardingPass.Flight);
        Assert.NotNull(boardingPass.Passenger);
        Assert.NotNull(boardingPass.Seat);
    }

    [Fact]
    public void ConcurrentSeatTestRequestDto_Properties_ShouldBeSettable()
    {
        // Arrange
        var testRequest = new ConcurrentSeatTestRequestDto
        {
            FlightId = 1,
            SeatNumber = "20C",
            Passenger1Passport = "TEST111111",
            Passenger2Passport = "TEST222222"
        };

        // Assert
        Assert.Equal(1, testRequest.FlightId);
        Assert.Equal("20C", testRequest.SeatNumber);
        Assert.Equal("TEST111111", testRequest.Passenger1Passport);
        Assert.Equal("TEST222222", testRequest.Passenger2Passport);
    }

    [Fact]
    public void ConcurrentSeatTestResultDto_Properties_ShouldBeSettable()
    {
        // Arrange
        var testResult = new ConcurrentSeatTestResultDto
        {
            TestId = "TEST123",
            TestStartTime = DateTime.UtcNow,
            TestEndTime = DateTime.UtcNow.AddMilliseconds(100),
            WinnerPassenger = "TEST111111",
            Summary = "Test completed successfully"
        };

        // Assert
        Assert.Equal("TEST123", testResult.TestId);
        Assert.NotNull(testResult.TestStartTime);
        Assert.NotNull(testResult.TestEndTime);
        Assert.Equal("TEST111111", testResult.WinnerPassenger);
        Assert.Equal("Test completed successfully", testResult.Summary);
    }

    [Fact]
    public void SeatAssignmentDto_Properties_ShouldBeSettable()
    {
        // Arrange
        var seatAssignment = new SeatAssignmentDto
        {
            Id = 1,
            AssignedAt = DateTime.UtcNow,
            AssignedByEmployee = "EMP001",
            IsActive = true,
            Notes = "Window seat preference",
            Seat = new SeatInfoDto(),
            Passenger = new PassengerDto()
        };

        // Assert
        Assert.Equal(1, seatAssignment.Id);
        Assert.Equal("EMP001", seatAssignment.AssignedByEmployee);
        Assert.True(seatAssignment.IsActive);
        Assert.Equal("Window seat preference", seatAssignment.Notes);
        Assert.NotNull(seatAssignment.Seat);
        Assert.NotNull(seatAssignment.Passenger);
    }

    [Fact]
    public void FlightPassengerDto_Properties_ShouldBeSettable()
    {
        // Arrange
        var flightPassenger = new FlightPassengerDto
        {
            Id = 1,
            BookingReference = "ABC123",
            IsCheckedIn = true,
            CheckinTime = DateTime.UtcNow,
            CheckinByEmployee = "EMP001",
            SpecialRequests = "Window seat",
            BaggageInfo = "1 checked bag",
            Passenger = new PassengerDto(),
            Flight = new FlightInfoDto(),
            AssignedSeat = new SeatInfoDto()
        };

        // Assert
        Assert.Equal(1, flightPassenger.Id);
        Assert.Equal("ABC123", flightPassenger.BookingReference);
        Assert.True(flightPassenger.IsCheckedIn);
        Assert.Equal("EMP001", flightPassenger.CheckinByEmployee);
        Assert.Equal("Window seat", flightPassenger.SpecialRequests);
        Assert.Equal("1 checked bag", flightPassenger.BaggageInfo);
        Assert.NotNull(flightPassenger.Passenger);
        Assert.NotNull(flightPassenger.Flight);
        Assert.NotNull(flightPassenger.AssignedSeat);
    }

    [Fact]
    public void CreateFlightPassengerDto_Properties_ShouldBeSettable()
    {
        // Arrange
        var createFlightPassenger = new CreateFlightPassengerDto
        {
            FlightId = 1,
            PassengerId = 1,
            BookingReference = "XYZ789"
        };

        // Assert
        Assert.Equal(1, createFlightPassenger.FlightId);
        Assert.Equal(1, createFlightPassenger.PassengerId);
        Assert.Equal("XYZ789", createFlightPassenger.BookingReference);
    }

    [Fact]
    public void AssignSeatRequestDto_Properties_ShouldBeSettable()
    {
        // Arrange
        var assignSeatRequest = new AssignSeatRequestDto
        {
            FlightPassengerId = 1,
            SeatId = 15,
            EmployeeId = 1,
            Notes = "Window seat preference"
        };

        // Assert
        Assert.Equal(1, assignSeatRequest.FlightPassengerId);
        Assert.Equal(15, assignSeatRequest.SeatId);
        Assert.Equal(1, assignSeatRequest.EmployeeId);
        Assert.Equal("Window seat preference", assignSeatRequest.Notes);
    }

    [Fact]
    public void FlightInfoDto_Properties_ShouldBeSettable()
    {
        // Arrange
        var flightInfo = new FlightInfoDto
        {
            Id = 1,
            FlightNumber = "PM001",
            DepartureAirport = "UB",
            ArrivalAirport = "TOK",
            ScheduledDeparture = DateTime.UtcNow.AddHours(2),
            ScheduledArrival = DateTime.UtcNow.AddHours(6),
            Status = "Scheduled",
            GateNumber = "G1"
        };

        // Assert
        Assert.Equal(1, flightInfo.Id);
        Assert.Equal("PM001", flightInfo.FlightNumber);
        Assert.Equal("UB", flightInfo.DepartureAirport);
        Assert.Equal("TOK", flightInfo.ArrivalAirport);
        Assert.Equal("Scheduled", flightInfo.Status);
        Assert.Equal("G1", flightInfo.GateNumber);
    }
}
