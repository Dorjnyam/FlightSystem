using Microsoft.AspNetCore.Mvc;
using FlightSystem.Shared.Interfaces.Services;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Shared.DTOs.Common;

namespace FlightSystem.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeatController : ControllerBase
{
    private readonly ISeatService _seatService;
    private readonly ILogger<SeatController> _logger;

    public SeatController(ISeatService seatService, ILogger<SeatController> logger)
    {
        _seatService = seatService;
        _logger = logger;
    }

    /// <summary>
    /// Нислэгийн суудлуудын жагсаалт авах
    /// </summary>
    /// <param name="flightId">Нислэгийн ID</param>
    /// <returns>Суудлуудын жагсаалт</returns>
    [HttpGet("flight/{flightId}")]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<SeatInfoDto>>>> GetSeatsByFlight(int flightId)
    {
        try
        {
            var seats = await _seatService.GetSeatsByFlightAsync(flightId);
            return Ok(new ApiResponseDto<IEnumerable<SeatInfoDto>>
            {
                Success = true,
                Data = seats,
                Message = "Суудлуудын жагсаалт амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting seats for flight {FlightId}", flightId);
            return StatusCode(500, new ApiResponseDto<IEnumerable<SeatInfoDto>>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// ID-аар суудлын мэдээллийг авах
    /// </summary>
    /// <param name="id">Суудлын ID</param>
    /// <returns>Суудлын мэдээлэл</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponseDto<SeatInfoDto>>> GetSeat(int id)
    {
        try
        {
            var seat = await _seatService.GetSeatByIdAsync(id);
            if (seat == null)
            {
                return NotFound(new ApiResponseDto<SeatInfoDto>
                {
                    Success = false,
                    Message = "Суудал олдсонгүй"
                });
            }

            var seatInfo = new SeatInfoDto
            {
                Id = seat.Id,
                SeatNumber = seat.SeatNumber,
                Row = seat.Row,
                Column = seat.Column,
                SeatClass = seat.SeatClass,
                IsAvailable = true, // This should be determined based on assignments
                IsWindowSeat = seat.IsWindowSeat,
                IsAisleSeat = seat.IsAisleSeat,
                IsEmergencyExit = seat.IsEmergencyExit
            };

            return Ok(new ApiResponseDto<SeatInfoDto>
            {
                Success = true,
                Data = seatInfo,
                Message = "Суудлын мэдээлэл амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting seat {SeatId}", id);
            return StatusCode(500, new ApiResponseDto<SeatInfoDto>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Нислэгийн боломжтой суудлууд авах
    /// </summary>
    /// <param name="flightId">Нислэгийн ID</param>
    /// <returns>Боломжтой суудлууд</returns>
    [HttpGet("available/{flightId}")]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<SeatInfoDto>>>> GetAvailableSeats(int flightId)
    {
        try
        {
            var seats = await _seatService.GetAvailableSeatsAsync(flightId);
            return Ok(new ApiResponseDto<IEnumerable<SeatInfoDto>>
            {
                Success = true,
                Data = seats,
                Message = "Боломжтой суудлууд амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting available seats for flight {FlightId}", flightId);
            return StatusCode(500, new ApiResponseDto<IEnumerable<SeatInfoDto>>
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
    /// <returns>Хуваарилалтын үр дүн</returns>
    [HttpPost("assign")]
    public async Task<ActionResult<ApiResponseDto<SeatAssignmentDto>>> AssignSeat([FromBody] AssignSeatRequestDto request)
    {
        try
        {
            var assignment = await _seatService.AssignSeatAsync(request);
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
    /// Суудлын хуваарилалт цуцлах
    /// </summary>
    /// <param name="seatId">Суудлын ID</param>
    /// <returns>Цуцлалтын үр дүн</returns>
    [HttpDelete("unassign/{seatId}")]
    public async Task<ActionResult<ApiResponseDto<bool>>> UnassignSeat(int seatId)
    {
        try
        {
            var result = await _seatService.UnassignSeatAsync(seatId);
            return Ok(new ApiResponseDto<bool>
            {
                Success = true,
                Data = result,
                Message = result ? "Суудлын хуваарилалт амжилттай цуцлагдлаа" : "Цуцлахад алдаа гарлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error unassigning seat {SeatId}", seatId);
            return BadRequest(new ApiResponseDto<bool>
            {
                Success = false,
                Message = "Суудлын хуваарилалт цуцлахад алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Суудлын төлөв өөрчлөх
    /// </summary>
    /// <param name="seatId">Суудлын ID</param>
    /// <param name="isAvailable">Боломжтой эсэх</param>
    /// <returns>Шинэчлэлтийн үр дүн</returns>
    [HttpPut("{seatId}/availability")]
    public async Task<ActionResult<ApiResponseDto<SeatInfoDto>>> UpdateSeatAvailability(int seatId, [FromBody] bool isAvailable)
    {
        try
        {
            var seat = await _seatService.UpdateSeatAvailabilityAsync(seatId, isAvailable);
            return Ok(new ApiResponseDto<SeatInfoDto>
            {
                Success = true,
                Data = seat,
                Message = "Суудлын төлөв амжилттай өөрчлөгдлөө"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating seat availability {SeatId}", seatId);
            return BadRequest(new ApiResponseDto<SeatInfoDto>
            {
                Success = false,
                Message = "Суудлын төлөв өөрчлөхөд алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }
}

