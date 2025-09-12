using FlightSystem.Core.Interfaces.Services;
using FlightSystem.Core.Interfaces.Repositories;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Core.Models;
using FlightSystem.Shared.Enums;
using FlightSystem.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace FlightSystem.Core.Services
{
    public class CheckinService : ICheckinService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IFlightPassengerRepository _flightPassengerRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly ISeatAssignmentRepository _seatAssignmentRepository;
        private readonly IBoardingPassRepository _boardingPassRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly INotificationService _notificationService;
        private readonly ILogger<CheckinService> _logger;

        public CheckinService(
            IFlightRepository flightRepository,
            IPassengerRepository passengerRepository,
            IFlightPassengerRepository flightPassengerRepository,
            ISeatRepository seatRepository,
            ISeatAssignmentRepository seatAssignmentRepository,
            IBoardingPassRepository boardingPassRepository,
            IEmployeeRepository employeeRepository,
            INotificationService notificationService,
            ILogger<CheckinService> logger)
        {
            _flightRepository = flightRepository;
            _passengerRepository = passengerRepository;
            _flightPassengerRepository = flightPassengerRepository;
            _seatRepository = seatRepository;
            _seatAssignmentRepository = seatAssignmentRepository;
            _boardingPassRepository = boardingPassRepository;
            _employeeRepository = employeeRepository;
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task<CheckinResultDto> CheckinPassengerAsync(CheckinRequestDto request)
        {
            _logger.LogInformation("Зорчигч {PassportNumber}-ийн check-in процесс эхэллээ, нислэг {FlightNumber}",
                request.PassportNumber, request.FlightNumber);

            try
            {
                var flight = await _flightRepository.GetByFlightNumberAsync(request.FlightNumber);
                if (flight == null)
                    throw new FlightNotFoundException($"Нислэг {request.FlightNumber} олдсонгүй");

                var passenger = await _passengerRepository.GetByPassportNumberAsync(request.PassportNumber);
                if (passenger == null)
                    throw new PassengerNotFoundException($"Зорчигч {request.PassportNumber} олдсонгүй");

                var flightPassenger = await _flightPassengerRepository
                    .GetFlightPassengerAsync(flight.Id, passenger.Id);
                if (flightPassenger == null)
                    throw new FlightSystemException("Зорчигч энэ нислэгт бүртгэлгүй байна", "PASSENGER_NOT_BOOKED");

                var eligibility = await ValidateCheckinEligibilityAsync(flight.Id, request.PassportNumber);
                if (!eligibility.IsEligible)
                    throw new CheckinNotAllowedException(eligibility.Reason);

                flightPassenger = await _flightPassengerRepository
                    .CheckinPassengerAsync(flightPassenger.Id, request.EmployeeId);

                SeatAssignmentDto? seatAssignment = null;
                if (request.PreferredSeatId.HasValue)
                {
                    var assignSeatRequest = new AssignSeatRequestDto
                    {
                        FlightPassengerId = flightPassenger.Id,
                        SeatId = request.PreferredSeatId.Value,
                        EmployeeId = request.EmployeeId
                    };

                    seatAssignment = await AssignSeatAsync(assignSeatRequest);
                }

                BoardingPassDto? boardingPass = null;
                if (seatAssignment != null)
                {
                    var boardingPassRequest = new GenerateBoardingPassRequestDto
                    {
                        FlightPassengerId = flightPassenger.Id,
                        SeatAssignmentId = seatAssignment.Id,
                        EmployeeId = request.EmployeeId,
                        Gate = flight.GateNumber
                    };

                    boardingPass = await GenerateBoardingPassAsync(boardingPassRequest);
                }

                await _notificationService.NotifyCheckinCompleteAsync(flight.Id,
                    $"{passenger.FirstName} {passenger.LastName}");

                _logger.LogInformation("Зорчигч {PassportNumber}-ийн check-in амжилттай дууслаа",
                    request.PassportNumber);

                return new CheckinResultDto
                {
                    IsSuccess = true,
                    Message = "Check-in амжилттай дууслаа",
                    FlightPassenger = MapToFlightPassengerDto(flightPassenger),
                    SeatAssignment = seatAssignment,
                    BoardingPass = boardingPass
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Зорчигч {PassportNumber}-ийн check-in процесс амжилтгүй боллоо",
                    request.PassportNumber);

                return new CheckinResultDto
                {
                    IsSuccess = false,
                    Message = "Check-in амжилтгүй боллоо",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<SeatAssignmentDto> AssignSeatAsync(AssignSeatRequestDto request)
        {
            using var semaphore = new SemaphoreSlim(1, 1);
            await semaphore.WaitAsync();

            try
            {
                var flightPassenger = await _flightPassengerRepository.GetByIdAsync(request.FlightPassengerId);
                if (flightPassenger == null)
                    throw new FlightSystemException("Нислэгт бүртгэлтэй зорчигч олдсонгүй", "FLIGHT_PASSENGER_NOT_FOUND");

                var isAvailable = await IsSeatAvailableAsync(flightPassenger.FlightId, request.SeatId);
                if (!isAvailable)
                    throw new SeatNotAvailableException($"Суудал ID {request.SeatId} ашиглагдсан байна");

                var seatAssignment = new SeatAssignment
                {
                    FlightPassengerId = request.FlightPassengerId,
                    SeatId = request.SeatId,
                    AssignedAt = DateTime.UtcNow,
                    AssignedByEmployeeId = request.EmployeeId,
                    IsActive = true,
                    Notes = request.Notes
                };

                seatAssignment = await _seatAssignmentRepository.AssignSeatAsync(seatAssignment);

                var seat = await _seatRepository.GetByIdAsync(request.SeatId);
                var passenger = await _passengerRepository.GetByIdAsync(flightPassenger.PassengerId);

                await _notificationService.NotifySeatAssignmentAsync(
                    flightPassenger.FlightId,
                    seat!.SeatNumber,
                    $"{passenger!.FirstName} {passenger.LastName}");

                return MapToSeatAssignmentDto(seatAssignment);
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task<BoardingPassDto> GenerateBoardingPassAsync(GenerateBoardingPassRequestDto request)
        {
            var boardingPassCode = GenerateBoardingPassCode();
            var qrCodeData = GenerateQRCodeData(request);
            var barcodeData = GenerateBarcodeData(request);

            var boardingPass = new BoardingPass
            {
                FlightPassengerId = request.FlightPassengerId,
                SeatAssignmentId = request.SeatAssignmentId,
                BoardingPassCode = boardingPassCode,
                QRCode = qrCodeData,
                BarcodeData = barcodeData,
                IssuedAt = DateTime.UtcNow,
                IssuedByEmployeeId = request.EmployeeId,
                IsBoardingComplete = false,
                Gate = request.Gate
            };

            boardingPass = await _boardingPassRepository.AddAsync(boardingPass);

            return MapToBoardingPassDto(boardingPass);
        }

        public async Task<bool> IsSeatAvailableAsync(int flightId, int seatId)
        {
            return !await _seatAssignmentRepository.IsSeatAssignedAsync(seatId, flightId);
        }

        public async Task<IEnumerable<SeatInfoDto>> GetAvailableSeatsAsync(int flightId, SeatClass? seatClass = null)
        {
            var availableSeats = await _seatRepository.GetAvailableSeatsForFlightAsync(flightId);

            if (seatClass.HasValue)
                availableSeats = availableSeats.Where(s => s.Class == seatClass.Value);

            return availableSeats.Select(MapToSeatInfoDto);
        }

        public async Task<CheckinEligibilityDto> ValidateCheckinEligibilityAsync(int flightId, string passportNumber)
        {
            var flight = await _flightRepository.GetByIdAsync(flightId);
            var passenger = await _passengerRepository.GetByPassportNumberAsync(passportNumber);

            if (flight == null || passenger == null)
                return new CheckinEligibilityDto
                {
                    IsEligible = false,
                    Reason = "Нислэг эсвэл зорчигч олдсонгүй"
                };

            var flightPassenger = await _flightPassengerRepository
                .GetFlightPassengerAsync(flightId, passenger.Id);

            if (flightPassenger == null)
                return new CheckinEligibilityDto
                {
                    IsEligible = false,
                    Reason = "Зорчигч энэ нислэгт бүртгэлгүй"
                };

            if (flightPassenger.IsCheckedIn)
                return new CheckinEligibilityDto
                {
                    IsEligible = false,
                    Reason = "Зорчигч аль хэдийн check-in хийсэн",
                    IsAlreadyCheckedIn = true
                };

            var now = DateTime.UtcNow;
            if (now < flight.CheckinOpenTime)
                return new CheckinEligibilityDto
                {
                    IsEligible = false,
                    Reason = "Check-in эхлээгүй байна",
                    CheckinOpenTime = flight.CheckinOpenTime
                };

            if (now > flight.CheckinCloseTime)
                return new CheckinEligibilityDto
                {
                    IsEligible = false,
                    Reason = "Check-in дууссан байна",
                    CheckinCloseTime = flight.CheckinCloseTime
                };

            if (flight.Status == FlightStatus.Cancelled)
                return new CheckinEligibilityDto
                {
                    IsEligible = false,
                    Reason = "Нислэг цуцлагдсан",
                    FlightStatus = flight.Status
                };

            return new CheckinEligibilityDto
            {
                IsEligible = true,
                Reason = "Check-in хийх боломжтой",
                CheckinOpenTime = flight.CheckinOpenTime,
                CheckinCloseTime = flight.CheckinCloseTime,
                FlightStatus = flight.Status
            };
        }

        public async Task<SeatMapDto> GetSeatMapAsync(int flightId)
        {
            var flight = await _flightRepository.GetFlightWithDetailsAsync(flightId);
            if (flight == null)
                throw new FlightNotFoundException($"Нислэг ID: {flightId}");

            var seats = await _seatRepository.GetSeatsByAircraftAsync(flight.AircraftId);
            var assignments = await _seatAssignmentRepository.GetAssignmentsForFlightAsync(flightId);

            var seatMap = new SeatMapDto
            {
                FlightId = flightId,
                FlightNumber = flight.FlightNumber,
                AircraftType = flight.Aircraft.AircraftType,
                TotalSeats = seats.Count(),
                AvailableSeats = seats.Count() - assignments.Count()
            };

            var seatRows = seats.GroupBy(s => s.Row)
                .OrderBy(g => int.Parse(g.Key))
                .Select(g => new SeatRowDto
                {
                    Row = g.Key,
                    Seats = g.OrderBy(s => s.Column)
                        .Select(seat => {
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
                                IsEmergencyExit = seat.IsEmergencyExit
                            };
                        }).ToList()
                }).ToList();

            seatMap.SeatRows = seatRows;
            seatMap.SeatClassCounts = seats.GroupBy(s => s.Class.ToString())
                .ToDictionary(g => g.Key, g => g.Count());

            return seatMap;
        }

        public async Task<CheckinStatusDto> GetCheckinStatusAsync(int flightId, string passportNumber)
        {
            var passenger = await _passengerRepository.GetByPassportNumberAsync(passportNumber);
            if (passenger == null)
                return new CheckinStatusDto { IsCheckedIn = false };

            var flightPassenger = await _flightPassengerRepository
                .GetFlightPassengerAsync(flightId, passenger.Id);

            if (flightPassenger == null || !flightPassenger.IsCheckedIn)
                return new CheckinStatusDto { IsCheckedIn = false };

            var seatAssignment = await _seatAssignmentRepository
                .GetByFlightPassengerAsync(flightPassenger.Id);

            var boardingPass = await _boardingPassRepository
                .GetByFlightPassengerAsync(flightPassenger.Id);

            return new CheckinStatusDto
            {
                IsCheckedIn = true,
                CheckinTime = flightPassenger.CheckinTime,
                EmployeeName = flightPassenger.CheckinByEmployee?.FirstName + " " +
                              flightPassenger.CheckinByEmployee?.LastName,
                HasSeatAssignment = seatAssignment != null,
                HasBoardingPass = boardingPass != null
            };
        }

        public async Task<bool> CancelCheckinAsync(int flightPassengerId, int employeeId)
        {
            var flightPassenger = await _flightPassengerRepository.GetByIdAsync(flightPassengerId);
            if (flightPassenger == null || !flightPassenger.IsCheckedIn)
                return false;

            var boardingPass = await _boardingPassRepository.GetByFlightPassengerAsync(flightPassengerId);
            if (boardingPass != null)
                await _boardingPassRepository.DeleteAsync(boardingPass.Id);

            var seatAssignment = await _seatAssignmentRepository.GetByFlightPassengerAsync(flightPassengerId);
            if (seatAssignment != null)
                await _seatAssignmentRepository.ReleaseSeatAsync(seatAssignment.Id);

            flightPassenger.IsCheckedIn = false;
            flightPassenger.CheckinTime = null;
            flightPassenger.CheckinByEmployeeId = null;
            await _flightPassengerRepository.UpdateAsync(flightPassenger);

            return true;
        }

        public async Task<bool> CanCheckinAsync(int flightId)
        {
            var flight = await _flightRepository.GetByIdAsync(flightId);
            if (flight == null) return false;

            var now = DateTime.UtcNow;
            return now >= flight.CheckinOpenTime &&
                   now <= flight.CheckinCloseTime &&
                   flight.Status == FlightStatus.CheckinOpen;
        }

        private FlightPassengerDto MapToFlightPassengerDto(FlightPassenger flightPassenger)
        {
            return new FlightPassengerDto
            {
                Id = flightPassenger.Id,
                BookingReference = flightPassenger.BookingReference,
                IsCheckedIn = flightPassenger.IsCheckedIn,
                CheckinTime = flightPassenger.CheckinTime,
                SpecialRequests = flightPassenger.SpecialRequests,
                BaggageInfo = flightPassenger.BaggageInfo
            };
        }

        private SeatAssignmentDto MapToSeatAssignmentDto(SeatAssignment seatAssignment)
        {
            return new SeatAssignmentDto
            {
                Id = seatAssignment.Id,
                AssignedAt = seatAssignment.AssignedAt,
                IsActive = seatAssignment.IsActive,
                Notes = seatAssignment.Notes
            };
        }

        private BoardingPassDto MapToBoardingPassDto(BoardingPass boardingPass)
        {
            return new BoardingPassDto
            {
                Id = boardingPass.Id,
                BoardingPassCode = boardingPass.BoardingPassCode,
                QRCode = boardingPass.QRCode,
                BarcodeData = boardingPass.BarcodeData,
                IssuedAt = boardingPass.IssuedAt,
                IsBoardingComplete = boardingPass.IsBoardingComplete,
                Gate = boardingPass.Gate
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
                IsWindowSeat = seat.IsWindowSeat,
                IsAisleSeat = seat.IsAisleSeat,
                IsEmergencyExit = seat.IsEmergencyExit,
                IsAvailable = true
            };
        }

        private string GenerateBoardingPassCode()
        {
            return $"BP{DateTime.UtcNow:yyyyMMddHHmmss}{Random.Shared.Next(1000, 9999)}";
        }

        private string GenerateQRCodeData(GenerateBoardingPassRequestDto request)
        {
            return $"FP:{request.FlightPassengerId}|SA:{request.SeatAssignmentId}|T:{DateTime.UtcNow:O}";
        }

        private string GenerateBarcodeData(GenerateBoardingPassRequestDto request)
        {
            return $"{request.FlightPassengerId:D8}{request.SeatAssignmentId:D8}";
        }
    }
}
