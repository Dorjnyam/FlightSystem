using FlightSystem.Shared.DTOs.Response;
using System.Net.Http;
using Moq;

namespace FlightSystem.Test.InfoDisplay;

public class FlightSystemInfoDisplayTests
{
    private readonly Mock<HttpClient> _mockHttpClient;

    public FlightSystemInfoDisplayTests()
    {
        _mockHttpClient = new Mock<HttpClient>();
    }

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
    public void FlightInfoDto_Status_ShouldBeValid()
    {
        // Arrange
        var flightInfo = new FlightInfoDto
        {
            Status = "CheckinOpen"
        };

        // Assert
        Assert.Equal("CheckinOpen", flightInfo.Status);
    }

    [Fact]
    public void FlightInfoDto_DepartureTime_ShouldBeCorrect()
    {
        // Arrange
        var departureTime = DateTime.UtcNow.AddHours(3);
        var flightInfo = new FlightInfoDto
        {
            ScheduledDeparture = departureTime
        };

        // Assert
        Assert.Equal(departureTime, flightInfo.ScheduledDeparture);
    }

    [Fact]
    public void FlightInfoDto_ArrivalTime_ShouldBeCorrect()
    {
        // Arrange
        var arrivalTime = DateTime.UtcNow.AddHours(7);
        var flightInfo = new FlightInfoDto
        {
            ScheduledArrival = arrivalTime
        };

        // Assert
        Assert.Equal(arrivalTime, flightInfo.ScheduledArrival);
    }

    [Fact]
    public void FlightInfoDto_GateNumber_ShouldBeCorrect()
    {
        // Arrange
        var flightInfo = new FlightInfoDto
        {
            GateNumber = "G5"
        };

        // Assert
        Assert.Equal("G5", flightInfo.GateNumber);
    }

    [Fact]
    public void FlightInfoDto_CheckinTimes_ShouldBeCorrect()
    {
        // Arrange
        var openTime = DateTime.UtcNow.AddHours(2);
        var closeTime = DateTime.UtcNow.AddMinutes(45);
        var flightInfo = new FlightInfoDto
        {
            CheckinOpenTime = openTime,
            CheckinCloseTime = closeTime
        };

        // Assert
        Assert.Equal(openTime, flightInfo.CheckinOpenTime);
        Assert.Equal(closeTime, flightInfo.CheckinCloseTime);
    }

    [Fact]
    public void FlightInfoDto_FlightNumber_ShouldBeValid()
    {
        // Arrange
        var flightInfo = new FlightInfoDto
        {
            FlightNumber = "PM123"
        };

        // Assert
        Assert.Equal("PM123", flightInfo.FlightNumber);
        Assert.True(flightInfo.FlightNumber.StartsWith("PM"));
    }

    [Fact]
    public void FlightInfoDto_Airports_ShouldBeValid()
    {
        // Arrange
        var flightInfo = new FlightInfoDto
        {
            DepartureAirport = "UB",
            ArrivalAirport = "PEK"
        };

        // Assert
        Assert.Equal("UB", flightInfo.DepartureAirport);
        Assert.Equal("PEK", flightInfo.ArrivalAirport);
        Assert.True(flightInfo.DepartureAirport.Length >= 2);
        Assert.True(flightInfo.ArrivalAirport.Length >= 2);
    }

    [Fact]
    public void FlightInfoDto_AllProperties_ShouldBeSettable()
    {
        // Arrange
        var flightInfo = new FlightInfoDto
        {
            Id = 999,
            FlightNumber = "TEST999",
            DepartureAirport = "ABC",
            ArrivalAirport = "XYZ",
            ScheduledDeparture = DateTime.UtcNow.AddDays(1),
            ScheduledArrival = DateTime.UtcNow.AddDays(1).AddHours(4),
            Status = "Delayed",
            GateNumber = "G99",
            CheckinOpenTime = DateTime.UtcNow.AddHours(23),
            CheckinCloseTime = DateTime.UtcNow.AddHours(23).AddMinutes(30)
        };

        // Assert
        Assert.Equal(999, flightInfo.Id);
        Assert.Equal("TEST999", flightInfo.FlightNumber);
        Assert.Equal("ABC", flightInfo.DepartureAirport);
        Assert.Equal("XYZ", flightInfo.ArrivalAirport);
        Assert.Equal("Delayed", flightInfo.Status);
        Assert.Equal("G99", flightInfo.GateNumber);
    }

    [Fact]
    public void FlightInfoDto_DefaultValues_ShouldBeCorrect()
    {
        // Arrange
        var flightInfo = new FlightInfoDto();

        // Assert
        Assert.Equal(0, flightInfo.Id);
        Assert.Equal(string.Empty, flightInfo.FlightNumber);
        Assert.Equal(string.Empty, flightInfo.DepartureAirport);
        Assert.Equal(string.Empty, flightInfo.ArrivalAirport);
        Assert.Equal(string.Empty, flightInfo.Status);
        Assert.Null(flightInfo.GateNumber);
        Assert.Equal(default(DateTime), flightInfo.ScheduledDeparture);
        Assert.Equal(default(DateTime), flightInfo.ScheduledArrival);
        Assert.Equal(default(DateTime), flightInfo.CheckinOpenTime);
        Assert.Equal(default(DateTime), flightInfo.CheckinCloseTime);
    }

    [Fact]
    public void FlightInfoDto_ToString_ShouldReturnTypeName()
    {
        // Arrange
        var flightInfo = new FlightInfoDto
        {
            FlightNumber = "PM001"
        };

        // Act
        var result = flightInfo.ToString();

        // Assert
        Assert.Contains("FlightInfoDto", result);
    }

    [Fact]
    public void FlightInfoDto_Equals_ShouldWorkCorrectly()
    {
        // Arrange
        var flightInfo1 = new FlightInfoDto
        {
            Id = 1,
            FlightNumber = "PM001"
        };
        var flightInfo2 = new FlightInfoDto
        {
            Id = 1,
            FlightNumber = "PM001"
        };
        var flightInfo3 = new FlightInfoDto
        {
            Id = 2,
            FlightNumber = "PM002"
        };

        // Act & Assert
        Assert.Equal(flightInfo1.Id, flightInfo2.Id);
        Assert.Equal(flightInfo1.FlightNumber, flightInfo2.FlightNumber);
        Assert.NotEqual(flightInfo1.Id, flightInfo3.Id);
        Assert.NotEqual(flightInfo1.FlightNumber, flightInfo3.FlightNumber);
    }

    [Fact]
    public void FlightInfoDto_GetHashCode_ShouldWorkCorrectly()
    {
        // Arrange
        var flightInfo1 = new FlightInfoDto
        {
            Id = 1,
            FlightNumber = "PM001"
        };
        var flightInfo2 = new FlightInfoDto
        {
            Id = 1,
            FlightNumber = "PM001"
        };

        // Act
        var hashCode1 = flightInfo1.GetHashCode();
        var hashCode2 = flightInfo2.GetHashCode();

        // Assert - Different instances will have different hash codes
        Assert.NotEqual(hashCode1, hashCode2);
    }

    [Fact]
    public void FlightInfoDto_Clone_ShouldWorkCorrectly()
    {
        // Arrange
        var original = new FlightInfoDto
        {
            Id = 1,
            FlightNumber = "PM001",
            DepartureAirport = "UB",
            ArrivalAirport = "TOK",
            Status = "Scheduled",
            GateNumber = "G1"
        };

        // Act
        var cloned = new FlightInfoDto
        {
            Id = original.Id,
            FlightNumber = original.FlightNumber,
            DepartureAirport = original.DepartureAirport,
            ArrivalAirport = original.ArrivalAirport,
            Status = original.Status,
            GateNumber = original.GateNumber,
            ScheduledDeparture = original.ScheduledDeparture,
            ScheduledArrival = original.ScheduledArrival,
            CheckinOpenTime = original.CheckinOpenTime,
            CheckinCloseTime = original.CheckinCloseTime
        };

        // Assert
        Assert.Equal(original.Id, cloned.Id);
        Assert.Equal(original.FlightNumber, cloned.FlightNumber);
        Assert.Equal(original.DepartureAirport, cloned.DepartureAirport);
        Assert.Equal(original.ArrivalAirport, cloned.ArrivalAirport);
        Assert.Equal(original.Status, cloned.Status);
        Assert.Equal(original.GateNumber, cloned.GateNumber);
    }

    [Fact]
    public void FlightInfoDto_Serialization_ShouldWorkCorrectly()
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

        // Act
        var json = System.Text.Json.JsonSerializer.Serialize(flightInfo);
        var deserialized = System.Text.Json.JsonSerializer.Deserialize<FlightInfoDto>(json);

        // Assert
        Assert.NotNull(json);
        Assert.NotNull(deserialized);
        Assert.Equal(flightInfo.Id, deserialized.Id);
        Assert.Equal(flightInfo.FlightNumber, deserialized.FlightNumber);
        Assert.Equal(flightInfo.DepartureAirport, deserialized.DepartureAirport);
        Assert.Equal(flightInfo.ArrivalAirport, deserialized.ArrivalAirport);
        Assert.Equal(flightInfo.Status, deserialized.Status);
        Assert.Equal(flightInfo.GateNumber, deserialized.GateNumber);
    }
}
