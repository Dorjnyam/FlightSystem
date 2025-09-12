using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using FlightSystem.Core.Models;
using FlightSystem.Core.Enums;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Core.Interfaces.Repositories;
using FlightSystem.Core.Interfaces.Services;
using FlightSystem.Core.Services;
using FlightSystem.Core.Exceptions;
using FlightSystem.Tests.Core.Helpers;

namespace FlightSystem.Tests.Core.Services;

public class CheckinServiceTests
{
    private readonly Mock<IFlightRepository> _mockFlightRepo;
    private readonly Mock<IPassengerRepository> _mockPassengerRepo;
    private readonly Mock<IFlightPassengerRepository> _mockFlightPassengerRepo;
    private readonly Mock<ISeatRepository> _mockSeatRepo;
    private readonly Mock<ISeatAssignmentRepository> _mockSeatAssignmentRepo;
    private readonly Mock<IBoardingPassRepository> _mockBoardingPassRepo;
    private readonly Mock<IEmployeeRepository> _mockEmployeeRepo;
    private readonly Mock<INotificationService> _mockNotificationService;
    private readonly Mock<ILogger<CheckinService>> _mockLogger;
    private readonly CheckinService _checkinService;

    public CheckinServiceTests()
    {
        _mockFlightRepo = new Mock<IFlightRepository>();
        _mockPassengerRepo = new Mock<IPassengerRepository>();
        _mockFlightPassengerRepo = new Mock<IFlightPassengerRepository>();
        _mockSeatRepo = new Mock<ISeatRepository>();
        _mockSeatAssignmentRepo = new Mock<ISeatAssignmentRepository>();
        _mockBoardingPassRepo = new Mock<IBoardingPassRepository>();
        _mockEmployeeRepo = new Mock<IEmployeeRepository>();
        _mockNotificationService = new Mock<INotificationService>();
        _mockLogger = new Mock<ILogger<CheckinService>>();

        _checkinService = new CheckinService(
            _mockFlightRepo.Object,
            _mockPassengerRepo.Object,
            _mockFlightPassengerRepo.Object,
            _mockSeatRepo.Object,
            _mockSeatAssignmentRepo.Object,
            _mockBoardingPassRepo.Object,
            _mockEmployeeRepo.Object,
            _mockNotificationService.Object,
            _mockLogger.Object
        );
    }

    [Fact]
    public async Task CheckinPassengerAsync_ValidRequest_ShouldReturnSuccessResult()
    {
        // Arrange
        var request = TestDataHelper.CreateValidCheckinRequest();
        SetupValidCheckinScenario();

        // Act
        var result = await _checkinService.CheckinPassengerAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.FlightPassenger.Should().NotBeNull();
        result.FlightPassenger.IsCheckedIn.Should().BeTrue();
    }

    [Fact]
    public async Task CheckinPassengerAsync_FlightNotFound_ShouldThrowFlightNotFoundException()
    {
        // Arrange
        var request = TestDataHelper.CreateValidCheckinRequest();
        _mockFlightRepo.Setup(r => r.GetByFlightNumberAsync(It.IsAny<string>()))
                      .ReturnsAsync((Flight)null);

        // Act & Assert
        await Assert.ThrowsAsync<FlightNotFoundException>(() => 
            _checkinService.CheckinPassengerAsync(request));
    }

    [Fact]
    public async Task CheckinPassengerAsync_PassengerNotFound_ShouldThrowPassengerNotFoundException()
    {
        // Arrange
        var request = TestDataHelper.CreateValidCheckinRequest();
        _mockPassengerRepo.Setup(r => r.GetByPassportNumberAsync(It.IsAny<string>()))
                         .ReturnsAsync((Passenger)null);

        // Act & Assert
        await Assert.ThrowsAsync<PassengerNotFoundException>(() => 
            _checkinService.CheckinPassengerAsync(request));
    }

    [Fact]
    public async Task CheckinPassengerAsync_PassengerAlreadyCheckedIn_ShouldReturnError()
    {
        // Arrange
        var request = TestDataHelper.CreateValidCheckinRequest();
        var checkedInFlightPassenger = TestDataHelper.CreateTestFlightPassenger();
        checkedInFlightPassenger.IsCheckedIn = true;
        
        _mockFlightPassengerRepo.Setup(r => r.GetFlightPassengerAsync(It.IsAny<int>(), It.IsAny<int>()))
                               .ReturnsAsync(checkedInFlightPassenger);

        SetupBasicCheckinScenario();

        // Act
        var result = await _checkinService.CheckinPassengerAsync(request);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task AssignSeatAsync_ValidRequest_ShouldReturnSeatAssignment()
    {
        // Arrange
        var request = new AssignSeatRequestDto
        {
            FlightPassengerId = 1,
            SeatId = 1,
            EmployeeId = 1,
            Notes = "Тест суудал"
        };

        var flightPassenger = TestDataHelper.CreateTestFlightPassenger();
        _mockFlightPassengerRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                               .ReturnsAsync(flightPassenger);

        // Act
        var result = await _checkinService.AssignSeatAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.IsActive.Should().BeTrue();
    }

    [Fact]
    public async Task IsSeatAvailableAsync_AvailableSeat_ShouldReturnTrue()
    {
        // Arrange
        var flightId = 1;
        var seatId = 1;

        // Act
        var result = await _checkinService.IsSeatAvailableAsync(flightId, seatId);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task ValidateCheckinEligibilityAsync_ValidScenario_ShouldReturnEligible()
    {
        // Arrange
        var flightId = 1;
        var passportNumber = "MP12345678";
        SetupValidCheckinScenario();

        // Act
        var result = await _checkinService.ValidateCheckinEligibilityAsync(flightId, passportNumber);

        // Assert
        result.Should().NotBeNull();
        result.IsEligible.Should().BeTrue();
        result.Reason.Should().NotBeNullOrEmpty();
    }

    private void SetupValidCheckinScenario()
    {
        SetupBasicCheckinScenario();
        
        var seat = TestDataHelper.CreateTestSeat();
        _mockSeatRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(seat);

        _mockBoardingPassRepo.Setup(r => r.AddAsync(It.IsAny<BoardingPass>()))
                           .ReturnsAsync((BoardingPass bp) => { bp.Id = 1; return bp; });
    }

    private void SetupBasicCheckinScenario()
    {
        var flight = TestDataHelper.CreateTestFlight();
        var passenger = TestDataHelper.CreateTestPassenger();
        var flightPassenger = TestDataHelper.CreateTestFlightPassenger();

        _mockFlightRepo.Setup(r => r.GetByFlightNumberAsync(It.IsAny<string>()))
                      .ReturnsAsync(flight);
        
        _mockPassengerRepo.Setup(r => r.GetByPassportNumberAsync(It.IsAny<string>()))
                         .ReturnsAsync(passenger);
        
        _mockFlightPassengerRepo.Setup(r => r.GetFlightPassengerAsync(It.IsAny<int>(), It.IsAny<int>()))
                               .ReturnsAsync(flightPassenger);
    }
}
