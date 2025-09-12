using Microsoft.AspNetCore.Mvc;
using FlightSystem.Shared.Interfaces.Services;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Shared.DTOs.Common;

namespace FlightSystem.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AircraftController : ControllerBase
{
    private readonly IAircraftService _aircraftService;
    private readonly ILogger<AircraftController> _logger;

    public AircraftController(IAircraftService aircraftService, ILogger<AircraftController> logger)
    {
        _aircraftService = aircraftService;
        _logger = logger;
    }

    /// <summary>
    /// Бүх нисэх онгоцны мэдээллийг авах
    /// </summary>
    /// <returns>Нисэх онгоцны жагсаалт</returns>
    [HttpGet]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<AircraftDto>>>> GetAllAircraft()
    {
        try
        {
            var aircraft = await _aircraftService.GetAllAircraftAsync();
            return Ok(new ApiResponseDto<IEnumerable<AircraftDto>>
            {
                Success = true,
                Data = aircraft,
                Message = "Нисэх онгоцны жагсаалт амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all aircraft");
            return StatusCode(500, new ApiResponseDto<IEnumerable<AircraftDto>>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// ID-аар нисэх онгоцны мэдээллийг авах
    /// </summary>
    /// <param name="id">Нисэх онгоцны ID</param>
    /// <returns>Нисэх онгоцны мэдээлэл</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponseDto<AircraftDto>>> GetAircraft(int id)
    {
        try
        {
            var aircraft = await _aircraftService.GetAircraftByIdAsync(id);
            if (aircraft == null)
            {
                return NotFound(new ApiResponseDto<AircraftDto>
                {
                    Success = false,
                    Message = "Нисэх онгоц олдсонгүй"
                });
            }

            return Ok(new ApiResponseDto<AircraftDto>
            {
                Success = true,
                Data = aircraft,
                Message = "Нисэх онгоцны мэдээлэл амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting aircraft {AircraftId}", id);
            return StatusCode(500, new ApiResponseDto<AircraftDto>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Шинэ нисэх онгоц үүсгэх
    /// </summary>
    /// <param name="createDto">Нисэх онгоц үүсгэх мэдээлэл</param>
    /// <returns>Үүссэн нисэх онгоцны мэдээлэл</returns>
    [HttpPost]
    public async Task<ActionResult<ApiResponseDto<AircraftDto>>> CreateAircraft([FromBody] CreateAircraftDto createDto)
    {
        try
        {
            var aircraft = await _aircraftService.CreateAircraftAsync(createDto);
            return CreatedAtAction(nameof(GetAircraft), new { id = aircraft.Id }, new ApiResponseDto<AircraftDto>
            {
                Success = true,
                Data = aircraft,
                Message = "Нисэх онгоц амжилттай үүсгэгдлээ"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating aircraft");
            return BadRequest(new ApiResponseDto<AircraftDto>
            {
                Success = false,
                Message = "Нисэх онгоц үүсгэхэд алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Нисэх онгоцны мэдээлэл шинэчлэх
    /// </summary>
    /// <param name="id">Нисэх онгоцны ID</param>
    /// <param name="updateDto">Шинэчлэх мэдээлэл</param>
    /// <returns>Шинэчлэгдсэн нисэх онгоцны мэдээлэл</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponseDto<AircraftDto>>> UpdateAircraft(int id, [FromBody] UpdateAircraftDto updateDto)
    {
        try
        {
            var aircraft = await _aircraftService.UpdateAircraftAsync(id, updateDto);
            return Ok(new ApiResponseDto<AircraftDto>
            {
                Success = true,
                Data = aircraft,
                Message = "Нисэх онгоцны мэдээлэл амжилттай шинэчлэгдлээ"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating aircraft {AircraftId}", id);
            return BadRequest(new ApiResponseDto<AircraftDto>
            {
                Success = false,
                Message = "Нисэх онгоцны мэдээлэл шинэчлэхэд алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Нисэх онгоцыг устгах
    /// </summary>
    /// <param name="id">Нисэх онгоцны ID</param>
    /// <returns>Устгах үр дүн</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponseDto<bool>>> DeleteAircraft(int id)
    {
        try
        {
            var result = await _aircraftService.DeleteAircraftAsync(id);
            return Ok(new ApiResponseDto<bool>
            {
                Success = true,
                Data = result,
                Message = result ? "Нисэх онгоц амжилттай устгагдлаа" : "Нисэх онгоц устгахад алдаа гарлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting aircraft {AircraftId}", id);
            return BadRequest(new ApiResponseDto<bool>
            {
                Success = false,
                Message = "Нисэх онгоц устгахад алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Нисэх онгоцны суудлуудын жагсаалт авах
    /// </summary>
    /// <param name="id">Нисэх онгоцны ID</param>
    /// <returns>Суудлуудын жагсаалт</returns>
    [HttpGet("{id}/seats")]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<SeatInfoDto>>>> GetAircraftSeats(int id)
    {
        try
        {
            var seats = await _aircraftService.GetAircraftSeatsAsync(id);
            return Ok(new ApiResponseDto<IEnumerable<SeatInfoDto>>
            {
                Success = true,
                Data = seats,
                Message = "Нисэх онгоцны суудлуудын жагсаалт амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting seats for aircraft {AircraftId}", id);
            return StatusCode(500, new ApiResponseDto<IEnumerable<SeatInfoDto>>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }
}
