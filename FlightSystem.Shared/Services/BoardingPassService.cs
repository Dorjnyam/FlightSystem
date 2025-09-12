using FlightSystem.Shared.Interfaces.Services;
using FlightSystem.Shared.Interfaces.Repositories;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Core.Models;
using FlightSystem.Core.Exceptions;
using FlightSystem.Core.Extensions;
using Microsoft.Extensions.Logging;

namespace FlightSystem.Shared.Services
{
    public class BoardingPassService : IBoardingPassService
    {
        private readonly IBoardingPassRepository _boardingPassRepository;
        private readonly IFlightPassengerRepository _flightPassengerRepository;
        private readonly ISeatAssignmentRepository _seatAssignmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<BoardingPassService> _logger;

        public BoardingPassService(
            IBoardingPassRepository boardingPassRepository,
            IFlightPassengerRepository flightPassengerRepository,
            ISeatAssignmentRepository seatAssignmentRepository,
            IEmployeeRepository employeeRepository,
            ILogger<BoardingPassService> logger)
        {
            _boardingPassRepository = boardingPassRepository;
            _flightPassengerRepository = flightPassengerRepository;
            _seatAssignmentRepository = seatAssignmentRepository;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<BoardingPassDto> CreateBoardingPassAsync(CreateBoardingPassDto createDto)
        {
            var flightPassenger = await _flightPassengerRepository.GetByIdAsync(createDto.FlightPassengerId);
            if (flightPassenger == null)
                throw new FlightSystemException("Нислэгийн зорчигч олдсонгүй", "FLIGHT_PASSENGER_NOT_FOUND");

            var seatAssignment = await _seatAssignmentRepository.GetByIdAsync(createDto.SeatAssignmentId);
            if (seatAssignment == null)
                throw new FlightSystemException("Суудлын оноосон мэдээлэл олдсонгүй", "SEAT_ASSIGNMENT_NOT_FOUND");

            var existingBoardingPass = await _boardingPassRepository.GetByFlightPassengerAsync(createDto.FlightPassengerId);
            if (existingBoardingPass != null)
                throw new FlightSystemException("Энэ зорчигчийн зорчигчийн карт аль хэдийн үүссэн байна", "BOARDING_PASS_EXISTS");

            var boardingPassCode = GenerateBoardingPassCode();
            var qrCodeData = GenerateQRCodeData(createDto);
            var barcodeData = GenerateBarcodeData(createDto);

            var boardingPass = new BoardingPass
            {
                FlightPassengerId = createDto.FlightPassengerId,
                SeatAssignmentId = createDto.SeatAssignmentId,
                BoardingPassCode = boardingPassCode,
                QRCode = qrCodeData,
                BarcodeData = barcodeData,
                IssuedAt = DateTime.UtcNow,
                IssuedByEmployeeId = createDto.IssuedByEmployeeId,
                IsBoardingComplete = false,
                Gate = createDto.Gate
            };

            boardingPass = await _boardingPassRepository.AddAsync(boardingPass);
            _logger.LogInformation("Зорчигч {FlightPassengerId}-ийн зорчигчийн карт {BoardingPassCode} амжилттай үүсгэгдлээ",
                createDto.FlightPassengerId, boardingPass.BoardingPassCode);

            return MapToBoardingPassDto(boardingPass);
        }

        public async Task<BoardingPassDto?> GetBoardingPassByIdAsync(int id)
        {
            var boardingPass = await _boardingPassRepository.GetByIdAsync(id);
            return boardingPass != null ? MapToBoardingPassDto(boardingPass) : null;
        }

        public async Task<IEnumerable<BoardingPassDto>> GetAllBoardingPassesAsync()
        {
            var boardingPasses = await _boardingPassRepository.GetAllAsync();
            return boardingPasses.Select(MapToBoardingPassDto);
        }

        public async Task<BoardingPassDto> UpdateBoardingPassAsync(int id, UpdateBoardingPassDto updateDto)
        {
            var boardingPass = await _boardingPassRepository.GetByIdAsync(id);
            if (boardingPass == null)
                throw new FlightSystemException("Зорчигчийн карт олдсонгүй", "BOARDING_PASS_NOT_FOUND");

            if (updateDto.BoardingTime.HasValue)
                boardingPass.BoardingTime = updateDto.BoardingTime.Value;

            if (updateDto.IsBoardingComplete.HasValue)
                boardingPass.IsBoardingComplete = updateDto.IsBoardingComplete.Value;

            if (updateDto.Gate != null)
                boardingPass.Gate = updateDto.Gate;

            boardingPass = await _boardingPassRepository.UpdateAsync(boardingPass);
            _logger.LogInformation("Зорчигчийн карт {BoardingPassCode} шинэчлэгдлээ (ID: {Id})", boardingPass.BoardingPassCode, id);

            return MapToBoardingPassDto(boardingPass);
        }

        public async Task<bool> DeleteBoardingPassAsync(int id)
        {
            var boardingPass = await _boardingPassRepository.GetByIdAsync(id);
            if (boardingPass == null)
                return false;

            await _boardingPassRepository.DeleteAsync(id);
            _logger.LogInformation("Зорчигчийн карт {BoardingPassCode} устгагдлаа (ID: {Id})", boardingPass.BoardingPassCode, id);
            return true;
        }

        public async Task<BoardingPassDto?> GetByCodeAsync(string boardingPassCode)
        {
            var boardingPass = await _boardingPassRepository.GetByCodeAsync(boardingPassCode);
            return boardingPass != null ? MapToBoardingPassDto(boardingPass) : null;
        }

        public async Task<BoardingPassDto> GenerateBoardingPassAsync(int flightPassengerId, int seatAssignmentId, int employeeId)
        {
            var createDto = new CreateBoardingPassDto
            {
                FlightPassengerId = flightPassengerId,
                SeatAssignmentId = seatAssignmentId,
                IssuedByEmployeeId = employeeId
            };

            return await CreateBoardingPassAsync(createDto);
        }

        public async Task<bool> ValidateBoardingPassAsync(string code)
        {
            var boardingPass = await _boardingPassRepository.GetByCodeAsync(code);
            return boardingPass != null && !boardingPass.IsBoardingComplete;
        }

        public async Task<BoardingPassDto> ProcessBoardingAsync(string code)
        {
            var boardingPass = await _boardingPassRepository.GetByCodeAsync(code);
            if (boardingPass == null)
                throw new FlightSystemException("Зорчигчийн картын код буруу байна", "INVALID_BOARDING_PASS");

            if (boardingPass.IsBoardingComplete)
                throw new FlightSystemException("Зорчигч аль хэдийн суудалд суулаа", "ALREADY_BOARDED");

            boardingPass.IsBoardingComplete = true;
            boardingPass.BoardingTime = DateTime.UtcNow;
            boardingPass = await _boardingPassRepository.UpdateAsync(boardingPass);

            _logger.LogInformation("Зорчигч {FlightPassengerId} {BoardingPassCode} карттайгаар суудалд орлоо",
                boardingPass.FlightPassengerId, boardingPass.BoardingPassCode);

            return MapToBoardingPassDto(boardingPass);
        }

        public async Task<IEnumerable<BoardingPassDto>> GetBoardingPassesForFlightAsync(int flightId)
        {
            var boardingPasses = await _boardingPassRepository.GetBoardingPassesForFlightAsync(flightId);
            return boardingPasses.Select(MapToBoardingPassDto);
        }

        public async Task<BoardingPassDto?> GetBoardingPassByPassengerAndFlightAsync(int passengerId, int flightId)
        {
            // This is a stub implementation
            var boardingPasses = await _boardingPassRepository.GetBoardingPassesForFlightAsync(flightId);
            var boardingPass = boardingPasses.FirstOrDefault(bp => bp.FlightPassenger?.PassengerId == passengerId);
            return boardingPass != null ? MapToBoardingPassDto(boardingPass) : null;
        }

        public async Task<byte[]> GenerateQRCodeAsync(int id)
        {
            // This is a stub implementation - return a simple QR code
            var boardingPass = await _boardingPassRepository.GetByIdAsync(id);
            if (boardingPass == null)
                throw new FlightSystemException("Boarding pass олдсонгүй", "BOARDING_PASS_NOT_FOUND");

            // Return a simple placeholder QR code data
            return System.Text.Encoding.UTF8.GetBytes($"QR:{boardingPass.BoardingPassCode}");
        }

        public async Task<byte[]> GeneratePrintVersionAsync(int id)
        {
            // This is a stub implementation - return a simple PDF-like data
            var boardingPass = await _boardingPassRepository.GetByIdAsync(id);
            if (boardingPass == null)
                throw new FlightSystemException("Boarding pass олдсонгүй", "BOARDING_PASS_NOT_FOUND");

            // Return a simple placeholder PDF data
            return System.Text.Encoding.UTF8.GetBytes($"PDF:{boardingPass.BoardingPassCode}");
        }

        public async Task<IEnumerable<BoardingPassDto>> GetBoardingPassesByFlightAsync(int flightId)
        {
            return await GetBoardingPassesForFlightAsync(flightId);
        }

        public async Task<BoardingPassDto> UpdateBoardingPassStatusAsync(int id, bool isUsed)
        {
            var boardingPass = await _boardingPassRepository.GetByIdAsync(id);
            if (boardingPass == null)
                throw new FlightSystemException("Boarding pass олдсонгүй", "BOARDING_PASS_NOT_FOUND");

            boardingPass.IsBoardingComplete = isUsed;
            if (isUsed)
                boardingPass.BoardingTime = DateTime.UtcNow;

            boardingPass = await _boardingPassRepository.UpdateAsync(boardingPass);
            return MapToBoardingPassDto(boardingPass);
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
                IssuedByEmployee = boardingPass.IssuedByEmployee?.GetFullName() ?? "Тодорхойгүй",
                BoardingTime = boardingPass.BoardingTime,
                IsBoardingComplete = boardingPass.IsBoardingComplete,
                Gate = boardingPass.Gate,
                Flight = new FlightInfoDto
                {
                    Id = boardingPass.FlightPassenger?.FlightId ?? 0,
                    FlightNumber = boardingPass.FlightPassenger?.Flight?.FlightNumber ?? "Тодорхойгүй",
                    DepartureAirport = boardingPass.FlightPassenger?.Flight?.DepartureAirport ?? "Тодорхойгүй",
                    ArrivalAirport = boardingPass.FlightPassenger?.Flight?.ArrivalAirport ?? "Тодорхойгүй",
                    ScheduledDeparture = boardingPass.FlightPassenger?.Flight?.ScheduledDeparture ?? DateTime.MinValue,
                    ScheduledArrival = boardingPass.FlightPassenger?.Flight?.ScheduledArrival ?? DateTime.MinValue,
                    Status = boardingPass.FlightPassenger?.Flight?.Status.GetDisplayName() ?? "Тодорхойгүй"
                },
                Passenger = new PassengerDto
                {
                    Id = boardingPass.FlightPassenger?.PassengerId ?? 0,
                    PassportNumber = boardingPass.FlightPassenger?.Passenger?.PassportNumber ?? "Тодорхойгүй",
                    FirstName = boardingPass.FlightPassenger?.Passenger?.FirstName ?? "Тодорхойгүй",
                    LastName = boardingPass.FlightPassenger?.Passenger?.LastName ?? "Тодорхойгүй",
                    Nationality = boardingPass.FlightPassenger?.Passenger?.Nationality ?? "Тодорхойгүй",
                    DateOfBirth = boardingPass.FlightPassenger?.Passenger?.DateOfBirth ?? DateTime.MinValue,
                    PassengerType = boardingPass.FlightPassenger?.Passenger?.Type.GetDisplayName() ?? "Тодорхойгүй"
                },
                Seat = new SeatInfoDto
                {
                    Id = boardingPass.SeatAssignment?.SeatId ?? 0,
                    SeatNumber = boardingPass.SeatAssignment?.Seat?.SeatNumber ?? "Тодорхойгүй",
                    Row = boardingPass.SeatAssignment?.Seat?.Row ?? "Тодорхойгүй",
                    Column = boardingPass.SeatAssignment?.Seat?.Column ?? "Тодорхойгүй",
                    SeatClass = boardingPass.SeatAssignment?.Seat?.Class.ToString() ?? "Тодорхойгүй",
                    IsAvailable = false,
                    IsWindowSeat = boardingPass.SeatAssignment?.Seat?.IsWindowSeat ?? false,
                    IsAisleSeat = boardingPass.SeatAssignment?.Seat?.IsAisleSeat ?? false,
                    IsEmergencyExit = boardingPass.SeatAssignment?.Seat?.IsEmergencyExit ?? false
                }
            };
        }

        private string GenerateBoardingPassCode()
        {
            return $"BP{DateTime.UtcNow:yyyyMMddHHmmss}{Random.Shared.Next(1000, 9999)}";
        }

        private string GenerateQRCodeData(CreateBoardingPassDto createDto)
        {
            return $"FP:{createDto.FlightPassengerId}|SA:{createDto.SeatAssignmentId}|T:{DateTime.UtcNow:O}";
        }

        private string GenerateBarcodeData(CreateBoardingPassDto createDto)
        {
            return $"{createDto.FlightPassengerId:D8}{createDto.SeatAssignmentId:D8}";
        }
    }
}
