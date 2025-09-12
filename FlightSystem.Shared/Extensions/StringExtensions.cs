using System.Text.RegularExpressions;

namespace FlightSystem.Shared.Extensions;

public static class StringExtensions
{
    public static bool IsValidFlightNumber(this string flightNumber)
    {
        return Regex.IsMatch(flightNumber, @"^[A-Z]{2,3}[0-9]{3,4}$");
    }

    public static bool IsValidPassportNumber(this string passportNumber)
    {
        return Regex.IsMatch(passportNumber, @"^[A-Z0-9АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ]{6,15}$");
    }

    public static bool IsValidEmployeeCode(this string employeeCode)
    {
        return Regex.IsMatch(employeeCode, @"^[A-Z]{3}[0-9]{3}$");
    }

    public static bool IsValidSeatNumber(this string seatNumber)
    {
        return Regex.IsMatch(seatNumber, @"^[0-9]{1,2}[A-F]$");
    }

    public static string ToTitleCase(this string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return input;
        
        return char.ToUpper(input[0]) + input[1..].ToLower();
    }

    public static string TruncateWithEllipsis(this string input, int maxLength)
    {
        if (string.IsNullOrEmpty(input) || input.Length <= maxLength)
            return input;
            
        return input[..(maxLength - 3)] + "...";
    }

    public static string FormatPhoneNumber(this string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber)) return phoneNumber;
        
        var digits = Regex.Replace(phoneNumber, @"[^\d]", "");
        
        return digits.Length switch
        {
            8 => $"{digits[..4]}-{digits[4..]}",
            _ => phoneNumber
        };
    }
}
