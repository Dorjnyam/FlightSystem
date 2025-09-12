using Microsoft.AspNetCore.Mvc;
using FlightSystem.Shared.Interfaces.Services;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Shared.DTOs.Common;

namespace FlightSystem.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoardingPassController : ControllerBase
{
    private readonly IBoardingPassService _boardingPassService;
    private readonly ILogger<BoardingPassController> _logger;

    public BoardingPassController(IBoardingPassService boardingPassService, ILogger<BoardingPassController> logger)
    {
        _boardingPassService = boardingPassService;
        _logger = logger;
    }

    /// <summary>
    /// ID-аар boarding pass-ийн мэдээллийг авах
    /// </summary>
    /// <param name="id">Boarding pass-ийн ID</param>
    /// <returns>Boarding pass-ийн мэдээлэл</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponseDto<BoardingPassDto>>> GetBoardingPass(int id)
    {
        try
        {
            var boardingPass = await _boardingPassService.GetBoardingPassByIdAsync(id);
            if (boardingPass == null)
            {
                return NotFound(new ApiResponseDto<BoardingPassDto>
                {
                    Success = false,
                    Message = "Boarding pass олдсонгүй"
                });
            }

            return Ok(new ApiResponseDto<BoardingPassDto>
            {
                Success = true,
                Data = boardingPass,
                Message = "Boarding pass-ийн мэдээлэл амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting boarding pass {BoardingPassId}", id);
            return StatusCode(500, new ApiResponseDto<BoardingPassDto>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Зорчигчийн boarding pass авах
    /// </summary>
    /// <param name="passengerId">Зорчигчийн ID</param>
    /// <param name="flightId">Нислэгийн ID</param>
    /// <returns>Boarding pass мэдээлэл</returns>
    [HttpGet("passenger/{passengerId}/flight/{flightId}")]
    public async Task<ActionResult<ApiResponseDto<BoardingPassDto>>> GetBoardingPassByPassengerAndFlight(int passengerId, int flightId)
    {
        try
        {
            var boardingPass = await _boardingPassService.GetBoardingPassByPassengerAndFlightAsync(passengerId, flightId);
            if (boardingPass == null)
            {
                return NotFound(new ApiResponseDto<BoardingPassDto>
                {
                    Success = false,
                    Message = "Boarding pass олдсонгүй"
                });
            }

            return Ok(new ApiResponseDto<BoardingPassDto>
            {
                Success = true,
                Data = boardingPass,
                Message = "Boarding pass-ийн мэдээлэл амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting boarding pass for passenger {PassengerId}, flight {FlightId}", passengerId, flightId);
            return StatusCode(500, new ApiResponseDto<BoardingPassDto>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Шинэ boarding pass үүсгэх
    /// </summary>
    /// <param name="createDto">Boarding pass үүсгэх мэдээлэл</param>
    /// <returns>Үүссэн boarding pass-ийн мэдээлэл</returns>
    [HttpPost]
    public async Task<ActionResult<ApiResponseDto<BoardingPassDto>>> CreateBoardingPass([FromBody] CreateBoardingPassDto createDto)
    {
        try
        {
            var boardingPass = await _boardingPassService.CreateBoardingPassAsync(createDto);
            return CreatedAtAction(nameof(GetBoardingPass), new { id = boardingPass.Id }, new ApiResponseDto<BoardingPassDto>
            {
                Success = true,
                Data = boardingPass,
                Message = "Boarding pass амжилттай үүсгэгдлээ"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating boarding pass");
            return BadRequest(new ApiResponseDto<BoardingPassDto>
            {
                Success = false,
                Message = "Boarding pass үүсгэхэд алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Boarding pass-ийн QR код авах
    /// </summary>
    /// <param name="id">Boarding pass-ийн ID</param>
    /// <returns>QR кодын зураг</returns>
    [HttpGet("{id}/qrcode")]
    public async Task<ActionResult> GetBoardingPassQRCode(int id)
    {
        try
        {
            var qrCodeBytes = await _boardingPassService.GenerateQRCodeAsync(id);
            return File(qrCodeBytes, "image/png", $"boarding_pass_{id}_qrcode.png");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating QR code for boarding pass {BoardingPassId}", id);
            return StatusCode(500, "QR код үүсгэхэд алдаа гарлаа");
        }
    }

    /// <summary>
    /// Boarding pass хэвлэх
    /// </summary>
    /// <param name="id">Boarding pass-ийн ID</param>
    /// <returns>Хэвлэх боломжтой PDF</returns>
    [HttpGet("{id}/print")]
    public async Task<ActionResult> PrintBoardingPass(int id)
    {
        try
        {
            var pdfBytes = await _boardingPassService.GeneratePrintVersionAsync(id);
            return File(pdfBytes, "application/pdf", $"boarding_pass_{id}.pdf");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating print version for boarding pass {BoardingPassId}", id);
            return StatusCode(500, "Хэвлэх хувилбар үүсгэхэд алдаа гарлаа");
        }
    }

    /// <summary>
    /// Нислэгийн бүх boarding pass-ууд авах
    /// </summary>
    /// <param name="flightId">Нислэгийн ID</param>
    /// <returns>Boarding pass-уудын жагсаалт</returns>
    [HttpGet("flight/{flightId}")]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<BoardingPassDto>>>> GetBoardingPassesByFlight(int flightId)
    {
        try
        {
            var boardingPasses = await _boardingPassService.GetBoardingPassesByFlightAsync(flightId);
            return Ok(new ApiResponseDto<IEnumerable<BoardingPassDto>>
            {
                Success = true,
                Data = boardingPasses,
                Message = "Нислэгийн boarding pass-ууд амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting boarding passes for flight {FlightId}", flightId);
            return StatusCode(500, new ApiResponseDto<IEnumerable<BoardingPassDto>>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Boarding pass-ийн төлөв өөрчлөх
    /// </summary>
    /// <param name="id">Boarding pass-ийн ID</param>
    /// <param name="isUsed">Хэрэглэгдсэн эсэх</param>
    /// <returns>Шинэчлэлтийн үр дүн</returns>
    [HttpPut("{id}/status")]
    public async Task<ActionResult<ApiResponseDto<BoardingPassDto>>> UpdateBoardingPassStatus(int id, [FromBody] bool isUsed)
    {
        try
        {
            var boardingPass = await _boardingPassService.UpdateBoardingPassStatusAsync(id, isUsed);
            return Ok(new ApiResponseDto<BoardingPassDto>
            {
                Success = true,
                Data = boardingPass,
                Message = "Boarding pass-ийн төлөв амжилттай өөрчлөгдлөө"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating boarding pass status {BoardingPassId}", id);
            return BadRequest(new ApiResponseDto<BoardingPassDto>
            {
                Success = false,
                Message = "Boarding pass-ийн төлөв өөрчлөхөд алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }
}
