using FlightSystem.Core.Models;
using FlightSystem.Core.Enums;
using FlightSystem.Data.Context;

namespace FlightSystem.Data.Seed;

public static class DataSeeder
{
    public static async Task SeedDataAsync(FlightSystemDbContext context)
    {
                if (!context.Employees.Any())
        {
            var employees = new[]
            {
                new Employee
                {
                    EmployeeCode = "ADM001",
                    FirstName = "Батбаяр",
                    LastName = "Доржийн",
                    Email = "admin@flightsystem.mn",
                    Password = "admin123",                     Role = EmployeeRole.Admin,
                    WorkStationId = "DESK01",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Employee
                {
                    EmployeeCode = "CHK001",
                    FirstName = "Энхтуяа",
                    LastName = "Баатарын",
                    Email = "checkin1@flightsystem.mn",
                    Password = "checkin123",
                    Role = EmployeeRole.CheckinAgent,
                    WorkStationId = "DESK02",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Employee
                {
                    EmployeeCode = "CHK002",
                    FirstName = "Сайханбаяр",
                    LastName = "Мөнхбаярын",
                    Email = "checkin2@flightsystem.mn",
                    Password = "checkin456",
                    Role = EmployeeRole.CheckinAgent,
                    WorkStationId = "DESK03",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Employee
                {
                    EmployeeCode = "SUP001",
                    FirstName = "Цэцэгмаа",
                    LastName = "Батбаярын",
                    Email = "supervisor@flightsystem.mn",
                    Password = "super123",
                    Role = EmployeeRole.Supervisor,
                    WorkStationId = "DESK04",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            await context.Employees.AddRangeAsync(employees);
        }

                if (!context.Aircraft.Any())
        {
            var aircraft = new[]
            {
                new Aircraft
                {
                    AircraftCode = "MN-ABC",
                    AircraftType = "Boeing 737-800",
                    TotalSeats = 180,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Aircraft
                {
                    AircraftCode = "MN-DEF",
                    AircraftType = "Airbus A320",
                    TotalSeats = 150,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Aircraft
                {
                    AircraftCode = "MN-GHI",
                    AircraftType = "Boeing 737-800",
                    TotalSeats = 180,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            await context.Aircraft.AddRangeAsync(aircraft);
            await context.SaveChangesAsync();

                        await SeedSeatsForAircraft(context, aircraft);
        }

                if (!context.Passengers.Any())
        {
            var passengers = new[]
            {
                new Passenger
                {
                    PassportNumber = "MP12345678",
                    FirstName = "Батбаяр",
                    LastName = "Доржийн",
                    Nationality = "Монгол",
                    DateOfBirth = new DateTime(1990, 5, 15),
                    Type = PassengerType.Adult,
                    Email = "batbayar@example.com",
                    Phone = "99112233",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Passenger
                {
                    PassportNumber = "MP87654321",
                    FirstName = "Энхтуяа",
                    LastName = "Баатарын",
                    Nationality = "Монгол",
                    DateOfBirth = new DateTime(1985, 8, 22),
                    Type = PassengerType.Adult,
                    Email = "enkhtuya@example.com",
                    Phone = "99223344",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Passenger
                {
                    PassportNumber = "MP11223344",
                    FirstName = "Сайханбаяр",
                    LastName = "Мөнхбаярын",
                    Nationality = "Монгол",
                    DateOfBirth = new DateTime(1992, 3, 10),
                    Type = PassengerType.Adult,
                    Email = "saihanbayar@example.com",
                    Phone = "99334455",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            await context.Passengers.AddRangeAsync(passengers);
        }

                if (!context.Flights.Any())
        {
            var now = DateTime.UtcNow;
            var flights = new[]
            {
                new Flight
                {
                    FlightNumber = "MN123",
                    AircraftId = 1,                     DepartureAirport = "ULN",
                    ArrivalAirport = "PEK",
                    ScheduledDeparture = now.AddHours(2),
                    ScheduledArrival = now.AddHours(4),
                    Status = FlightStatus.CheckinOpen,
                    GateNumber = "A5",
                    CheckinOpenTime = now.AddHours(-1),
                    CheckinCloseTime = now.AddMinutes(75),
                    BoardingTime = now.AddMinutes(90),
                    CreatedByEmployeeId = 1,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Flight
                {
                    FlightNumber = "MN456",
                    AircraftId = 2,                     DepartureAirport = "ULN",
                    ArrivalAirport = "ICN",
                    ScheduledDeparture = now.AddHours(6),
                    ScheduledArrival = now.AddHours(8),
                    Status = FlightStatus.Scheduled,
                    GateNumber = "B3",
                    CheckinOpenTime = now.AddHours(3),
                    CheckinCloseTime = now.AddHours(5).AddMinutes(45),
                    BoardingTime = now.AddHours(5).AddMinutes(30),
                    CreatedByEmployeeId = 1,
                    CreatedAt = now,
                    UpdatedAt = now
                }
            };

            await context.Flights.AddRangeAsync(flights);
        }

        await context.SaveChangesAsync();
    }

    private static async Task SeedSeatsForAircraft(FlightSystemDbContext context, Aircraft[] aircraft)
    {
        var seats = new List<Seat>();

        foreach (var aircraftItem in aircraft)
        {
            var aircraftId = aircraftItem.Id;
            var totalRows = aircraftItem.AircraftType.Contains("737") ? 30 : 25;             var seatsPerRow = aircraftItem.AircraftType.Contains("737") ? 6 : 6; 
            for (int row = 1; row <= totalRows; row++)
            {
                for (char col = 'A'; col <= 'F'; col++)
                {
                    var seatClass = row <= 5 ? SeatClass.Business : SeatClass.Economy;
                    var isWindow = col == 'A' || col == 'F';
                    var isAisle = col == 'C' || col == 'D';
                    var isEmergencyExit = (aircraftItem.AircraftType.Contains("737") && (row == 12 || row == 13)) ||
                                        (aircraftItem.AircraftType.Contains("A320") && (row == 10 || row == 11));

                    seats.Add(new Seat
                    {
                        AircraftId = aircraftId,
                        SeatNumber = $"{row}{col}",
                        Row = row.ToString(),
                        Column = col.ToString(),
                        Class = seatClass,
                        IsWindowSeat = isWindow,
                        IsAisleSeat = isAisle,
                        IsEmergencyExit = isEmergencyExit,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    });
                }
            }
        }

        await context.Seats.AddRangeAsync(seats);
    }
}
