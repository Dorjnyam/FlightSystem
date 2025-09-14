using FlightSystem.Data.Context;
using FlightSystem.Data.Repositories;
using FlightSystem.Core.Models;
using FlightSystem.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace FlightSystem.Test.Data;

public class FlightSystemDataTests : IDisposable
{
    private readonly FlightSystemDbContext _context;
    private readonly FlightRepository _flightRepository;
    private readonly PassengerRepository _passengerRepository;
    private readonly EmployeeRepository _employeeRepository;

    public FlightSystemDataTests()
    {
        // Create in-memory database for testing
        var options = new DbContextOptionsBuilder<FlightSystemDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new FlightSystemDbContext(options);
        _flightRepository = new FlightRepository(_context);
        _passengerRepository = new PassengerRepository(_context);
        _employeeRepository = new EmployeeRepository(_context);
    }

    [Fact]
    public async Task FlightRepository_AddFlight_ShouldWork()
    {
        // Arrange
        var flight = new Flight
        {
            FlightNumber = "TEST001",
            DepartureAirport = "UB",
            ArrivalAirport = "TOK",
            ScheduledDeparture = DateTime.UtcNow.AddHours(2),
            ScheduledArrival = DateTime.UtcNow.AddHours(6),
            Status = FlightStatus.Scheduled,
            GateNumber = "G1"
        };

        // Act
        var result = await _flightRepository.AddAsync(flight);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("TEST001", result.FlightNumber);
        Assert.Equal("UB", result.DepartureAirport);
        Assert.Equal("TOK", result.ArrivalAirport);
    }

    [Fact]
    public async Task FlightRepository_GetFlightById_ShouldWork()
    {
        // Arrange
        var flight = new Flight
        {
            FlightNumber = "TEST002",
            DepartureAirport = "UB",
            ArrivalAirport = "PEK",
            ScheduledDeparture = DateTime.UtcNow.AddHours(3),
            ScheduledArrival = DateTime.UtcNow.AddHours(7),
            Status = FlightStatus.Scheduled,
            GateNumber = "G2"
        };
        await _flightRepository.AddAsync(flight);

        // Act
        var result = await _flightRepository.GetByIdAsync(flight.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("TEST002", result.FlightNumber);
        Assert.Equal("UB", result.DepartureAirport);
        Assert.Equal("PEK", result.ArrivalAirport);
    }

    [Fact]
    public async Task FlightRepository_GetAllFlights_ShouldWork()
    {
        // Arrange
        var flight1 = new Flight
        {
            FlightNumber = "TEST003",
            DepartureAirport = "UB",
            ArrivalAirport = "ICN",
            ScheduledDeparture = DateTime.UtcNow.AddHours(4),
            ScheduledArrival = DateTime.UtcNow.AddHours(8),
            Status = FlightStatus.Scheduled,
            GateNumber = "G3"
        };
        var flight2 = new Flight
        {
            FlightNumber = "TEST004",
            DepartureAirport = "UB",
            ArrivalAirport = "NRT",
            ScheduledDeparture = DateTime.UtcNow.AddHours(5),
            ScheduledArrival = DateTime.UtcNow.AddHours(9),
            Status = FlightStatus.Scheduled,
            GateNumber = "G4"
        };
        await _flightRepository.AddAsync(flight1);
        await _flightRepository.AddAsync(flight2);

        // Act
        var result = await _flightRepository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() >= 2);
    }

    [Fact]
    public async Task FlightRepository_UpdateFlight_ShouldWork()
    {
        // Arrange
        var flight = new Flight
        {
            FlightNumber = "TEST005",
            DepartureAirport = "UB",
            ArrivalAirport = "SEL",
            ScheduledDeparture = DateTime.UtcNow.AddHours(6),
            ScheduledArrival = DateTime.UtcNow.AddHours(10),
            Status = FlightStatus.Scheduled,
            GateNumber = "G5"
        };
        await _flightRepository.AddAsync(flight);

        // Act
        flight.Status = FlightStatus.CheckinOpen;
        flight.GateNumber = "G6";
        var result = await _flightRepository.UpdateAsync(flight);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(FlightStatus.CheckinOpen, result.Status);
        Assert.Equal("G6", result.GateNumber);
    }

    [Fact]
    public async Task FlightRepository_DeleteFlight_ShouldWork()
    {
        // Arrange
        var flight = new Flight
        {
            FlightNumber = "TEST006",
            DepartureAirport = "UB",
            ArrivalAirport = "BKK",
            ScheduledDeparture = DateTime.UtcNow.AddHours(7),
            ScheduledArrival = DateTime.UtcNow.AddHours(11),
            Status = FlightStatus.Scheduled,
            GateNumber = "G7"
        };
        await _flightRepository.AddAsync(flight);
        var flightId = flight.Id;

        // Act
        await _flightRepository.DeleteAsync(flightId);

        // Assert
        var result = await _flightRepository.GetByIdAsync(flightId);
        Assert.Null(result);
    }

    [Fact]
    public async Task PassengerRepository_AddPassenger_ShouldWork()
    {
        // Arrange
        var passenger = new Passenger
        {
            PassportNumber = "TEST123456",
            FirstName = "Test",
            LastName = "Passenger",
            Nationality = "Mongolian",
            DateOfBirth = DateTime.Now.AddYears(-30),
            Type = PassengerType.Adult,
            Email = "test@example.com",
            Phone = "1234567890"
        };

        // Act
        var result = await _passengerRepository.AddAsync(passenger);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("TEST123456", result.PassportNumber);
        Assert.Equal("Test", result.FirstName);
        Assert.Equal("Passenger", result.LastName);
    }

    [Fact]
    public async Task PassengerRepository_GetPassengerByPassport_ShouldWork()
    {
        // Arrange
        var passenger = new Passenger
        {
            PassportNumber = "TEST789012",
            FirstName = "John",
            LastName = "Doe",
            Nationality = "American",
            DateOfBirth = DateTime.Now.AddYears(-25),
            Type = PassengerType.Adult,
            Email = "john@example.com",
            Phone = "0987654321"
        };
        await _passengerRepository.AddAsync(passenger);

        // Act
        var result = await _passengerRepository.GetByPassportNumberAsync("TEST789012");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("TEST789012", result.PassportNumber);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);
    }

    [Fact]
    public async Task EmployeeRepository_AddEmployee_ShouldWork()
    {
        // Arrange
        var employee = new Employee
        {
            EmployeeCode = "EMP001",
            FirstName = "Test",
            LastName = "Employee",
            Email = "employee@example.com",
            Role = EmployeeRole.CheckinAgent,
            IsActive = true
        };

        // Act
        var result = await _employeeRepository.AddAsync(employee);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("EMP001", result.EmployeeCode);
        Assert.Equal("Test", result.FirstName);
        Assert.Equal("Employee", result.LastName);
        Assert.Equal(EmployeeRole.CheckinAgent, result.Role);
    }

    [Fact]
    public async Task DatabaseContext_SaveChanges_ShouldWork()
    {
        // Arrange
        var flight = new Flight
        {
            FlightNumber = "TEST007",
            DepartureAirport = "UB",
            ArrivalAirport = "LAX",
            ScheduledDeparture = DateTime.UtcNow.AddHours(8),
            ScheduledArrival = DateTime.UtcNow.AddHours(12),
            Status = FlightStatus.Scheduled,
            GateNumber = "G8"
        };

        // Act
        _context.Flights.Add(flight);
        var changes = await _context.SaveChangesAsync();

        // Assert
        Assert.True(changes > 0);
        Assert.True(flight.Id > 0);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
