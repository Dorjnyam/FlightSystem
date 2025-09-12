using Microsoft.EntityFrameworkCore;
using FlightSystem.Shared.Interfaces.Repositories;
using FlightSystem.Core.Models;
using FlightSystem.Data.Context;

namespace FlightSystem.Data.Repositories;

public class BoardingPassRepository(FlightSystemDbContext context) : Repository<BoardingPass>(context), IBoardingPassRepository
{
    public async Task<BoardingPass?> GetByBoardingPassCodeAsync(string boardingPassCode)
    {
        return await _dbSet
            .Include(bp => bp.FlightPassenger)
                .ThenInclude(fp => fp!.Flight)
            .Include(bp => bp.FlightPassenger)
                .ThenInclude(fp => fp!.Passenger)
            .Include(bp => bp.IssuedByEmployee)
            .FirstOrDefaultAsync(bp => bp.BoardingPassCode == boardingPassCode);
    }

    public async Task<IEnumerable<BoardingPass>> GetBoardingPassesForFlightAsync(int flightId)
    {
        return await _dbSet
            .Include(bp => bp.FlightPassenger)
                .ThenInclude(fp => fp!.Passenger)
            .Include(bp => bp.IssuedByEmployee)
            .Where(bp => bp.FlightPassenger!.FlightId == flightId)
            .OrderBy(bp => bp.IssuedAt)
            .ToListAsync();
    }

    public async Task<BoardingPass> GenerateBoardingPassAsync(BoardingPass boardingPass)
    {
        var entry = await _dbSet.AddAsync(boardingPass);
        await SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<bool> ProcessBoardingAsync(string boardingPassCode, int employeeId)
    {
        var boardingPass = await GetByBoardingPassCodeAsync(boardingPassCode);
        if (boardingPass == null) return false;

        boardingPass.IsBoardingComplete = true;
        boardingPass.BoardingTime = DateTime.UtcNow;
        boardingPass.BoardingByEmployeeId = employeeId;
        boardingPass.UpdatedAt = DateTime.UtcNow;

        await SaveChangesAsync();
        return true;
    }

    public async Task<bool> ValidateBoardingPassAsync(string boardingPassCode)
    {
        var boardingPass = await GetByBoardingPassCodeAsync(boardingPassCode);
        return boardingPass != null && !boardingPass.IsBoardingComplete;
    }

    public async Task<IEnumerable<BoardingPass>> GetBoardingPassesByDateAsync(DateTime date)
    {
        var startDate = date.Date;
        var endDate = startDate.AddDays(1);

        return await _dbSet
            .Include(bp => bp.FlightPassenger)
                .ThenInclude(fp => fp!.Flight)
            .Include(bp => bp.FlightPassenger)
                .ThenInclude(fp => fp!.Passenger)
            .Where(bp => bp.IssuedAt >= startDate && bp.IssuedAt < endDate)
            .OrderBy(bp => bp.IssuedAt)
            .ToListAsync();
    }

    public async Task<BoardingPass?> GetByCodeAsync(string boardingPassCode)
    {
        return await _dbSet
            .Include(bp => bp.FlightPassenger)
                .ThenInclude(fp => fp!.Flight)
            .Include(bp => bp.FlightPassenger)
                .ThenInclude(fp => fp!.Passenger)
            .Include(bp => bp.IssuedByEmployee)
            .FirstOrDefaultAsync(bp => bp.BoardingPassCode == boardingPassCode);
    }

    public async Task<BoardingPass?> GetByFlightPassengerAsync(int flightPassengerId)
    {
        return await _dbSet
            .Include(bp => bp.FlightPassenger)
                .ThenInclude(fp => fp!.Flight)
            .Include(bp => bp.IssuedByEmployee)
            .FirstOrDefaultAsync(bp => bp.FlightPassengerId == flightPassengerId);
    }

    public async Task<BoardingPass?> UpdateBoardingStatusAsync(int boardingPassId, bool isBoardingComplete, DateTime? boardingTime = null)
    {
        var boardingPass = await GetByIdAsync(boardingPassId);
        if (boardingPass == null) return null;

        boardingPass.IsBoardingComplete = isBoardingComplete;
        if (boardingTime.HasValue)
            boardingPass.BoardingTime = boardingTime.Value;
        else if (isBoardingComplete)
            boardingPass.BoardingTime = DateTime.UtcNow;

        boardingPass.UpdatedAt = DateTime.UtcNow;
        await SaveChangesAsync();

        return boardingPass;
    }

    public async Task<IEnumerable<BoardingPass>> GetBoardingPassesByEmployeeAsync(int employeeId)
    {
        return await _dbSet
            .Include(bp => bp.FlightPassenger)
                .ThenInclude(fp => fp!.Flight)
            .Include(bp => bp.FlightPassenger)
                .ThenInclude(fp => fp!.Passenger)
            .Where(bp => bp.IssuedByEmployeeId == employeeId)
            .OrderBy(bp => bp.IssuedAt)
            .ToListAsync();
    }

    public async Task<int> GetBoardedCountAsync(int flightId)
    {
        return await _dbSet
            .CountAsync(bp => bp.FlightPassenger!.FlightId == flightId && bp.IsBoardingComplete);
    }
}
