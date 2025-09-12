using Microsoft.AspNetCore.Mvc;
using FlightSystem.Shared.Interfaces.Services;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Shared.DTOs.Common;

namespace FlightSystem.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    /// <summary>
    /// Бүх ажилтнуудын мэдээллийг авах
    /// </summary>
    /// <returns>Ажилтнуудын жагсаалт</returns>
    [HttpGet]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<EmployeeDto>>>> GetAllEmployees()
    {
        try
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(new ApiResponseDto<IEnumerable<EmployeeDto>>
            {
                Success = true,
                Data = employees,
                Message = "Ажилтнуудын жагсаалт амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all employees");
            return StatusCode(500, new ApiResponseDto<IEnumerable<EmployeeDto>>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// ID-аар ажилтны мэдээллийг авах
    /// </summary>
    /// <param name="id">Ажилтны ID</param>
    /// <returns>Ажилтны мэдээлэл</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponseDto<EmployeeDto>>> GetEmployee(int id)
    {
        try
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound(new ApiResponseDto<EmployeeDto>
                {
                    Success = false,
                    Message = "Ажилтан олдсонгүй"
                });
            }

            return Ok(new ApiResponseDto<EmployeeDto>
            {
                Success = true,
                Data = employee,
                Message = "Ажилтны мэдээлэл амжилттай авлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting employee {EmployeeId}", id);
            return StatusCode(500, new ApiResponseDto<EmployeeDto>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Ажилтны нэвтрэх эрх шалгах
    /// </summary>
    /// <param name="loginDto">Нэвтрэх мэдээлэл</param>
    /// <returns>Нэвтрэх үр дүн</returns>
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponseDto<EmployeeLoginResultDto>>> Login([FromBody] EmployeeLoginDto loginDto)
    {
        try
        {
            var authResult = await _employeeService.AuthenticateAsync(loginDto.EmployeeCode, loginDto.Password);
            
            var result = new EmployeeLoginResultDto
            {
                IsSuccess = authResult?.IsSuccess ?? false,
                Token = authResult?.Token,
                Employee = authResult?.Employee,
                Errors = authResult?.IsSuccess == false ? ["Нэвтрэх эрхгүй"] : []
            };
            
            if (!result.IsSuccess)
            {
                return Unauthorized(new ApiResponseDto<EmployeeLoginResultDto>
                {
                    Success = false,
                    Message = "Нэвтрэх эрхгүй",
                    Errors = result.Errors
                });
            }

            return Ok(new ApiResponseDto<EmployeeLoginResultDto>
            {
                Success = true,
                Data = result,
                Message = "Амжилттай нэвтэрлээ"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during employee login");
            return StatusCode(500, new ApiResponseDto<EmployeeLoginResultDto>
            {
                Success = false,
                Message = "Серверийн алдаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Шинэ ажилтан үүсгэх
    /// </summary>
    /// <param name="createDto">Ажилтан үүсгэх мэдээлэл</param>
    /// <returns>Үүссэн ажилтны мэдээлэл</returns>
    [HttpPost]
    public async Task<ActionResult<ApiResponseDto<EmployeeDto>>> CreateEmployee([FromBody] CreateEmployeeDto createDto)
    {
        try
        {
            var employee = await _employeeService.CreateEmployeeAsync(createDto);
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, new ApiResponseDto<EmployeeDto>
            {
                Success = true,
                Data = employee,
                Message = "Ажилтан амжилттай үүсгэгдлээ"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating employee");
            return BadRequest(new ApiResponseDto<EmployeeDto>
            {
                Success = false,
                Message = "Ажилтан үүсгэхэд алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Ажилтны мэдээлэл шинэчлэх
    /// </summary>
    /// <param name="id">Ажилтны ID</param>
    /// <param name="updateDto">Шинэчлэх мэдээлэл</param>
    /// <returns>Шинэчлэгдсэн ажилтны мэдээлэл</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponseDto<EmployeeDto>>> UpdateEmployee(int id, [FromBody] UpdateEmployeeDto updateDto)
    {
        try
        {
            var employee = await _employeeService.UpdateEmployeeAsync(id, updateDto);
            return Ok(new ApiResponseDto<EmployeeDto>
            {
                Success = true,
                Data = employee,
                Message = "Ажилтны мэдээлэл амжилттай шинэчлэгдлээ"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating employee {EmployeeId}", id);
            return BadRequest(new ApiResponseDto<EmployeeDto>
            {
                Success = false,
                Message = "Ажилтны мэдээлэл шинэчлэхэд алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Ажилтны нууц үг өөрчлөх
    /// </summary>
    /// <param name="id">Ажилтны ID</param>
    /// <param name="changePasswordDto">Нууц үг өөрчлөх мэдээлэл</param>
    /// <returns>Өөрчлөлтийн үр дүн</returns>
    [HttpPut("{id}/password")]
    public async Task<ActionResult<ApiResponseDto<bool>>> ChangePassword(int id, [FromBody] ChangePasswordDto changePasswordDto)
    {
        try
        {
            var result = await _employeeService.ChangePasswordAsync(id, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            return Ok(new ApiResponseDto<bool>
            {
                Success = true,
                Data = result,
                Message = result ? "Нууц үг амжилттай өөрчлөгдлөө" : "Нууц үг өөрчлөхөд алдаа гарлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error changing password for employee {EmployeeId}", id);
            return BadRequest(new ApiResponseDto<bool>
            {
                Success = false,
                Message = "Нууц үг өөрчлөхөд алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Ажилтныг устгах
    /// </summary>
    /// <param name="id">Ажилтны ID</param>
    /// <returns>Устгах үр дүн</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponseDto<bool>>> DeleteEmployee(int id)
    {
        try
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            return Ok(new ApiResponseDto<bool>
            {
                Success = true,
                Data = result,
                Message = result ? "Ажилтан амжилттай устгагдлаа" : "Ажилтан устгахад алдаа гарлаа"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting employee {EmployeeId}", id);
            return BadRequest(new ApiResponseDto<bool>
            {
                Success = false,
                Message = "Ажилтан устгахад алдаа гарлаа",
                Errors = [ex.Message]
            });
        }
    }
}
