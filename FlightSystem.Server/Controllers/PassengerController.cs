using Microsoft.AspNetCore.Mvc;
using FlightSystem.Shared.Interfaces.Services;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Shared.DTOs.Common;

namespace FlightSystem.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PassengerController : ControllerBase
{
    private readonly IPassengerService _passengerService;
    private readonly ILogger<PassengerController> _logger;

    public PassengerController(IPassengerService passengerService, ILogger<PassengerController> logger)
    {
        _passengerService = passengerService;
        _logger = logger;
    }

    /// <summary>
    /// Бүх зорчигчдийн мэдээллийг авах
    /// </summary>
    /// <returns>Зорчигчдийн жагсаалт</returns>
    [HttpGet]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<PassengerDto>>>> GetAllPassengers()
    {
        try
        {
            var passengers = await _passengerService.GetAllPassengersAsync();
            return Ok(new ApiResponseDto<IEnumerable<PassengerDto>>
            {
                Success = true,
                Data = passengers,
                Message = "Зорчигчдийн жагсаалт амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all passengers");
            return StatusCode(500, new ApiResponseDto<IEnumerable<PassengerDto>>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// ID-аар зорчигчийн мэдээллийг авах
    /// </summary>
    /// <param name="id">Зорчигчийн ID</param>
    /// <returns>Зорчигчийн мэдээлэл</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponseDto<PassengerDto>>> GetPassenger(int id)
    {
        try
        {
            var passenger = await _passengerService.GetPassengerByIdAsync(id);
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
            _logger.LogError(ex, "Error getting passenger {PassengerId}", id);
            return StatusCode(500, new ApiResponseDto<PassengerDto>
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
    [HttpGet("passport/{passportNumber}")]
    public async Task<ActionResult<ApiResponseDto<PassengerDto>>> GetPassengerByPassport(string passportNumber)
    {
        try
        {
            var passenger = await _passengerService.GetPassengerByPassportAsync(passportNumber);
            if (passenger == null)
            {
                return NotFound(new ApiResponseDto<PassengerDto>
                {
                    Success = false,
                    Message = "Пасспортын дугаараар зорчигч олдсонгүй"
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
    /// Шинэ зорчигч үүсгэх
    /// </summary>
    /// <param name="createDto">Зорчигч үүсгэх мэдээлэл</param>
    /// <returns>Үүссэн зорчигчийн мэдээлэл</returns>
    [HttpPost]
    public async Task<ActionResult<ApiResponseDto<PassengerDto>>> CreatePassenger([FromBody] CreatePassengerDto createDto)
    {
        try
        {
            var passenger = await _passengerService.CreatePassengerAsync(createDto);
            return CreatedAtAction(nameof(GetPassenger), new { id = passenger.Id }, new ApiResponseDto<PassengerDto>
            {
                Success = true,
                Data = passenger,
                Message = "Зорчигч амжилттай үүсгэгдлээ"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating passenger");
            return BadRequest(new ApiResponseDto<PassengerDto>
            {
                Success = false,
                Message = "Зорчигч үүсгэхэд алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Зорчигчийн мэдээлэл шинэчлэх
    /// </summary>
    /// <param name="id">Зорчигчийн ID</param>
    /// <param name="updateDto">Шинэчлэх мэдээлэл</param>
    /// <returns>Шинэчлэгдсэн зорчигчийн мэдээлэл</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponseDto<PassengerDto>>> UpdatePassenger(int id, [FromBody] UpdatePassengerDto updateDto)
    {
        try
        {
            var passenger = await _passengerService.UpdatePassengerAsync(id, updateDto);
            return Ok(new ApiResponseDto<PassengerDto>
            {
                Success = true,
                Data = passenger,
                Message = "Зорчигчийн мэдээлэл амжилттай шинэчлэгдлээ"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating passenger {PassengerId}", id);
            return BadRequest(new ApiResponseDto<PassengerDto>
            {
                Success = false,
                Message = "Зорчигчийн мэдээлэл шинэчлэхэд алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Зорчигчийг устгах
    /// </summary>
    /// <param name="id">Зорчигчийн ID</param>
    /// <returns>Устгах үр дүн</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponseDto<bool>>> DeletePassenger(int id)
    {
        try
        {
            var result = await _passengerService.DeletePassengerAsync(id);
            return Ok(new ApiResponseDto<bool>
            {
                Success = true,
                Data = result,
                Message = result ? "Зорчигч амжилттай устгагдлаа" : "Зорчигч устгахад алдаа гарлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting passenger {PassengerId}", id);
            return BadRequest(new ApiResponseDto<bool>
            {
                Success = false,
                Message = "Зорчигч устгахад алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }
}

