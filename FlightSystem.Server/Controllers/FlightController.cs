using Microsoft.AspNetCore.Mvc;
using FlightSystem.Shared.Interfaces.Services;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Shared.DTOs.Common;
using FlightSystem.Core.Enums;

namespace FlightSystem.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightController : ControllerBase
{
    private readonly IFlightService _flightService;
    private readonly ILogger<FlightController> _logger;

    public FlightController(IFlightService flightService, ILogger<FlightController> logger)
    {
        _flightService = flightService;
        _logger = logger;
    }

    /// <summary>
    /// Бүх нислэгийн мэдээллийг авах
    /// </summary>
    /// <returns>Нислэгүүдийн жагсаалт</returns>
    [HttpGet]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<FlightInfoDto>>>> GetAllFlights()
    {
        try
        {
            var flights = await _flightService.GetAllFlightsAsync();
            return Ok(new ApiResponseDto<IEnumerable<FlightInfoDto>>
            {
                Success = true,
                Data = flights,
                Message = "Нислэгүүд амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all flights");
            return StatusCode(500, new ApiResponseDto<IEnumerable<FlightInfoDto>>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// ID-аар нислэгийн мэдээллийг авах
    /// </summary>
    /// <param name="id">Нислэгийн ID</param>
    /// <returns>Нислэгийн мэдээлэл</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponseDto<FlightInfoDto>>> GetFlight(int id)
    {
        try
        {
            var flight = await _flightService.GetFlightByIdAsync(id);
            if (flight == null)
            {
                return NotFound(new ApiResponseDto<FlightInfoDto>
                {
                    Success = false,
                    Message = "Нислэг олдсонгүй"
                });
            }

            return Ok(new ApiResponseDto<FlightInfoDto>
            {
                Success = true,
                Data = flight,
                Message = "Нислэгийн мэдээлэл амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting flight {FlightId}", id);
            return StatusCode(500, new ApiResponseDto<FlightInfoDto>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Шинэ нислэг үүсгэх
    /// </summary>
    /// <param name="createDto">Нислэг үүсгэх мэдээлэл</param>
    /// <returns>Үүссэн нислэгийн мэдээлэл</returns>
    [HttpPost]
    public async Task<ActionResult<ApiResponseDto<FlightInfoDto>>> CreateFlight([FromBody] CreateFlightDto createDto)
    {
        try
        {
            var flight = await _flightService.CreateFlightAsync(createDto);
            return CreatedAtAction(nameof(GetFlight), new { id = flight.Id }, new ApiResponseDto<FlightInfoDto>
            {
                Success = true,
                Data = flight,
                Message = "Нислэг амжилттай үүсгэгдлээ"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating flight");
            return BadRequest(new ApiResponseDto<FlightInfoDto>
            {
                Success = false,
                Message = "Нислэг үүсгэхэд алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Нислэгийн төлөв өөрчлөх
    /// </summary>
    /// <param name="id">Нислэгийн ID</param>
    /// <param name="statusDto">Шинэ төлөв</param>
    /// <returns>Шинэчлэгдсэн нислэгийн мэдээлэл</returns>
    [HttpPut("{id}/status")]
    public async Task<ActionResult<ApiResponseDto<FlightInfoDto>>> UpdateFlightStatus(int id, [FromBody] UpdateFlightStatusDto statusDto)
    {
        try
        {
            var flight = await _flightService.UpdateFlightStatusAsync(id, statusDto.Status);
            return Ok(new ApiResponseDto<FlightInfoDto>
            {
                Success = true,
                Data = flight,
                Message = "Нислэгийн төлөв амжилттай өөрчлөгдлөө"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating flight status {FlightId}", id);
            return BadRequest(new ApiResponseDto<FlightInfoDto>
            {
                Success = false,
                Message = "Нислэгийн төлөв өөрчлөхөд алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Өдрөөр нислэгүүдийг авах
    /// </summary>
    /// <param name="date">Огноо (yyyy-MM-dd)</param>
    /// <returns>Тухайн өдрийн нислэгүүд</returns>
    [HttpGet("date/{date:datetime}")]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<FlightInfoDto>>>> GetFlightsByDate(DateTime date)
    {
        try
        {
            var flights = await _flightService.GetFlightsByDateAsync(date);
            return Ok(new ApiResponseDto<IEnumerable<FlightInfoDto>>
            {
                Success = true,
                Data = flights,
                Message = $"{date:yyyy-MM-dd} өдрийн нислэгүүд"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting flights by date {Date}", date);
            return StatusCode(500, new ApiResponseDto<IEnumerable<FlightInfoDto>>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Идэвхтэй нислэгүүдийг авах
    /// </summary>
    /// <returns>Идэвхтэй нислэгүүдийн жагсаалт</returns>
    [HttpGet("active")]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<FlightInfoDto>>>> GetActiveFlights()
    {
        try
        {
            var flights = await _flightService.GetActiveFlightsAsync();
            return Ok(new ApiResponseDto<IEnumerable<FlightInfoDto>>
            {
                Success = true,
                Data = flights,
                Message = "Идэвхтэй нислэгүүд амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting active flights");
            return StatusCode(500, new ApiResponseDto<IEnumerable<FlightInfoDto>>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }
}

