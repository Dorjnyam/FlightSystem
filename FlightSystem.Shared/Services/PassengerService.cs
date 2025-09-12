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
    public class PassengerService : IPassengerService
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly IFlightPassengerRepository _flightPassengerRepository;
        private readonly ILogger<PassengerService> _logger;

        public PassengerService(
            IPassengerRepository passengerRepository,
            IFlightPassengerRepository flightPassengerRepository,
            ILogger<PassengerService> logger)
        {
            _passengerRepository = passengerRepository;
            _flightPassengerRepository = flightPassengerRepository;
            _logger = logger;
        }

        public async Task<PassengerDto> CreatePassengerAsync(CreatePassengerDto createDto)
        {
            var existingPassenger = await _passengerRepository.GetByPassportNumberAsync(createDto.PassportNumber);
            if (existingPassenger != null)
                throw new FlightSystemException($"Пасспорт дугаар {createDto.PassportNumber}-тай зорчигч аль хэдийн бүртгэгдсэн", "PASSENGER_EXISTS");

            var passenger = new Passenger
            {
                PassportNumber = createDto.PassportNumber,
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                Nationality = createDto.Nationality,
                DateOfBirth = createDto.DateOfBirth,
                Type = createDto.Type,
                Email = createDto.Email,
                Phone = createDto.Phone,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            passenger = await _passengerRepository.AddAsync(passenger);
            _logger.LogInformation("Зорчигч {PassportNumber} амжилттай үүсгэлээ. ID: {PassengerId}", passenger.PassportNumber, passenger.Id);

            return MapToPassengerDto(passenger);
        }

        public async Task<PassengerDto?> GetPassengerByIdAsync(int passengerId)
        {
            var passenger = await _passengerRepository.GetByIdAsync(passengerId);
            return passenger != null ? MapToPassengerDto(passenger) : null;
        }

        public async Task<IEnumerable<PassengerDto>> GetAllPassengersAsync()
        {
            var passengers = await _passengerRepository.GetAllAsync();
            return passengers.Select(MapToPassengerDto);
        }

        public async Task<PassengerDto> UpdatePassengerAsync(int passengerId, UpdatePassengerDto updateDto)
        {
            var passenger = await _passengerRepository.GetByIdAsync(passengerId);
            if (passenger == null)
                throw new PassengerNotFoundException($"ID {passengerId}-тай зорчигч олдсонгүй");

            if (updateDto.Email != null)
                passenger.Email = updateDto.Email;

            if (updateDto.Phone != null)
                passenger.Phone = updateDto.Phone;

            passenger.UpdatedAt = DateTime.UtcNow;
            passenger = await _passengerRepository.UpdateAsync(passenger);

            _logger.LogInformation("Зорчигч {PassportNumber}-ийн мэдээлэл амжилттай шинэчилэгдлээ. ID: {PassengerId}", passenger.PassportNumber, passengerId);
            return MapToPassengerDto(passenger);
        }

        public async Task<bool> DeletePassengerAsync(int passengerId)
        {
            var passenger = await _passengerRepository.GetByIdAsync(passengerId);
            if (passenger == null)
                return false;

            var flightPassengers = await _flightPassengerRepository.GetFlightsForPassengerAsync(passengerId);
            if (flightPassengers.Any())
                throw new FlightSystemException("Нислэгийн захиалгатай зорчигчийг устгах боломжгүй", "CANNOT_DELETE_PASSENGER");

            await _passengerRepository.DeleteAsync(passengerId);
            _logger.LogInformation("Зорчигч {PassportNumber}-ийг амжилттай устгалаа. ID: {PassengerId}", passenger.PassportNumber, passengerId);
            return true;
        }

        public async Task<PassengerDto?> GetByPassportNumberAsync(string passportNumber)
        {
            var passenger = await _passengerRepository.GetByPassportNumberAsync(passportNumber);
            return passenger != null ? MapToPassengerDto(passenger) : null;
        }

        public async Task<PassengerDto?> GetPassengerByPassportAsync(string passportNumber)
        {
            return await GetByPassportNumberAsync(passportNumber);
        }

        public async Task<FlightPassengerDto?> GetFlightPassengerAsync(int flightId, string passportNumber)
        {
            var passenger = await _passengerRepository.GetByPassportNumberAsync(passportNumber);
            if (passenger == null)
                return null;

            var flightPassenger = await _flightPassengerRepository.GetFlightPassengerAsync(flightId, passenger.Id);
            if (flightPassenger == null)
                return null;

            return MapToFlightPassengerDto(flightPassenger);
        }

        public async Task<IEnumerable<FlightPassengerDto>> GetPassengersForFlightAsync(int flightId)
        {
            var flightPassengers = await _flightPassengerRepository.GetPassengersForFlightAsync(flightId);
            return flightPassengers.Select(MapToFlightPassengerDto);
        }

        public async Task<bool> IsPassengerBookedForFlightAsync(int flightId, string passportNumber)
        {
            var passenger = await _passengerRepository.GetByPassportNumberAsync(passportNumber);
            if (passenger == null)
                return false;

            var flightPassenger = await _flightPassengerRepository.GetFlightPassengerAsync(flightId, passenger.Id);
            return flightPassenger != null;
        }

        public async Task<IEnumerable<PassengerDto>> SearchPassengersAsync(string searchTerm)
        {
            var passengers = await _passengerRepository.SearchPassengersAsync(searchTerm);
            return passengers.Select(MapToPassengerDto);
        }

        private PassengerDto MapToPassengerDto(Passenger passenger)
        {
            return new PassengerDto
            {
                Id = passenger.Id,
                PassportNumber = passenger.PassportNumber,
                FirstName = passenger.FirstName,
                LastName = passenger.LastName,
                Nationality = passenger.Nationality,
                DateOfBirth = passenger.DateOfBirth,
                PassengerType = passenger.Type.GetDisplayName(),
                Email = passenger.Email,
                Phone = passenger.Phone
            };
        }

        private FlightPassengerDto MapToFlightPassengerDto(FlightPassenger flightPassenger)
        {
            return new FlightPassengerDto
            {
                Id = flightPassenger.Id,
                BookingReference = flightPassenger.BookingReference,
                IsCheckedIn = flightPassenger.IsCheckedIn,
                CheckinTime = flightPassenger.CheckinTime,
                CheckinByEmployee = flightPassenger.CheckinByEmployee?.GetFullName(),
                SpecialRequests = flightPassenger.SpecialRequests,
                BaggageInfo = flightPassenger.BaggageInfo,
                Passenger = MapToPassengerDto(flightPassenger.Passenger),
                Flight = new FlightInfoDto
                {
                    Id = flightPassenger.FlightId,
                    FlightNumber = flightPassenger.Flight?.FlightNumber ?? "Тодорхойгүй",
                    DepartureAirport = flightPassenger.Flight?.DepartureAirport ?? "Тодорхойгүй",
                    ArrivalAirport = flightPassenger.Flight?.ArrivalAirport ?? "Тодорхойгүй",
                    ScheduledDeparture = flightPassenger.Flight?.ScheduledDeparture ?? DateTime.MinValue,
                    ScheduledArrival = flightPassenger.Flight?.ScheduledArrival ?? DateTime.MinValue,
                    Status = flightPassenger.Flight?.Status.GetDisplayName() ?? "Тодорхойгүй"
                }
            };
        }
    }
}
