using FlightSystem.Shared.Interfaces.Services;
using FlightSystem.Shared.Interfaces.Repositories;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Core.Models;
using FlightSystem.Core.Enums;
using FlightSystem.Core.Exceptions;
using Microsoft.Extensions.Logging;
using FlightSystem.Core.Extensions;

namespace FlightSystem.Shared.Services
{
    public class SeatService : ISeatService
    {
        private readonly ISeatRepository _seatRepository;
        private readonly ISeatAssignmentRepository _seatAssignmentRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly ILogger<SeatService> _logger;

        public SeatService(
            ISeatRepository seatRepository,
            ISeatAssignmentRepository seatAssignmentRepository,
            IFlightRepository flightRepository,
            ILogger<SeatService> logger)
        {
            _seatRepository = seatRepository;
            _seatAssignmentRepository = seatAssignmentRepository;
            _flightRepository = flightRepository;
            _logger = logger;
        }

        public async Task<SeatDto> CreateSeatAsync(CreateSeatDto createDto)
        {
            var existingSeat = await _seatRepository.GetSeatByNumberAsync(createDto.AircraftId, createDto.SeatNumber);
            if (existingSeat != null)
                throw new FlightSystemException($"Энэ суудал {createDto.SeatNumber} аль хэдийн бүртгэлтэй байна", "SEAT_EXISTS");

            var seat = new Seat
            {
                AircraftId = createDto.AircraftId,
                SeatNumber = createDto.SeatNumber,
                Row = createDto.Row,
                Column = createDto.Column,
                Class = createDto.Class,
                IsWindowSeat = createDto.IsWindowSeat,
                IsAisleSeat = createDto.IsAisleSeat,
                IsEmergencyExit = createDto.IsEmergencyExit,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            seat = await _seatRepository.AddAsync(seat);
            _logger.LogInformation("Суудал {SeatNumber} амжилттай бүртгэгдлээ. Онгоц: {AircraftId}", seat.SeatNumber, createDto.AircraftId);

            return MapToSeatDto(seat);
        }

        public async Task<SeatDto?> GetSeatByIdAsync(int seatId)
        {
            var seat = await _seatRepository.GetByIdAsync(seatId);
            return seat != null ? MapToSeatDto(seat) : null;
        }

        public async Task<IEnumerable<SeatDto>> GetAllSeatsAsync()
        {
            var seats = await _seatRepository.GetAllAsync();
            return seats.Select(MapToSeatDto);
        }

        public async Task<SeatDto> UpdateSeatAsync(int seatId, UpdateSeatDto updateDto)
        {
            var seat = await _seatRepository.GetByIdAsync(seatId);
            if (seat == null)
                throw new FlightSystemException("Суудал олдсонгүй", "SEAT_NOT_FOUND");

            if (updateDto.Class.HasValue)
                seat.Class = updateDto.Class.Value;

            if (updateDto.IsActive.HasValue)
                seat.IsActive = updateDto.IsActive.Value;

            if (updateDto.IsEmergencyExit.HasValue)
                seat.IsEmergencyExit = updateDto.IsEmergencyExit.Value;

            seat.UpdatedAt = DateTime.UtcNow;
            seat = await _seatRepository.UpdateAsync(seat);

            _logger.LogInformation("Суудал {SeatNumber} шинэчлэгдлээ. ID: {SeatId}", seat.SeatNumber, seatId);
            return MapToSeatDto(seat);
        }

        public async Task<bool> DeleteSeatAsync(int seatId)
        {
            var seat = await _seatRepository.GetByIdAsync(seatId);
            if (seat == null)
                return false;

            var assignments = await _seatAssignmentRepository.GetAssignmentsForFlightAsync(0);
            if (assignments.Any(a => a.SeatId == seatId && a.IsActive))
                throw new FlightSystemException("Идэвхтэй суудлын даалгавартай суудлыг устгаж болохгүй", "CANNOT_DELETE_SEAT");

            await _seatRepository.DeleteAsync(seatId);
            _logger.LogInformation("Суудал {SeatNumber} амжилттай устгагдлаа. ID: {SeatId}", seat.SeatNumber, seatId);
            return true;
        }

        public async Task<IEnumerable<SeatInfoDto>> GetSeatsByFlightAsync(int flightId)
        {
            var flight = await _flightRepository.GetFlightWithDetailsAsync(flightId);
            if (flight == null)
                throw new FlightNotFoundException($"Нислэг олдсонгүй. ID: {flightId}");

            var seats = await _seatRepository.GetSeatsByAircraftAsync(flight.AircraftId);
            var assignments = await _seatAssignmentRepository.GetAssignmentsForFlightAsync(flightId);

            return seats.Select(seat =>
            {
                var assignment = assignments.FirstOrDefault(a => a.SeatId == seat.Id && a.IsActive);
                return new SeatInfoDto
                {
                    Id = seat.Id,
                    SeatNumber = seat.SeatNumber,
                    Row = seat.Row,
                    Column = seat.Column,
                    SeatClass = seat.Class.ToString(),
                    IsAvailable = assignment == null,
                    IsWindowSeat = seat.IsWindowSeat,
                    IsAisleSeat = seat.IsAisleSeat,
                    IsEmergencyExit = seat.IsEmergencyExit,
                    PassengerName = assignment?.FlightPassenger?.Passenger?.GetFullName(),
                    PassengerPassport = assignment?.FlightPassenger?.Passenger?.PassportNumber
                };
            });
        }

        public async Task<IEnumerable<SeatInfoDto>> GetAvailableSeatsAsync(int flightId, SeatClass? seatClass = null)
        {
            var flight = await _flightRepository.GetFlightWithDetailsAsync(flightId);
            if (flight == null)
                throw new FlightNotFoundException($"Нислэг олдсонгүй. ID: {flightId}");

            var availableSeats = await _seatRepository.GetAvailableSeatsForFlightAsync(flightId);

            if (seatClass.HasValue)
                availableSeats = availableSeats.Where(s => s.Class == seatClass.Value);

            return availableSeats.Select(MapToSeatInfoDto);
        }

        public async Task<SeatInfoDto?> GetSeatInfoAsync(int aircraftId, string seatNumber)
        {
            var seat = await _seatRepository.GetSeatByNumberAsync(aircraftId, seatNumber);
            return seat != null ? MapToSeatInfoDto(seat) : null;
        }

        public async Task<SeatAssignmentDto?> ReserveSeatAsync(ReserveSeatRequestDto request)
        {
            using var semaphore = new SemaphoreSlim(1, 1);
            await semaphore.WaitAsync();

            try
            {
                var isAvailable = await _seatAssignmentRepository.IsSeatAssignedAsync(request.SeatId, request.FlightId);
                if (isAvailable)
                    throw new SeatNotAvailableException($"Суудал ID {request.SeatId} ашиглагдсан байна");

                var seatAssignment = new SeatAssignment
                {
                    FlightPassengerId = request.PassengerId,
                    SeatId = request.SeatId,
                    AssignedAt = DateTime.UtcNow,
                    AssignedByEmployeeId = request.EmployeeId,
                    IsActive = true
                };

                seatAssignment = await _seatAssignmentRepository.AssignSeatAsync(seatAssignment);
                _logger.LogInformation("Суудал {SeatId} амжилттай захиалагдлаа. Иргэн: {PassengerId}", request.SeatId, request.PassengerId);

                return MapToSeatAssignmentDto(seatAssignment);
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task<bool> ReleaseSeatAsync(int seatAssignmentId, int employeeId)
        {
            var result = await _seatAssignmentRepository.ReleaseSeatAsync(seatAssignmentId);
            if (result)
                _logger.LogInformation("Суудлын даалгавар {SeatAssignmentId} чөлөөлөгдлөө. Ажилтан: {EmployeeId}", seatAssignmentId, employeeId);

            return result;
        }

        public async Task<SeatMapDto> GenerateSeatMapAsync(int flightId)
        {
            var flight = await _flightRepository.GetFlightWithDetailsAsync(flightId);
            if (flight == null)
                throw new FlightNotFoundException($"Нислэг олдсонгүй. ID: {flightId}");

            var seats = await _seatRepository.GetSeatsByAircraftAsync(flight.AircraftId);
            var assignments = await _seatAssignmentRepository.GetAssignmentsForFlightAsync(flightId);

            var seatMap = new SeatMapDto
            {
                FlightId = flightId,
                FlightNumber = flight.FlightNumber,
                AircraftType = flight.Aircraft?.AircraftType ?? "Unknown",
                TotalSeats = seats.Count(),
                AvailableSeats = seats.Count() - assignments.Count()
            };

            var seatRows = seats.GroupBy(s => s.Row)
                .OrderBy(g => int.Parse(g.Key))
                .Select(g => new SeatRowDto
                {
                    Row = g.Key,
                    Seats = g.OrderBy(s => s.Column)
                        .Select(seat =>
                        {
                            var assignment = assignments.FirstOrDefault(a => a.SeatId == seat.Id && a.IsActive);
                            return new SeatInfoDto
                            {
                                Id = seat.Id,
                                SeatNumber = seat.SeatNumber,
                                Row = seat.Row,
                                Column = seat.Column,
                                SeatClass = seat.Class.ToString(),
                                IsAvailable = assignment == null,
                                IsWindowSeat = seat.IsWindowSeat,
                                IsAisleSeat = seat.IsAisleSeat,
                                IsEmergencyExit = seat.IsEmergencyExit,
                                PassengerName = assignment?.FlightPassenger?.Passenger?.GetFullName(),
                                PassengerPassport = assignment?.FlightPassenger?.Passenger?.PassportNumber
                            };
                        }).ToList()
                }).ToList();

            seatMap.SeatRows = seatRows;

            seatMap.SeatClassCounts = seats.GroupBy(s => s.Class.ToString())
                .ToDictionary(g => g.Key, g => g.Count());

            return seatMap;
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
                AircraftCode = seat.Aircraft?.AircraftCode ?? "Unknown"
            };
        }

        private SeatInfoDto MapToSeatInfoDto(Seat seat)
        {
            return new SeatInfoDto
            {
                Id = seat.Id,
                SeatNumber = seat.SeatNumber,
                Row = seat.Row,
                Column = seat.Column,
                SeatClass = seat.Class.ToString(),
                IsAvailable = true,
                IsWindowSeat = seat.IsWindowSeat,
                IsAisleSeat = seat.IsAisleSeat,
                IsEmergencyExit = seat.IsEmergencyExit
            };
        }

        private SeatAssignmentDto MapToSeatAssignmentDto(SeatAssignment seatAssignment)
        {
            return new SeatAssignmentDto
            {
                Id = seatAssignment.Id,
                AssignedAt = seatAssignment.AssignedAt,
                IsActive = seatAssignment.IsActive,
                Notes = seatAssignment.Notes,
                AssignedByEmployee = seatAssignment.AssignedByEmployee?.GetFullName() ?? "Unknown"
            };
        }
    }
}
