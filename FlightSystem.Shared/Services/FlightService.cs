using FlightSystem.Shared.Interfaces.Services;
using FlightSystem.Shared.Interfaces.Repositories;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Core.Models;
using FlightSystem.Core.Enums;
using FlightSystem.Core.Exceptions;
using FlightSystem.Core.Extensions;
using Microsoft.Extensions.Logging;

namespace FlightSystem.Shared.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly INotificationService _notificationService;
        private readonly ILogger<FlightService> _logger;

        public FlightService(
            IFlightRepository flightRepository,
            IAircraftRepository aircraftRepository,
            IEmployeeRepository employeeRepository,
            INotificationService notificationService,
            ILogger<FlightService> logger)
        {
            _flightRepository = flightRepository;
            _aircraftRepository = aircraftRepository;
            _employeeRepository = employeeRepository;
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task<FlightInfoDto?> GetFlightByIdAsync(int flightId)
        {
            var flight = await _flightRepository.GetFlightWithDetailsAsync(flightId);
            return flight != null ? MapToFlightInfoDto(flight) : null;
        }

        public async Task<IEnumerable<FlightInfoDto>> GetAllFlightsAsync()
        {
            var flights = await _flightRepository.GetAllAsync();
            return flights.Select(MapToFlightInfoDto);
        }

        public async Task<FlightInfoDto> CreateFlightAsync(CreateFlightDto createDto)
        {
            var aircraft = await _aircraftRepository.GetByIdAsync(createDto.AircraftId);
            if (aircraft == null)
                throw new FlightSystemException("Онгоц олдсонгүй", "AIRCRAFT_NOT_FOUND");

            var employee = await _employeeRepository.GetByIdAsync(createDto.CreatedByEmployeeId);
            if (employee == null)
                throw new FlightSystemException("Ажилтан олдсонгүй", "EMPLOYEE_NOT_FOUND");

            var existingFlight = await _flightRepository.GetByFlightNumberAsync(createDto.FlightNumber);
            if (existingFlight != null)
                throw new FlightSystemException($"Нислэгийн дугаар {createDto.FlightNumber} аль хэдийн ашиглагдсан байна", "FLIGHT_NUMBER_EXISTS");

            var flight = new Flight
            {
                FlightNumber = createDto.FlightNumber,
                AircraftId = createDto.AircraftId,
                DepartureAirport = createDto.DepartureAirport,
                ArrivalAirport = createDto.ArrivalAirport,
                ScheduledDeparture = createDto.ScheduledDeparture,
                ScheduledArrival = createDto.ScheduledArrival,
                GateNumber = createDto.GateNumber,
                CheckinOpenTime = createDto.CheckinOpenTime,
                CheckinCloseTime = createDto.CheckinCloseTime,
                BoardingTime = createDto.BoardingTime,
                Status = FlightStatus.Scheduled,
                CreatedByEmployeeId = createDto.CreatedByEmployeeId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            flight = await _flightRepository.AddAsync(flight);
            _logger.LogInformation("Нислэг {FlightNumber} амжилттай үүслээ, ID {FlightId}", flight.FlightNumber, flight.Id);

            return MapToFlightInfoDto(flight);
        }

        public async Task<FlightInfoDto> UpdateFlightAsync(int flightId, UpdateFlightDto updateDto)
        {
            var flight = await _flightRepository.GetByIdAsync(flightId);
            if (flight == null)
                throw new FlightNotFoundException($"Нислэг олдсонгүй, ID: {flightId}");

            if (updateDto.GateNumber != null)
                flight.GateNumber = updateDto.GateNumber;
            if (updateDto.ActualDeparture.HasValue)
                flight.ActualDeparture = updateDto.ActualDeparture.Value;
            if (updateDto.ActualArrival.HasValue)
                flight.ActualArrival = updateDto.ActualArrival.Value;
            if (updateDto.Status.HasValue)
            {
                if (!flight.Status.CanTransitionTo(updateDto.Status.Value))
                    throw new InvalidFlightStatusException(flight.Status, updateDto.Status.Value);
                flight.Status = updateDto.Status.Value;
            }
            if (updateDto.CheckinOpenTime.HasValue)
                flight.CheckinOpenTime = updateDto.CheckinOpenTime.Value;
            if (updateDto.CheckinCloseTime.HasValue)
                flight.CheckinCloseTime = updateDto.CheckinCloseTime.Value;
            if (updateDto.BoardingTime.HasValue)
                flight.BoardingTime = updateDto.BoardingTime.Value;

            flight.UpdatedAt = DateTime.UtcNow;
            flight = await _flightRepository.UpdateAsync(flight);

            if (updateDto.Status.HasValue)
            {
                await _notificationService.NotifyFlightStatusChangeAsync(flightId, updateDto.Status.Value);
            }

            _logger.LogInformation("Нислэг {FlightNumber} амжилттай шинэчлэгдлээ, ID {FlightId}", flight.FlightNumber, flight.Id);
            return MapToFlightInfoDto(flight);
        }

        public async Task<bool> DeleteFlightAsync(int flightId)
        {
            var flight = await _flightRepository.GetByIdAsync(flightId);
            if (flight == null)
                return false;

            if (flight.Status != FlightStatus.Scheduled)
                throw new FlightSystemException("Төлөв нь Scheduled биш нислэгийг устгах боломжгүй", "CANNOT_DELETE_FLIGHT");

            await _flightRepository.DeleteAsync(flightId);
            _logger.LogInformation("Нислэг {FlightNumber} устгагдлаа, ID {FlightId}", flight.FlightNumber, flightId);
            return true;
        }

        public async Task<FlightInfoDto?> GetFlightByNumberAsync(string flightNumber)
        {
            var flight = await _flightRepository.GetByFlightNumberAsync(flightNumber);
            return flight != null ? MapToFlightInfoDto(flight) : null;
        }

        public async Task<IEnumerable<FlightInfoDto>> GetActiveFlightsAsync()
        {
            var flights = await _flightRepository.GetActiveFlightsAsync();
            return flights.Select(MapToFlightInfoDto);
        }

        public async Task<FlightInfoDto> UpdateFlightStatusAsync(int flightId, FlightStatus status)
        {
            var flight = await _flightRepository.GetByIdAsync(flightId);
            if (flight == null)
                throw new FlightNotFoundException($"Нислэг олдсонгүй, ID: {flightId}");

            if (!flight.Status.CanTransitionTo(status))
                throw new InvalidFlightStatusException(flight.Status, status);

            flight.Status = status;
            flight.UpdatedAt = DateTime.UtcNow;
            flight = await _flightRepository.UpdateAsync(flight);

            await _notificationService.NotifyFlightStatusChangeAsync(flightId, status);
            _logger.LogInformation("Нислэг {FlightNumber} статус {Status}-д өөрчлөгдлөө", flight.FlightNumber, status);

            return MapToFlightInfoDto(flight);
        }

        public async Task<IEnumerable<FlightInfoDto>> GetFlightsByDateAsync(DateTime date)
        {
            var flights = await _flightRepository.GetFlightsByDateAsync(date);
            return flights.Select(MapToFlightInfoDto);
        }

        public async Task<bool> CanUpdateFlightStatusAsync(int flightId, FlightStatus newStatus)
        {
            var flight = await _flightRepository.GetByIdAsync(flightId);
            if (flight == null)
                return false;

            return flight.Status.CanTransitionTo(newStatus);
        }

        public async Task<FlightDetailDto?> GetFlightDetailsAsync(int flightId)
        {
            var flight = await _flightRepository.GetFlightWithDetailsAsync(flightId);
            if (flight == null)
                return null;

            var detailDto = new FlightDetailDto
            {
                Id = flight.Id,
                FlightNumber = flight.FlightNumber,
                DepartureAirport = flight.DepartureAirport,
                ArrivalAirport = flight.ArrivalAirport,
                ScheduledDeparture = flight.ScheduledDeparture,
                ScheduledArrival = flight.ScheduledArrival,
                ActualDeparture = flight.ActualDeparture,
                ActualArrival = flight.ActualArrival,
                Status = flight.Status.GetDisplayName(),
                GateNumber = flight.GateNumber,
                AircraftType = flight.Aircraft?.AircraftType ?? "Тодорхойгүй",
                CheckinOpenTime = flight.CheckinOpenTime,
                CheckinCloseTime = flight.CheckinCloseTime,
                BoardingTime = flight.BoardingTime,
                TotalSeats = flight.Aircraft?.TotalSeats ?? 0,
                AvailableSeats = flight.Aircraft?.TotalSeats ?? 0,
                CheckedInPassengers = 0,
                BoardedPassengers = 0,
                CreatedByEmployee = flight.CreatedByEmployee?.GetFullName() ?? "Тодорхойгүй"
            };

            return detailDto;
        }

        public async Task<bool> IsCheckinOpenAsync(int flightId)
        {
            var flight = await _flightRepository.GetByIdAsync(flightId);
            return flight?.IsCheckinOpen() ?? false;
        }

        public async Task<bool> IsBoardingOpenAsync(int flightId)
        {
            var flight = await _flightRepository.GetByIdAsync(flightId);
            return flight?.IsBoardingOpen() ?? false;
        }

        private FlightInfoDto MapToFlightInfoDto(Flight flight)
        {
            return new FlightInfoDto
            {
                Id = flight.Id,
                FlightNumber = flight.FlightNumber,
                DepartureAirport = flight.DepartureAirport,
                ArrivalAirport = flight.ArrivalAirport,
                ScheduledDeparture = flight.ScheduledDeparture,
                ScheduledArrival = flight.ScheduledArrival,
                ActualDeparture = flight.ActualDeparture,
                ActualArrival = flight.ActualArrival,
                Status = flight.Status.GetDisplayName(),
                GateNumber = flight.GateNumber,
                AircraftType = flight.Aircraft?.AircraftType ?? "Тодорхойгүй",
                CheckinOpenTime = flight.CheckinOpenTime,
                CheckinCloseTime = flight.CheckinCloseTime,
                BoardingTime = flight.BoardingTime
            };
        }
    }
}
