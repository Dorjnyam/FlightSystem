using Microsoft.AspNetCore.Mvc;
using FlightSystem.Shared.Interfaces.Services;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Shared.DTOs.Common;

namespace FlightSystem.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckinController : ControllerBase
{
    private readonly ICheckinService _checkinService;
    private readonly ILogger<CheckinController> _logger;

    public CheckinController(ICheckinService checkinService, ILogger<CheckinController> logger)
    {
        _checkinService = checkinService;
        _logger = logger;
    }

    /// <summary>
    /// Зорчигчийн check-in хийх
    /// </summary>
    /// <param name="request">Check-in хүсэлт</param>
    /// <returns>Check-in үр дүн</returns>
    [HttpPost("passenger")]
    public async Task<ActionResult<ApiResponseDto<CheckinResultDto>>> CheckinPassenger([FromBody] CheckinRequestDto request)
    {
        try
        {
            var result = await _checkinService.CheckinPassengerAsync(request);
            
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponseDto<CheckinResultDto>
                {
                    Success = false,
                    Data = result,
                    Message = "Check-in амжилтгүй",
                    Errors = result.Errors
                });
            }

            return Ok(new ApiResponseDto<CheckinResultDto>
            {
                Success = true,
                Data = result,
                Message = "Check-in амжилттай хийгдлээ"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during passenger checkin");
            return StatusCode(500, new ApiResponseDto<CheckinResultDto>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Суудал хуваарилах
    /// </summary>
    /// <param name="request">Суудал хуваарилах хүсэлт</param>
    /// <returns>Суудлын хуваарилалт</returns>
    [HttpPost("assign-seat")]
    public async Task<ActionResult<ApiResponseDto<SeatAssignmentDto>>> AssignSeat([FromBody] AssignSeatRequestDto request)
    {
        try
        {
            var assignment = await _checkinService.AssignSeatAsync(request);
            return Ok(new ApiResponseDto<SeatAssignmentDto>
            {
                Success = true,
                Data = assignment,
                Message = "Суудал амжилттай хуваарилагдлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error assigning seat");
            return BadRequest(new ApiResponseDto<SeatAssignmentDto>
            {
                Success = false,
                Message = "Суудал хуваарилахад алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Нислэгийн суудлын зураглал авах
    /// </summary>
    /// <param name="flightId">Нислэгийн ID</param>
    /// <returns>Суудлын зураглал</returns>
    [HttpGet("seatmap/{flightId}")]
    public async Task<ActionResult<ApiResponseDto<SeatMapDto>>> GetSeatMap(int flightId)
    {
        try
        {
            var seatMap = await _checkinService.GetSeatMapAsync(flightId);
            return Ok(new ApiResponseDto<SeatMapDto>
            {
                Success = true,
                Data = seatMap,
                Message = "Суудлын зураглал амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting seat map for flight {FlightId}", flightId);
            return StatusCode(500, new ApiResponseDto<SeatMapDto>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Check-in боломжтой эсэхийг шалгах
    /// </summary>
    /// <param name="flightId">Нислэгийн ID</param>
    /// <param name="passportNumber">Пасспортын дугаар</param>
    /// <returns>Check-in боломжийн мэдээлэл</returns>
    [HttpGet("eligibility/{flightId}/{passportNumber}")]
    public async Task<ActionResult<ApiResponseDto<CheckinEligibilityDto>>> GetCheckinEligibility(int flightId, string passportNumber)
    {
        try
        {
            var eligibility = await _checkinService.GetCheckinEligibilityAsync(flightId, passportNumber);
            return Ok(new ApiResponseDto<CheckinEligibilityDto>
            {
                Success = true,
                Data = eligibility,
                Message = "Check-in боломжийн мэдээлэл амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting checkin eligibility for flight {FlightId}, passport {PassportNumber}", 
                flightId, passportNumber);
            return StatusCode(500, new ApiResponseDto<CheckinEligibilityDto>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Пасспортын дугаараар зорчигчийг хайх
    /// </summary>
    /// <param name="passportNumber">Пасспортын дугаар</param>
    /// <returns>Зорчигчийн мэдээлэл</returns>
    [HttpGet("passenger/{passportNumber}")]
    public async Task<ActionResult<ApiResponseDto<PassengerDto>>> GetPassengerByPassport(string passportNumber)
    {
        try
        {
            var passenger = await _checkinService.GetPassengerByPassportAsync(passportNumber);
            if (passenger == null)
            {
                return NotFound(new ApiResponseDto<PassengerDto>
                {
                    Success = false,
                    Message = "Зорчигч олдсонгүй"
                });
            }

            return Ok(new ApiResponseDto<PassengerDto>
            {
                Success = true,
                Data = passenger,
                Message = "Зорчигчийн мэдээлэл амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting passenger by passport {PassportNumber}", passportNumber);
            return StatusCode(500, new ApiResponseDto<PassengerDto>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Зорчигч нислэгт бүртгэлтэй эсэхийг шалгах
    /// </summary>
    /// <param name="flightId">Нислэгийн ID</param>
    /// <param name="passportNumber">Пасспортын дугаар</param>
    /// <returns>Бүртгэлийн мэдээлэл</returns>
    [HttpGet("validate-flight-passenger/{flightId}/{passportNumber}")]
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
    [HttpGet("flight-passengers/{flightId}")]
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
    /// Check-in хийгдсэн зорчигчдын жагсаалт авах
    /// </summary>
    /// <param name="flightId">Нислэгийн ID</param>
    /// <returns>Check-in хийгдсэн зорчигчдын жагсаалт</returns>
    [HttpGet("checked-in/{flightId}")]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<PassengerDto>>>> GetCheckedInPassengers(int flightId)
    {
        try
        {
            var passengers = await _checkinService.GetCheckedInPassengersAsync(flightId);
            return Ok(new ApiResponseDto<IEnumerable<PassengerDto>>
            {
                Success = true,
                Data = passengers,
                Message = "Check-in хийгдсэн зорчигчдын жагсаалт амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting checked-in passengers for flight {FlightId}", flightId);
            return StatusCode(500, new ApiResponseDto<IEnumerable<PassengerDto>>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Check-in цуцлах
    /// </summary>
    /// <param name="flightId">Нислэгийн ID</param>
    /// <param name="passengerId">Зорчигчийн ID</param>
    /// <returns>Цуцлалтын үр дүн</returns>
    [HttpDelete("{flightId}/{passengerId}")]
    public async Task<ActionResult<ApiResponseDto<bool>>> CancelCheckin(int flightId, int passengerId)
    {
        try
        {
            var result = await _checkinService.CancelCheckinByFlightAndPassengerAsync(flightId, passengerId);
            return Ok(new ApiResponseDto<bool>
            {
                Success = true,
                Data = result,
                Message = result ? "Check-in амжилттай цуцлагдлаа" : "Check-in цуцлахад алдаа гарлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error canceling checkin for flight {FlightId}, passenger {PassengerId}", 
                flightId, passengerId);
            return BadRequest(new ApiResponseDto<bool>
            {
                Success = false,
                Message = "Check-in цуцлахад алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }
}

