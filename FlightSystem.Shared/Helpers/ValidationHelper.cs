using FlightSystem.Shared.Constants;
using FlightSystem.Shared.DTOs.Request;

namespace FlightSystem.Shared.Helpers;

public interface IValidationHelper
{
    ValidationResult ValidatePassenger(CreatePassengerDto passenger);
    ValidationResult ValidateFlight(CreateFlightDto flight);
    ValidationResult ValidateEmployee(CreateEmployeeDto employee);
    ValidationResult ValidateCheckin(CheckinRequestDto checkin);
}

public class ValidationHelper : IValidationHelper
{
    public ValidationResult ValidatePassenger(CreatePassengerDto passenger)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(passenger.PassportNumber))
            errors.Add("Пасспортын дугаар оруулах шаардлагатай");
        else if (!IsValidPassport(passenger.PassportNumber))
            errors.Add("Пасспортын дугаар буруу форматтай");

        if (string.IsNullOrWhiteSpace(passenger.FirstName))
            errors.Add("Нэр оруулах шаардлагатай");
        else if (passenger.FirstName.Length < ValidationRules.MIN_NAME_LENGTH)
            errors.Add($"Нэр хамгийн багадаа {ValidationRules.MIN_NAME_LENGTH} тэмдэгт байх ёстой");

        if (string.IsNullOrWhiteSpace(passenger.LastName))
            errors.Add("Овог оруулах шаардлагатай");
        else if (passenger.LastName.Length < ValidationRules.MIN_NAME_LENGTH)
            errors.Add($"Овог хамгийн багадаа {ValidationRules.MIN_NAME_LENGTH} тэмдэгт байх ёстой");

        if (passenger.DateOfBirth > DateTime.Today)
            errors.Add("Төрсөн огноо буруу байна");

        return new ValidationResult { IsValid = !errors.Any(), Errors = errors };
    }

    public ValidationResult ValidateFlight(CreateFlightDto flight)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(flight.FlightNumber))
            errors.Add("Нислэгийн дугаар оруулах шаардлагатай");
        else if (!IsValidFlightNumber(flight.FlightNumber))
            errors.Add("Нислэгийн дугаар буруу форматтай");

        if (flight.ScheduledDeparture >= flight.ScheduledArrival)
            errors.Add("Хөөрөх цаг буух цагаас эрт байх ёстой");

        if (flight.CheckinOpenTime >= flight.CheckinCloseTime)
            errors.Add("Бүртгэл нээх цаг хаах цагаас эрт байх ёстой");

        return new ValidationResult { IsValid = !errors.Any(), Errors = errors };
    }

    public ValidationResult ValidateEmployee(CreateEmployeeDto employee)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(employee.EmployeeCode))
            errors.Add("Ажилтны код оруулах шаардлагатай");
        else if (!IsValidEmployeeCode(employee.EmployeeCode))
            errors.Add("Ажилтны код буруу форматтай");

        if (string.IsNullOrWhiteSpace(employee.Password) || employee.Password.Length < 6)
            errors.Add("Нууц үг хамгийн багадаа 6 тэмдэгт байх ёстой");

        return new ValidationResult { IsValid = !errors.Any(), Errors = errors };
    }

    public ValidationResult ValidateCheckin(CheckinRequestDto checkin)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(checkin.FlightNumber))
            errors.Add("Нислэгийн дугаар оруулах шаардлагатай");

        if (string.IsNullOrWhiteSpace(checkin.PassportNumber))
            errors.Add("Пасспортын дугаар оруулах шаардлагатай");

        if (checkin.EmployeeId <= 0)
            errors.Add("Ажилтны мэдээлэл буруу байна");

        return new ValidationResult { IsValid = !errors.Any(), Errors = errors };
    }

    private static bool IsValidPassport(string passport) => 
        System.Text.RegularExpressions.Regex.IsMatch(passport, ValidationRules.PASSPORT_REGEX);

    private static bool IsValidFlightNumber(string flightNumber) => 
        System.Text.RegularExpressions.Regex.IsMatch(flightNumber, ValidationRules.FLIGHT_NUMBER_REGEX);

    private static bool IsValidEmployeeCode(string employeeCode) => 
        System.Text.RegularExpressions.Regex.IsMatch(employeeCode, ValidationRules.EMPLOYEE_CODE_REGEX);
}

public class ValidationResult
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; } = [];
}
