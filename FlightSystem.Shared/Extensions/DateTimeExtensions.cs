namespace FlightSystem.Shared.Extensions;

public static class DateTimeExtensions
{
    public static string ToMongolianFormat(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy оны MM сарын dd, HH:mm");
    }

    public static string ToFlightDisplayFormat(this DateTime dateTime)
    {
        return dateTime.ToString("MM/dd HH:mm");
    }

    public static string ToTimeOnlyFormat(this DateTime dateTime)
    {
        return dateTime.ToString("HH:mm");
    }

    public static bool IsToday(this DateTime dateTime)
    {
        return dateTime.Date == DateTime.Today;
    }

    public static bool IsTomorrow(this DateTime dateTime)
    {
        return dateTime.Date == DateTime.Today.AddDays(1);
    }

    public static string ToRelativeString(this DateTime dateTime)
    {
        var today = DateTime.Today;
        var date = dateTime.Date;

        return (date - today).Days switch
        {
            0 => "Өнөөдөр",
            1 => "Маргааш",
            -1 => "Өчигдөр",
            > 0 and <= 7 => $"{(date - today).Days} өдрийн дараа",
            < 0 and >= -7 => $"{Math.Abs((date - today).Days)} өдрийн өмнө",
            _ => dateTime.ToString("yyyy/MM/dd")
        };
    }
}
