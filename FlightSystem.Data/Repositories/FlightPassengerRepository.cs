using Microsoft.EntityFrameworkCore;
using FlightSystem.Core.Interfaces.Repositories;
using FlightSystem.Core.Models;
using FlightSystem.Data.Context;

namespace FlightSystem.Data.Repositories;

public class FlightPassengerRepository(FlightSystemDbContext context) : Repository<FlightPassenger>(context), IFlightPassengerRepository
{
    public async Task<FlightPassenger?> GetFlightPassengerAsync(int flightId, int passengerId)
    {
        return await _dbSet
            .Include(fp => fp.Flight)
            .Include(fp => fp.Passenger)
            .Include(fp => fp.CheckinByEmployee)
            .FirstOrDefaultAsync(fp => fp.FlightId == flightId && fp.PassengerId == passengerId);
    }

    public async Task<IEnumerable<FlightPassenger>> GetPassengersForFlightAsync(int flightId)
    {
        return await _dbSet
            .Include(fp => fp.Passenger)
            .Include(fp => fp.CheckinByEmployee)
            .Where(fp => fp.FlightId == flightId)
            .OrderBy(fp => fp.Passenger!.LastName)
            .ThenBy(fp => fp.Passenger!.FirstName)
            .ToListAsync();
    }

    public async Task<IEnumerable<FlightPassenger>> GetCheckedInPassengersAsync(int flightId)
    {
        return await _dbSet
            .Include(fp => fp.Passenger)
            .Include(fp => fp.CheckinByEmployee)
            .Where(fp => fp.FlightId == flightId && fp.IsCheckedIn)
            .OrderBy(fp => fp.CheckinTime)
            .ToListAsync();
    }

    public async Task<FlightPassenger?> CheckinPassengerAsync(int flightPassengerId, int employeeId)
    {
        var flightPassenger = await GetByIdAsync(flightPassengerId);
        if (flightPassenger == null)
            throw new ArgumentException($"FlightPassenger with ID {flightPassengerId} not found");

        flightPassenger.IsCheckedIn = true;
        flightPassenger.CheckinTime = DateTime.UtcNow;
        flightPassenger.CheckinByEmployeeId = employeeId;
        flightPassenger.UpdatedAt = DateTime.UtcNow;

        await SaveChangesAsync();
        return flightPassenger;
    }

    public async Task<bool> CancelCheckinAsync(int flightPassengerId)
    {
        var flightPassenger = await GetByIdAsync(flightPassengerId);
        if (flightPassenger == null) return false;

        flightPassenger.IsCheckedIn = false;
        flightPassenger.CheckinTime = null;
        flightPassenger.CheckinByEmployeeId = null;
        flightPassenger.UpdatedAt = DateTime.UtcNow;

        await SaveChangesAsync();
        return true;
    }

    public async Task<FlightPassenger?> GetByBookingReferenceAsync(string bookingReference)
    {
        return await _dbSet
            .Include(fp => fp.Flight)
            .Include(fp => fp.Passenger)
            .FirstOrDefaultAsync(fp => fp.BookingReference == bookingReference);
    }

    public async Task<IEnumerable<FlightPassenger>> GetFlightsForPassengerAsync(int passengerId)
    {
        return await _dbSet
            .Include(fp => fp.Flight)
            .Include(fp => fp.Passenger)
            .Where(fp => fp.PassengerId == passengerId)
            .OrderBy(fp => fp.Flight!.ScheduledDeparture)
            .ToListAsync();
    }

    public async Task<bool> IsPassengerCheckedInAsync(int flightId, int passengerId)
    {
        return await _dbSet
            .AnyAsync(fp => fp.FlightId == flightId && fp.PassengerId == passengerId && fp.IsCheckedIn);
    }
}
