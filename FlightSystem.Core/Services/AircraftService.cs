using FlightSystem.Core.Interfaces.Services;
using FlightSystem.Core.Interfaces.Repositories;
using FlightSystem.Core.DTOs.Request;
using FlightSystem.Core.DTOs.Response;
using FlightSystem.Core.Models;
using FlightSystem.Core.Exceptions;
using Microsoft.Extensions.Logging;
using FlightSystem.Core.Enums;

namespace FlightSystem.Core.Services
{
    public class AircraftService : IAircraftService
    {
        private readonly IAircraftRepository _aircraftRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly ILogger<AircraftService> _logger;

        public AircraftService(
            IAircraftRepository aircraftRepository,
            ISeatRepository seatRepository,
            IFlightRepository flightRepository,
            ILogger<AircraftService> logger)
        {
            _aircraftRepository = aircraftRepository;
            _seatRepository = seatRepository;
            _flightRepository = flightRepository;
            _logger = logger;
        }

        public async Task<AircraftDto> CreateAircraftAsync(CreateAircraftDto createDto)
        {
            var existingAircraft = await _aircraftRepository.GetByCodeAsync(createDto.AircraftCode);
            if (existingAircraft != null)
                throw new FlightSystemException($"Онгоцны код {createDto.AircraftCode} аль хэдийн бүртгэгдсэн байна", "AIRCRAFT_CODE_EXISTS");

            var aircraft = new Aircraft
            {
                AircraftCode = createDto.AircraftCode,
                AircraftType = createDto.AircraftType,
                TotalSeats = createDto.TotalSeats,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            aircraft = await _aircraftRepository.AddAsync(aircraft);
            _logger.LogInformation("Онгоц {AircraftCode} амжилттай үүсгэгдлээ (ID: {AircraftId})", aircraft.AircraftCode, aircraft.Id);

            return MapToAircraftDto(aircraft);
        }

        public async Task<AircraftDto?> GetAircraftByIdAsync(int aircraftId)
        {
            var aircraft = await _aircraftRepository.GetByIdAsync(aircraftId);
            return aircraft != null ? MapToAircraftDto(aircraft) : null;
        }

        public async Task<IEnumerable<AircraftDto>> GetAllAircraftAsync()
        {
            var aircraft = await _aircraftRepository.GetAllAsync();
            return aircraft.Select(MapToAircraftDto);
        }

        public async Task<AircraftDto> UpdateAircraftAsync(int aircraftId, UpdateAircraftDto updateDto)
        {
            var aircraft = await _aircraftRepository.GetByIdAsync(aircraftId);
            if (aircraft == null)
                throw new FlightSystemException("Онгоц олдсонгүй", "AIRCRAFT_NOT_FOUND");

            if (updateDto.AircraftType != null)
                aircraft.AircraftType = updateDto.AircraftType;

            if (updateDto.TotalSeats.HasValue)
                aircraft.TotalSeats = updateDto.TotalSeats.Value;

            if (updateDto.IsActive.HasValue)
                aircraft.IsActive = updateDto.IsActive.Value;

            aircraft.UpdatedAt = DateTime.UtcNow;
            aircraft = await _aircraftRepository.UpdateAsync(aircraft);

            _logger.LogInformation("Онгоц {AircraftCode} амжилттай шинэчлэгдлээ (ID: {AircraftId})", aircraft.AircraftCode, aircraftId);
            return MapToAircraftDto(aircraft);
        }

        public async Task<bool> DeleteAircraftAsync(int aircraftId)
        {
            var aircraft = await _aircraftRepository.GetByIdAsync(aircraftId);
            if (aircraft == null)
                return false;

            var flights = await _flightRepository.GetFlightsByAircraftAsync(aircraftId);
            var activeFlights = flights.Where(f => f.Status != FlightStatus.Cancelled && f.Status != FlightStatus.Departed);

            if (activeFlights.Any())
                throw new FlightSystemException("Идэвхтэй нислэгтэй онгоцыг устгаж болохгүй", "CANNOT_DELETE_AIRCRAFT");

            // Soft delete - mark as inactive
            aircraft.IsActive = false;
            aircraft.UpdatedAt = DateTime.UtcNow;
            await _aircraftRepository.UpdateAsync(aircraft);

            _logger.LogInformation("Онгоц {AircraftCode} идэвхгүй болж тэмдэглэгдлээ (ID: {AircraftId})", aircraft.AircraftCode, aircraftId);
            return true;
        }

        public async Task<AircraftDto?> GetByCodeAsync(string aircraftCode)
        {
            var aircraft = await _aircraftRepository.GetByCodeAsync(aircraftCode);
            return aircraft != null ? MapToAircraftDto(aircraft) : null;
        }

        public async Task<IEnumerable<AircraftDto>> GetActiveAircraftAsync()
        {
            var aircraft = await _aircraftRepository.GetActiveAircraftAsync();
            return aircraft.Select(MapToAircraftDto);
        }

        public async Task<AircraftDetailDto?> GetAircraftWithSeatsAsync(int aircraftId)
        {
            var aircraft = await _aircraftRepository.GetAircraftWithSeatsAsync(aircraftId);
            if (aircraft == null)
                return null;

            var flights = await _flightRepository.GetFlightsByAircraftAsync(aircraftId);
            var activeFlights = flights.Count(f => f.Status != FlightStatus.Cancelled && f.Status != FlightStatus.Departed);

            var detailDto = new AircraftDetailDto
            {
                Id = aircraft.Id,
                AircraftCode = aircraft.AircraftCode,
                AircraftType = aircraft.AircraftType,
                TotalSeats = aircraft.TotalSeats,
                IsActive = aircraft.IsActive,
                CreatedAt = aircraft.CreatedAt,
                ActiveFlights = activeFlights,
                Seats = aircraft.Seats?.Select(MapToSeatDto).ToList() ?? new List<SeatDto>(),
                SeatClassDistribution = aircraft.Seats?.GroupBy(s => s.Class.ToString())
                    .ToDictionary(g => g.Key, g => g.Count()) ?? new Dictionary<string, int>()
            };

            return detailDto;
        }

        private AircraftDto MapToAircraftDto(Aircraft aircraft)
        {
            return new AircraftDto
            {
                Id = aircraft.Id,
                AircraftCode = aircraft.AircraftCode,
                AircraftType = aircraft.AircraftType,
                TotalSeats = aircraft.TotalSeats,
                IsActive = aircraft.IsActive,
                CreatedAt = aircraft.CreatedAt
            };
        }

        private SeatDto MapToSeatDto(Seat seat)
        {
            return new SeatDto
            {
                Id = seat.Id,
                SeatNumber = seat.SeatNumber,
                Row = seat.Row,
                Column = seat.Column,
                SeatClass = seat.Class.ToString(),
                IsWindowSeat = seat.IsWindowSeat,
                IsAisleSeat = seat.IsAisleSeat,
                IsEmergencyExit = seat.IsEmergencyExit,
                IsActive = seat.IsActive,
                AircraftCode = seat.Aircraft?.AircraftCode ?? "Тодорхойгүй"
            };
        }
    }
}
