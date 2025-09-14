using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Shared.DTOs.Common;
using FlightSystem.Core.Enums;

namespace FlightSystem.CheckinApp.Services
{
    /// <summary>
    /// Service class to handle all API communication with the server
    /// </summary>
    public class CheckinService : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private bool _disposed = false;

        public CheckinService(string serverUrl = "https://localhost:7261")
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(serverUrl) };
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        #region Flight Operations

        /// <summary>
        /// Get all active flights
        /// </summary>
        /// <returns>List of flights</returns>
        public async Task<ApiResult<List<FlightInfoDto>>> GetActiveFlightsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/flight/active");
                var content = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightInfoDto[]>>(content, _jsonOptions);
                
                if (result?.Success == true && result.Data != null)
                {
                    return ApiResult<List<FlightInfoDto>>.Success(result.Data.ToList());
                }
                
                return ApiResult<List<FlightInfoDto>>.Failure(result?.Message ?? "Unknown error");
            }
            catch (Exception ex)
            {
                return ApiResult<List<FlightInfoDto>>.Failure($"Error loading flights: {ex.Message}");
            }
        }

        /// <summary>
        /// Get flight by ID
        /// </summary>
        /// <param name="flightId">Flight ID</param>
        /// <returns>Flight information</returns>
        public async Task<ApiResult<FlightInfoDto?>> GetFlightByIdAsync(int flightId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/flight/{flightId}");
                var content = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightInfoDto>>(content, _jsonOptions);
                
                if (result?.Success == true)
                {
                    return ApiResult<FlightInfoDto?>.Success(result.Data);
                }
                
                return ApiResult<FlightInfoDto?>.Failure(result?.Message ?? "Flight not found");
            }
            catch (Exception ex)
            {
                return ApiResult<FlightInfoDto?>.Failure($"Error loading flight: {ex.Message}");
            }
        }

        /// <summary>
        /// Create new flight
        /// </summary>
        /// <param name="createDto">Flight creation data</param>
        /// <returns>Created flight</returns>
        public async Task<ApiResult<FlightInfoDto?>> CreateFlightAsync(CreateFlightDto createDto)
        {
            try
            {
                var json = JsonSerializer.Serialize(createDto, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("/api/flight", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightInfoDto>>(responseContent, _jsonOptions);
                
                if (result?.Success == true)
                {
                    return ApiResult<FlightInfoDto?>.Success(result.Data);
                }
                
                return ApiResult<FlightInfoDto?>.Failure(result?.Message ?? "Failed to create flight");
            }
            catch (Exception ex)
            {
                return ApiResult<FlightInfoDto?>.Failure($"Error creating flight: {ex.Message}");
            }
        }

        /// <summary>
        /// Update flight status
        /// </summary>
        /// <param name="flightId">Flight ID</param>
        /// <param name="newStatus">New flight status</param>
        /// <param name="employeeId">Employee making the change</param>
        /// <returns>Updated flight</returns>
        public async Task<ApiResult<FlightInfoDto?>> UpdateFlightStatusAsync(int flightId, FlightStatus newStatus, int employeeId)
        {
            try
            {
                var updateDto = new UpdateFlightStatusDto
                {
                    Status = newStatus,
                    UpdatedByEmployeeId = employeeId
                };

                var json = JsonSerializer.Serialize(updateDto, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/flight/{flightId}/status", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightInfoDto>>(responseContent, _jsonOptions);

                if (result?.Success == true)
                {
                    return ApiResult<FlightInfoDto?>.Success(result.Data);
                }
                
                return ApiResult<FlightInfoDto?>.Failure(result?.Message ?? "Failed to update flight status");
            }
            catch (Exception ex)
            {
                return ApiResult<FlightInfoDto?>.Failure($"Error updating flight status: {ex.Message}");
            }
        }

        #endregion

        #region Passenger Operations

        /// <summary>
        /// Get all passengers
        /// </summary>
        /// <returns>List of passengers</returns>
        public async Task<ApiResult<List<PassengerDto>>> GetPassengersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/passenger");
                var content = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<PassengerDto[]>>(content, _jsonOptions);
                
                if (result?.Success == true && result.Data != null)
                {
                    return ApiResult<List<PassengerDto>>.Success(result.Data.ToList());
                }
                
                return ApiResult<List<PassengerDto>>.Failure(result?.Message ?? "Unknown error");
            }
            catch (Exception ex)
            {
                return ApiResult<List<PassengerDto>>.Failure($"Error loading passengers: {ex.Message}");
            }
        }

        /// <summary>
        /// Get passenger by passport number
        /// </summary>
        /// <param name="passportNumber">Passport number</param>
        /// <returns>Passenger information</returns>
        public async Task<ApiResult<PassengerDto?>> GetPassengerByPassportAsync(string passportNumber)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/passenger/passport/{passportNumber}");
                var content = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<PassengerDto>>(content, _jsonOptions);
                
                if (result?.Success == true)
                {
                    return ApiResult<PassengerDto?>.Success(result.Data);
                }
                
                return ApiResult<PassengerDto?>.Failure(result?.Message ?? "Passenger not found");
            }
            catch (Exception ex)
            {
                return ApiResult<PassengerDto?>.Failure($"Error finding passenger: {ex.Message}");
            }
        }

        /// <summary>
        /// Create new passenger
        /// </summary>
        /// <param name="createDto">Passenger creation data</param>
        /// <returns>Created passenger</returns>
        public async Task<ApiResult<PassengerDto?>> CreatePassengerAsync(CreatePassengerDto createDto)
        {
            try
            {
                var json = JsonSerializer.Serialize(createDto, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("/api/passenger", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<PassengerDto>>(responseContent, _jsonOptions);
                
                if (result?.Success == true)
                {
                    return ApiResult<PassengerDto?>.Success(result.Data);
                }
                
                return ApiResult<PassengerDto?>.Failure(result?.Message ?? "Failed to create passenger");
            }
            catch (Exception ex)
            {
                return ApiResult<PassengerDto?>.Failure($"Error creating passenger: {ex.Message}");
            }
        }

        #endregion

        #region FlightPassenger Operations

        /// <summary>
        /// Get all flight passengers (bookings)
        /// </summary>
        /// <returns>List of flight passenger bookings</returns>
        public async Task<ApiResult<List<FlightPassengerDto>>> GetFlightPassengersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/FlightPassenger");
                var content = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightPassengerDto[]>>(content, _jsonOptions);
                
                if (result?.Success == true && result.Data != null)
                {
                    return ApiResult<List<FlightPassengerDto>>.Success(result.Data.ToList());
                }
                
                return ApiResult<List<FlightPassengerDto>>.Failure(result?.Message ?? "Unknown error");
            }
            catch (Exception ex)
            {
                return ApiResult<List<FlightPassengerDto>>.Failure($"Error loading flight passengers: {ex.Message}");
            }
        }

        /// <summary>
        /// Create new flight passenger booking
        /// </summary>
        /// <param name="createDto">Booking creation data</param>
        /// <returns>Created booking</returns>
        public async Task<ApiResult<FlightPassengerDto?>> CreateFlightPassengerAsync(CreateFlightPassengerDto createDto)
        {
            try
            {
                var json = JsonSerializer.Serialize(createDto, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("/api/FlightPassenger", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightPassengerDto>>(responseContent, _jsonOptions);
                
                if (result?.Success == true)
                {
                    return ApiResult<FlightPassengerDto?>.Success(result.Data);
                }
                
                return ApiResult<FlightPassengerDto?>.Failure(result?.Message ?? "Failed to create booking");
            }
            catch (Exception ex)
            {
                return ApiResult<FlightPassengerDto?>.Failure($"Error creating booking: {ex.Message}");
            }
        }

        /// <summary>
        /// Update flight passenger booking
        /// </summary>
        /// <param name="id">Booking ID</param>
        /// <param name="updateDto">Update data</param>
        /// <returns>Updated booking</returns>
        public async Task<ApiResult<FlightPassengerDto?>> UpdateFlightPassengerAsync(int id, UpdateFlightPassengerDto updateDto)
        {
            try
            {
                var json = JsonSerializer.Serialize(updateDto, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PutAsync($"/api/FlightPassenger/{id}", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightPassengerDto>>(responseContent, _jsonOptions);
                
                if (result?.Success == true)
                {
                    return ApiResult<FlightPassengerDto?>.Success(result.Data);
                }
                
                return ApiResult<FlightPassengerDto?>.Failure(result?.Message ?? "Failed to update booking");
            }
            catch (Exception ex)
            {
                return ApiResult<FlightPassengerDto?>.Failure($"Error updating booking: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete flight passenger booking
        /// </summary>
        /// <param name="id">Booking ID</param>
        /// <returns>Success result</returns>
        public async Task<ApiResult<bool>> DeleteFlightPassengerAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/FlightPassenger/{id}");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<bool>>(responseContent, _jsonOptions);
                
                if (result?.Success == true)
                {
                    return ApiResult<bool>.Success(result.Data);
                }
                
                return ApiResult<bool>.Failure(result?.Message ?? "Failed to delete booking");
            }
            catch (Exception ex)
            {
                return ApiResult<bool>.Failure($"Error deleting booking: {ex.Message}");
            }
        }

        #endregion

        #region Check-in Operations

        /// <summary>
        /// Validate flight passenger for check-in
        /// </summary>
        /// <param name="flightId">Flight ID</param>
        /// <param name="passportNumber">Passenger passport number</param>
        /// <returns>Validation result</returns>
        public async Task<ApiResult<FlightPassengerValidationDto?>> ValidateFlightPassengerAsync(int flightId, string passportNumber)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Checkin/validate-flight-passenger/{flightId}/{passportNumber}");
                var content = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightPassengerValidationDto>>(content, _jsonOptions);
                
                if (result?.Success == true)
                {
                    return ApiResult<FlightPassengerValidationDto?>.Success(result.Data);
                }
                
                return ApiResult<FlightPassengerValidationDto?>.Failure(result?.Message ?? "Validation failed");
            }
            catch (Exception ex)
            {
                return ApiResult<FlightPassengerValidationDto?>.Failure($"Error validating passenger: {ex.Message}");
            }
        }

        /// <summary>
        /// Get check-in eligibility
        /// </summary>
        /// <param name="flightId">Flight ID</param>
        /// <param name="passportNumber">Passenger passport number</param>
        /// <returns>Eligibility result</returns>
        public async Task<ApiResult<CheckinEligibilityDto?>> GetCheckinEligibilityAsync(int flightId, string passportNumber)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Checkin/eligibility/{flightId}/{passportNumber}");
                var content = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<CheckinEligibilityDto>>(content, _jsonOptions);
                
                if (result?.Success == true)
                {
                    return ApiResult<CheckinEligibilityDto?>.Success(result.Data);
                }
                
                return ApiResult<CheckinEligibilityDto?>.Failure(result?.Message ?? "Eligibility check failed");
            }
            catch (Exception ex)
            {
                return ApiResult<CheckinEligibilityDto?>.Failure($"Error checking eligibility: {ex.Message}");
            }
        }

        /// <summary>
        /// Perform passenger check-in
        /// </summary>
        /// <param name="request">Check-in request</param>
        /// <returns>Check-in result</returns>
        public async Task<ApiResult<CheckinResultDto?>> CheckinPassengerAsync(CheckinRequestDto request)
        {
            try
            {
                var json = JsonSerializer.Serialize(request, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                // Debug: Log the request
                System.Diagnostics.Debug.WriteLine($"Checkin Request: {json}");
                
                var response = await _httpClient.PostAsync("/api/Checkin/passenger", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                // Debug: Log the response
                System.Diagnostics.Debug.WriteLine($"Checkin Response: {response.StatusCode} - {responseContent}");
                
                if (!response.IsSuccessStatusCode)
                {
                    return ApiResult<CheckinResultDto?>.Failure($"HTTP Error: {response.StatusCode} - {responseContent}");
                }
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<CheckinResultDto>>(responseContent, _jsonOptions);
                
                if (result?.Success == true)
                {
                    return ApiResult<CheckinResultDto?>.Success(result.Data);
                }
                
                return ApiResult<CheckinResultDto?>.Failure(result?.Message ?? "Check-in failed");
            }
            catch (Exception ex)
            {
                return ApiResult<CheckinResultDto?>.Failure($"Error during check-in: {ex.Message}");
            }
        }

        /// <summary>
        /// Get seat map for flight
        /// </summary>
        /// <param name="flightId">Flight ID</param>
        /// <returns>Seat map</returns>
        public async Task<ApiResult<SeatMapDto?>> GetSeatMapAsync(int flightId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Checkin/seatmap/{flightId}");
                var content = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<SeatMapDto>>(content, _jsonOptions);
                
                if (result?.Success == true)
                {
                    return ApiResult<SeatMapDto?>.Success(result.Data);
                }
                
                return ApiResult<SeatMapDto?>.Failure(result?.Message ?? "Failed to load seat map");
            }
            catch (Exception ex)
            {
                return ApiResult<SeatMapDto?>.Failure($"Error loading seat map: {ex.Message}");
            }
        }

        /// <summary>
        /// Get boarding pass
        /// </summary>
        /// <param name="flightId">Flight ID</param>
        /// <param name="passportNumber">Passenger passport number</param>
        /// <returns>Boarding pass</returns>
        public async Task<ApiResult<BoardingPassDto?>> GetBoardingPassAsync(int flightId, string passportNumber)
        {
            try
            {
                // Since the boarding-pass endpoint doesn't exist, we'll get the information
                // from the flight passenger validation which includes boarding pass status
                System.Diagnostics.Debug.WriteLine($"Getting boarding pass info from validation data for flight {flightId}, passport {passportNumber}");
                
                // Get the flight passenger validation data which should include boarding pass info
                var validationResult = await ValidateFlightPassengerAsync(flightId, passportNumber);
                
                if (validationResult.IsSuccess && validationResult.Data != null)
                {
                    // Check if passenger has boarding pass
                    if (validationResult.Data.HasBoardingPass)
                    {
                        // Create boarding pass DTO from validation data
                        var boardingPass = new BoardingPassDto
                        {
                            BoardingPassCode = validationResult.Data.BookingReference ?? $"BP{DateTime.Now:yyyyMMddHHmmss}",
                            IssuedAt = validationResult.Data.CheckinTime ?? DateTime.UtcNow,
                            IssuedByEmployee = "System", // We don't have this info from validation
                            BoardingTime = validationResult.Data.Flight?.ScheduledDeparture.AddMinutes(-30) ?? DateTime.UtcNow.AddHours(1),
                            IsBoardingComplete = false,
                            Gate = validationResult.Data.Flight?.GateNumber ?? "TBA",
                            Flight = validationResult.Data.Flight,
                            Passenger = validationResult.Data.Passenger,
                            Seat = validationResult.Data.FlightPassenger?.AssignedSeat,
                            QRCode = $"QR_{validationResult.Data.BookingReference}_{DateTime.Now:yyyyMMdd}",
                            BarcodeData = $"BAR_{validationResult.Data.BookingReference}_{DateTime.Now:yyyyMMdd}"
                        };
                        
                        System.Diagnostics.Debug.WriteLine($"Boarding pass created from validation data");
                        return ApiResult<BoardingPassDto?>.Success(boardingPass);
                    }
                    else
                    {
                        return ApiResult<BoardingPassDto?>.Failure("Passenger does not have a boarding pass");
                    }
                }
                
                return ApiResult<BoardingPassDto?>.Failure(validationResult.ErrorMessage ?? "Failed to get boarding pass information");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Boarding Pass Exception: {ex.Message}");
                return ApiResult<BoardingPassDto?>.Failure($"Error getting boarding pass: {ex.Message}");
            }
        }

        /// <summary>
        /// Assign seat to passenger
        /// </summary>
        /// <param name="request">Seat assignment request</param>
        /// <returns>Seat assignment result</returns>
        public async Task<ApiResult<SeatAssignmentDto?>> AssignSeatAsync(AssignSeatRequestDto request)
        {
            try
            {
                var json = JsonSerializer.Serialize(request, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("/api/Checkin/assign-seat", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<SeatAssignmentDto>>(responseContent, _jsonOptions);
                
                if (result?.Success == true)
                {
                    return ApiResult<SeatAssignmentDto?>.Success(result.Data);
                }
                
                return ApiResult<SeatAssignmentDto?>.Failure(result?.Message ?? "Seat assignment failed");
            }
            catch (Exception ex)
            {
                return ApiResult<SeatAssignmentDto?>.Failure($"Error assigning seat: {ex.Message}");
            }
        }

        #endregion

        #region Aircraft Operations

        /// <summary>
        /// Get all aircraft
        /// </summary>
        /// <returns>List of aircraft</returns>
        public async Task<ApiResult<List<AircraftDto>>> GetAircraftAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/aircraft");
                var content = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<AircraftDto[]>>(content, _jsonOptions);
                
                if (result?.Success == true && result.Data != null)
                {
                    return ApiResult<List<AircraftDto>>.Success(result.Data.ToList());
                }
                
                return ApiResult<List<AircraftDto>>.Failure(result?.Message ?? "Unknown error");
            }
            catch (Exception ex)
            {
                return ApiResult<List<AircraftDto>>.Failure($"Error loading aircraft: {ex.Message}");
            }
        }

        #endregion

        #region Employee Operations

        /// <summary>
        /// Authenticate employee
        /// </summary>
        /// <param name="loginDto">Login credentials</param>
        /// <returns>Authentication result</returns>
        public async Task<ApiResult<EmployeeLoginResultDto?>> AuthenticateAsync(EmployeeLoginDto loginDto)
        {
            try
            {
                var json = JsonSerializer.Serialize(loginDto, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("/api/employee/login", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<EmployeeLoginResultDto>>(responseContent, _jsonOptions);
                
                if (result?.Success == true)
                {
                    return ApiResult<EmployeeLoginResultDto?>.Success(result.Data);
                }
                
                return ApiResult<EmployeeLoginResultDto?>.Failure(result?.Message ?? "Authentication failed");
            }
            catch (Exception ex)
            {
                return ApiResult<EmployeeLoginResultDto?>.Failure($"Error during authentication: {ex.Message}");
            }
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _httpClient?.Dispose();
                _disposed = true;
            }
        }

        /// <summary>
        /// Test concurrent seat assignment - for testing purposes only
        /// </summary>
        /// <param name="flightId">Flight ID</param>
        /// <param name="seatNumber">Seat number to test</param>
        /// <param name="passenger1Passport">First passenger passport</param>
        /// <param name="passenger2Passport">Second passenger passport</param>
        /// <returns>Test results</returns>
        public async Task<ApiResult<ConcurrentSeatTestResultDto>> TestConcurrentSeatAssignmentAsync(
            int flightId, string seatNumber, string passenger1Passport, string passenger2Passport)
        {
            try
            {
                var testRequest = new ConcurrentSeatTestRequestDto
                {
                    FlightId = flightId,
                    SeatNumber = seatNumber,
                    Passenger1Passport = passenger1Passport,
                    Passenger2Passport = passenger2Passport
                };

                var json = JsonSerializer.Serialize(testRequest, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Checkin/test-concurrent-seat", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<ApiResponseDto<ConcurrentSeatTestResultDto>>(responseContent, _jsonOptions);
                    if (result?.Success == true)
                    {
                        return ApiResult<ConcurrentSeatTestResultDto>.Success(result.Data);
                    }
                }

                return ApiResult<ConcurrentSeatTestResultDto>.Failure($"Test failed: {response.StatusCode} - {responseContent}");
            }
            catch (Exception ex)
            {
                return ApiResult<ConcurrentSeatTestResultDto>.Failure($"Error running concurrent test: {ex.Message}");
            }
        }

        #endregion
    }

    /// <summary>
    /// Generic API result wrapper
    /// </summary>
    /// <typeparam name="T">Result data type</typeparam>
    public class ApiResult<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Data { get; private set; }
        public string? ErrorMessage { get; private set; }
        public List<string> Errors { get; private set; } = new();

        private ApiResult(bool isSuccess, T? data, string? errorMessage, List<string>? errors = null)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
            Errors = errors ?? new List<string>();
        }

        public static ApiResult<T> Success(T data)
        {
            return new ApiResult<T>(true, data, null);
        }

        public static ApiResult<T> Failure(string errorMessage, List<string>? errors = null)
        {
            return new ApiResult<T>(false, default(T), errorMessage, errors);
        }

        public static ApiResult<T> Failure(List<string> errors)
        {
            return new ApiResult<T>(false, default(T), string.Join(", ", errors), errors);
        }
    }
}
