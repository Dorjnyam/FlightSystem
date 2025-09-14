using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Core.Enums;

namespace FlightSystem.Test.Shared;

public class FlightSystemSharedTests
{
    [Fact]
    public void FlightInfoDto_Properties_ShouldBeCorrect()
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
            GateNumber = "G1",
            CheckinOpenTime = DateTime.UtcNow.AddHours(1),
            CheckinCloseTime = DateTime.UtcNow.AddMinutes(30)
        };

        // Assert
        Assert.Equal(1, flightInfo.Id);
        Assert.Equal("PM001", flightInfo.FlightNumber);
        Assert.Equal("UB", flightInfo.DepartureAirport);
        Assert.Equal("TOK", flightInfo.ArrivalAirport);
        Assert.Equal("Scheduled", flightInfo.Status);
        Assert.Equal("G1", flightInfo.GateNumber);
    }

    [Fact]
    public void PassengerDto_Properties_ShouldBeCorrect()
    {
        // Arrange
        var passenger = new PassengerDto
        {
            Id = 1,
            PassportNumber = "TEST123456",
            FirstName = "John",
            LastName = "Doe",
            Nationality = "Mongolian",
            DateOfBirth = DateTime.Now.AddYears(-30),
            PassengerType = "Adult",
            Email = "john@example.com",
            Phone = "1234567890"
        };

        // Assert
        Assert.Equal(1, passenger.Id);
        Assert.Equal("TEST123456", passenger.PassportNumber);
        Assert.Equal("John", passenger.FirstName);
        Assert.Equal("Doe", passenger.LastName);
        Assert.Equal("Mongolian", passenger.Nationality);
        Assert.Equal("Adult", passenger.PassengerType);
        Assert.Equal("john@example.com", passenger.Email);
    }

    [Fact]
    public void FlightPassengerDto_Properties_ShouldBeCorrect()
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
    public void CheckinRequestDto_Properties_ShouldBeCorrect()
    {
        // Arrange
        var checkinRequest = new CheckinRequestDto
        {
            FlightNumber = "PM001",
            PassportNumber = "TEST123456",
            EmployeeId = 1,
            PreferredSeatId = 15,
            SpecialRequests = "Window seat",
            BaggageInfo = "1 checked bag"
        };

        // Assert
        Assert.Equal("PM001", checkinRequest.FlightNumber);
        Assert.Equal("TEST123456", checkinRequest.PassportNumber);
        Assert.Equal(1, checkinRequest.EmployeeId);
        Assert.Equal(15, checkinRequest.PreferredSeatId);
        Assert.Equal("Window seat", checkinRequest.SpecialRequests);
        Assert.Equal("1 checked bag", checkinRequest.BaggageInfo);
    }

    [Fact]
    public void CheckinResultDto_Properties_ShouldBeCorrect()
    {
        // Arrange
        var checkinResult = new CheckinResultDto
        {
            IsSuccess = true,
            Message = "Check-in successful",
            FlightPassenger = new FlightPassengerDto(),
            SeatAssignment = new SeatAssignmentDto(),
            BoardingPass = new BoardingPassDto(),
            Errors = new List<string>()
        };

        // Assert
        Assert.True(checkinResult.IsSuccess);
        Assert.Equal("Check-in successful", checkinResult.Message);
        Assert.NotNull(checkinResult.FlightPassenger);
        Assert.NotNull(checkinResult.SeatAssignment);
        Assert.NotNull(checkinResult.BoardingPass);
        Assert.NotNull(checkinResult.Errors);
    }

    [Fact]
    public void BoardingPassDto_Properties_ShouldBeCorrect()
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
    public void CreateFlightDto_Properties_ShouldBeCorrect()
    {
        // Arrange
        var createFlight = new CreateFlightDto
        {
            FlightNumber = "PM002",
            DepartureAirport = "UB",
            ArrivalAirport = "PEK",
            ScheduledDeparture = DateTime.UtcNow.AddHours(3),
            ScheduledArrival = DateTime.UtcNow.AddHours(7),
            GateNumber = "G2",
            CheckinOpenTime = DateTime.UtcNow.AddHours(2),
            CheckinCloseTime = DateTime.UtcNow.AddMinutes(45)
        };

        // Assert
        Assert.Equal("PM002", createFlight.FlightNumber);
        Assert.Equal("UB", createFlight.DepartureAirport);
        Assert.Equal("PEK", createFlight.ArrivalAirport);
        Assert.Equal("G2", createFlight.GateNumber);
    }

    [Fact]
    public void CreatePassengerDto_Properties_ShouldBeCorrect()
    {
        // Arrange
        var createPassenger = new CreatePassengerDto
        {
            PassportNumber = "TEST789012",
            FirstName = "Jane",
            LastName = "Smith",
            Nationality = "American",
            DateOfBirth = DateTime.Now.AddYears(-25),
            Type = PassengerType.Adult,
            Email = "jane@example.com",
            Phone = "0987654321"
        };

        // Assert
        Assert.Equal("TEST789012", createPassenger.PassportNumber);
        Assert.Equal("Jane", createPassenger.FirstName);
        Assert.Equal("Smith", createPassenger.LastName);
        Assert.Equal("American", createPassenger.Nationality);
        Assert.Equal(PassengerType.Adult, createPassenger.Type);
        Assert.Equal("jane@example.com", createPassenger.Email);
    }

    [Fact]
    public void CreateFlightPassengerDto_Properties_ShouldBeCorrect()
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
    public void ConcurrentSeatTestRequestDto_Properties_ShouldBeCorrect()
    {
        // Arrange
        var testRequest = new ConcurrentSeatTestRequestDto
        {
            FlightId = 1,
            SeatNumber = "25A",
            Passenger1Passport = "TEST111111",
            Passenger2Passport = "TEST222222"
        };

        // Assert
        Assert.Equal(1, testRequest.FlightId);
        Assert.Equal("25A", testRequest.SeatNumber);
        Assert.Equal("TEST111111", testRequest.Passenger1Passport);
        Assert.Equal("TEST222222", testRequest.Passenger2Passport);
    }
}
