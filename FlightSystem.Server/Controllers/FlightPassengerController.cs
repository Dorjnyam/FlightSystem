using Microsoft.AspNetCore.Mvc;
using FlightSystem.Shared.Interfaces.Services;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Shared.DTOs.Common;

namespace FlightSystem.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightPassengerController : ControllerBase
{
    private readonly IFlightPassengerService _flightPassengerService;
    private readonly ICheckinService _checkinService;
    private readonly ILogger<FlightPassengerController> _logger;

    public FlightPassengerController(
        IFlightPassengerService flightPassengerService,
        ICheckinService checkinService, 
        ILogger<FlightPassengerController> logger)
    {
        _flightPassengerService = flightPassengerService;
        _checkinService = checkinService;
        _logger = logger;
    }

    /// <summary>
    /// Зорчигч нислэгт бүртгэлтэй эсэхийг шалгах
    /// </summary>
    /// <param name="flightId">Нислэгийн ID</param>
    /// <param name="passportNumber">Пасспортын дугаар</param>
    /// <returns>Бүртгэлийн мэдээлэл</returns>
    [HttpGet("validate/{flightId}/{passportNumber}")]
    public async Task<ActionResult<ApiResponseDto<FlightPassengerValidationDto>>> ValidateFlightPassenger(int flightId, string passportNumber)
    {
        try
        {
            var validation = await _checkinService.ValidateFlightPassengerAsync(flightId, passportNumber);
            return Ok(new ApiResponseDto<FlightPassengerValidationDto>
            {
                Success = true,
                Data = validation,
                Message = "Зорчигчийн бүртгэл шалгалт амжилттай хийгдлээ"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating flight passenger for flight {FlightId}, passport {PassportNumber}", 
                flightId, passportNumber);
            return StatusCode(500, new ApiResponseDto<FlightPassengerValidationDto>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Нислэгийн бүртгэлтэй зорчигчдын жагсаалт авах
    /// </summary>
    /// <param name="flightId">Нислэгийн ID</param>
    /// <returns>Бүртгэлтэй зорчигчдын жагсаалт</returns>
    [HttpGet("flight/{flightId}")]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<FlightPassengerDto>>>> GetFlightPassengers(int flightId)
    {
        try
        {
            var passengers = await _checkinService.GetFlightPassengersAsync(flightId);
            return Ok(new ApiResponseDto<IEnumerable<FlightPassengerDto>>
            {
                Success = true,
                Data = passengers,
                Message = "Нислэгийн бүртгэлтэй зорчигчдын жагсаалт амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting flight passengers for flight {FlightId}", flightId);
            return StatusCode(500, new ApiResponseDto<IEnumerable<FlightPassengerDto>>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Зорчигчийн нислэгийн бүртгэл авах
    /// </summary>
    /// <param name="passportNumber">Пасспортын дугаар</param>
    /// <returns>Зорчигчийн нислэгийн бүртгэлүүд</returns>
    [HttpGet("passenger-by-passport/{passportNumber}")]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<FlightPassengerDto>>>> GetPassengerFlights(string passportNumber)
    {
        try
        {
            var flights = await _checkinService.GetPassengerFlightsAsync(passportNumber);
            return Ok(new ApiResponseDto<IEnumerable<FlightPassengerDto>>
            {
                Success = true,
                Data = flights,
                Message = "Зорчигчийн нислэгийн бүртгэлүүд амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting passenger flights for passport {PassportNumber}", passportNumber);
            return StatusCode(500, new ApiResponseDto<IEnumerable<FlightPassengerDto>>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Booking reference-ээр нислэгийн бүртгэл авах
    /// </summary>
    /// <param name="bookingReference">Бүртгэлийн дугаар</param>
    /// <returns>Нислэгийн бүртгэл</returns>
    [HttpGet("booking/{bookingReference}")]
    public async Task<ActionResult<ApiResponseDto<FlightPassengerDto>>> GetByBookingReference(string bookingReference)
    {
        try
        {
            var flightPassenger = await _checkinService.GetFlightPassengerByBookingReferenceAsync(bookingReference);
            if (flightPassenger == null)
            {
                return NotFound(new ApiResponseDto<FlightPassengerDto>
                {
                    Success = false,
                    Message = "Бүртгэлийн дугаараар нислэгийн бүртгэл олдсонгүй"
                });
            }

            return Ok(new ApiResponseDto<FlightPassengerDto>
            {
                Success = true,
                Data = flightPassenger,
                Message = "Нислэгийн бүртгэл амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting flight passenger by booking reference {BookingReference}", bookingReference);
            return StatusCode(500, new ApiResponseDto<FlightPassengerDto>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    // ========== CRUD OPERATIONS ==========

    /// <summary>
    /// Бүх нислэгийн бүртгэлүүдийг авах
    /// </summary>
    /// <returns>Нислэгийн бүртгэлүүдийн жагсаалт</returns>
    [HttpGet]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<FlightPassengerDto>>>> GetAllFlightPassengers()
    {
        try
        {
            var flightPassengers = await _flightPassengerService.GetAllFlightPassengersAsync();
            return Ok(new ApiResponseDto<IEnumerable<FlightPassengerDto>>
            {
                Success = true,
                Data = flightPassengers,
                Message = "Нислэгийн бүртгэлүүдийн жагсаалт амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all flight passengers");
            return StatusCode(500, new ApiResponseDto<IEnumerable<FlightPassengerDto>>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// ID-аар нислэгийн бүртгэл авах
    /// </summary>
    /// <param name="id">FlightPassenger ID</param>
    /// <returns>Нислэгийн бүртгэл</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponseDto<FlightPassengerDto>>> GetFlightPassenger(int id)
    {
        try
        {
            var flightPassenger = await _flightPassengerService.GetFlightPassengerByIdAsync(id);
            if (flightPassenger == null)
            {
                return NotFound(new ApiResponseDto<FlightPassengerDto>
                {
                    Success = false,
                    Message = "Нислэгийн бүртгэл олдсонгүй"
                });
            }

            return Ok(new ApiResponseDto<FlightPassengerDto>
            {
                Success = true,
                Data = flightPassenger,
                Message = "Нислэгийн бүртгэл амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting flight passenger {FlightPassengerId}", id);
            return StatusCode(500, new ApiResponseDto<FlightPassengerDto>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Шинэ нислэгийн бүртгэл үүсгэх
    /// </summary>
    /// <param name="createDto">Бүртгэл үүсгэх мэдээлэл</param>
    /// <returns>Үүссэн бүртгэл</returns>
    [HttpPost]
    public async Task<ActionResult<ApiResponseDto<FlightPassengerDto>>> CreateFlightPassenger([FromBody] CreateFlightPassengerDto createDto)
    {
        try
        {
            var flightPassenger = await _flightPassengerService.CreateFlightPassengerAsync(createDto);
            return CreatedAtAction(nameof(GetFlightPassenger), new { id = flightPassenger.Id }, new ApiResponseDto<FlightPassengerDto>
            {
                Success = true,
                Data = flightPassenger,
                Message = "Нислэгийн бүртгэл амжилттай үүсгэгдлээ"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating flight passenger");
            return BadRequest(new ApiResponseDto<FlightPassengerDto>
            {
                Success = false,
                Message = "Нислэгийн бүртгэл үүсгэхэд алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Пасспортын дугаараар шинэ нислэгийн бүртгэл үүсгэх
    /// </summary>
    /// <param name="createDto">Бүртгэл үүсгэх мэдээлэл (пасспортын дугаартай)</param>
    /// <returns>Үүссэн бүртгэл</returns>
    [HttpPost("by-passport")]
    public async Task<ActionResult<ApiResponseDto<FlightPassengerDto>>> CreateFlightPassengerByPassport([FromBody] CreateFlightPassengerByPassportDto createDto)
    {
        try
        {
            var flightPassenger = await _flightPassengerService.CreateFlightPassengerByPassportAsync(createDto);
            return CreatedAtAction(nameof(GetFlightPassenger), new { id = flightPassenger.Id }, new ApiResponseDto<FlightPassengerDto>
            {
                Success = true,
                Data = flightPassenger,
                Message = "Нислэгийн бүртгэл амжилттай үүсгэгдлээ"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating flight passenger by passport");
            return BadRequest(new ApiResponseDto<FlightPassengerDto>
            {
                Success = false,
                Message = "Нислэгийн бүртгэл үүсгэхэд алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Нислэгийн бүртгэл шинэчлэх
    /// </summary>
    /// <param name="id">FlightPassenger ID</param>
    /// <param name="updateDto">Шинэчлэх мэдээлэл</param>
    /// <returns>Шинэчлэгдсэн бүртгэл</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponseDto<FlightPassengerDto>>> UpdateFlightPassenger(int id, [FromBody] UpdateFlightPassengerDto updateDto)
    {
        try
        {
            var flightPassenger = await _flightPassengerService.UpdateFlightPassengerAsync(id, updateDto);
            return Ok(new ApiResponseDto<FlightPassengerDto>
            {
                Success = true,
                Data = flightPassenger,
                Message = "Нислэгийн бүртгэл амжилттай шинэчлэгдлээ"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating flight passenger {FlightPassengerId}", id);
            return BadRequest(new ApiResponseDto<FlightPassengerDto>
            {
                Success = false,
                Message = "Нислэгийн бүртгэл шинэчлэхэд алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Нислэгийн бүртгэл устгах
    /// </summary>
    /// <param name="id">FlightPassenger ID</param>
    /// <returns>Устгах үр дүн</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponseDto<bool>>> DeleteFlightPassenger(int id)
    {
        try
        {
            var result = await _flightPassengerService.DeleteFlightPassengerAsync(id);
            return Ok(new ApiResponseDto<bool>
            {
                Success = true,
                Data = result,
                Message = result ? "Нислэгийн бүртгэл амжилттай устгагдлаа" : "Нислэгийн бүртгэл устгахад алдаа гарлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting flight passenger {FlightPassengerId}", id);
            return BadRequest(new ApiResponseDto<bool>
            {
                Success = false,
                Message = "Нислэгийн бүртгэл устгахад алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Нислэгийн бүртгэл цуцлах (soft delete - check-in цуцлах)
    /// </summary>
    /// <param name="id">FlightPassenger ID</param>
    /// <param name="employeeId">Ажилтны ID</param>
    /// <returns>Цуцлалтын үр дүн</returns>
    [HttpPost("{id}/cancel")]
    public async Task<ActionResult<ApiResponseDto<bool>>> CancelFlightPassenger(int id, [FromBody] int employeeId)
    {
        try
        {
            var result = await _flightPassengerService.CancelFlightPassengerAsync(id, employeeId);
            return Ok(new ApiResponseDto<bool>
            {
                Success = true,
                Data = result,
                Message = result ? "Нислэгийн бүртгэл амжилттай цуцлагдлаа" : "Нислэгийн бүртгэл цуцлахад алдаа гарлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cancelling flight passenger {FlightPassengerId}", id);
            return BadRequest(new ApiResponseDto<bool>
            {
                Success = false,
                Message = "Нислэгийн бүртгэл цуцлахад алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Зорчигчийн бүх нислэгийн бүртгэлүүд авах
    /// </summary>
    /// <param name="passengerId">Зорчигчийн ID</param>
    /// <returns>Зорчигчийн нислэгийн бүртгэлүүд</returns>
    [HttpGet("passenger-by-id/{passengerId}")]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<FlightPassengerDto>>>> GetFlightPassengersByPassenger(int passengerId)
    {
        try
        {
            var flightPassengers = await _flightPassengerService.GetFlightPassengersByPassengerAsync(passengerId);
            return Ok(new ApiResponseDto<IEnumerable<FlightPassengerDto>>
            {
                Success = true,
                Data = flightPassengers,
                Message = "Зорчигчийн нислэгийн бүртгэлүүд амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting flight passengers for passenger {PassengerId}", passengerId);
            return StatusCode(500, new ApiResponseDto<IEnumerable<FlightPassengerDto>>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }
}
