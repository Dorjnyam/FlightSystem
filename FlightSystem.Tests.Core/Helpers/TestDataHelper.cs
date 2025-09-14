using FlightSystem.Core.Models;
using FlightSystem.Core.Enums;
using FlightSystem.Shared.DTOs.Request;

namespace FlightSystem.Tests.Core.Helpers;

public static class TestDataHelper
{
    public static Aircraft CreateTestAircraft()
    {
        return new Aircraft
        {
            Id = 1,
            AircraftCode = "MN-TEST",
            AircraftType = "Boeing 737-800",
            TotalSeats = 180,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static Seat CreateTestSeat(int aircraftId = 1)
    {
        return new Seat
        {
            Id = 1,
            AircraftId = aircraftId,
            SeatNumber = "12A",
            Row = "12",
            Column = "A",
            Class = SeatClass.Economy,
            IsWindowSeat = true,
            IsAisleSeat = false,
            IsEmergencyExit = false,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static FlightPassenger CreateTestFlightPassenger(int flightId = 1, int passengerId = 1)
    {
        return new FlightPassenger
        {
            Id = 1,
            FlightId = flightId,
            PassengerId = passengerId,
            BookingReference = "TEST123",
            IsCheckedIn = false,
            SpecialRequests = "Цонхны дэргэд суух",
            BaggageInfo = "1 том цүнх",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static SeatAssignment CreateTestSeatAssignment(int flightPassengerId = 1, int seatId = 1, int employeeId = 1)
    {
        return new SeatAssignment
        {
            Id = 1,
            FlightPassengerId = flightPassengerId,
            SeatId = seatId,
            AssignedAt = DateTime.UtcNow,
            AssignedByEmployeeId = employeeId,
            IsActive = true,
            Notes = "Тест суудлын оноолт"
        };
    }

    public static CheckinRequestDto CreateValidCheckinRequest()
    {
        return new CheckinRequestDto
        {
            FlightNumber = "MN123",
            PassportNumber = "MP12345678",
            EmployeeId = 1,
            PreferredSeatId = 1,
            SpecialRequests = "Цонхны дэргэд суух",
            BaggageInfo = "1 том цүнх"
        };
    }

    public static CreatePassengerDto CreateValidPassengerDto()
    {
        return new CreatePassengerDto
        {
            PassportNumber = "MP12345678",
            FirstName = "Батбаяр",
            LastName = "Доржийн",
            Nationality = "Монгол",
            DateOfBirth = new DateTime(1990, 1, 1),
            Type = PassengerType.Adult,
            Email = "test@example.com",
            Phone = "99112233"
        };
    }

    public static CreateFlightDto CreateValidFlightDto()
    {
        var now = DateTime.UtcNow;
        return new CreateFlightDto
        {
            FlightNumber = "MN456",
            AircraftId = 1,
            DepartureAirport = "ULN",
            ArrivalAirport = "ICN",
            ScheduledDeparture = now.AddHours(4),
            ScheduledArrival = now.AddHours(6),
            GateNumber = "A5",
            CheckinOpenTime = now.AddHours(1),
            CheckinCloseTime = now.AddHours(3).AddMinutes(45),
            BoardingTime = now.AddHours(3).AddMinutes(30),
            CreatedByEmployeeId = 1
        };
    }

    public static Flight CreateTestFlight(int aircraftId = 1, int employeeId = 1)
    {
        var now = DateTime.UtcNow;
        return new Flight
        {
            Id = 1,
            FlightNumber = "MN123",
            AircraftId = aircraftId,
            DepartureAirport = "ULN",
            ArrivalAirport = "PEK",
            ScheduledDeparture = now.AddHours(2),
            ScheduledArrival = now.AddHours(4),
            Status = FlightStatus.CheckinOpen,
            GateNumber = "A5",
            CheckinOpenTime = now.AddHours(-2), // Check-in opened 2 hours ago
            CheckinCloseTime = now.AddHours(1),  // Check-in closes in 1 hour
            BoardingTime = now.AddMinutes(90),
            CreatedByEmployeeId = employeeId,
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    public static Passenger CreateTestPassenger()
    {
        return new Passenger
        {
            Id = 1,
            PassportNumber = "MP12345678",
            FirstName = "Батбаяр",
            LastName = "Доржийн",
            Nationality = "Монгол",
            DateOfBirth = new DateTime(1990, 1, 1),
            Type = PassengerType.Adult,
            Email = "test@example.com",
            Phone = "99112233",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static Employee CreateTestEmployee()
    {
        return new Employee
        {
            Id = 1,
            EmployeeCode = "CHK001",
            FirstName = "Энхтуяа",
            LastName = "Баатарын",
            Email = "checkin@test.com",
            Password = "test123",
            Role = EmployeeRole.CheckinAgent,
            WorkStationId = "DESK01",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
