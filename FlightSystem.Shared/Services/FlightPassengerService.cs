using FlightSystem.Shared.Interfaces.Services;
using FlightSystem.Shared.Interfaces.Repositories;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Core.Models;
using FlightSystem.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace FlightSystem.Shared.Services
{
    public class FlightPassengerService : IFlightPassengerService
    {
        private readonly IFlightPassengerRepository _flightPassengerRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly ISeatAssignmentRepository _seatAssignmentRepository;
        private readonly IBoardingPassRepository _boardingPassRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<FlightPassengerService> _logger;

        public FlightPassengerService(
            IFlightPassengerRepository flightPassengerRepository,
            IFlightRepository flightRepository,
            IPassengerRepository passengerRepository,
            ISeatAssignmentRepository seatAssignmentRepository,
            IBoardingPassRepository boardingPassRepository,
            IEmployeeRepository employeeRepository,
            ILogger<FlightPassengerService> logger)
        {
            _flightPassengerRepository = flightPassengerRepository;
            _flightRepository = flightRepository;
            _passengerRepository = passengerRepository;
            _seatAssignmentRepository = seatAssignmentRepository;
            _boardingPassRepository = boardingPassRepository;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<FlightPassengerDto> CreateFlightPassengerAsync(CreateFlightPassengerDto createDto)
        {
            _logger.LogInformation("Creating flight passenger for flight {FlightId}, passenger {PassengerId}", 
                createDto.FlightId, createDto.PassengerId);

            try
            {
                // Validate flight exists
                var flight = await _flightRepository.GetByIdAsync(createDto.FlightId);
                if (flight == null)
                    throw new FlightNotFoundException($"Нислэг ID: {createDto.FlightId} олдсонгүй");

                // Validate passenger exists
                var passenger = await _passengerRepository.GetByIdAsync(createDto.PassengerId);
                if (passenger == null)
                    throw new PassengerNotFoundException($"Зорчигч ID: {createDto.PassengerId} олдсонгүй");

                // Check if passenger is already booked for this flight
                var existingBooking = await _flightPassengerRepository.GetFlightPassengerAsync(createDto.FlightId, createDto.PassengerId);
                if (existingBooking != null)
                    throw new FlightSystemException("Зорчигч аль хэдийн энэ нислэгт бүртгэлтэй", "ALREADY_BOOKED");

                // Check if booking reference is unique
                var existingReference = await _flightPassengerRepository.GetByBookingReferenceAsync(createDto.BookingReference);
                if (existingReference != null)
                    throw new FlightSystemException("Бүртгэлийн дугаар аль хэдийн ашиглагдсан", "DUPLICATE_BOOKING_REFERENCE");

                var flightPassenger = new FlightPassenger
                {
                    FlightId = createDto.FlightId,
                    PassengerId = createDto.PassengerId,
                    BookingReference = createDto.BookingReference,
                    SpecialRequests = createDto.SpecialRequests,
                    BaggageInfo = createDto.BaggageInfo,
                    IsCheckedIn = false,
                    CheckinTime = null,
                    CheckinByEmployeeId = null,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                flightPassenger = await _flightPassengerRepository.AddAsync(flightPassenger);

                _logger.LogInformation("Flight passenger created successfully with ID {FlightPassengerId}", flightPassenger.Id);

                return await MapToFlightPassengerDto(flightPassenger);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating flight passenger for flight {FlightId}, passenger {PassengerId}", 
                    createDto.FlightId, createDto.PassengerId);
                throw;
            }
        }

        public async Task<FlightPassengerDto> CreateFlightPassengerByPassportAsync(CreateFlightPassengerByPassportDto createDto)
        {
            _logger.LogInformation("Creating flight passenger by passport for flight {FlightId}, passport {PassportNumber}", 
                createDto.FlightId, createDto.PassportNumber);

            try
            {
                // Validate flight exists
                var flight = await _flightRepository.GetByIdAsync(createDto.FlightId);
                if (flight == null)
                    throw new FlightNotFoundException($"Нислэг ID: {createDto.FlightId} олдсонгүй");

                // Find passenger by passport
                var passenger = await _passengerRepository.GetByPassportNumberAsync(createDto.PassportNumber);
                if (passenger == null)
                    throw new PassengerNotFoundException($"Пасспорт {createDto.PassportNumber} бүхий зорчигч олдсонгүй");

                // Check if passenger is already booked for this flight
                var existingBooking = await _flightPassengerRepository.GetFlightPassengerAsync(createDto.FlightId, passenger.Id);
                if (existingBooking != null)
                    throw new FlightSystemException("Зорчигч аль хэдийн энэ нислэгт бүртгэлтэй", "ALREADY_BOOKED");

                // Check if booking reference is unique
                var existingReference = await _flightPassengerRepository.GetByBookingReferenceAsync(createDto.BookingReference);
                if (existingReference != null)
                    throw new FlightSystemException("Бүртгэлийн дугаар аль хэдийн ашиглагдсан", "DUPLICATE_BOOKING_REFERENCE");

                var flightPassenger = new FlightPassenger
                {
                    FlightId = createDto.FlightId,
                    PassengerId = passenger.Id,
                    BookingReference = createDto.BookingReference,
                    SpecialRequests = createDto.SpecialRequests,
                    BaggageInfo = createDto.BaggageInfo,
                    IsCheckedIn = false,
                    CheckinTime = null,
                    CheckinByEmployeeId = null,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                flightPassenger = await _flightPassengerRepository.AddAsync(flightPassenger);

                _logger.LogInformation("Flight passenger created successfully with ID {FlightPassengerId}", flightPassenger.Id);

                return await MapToFlightPassengerDto(flightPassenger);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating flight passenger by passport for flight {FlightId}, passport {PassportNumber}", 
                    createDto.FlightId, createDto.PassportNumber);
                throw;
            }
        }

        public async Task<FlightPassengerDto> UpdateFlightPassengerAsync(int id, UpdateFlightPassengerDto updateDto)
        {
            _logger.LogInformation("Updating flight passenger {FlightPassengerId}", id);

            try
            {
                var flightPassenger = await _flightPassengerRepository.GetByIdAsync(id);
                if (flightPassenger == null)
                    throw new FlightSystemException($"FlightPassenger ID: {id} олдсонгүй", "FLIGHT_PASSENGER_NOT_FOUND");

                // Update only allowed fields
                flightPassenger.SpecialRequests = updateDto.SpecialRequests;
                flightPassenger.BaggageInfo = updateDto.BaggageInfo;
                flightPassenger.UpdatedAt = DateTime.UtcNow;

                flightPassenger = await _flightPassengerRepository.UpdateAsync(flightPassenger);

                _logger.LogInformation("Flight passenger {FlightPassengerId} updated successfully", id);

                return await MapToFlightPassengerDto(flightPassenger);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating flight passenger {FlightPassengerId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteFlightPassengerAsync(int id)
        {
            _logger.LogInformation("Deleting flight passenger {FlightPassengerId}", id);

            try
            {
                var flightPassenger = await _flightPassengerRepository.GetByIdAsync(id);
                if (flightPassenger == null)
                {
                    _logger.LogWarning("Flight passenger {FlightPassengerId} not found for deletion", id);
                    return false;
                }

                // Check if passenger is already checked in
                if (flightPassenger.IsCheckedIn)
                {
                    throw new FlightSystemException("Check-in хийгдсэн зорчигчийг устгах боломжгүй. Эхлээд check-in цуцлана уу.", "CANNOT_DELETE_CHECKED_IN");
                }

                // Delete associated boarding passes
                var boardingPasses = await _boardingPassRepository.GetByFlightPassengerAsync(id);
                if (boardingPasses != null)
                {
                    await _boardingPassRepository.DeleteAsync(boardingPasses.Id);
                }

                // Delete associated seat assignments
                var seatAssignment = await _seatAssignmentRepository.GetByFlightPassengerAsync(id);
                if (seatAssignment != null)
                {
                    await _seatAssignmentRepository.ReleaseSeatAsync(seatAssignment.Id);
                }

                await _flightPassengerRepository.DeleteAsync(id);

                _logger.LogInformation("Flight passenger {FlightPassengerId} deleted successfully", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting flight passenger {FlightPassengerId}", id);
                throw;
            }
        }

        public async Task<FlightPassengerDto?> GetFlightPassengerByIdAsync(int id)
        {
            try
            {
                var flightPassenger = await _flightPassengerRepository.GetByIdAsync(id);
                return flightPassenger != null ? await MapToFlightPassengerDto(flightPassenger) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting flight passenger {FlightPassengerId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<FlightPassengerDto>> GetAllFlightPassengersAsync()
        {
            try
            {
                var flightPassengers = await _flightPassengerRepository.GetAllAsync();
                var result = new List<FlightPassengerDto>();

                foreach (var fp in flightPassengers)
                {
                    result.Add(await MapToFlightPassengerDto(fp));
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all flight passengers");
                throw;
            }
        }

        public async Task<IEnumerable<FlightPassengerDto>> GetFlightPassengersByFlightAsync(int flightId)
        {
            try
            {
                var flightPassengers = await _flightPassengerRepository.GetPassengersForFlightAsync(flightId);
                var result = new List<FlightPassengerDto>();

                foreach (var fp in flightPassengers)
                {
                    result.Add(await MapToFlightPassengerDto(fp));
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting flight passengers for flight {FlightId}", flightId);
                throw;
            }
        }

        public async Task<IEnumerable<FlightPassengerDto>> GetFlightPassengersByPassengerAsync(int passengerId)
        {
            try
            {
                var flightPassengers = await _flightPassengerRepository.GetFlightsForPassengerAsync(passengerId);
                var result = new List<FlightPassengerDto>();

                foreach (var fp in flightPassengers)
                {
                    result.Add(await MapToFlightPassengerDto(fp));
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting flight passengers for passenger {PassengerId}", passengerId);
                throw;
            }
        }

        public async Task<FlightPassengerDto?> GetFlightPassengerByBookingReferenceAsync(string bookingReference)
        {
            try
            {
                var flightPassenger = await _flightPassengerRepository.GetByBookingReferenceAsync(bookingReference);
                return flightPassenger != null ? await MapToFlightPassengerDto(flightPassenger) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting flight passenger by booking reference {BookingReference}", bookingReference);
                throw;
            }
        }

        public async Task<bool> CancelFlightPassengerAsync(int id, int employeeId)
        {
            _logger.LogInformation("Cancelling flight passenger {FlightPassengerId} by employee {EmployeeId}", id, employeeId);

            try
            {
                var flightPassenger = await _flightPassengerRepository.GetByIdAsync(id);
                if (flightPassenger == null)
                    return false;

                // If checked in, cancel check-in first
                if (flightPassenger.IsCheckedIn)
                {
                    // Delete boarding passes
                    var boardingPass = await _boardingPassRepository.GetByFlightPassengerAsync(id);
                    if (boardingPass != null)
                    {
                        await _boardingPassRepository.DeleteAsync(boardingPass.Id);
                    }

                    // Release seat assignments
                    var seatAssignment = await _seatAssignmentRepository.GetByFlightPassengerAsync(id);
                    if (seatAssignment != null)
                    {
                        await _seatAssignmentRepository.ReleaseSeatAsync(seatAssignment.Id);
                    }

                    // Reset check-in status
                    flightPassenger.IsCheckedIn = false;
                    flightPassenger.CheckinTime = null;
                    flightPassenger.CheckinByEmployeeId = null;
                    flightPassenger.UpdatedAt = DateTime.UtcNow;

                    await _flightPassengerRepository.UpdateAsync(flightPassenger);
                }

                _logger.LogInformation("Flight passenger {FlightPassengerId} cancelled successfully", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling flight passenger {FlightPassengerId}", id);
                throw;
            }
        }

        private async Task<FlightPassengerDto> MapToFlightPassengerDto(FlightPassenger flightPassenger)
        {
            var dto = new FlightPassengerDto
            {
                Id = flightPassenger.Id,
                BookingReference = flightPassenger.BookingReference,
                IsCheckedIn = flightPassenger.IsCheckedIn,
                CheckinTime = flightPassenger.CheckinTime,
                SpecialRequests = flightPassenger.SpecialRequests,
                BaggageInfo = flightPassenger.BaggageInfo
            };

            // Load related data
            if (flightPassenger.Passenger != null)
            {
                dto.Passenger = new PassengerDto
                {
                    Id = flightPassenger.Passenger.Id,
                    PassportNumber = flightPassenger.Passenger.PassportNumber,
                    FirstName = flightPassenger.Passenger.FirstName,
                    LastName = flightPassenger.Passenger.LastName,
                    Nationality = flightPassenger.Passenger.Nationality,
                    DateOfBirth = flightPassenger.Passenger.DateOfBirth,
                    PassengerType = flightPassenger.Passenger.Type.ToString(),
                    Email = flightPassenger.Passenger.Email,
                    Phone = flightPassenger.Passenger.Phone
                };
            }
            else
            {
                var passenger = await _passengerRepository.GetByIdAsync(flightPassenger.PassengerId);
                if (passenger != null)
                {
                    dto.Passenger = new PassengerDto
                    {
                        Id = passenger.Id,
                        PassportNumber = passenger.PassportNumber,
                        FirstName = passenger.FirstName,
                        LastName = passenger.LastName,
                        Nationality = passenger.Nationality,
                        DateOfBirth = passenger.DateOfBirth,
                        PassengerType = passenger.Type.ToString(),
                        Email = passenger.Email,
                        Phone = passenger.Phone
                    };
                }
            }

            if (flightPassenger.Flight != null)
            {
                dto.Flight = new FlightInfoDto
                {
                    Id = flightPassenger.Flight.Id,
                    FlightNumber = flightPassenger.Flight.FlightNumber,
                    DepartureAirport = flightPassenger.Flight.DepartureAirport,
                    ArrivalAirport = flightPassenger.Flight.ArrivalAirport,
                    ScheduledDeparture = flightPassenger.Flight.ScheduledDeparture,
                    ScheduledArrival = flightPassenger.Flight.ScheduledArrival,
                    Status = flightPassenger.Flight.Status.ToString(),
                    GateNumber = flightPassenger.Flight.GateNumber
                };
            }
            else
            {
                var flight = await _flightRepository.GetByIdAsync(flightPassenger.FlightId);
                if (flight != null)
                {
                    dto.Flight = new FlightInfoDto
                    {
                        Id = flight.Id,
                        FlightNumber = flight.FlightNumber,
                        DepartureAirport = flight.DepartureAirport,
                        ArrivalAirport = flight.ArrivalAirport,
                        ScheduledDeparture = flight.ScheduledDeparture,
                        ScheduledArrival = flight.ScheduledArrival,
                        Status = flight.Status.ToString(),
                        GateNumber = flight.GateNumber
                    };
                }
            }

            // Load check-in employee info
            if (flightPassenger.CheckinByEmployeeId.HasValue && flightPassenger.CheckinByEmployee != null)
            {
                dto.CheckinByEmployee = $"{flightPassenger.CheckinByEmployee.FirstName} {flightPassenger.CheckinByEmployee.LastName}";
            }
            else if (flightPassenger.CheckinByEmployeeId.HasValue)
            {
                var employee = await _employeeRepository.GetByIdAsync(flightPassenger.CheckinByEmployeeId.Value);
                if (employee != null)
                {
                    dto.CheckinByEmployee = $"{employee.FirstName} {employee.LastName}";
                }
            }

            // Load assigned seat info
            var seatAssignment = await _seatAssignmentRepository.GetByFlightPassengerAsync(flightPassenger.Id);
            if (seatAssignment != null)
            {
                // You would need to load seat details here if needed
                dto.AssignedSeat = new SeatInfoDto
                {
                    Id = seatAssignment.SeatId,
                    // Add other seat properties as needed
                };
            }

            return dto;
        }
    }
}
