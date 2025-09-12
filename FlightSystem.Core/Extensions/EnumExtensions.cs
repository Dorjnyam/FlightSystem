using FlightSystem.Core.Enums;
using FlightSystem.Core.Constants;

namespace FlightSystem.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this FlightStatus status)
        {
            return status switch
            {
                FlightStatus.Scheduled => "Төлөвлөсөн",
                FlightStatus.CheckinOpen => "Бүртгэж байна",
                FlightStatus.CheckinClosed => "Бүртгэл хаагдсан",
                FlightStatus.Boarding => "Онгоцонд сууж байна",
                FlightStatus.LastCall => "Сүүлчийн дуудлага",
                FlightStatus.GateClosed => "Хаалга хаагдсан",
                FlightStatus.Departed => "Ниссэн",
                FlightStatus.Delayed => "Хойшилсон",
                FlightStatus.Cancelled => "Цуцалсан",
                _ => status.ToString()
            };
        }

        public static string GetDisplayName(this EmployeeRole role)
        {
            return role switch
            {
                EmployeeRole.Admin => "Админ",
                EmployeeRole.CheckinAgent => "Бүртгэлийн ажилтан",
                EmployeeRole.Supervisor => "Удирдагч",
                _ => role.ToString()
            };
        }

        public static string GetDisplayName(this PassengerType type)
        {
            return type switch
            {
                PassengerType.Adult => "Том хүн",
                PassengerType.Child => "Хүүхэд",
                PassengerType.Infant => "Нярай",
                PassengerType.UnaccompaniedMinor => "Асран хамгаалагчгүй хүүхэд",
                _ => type.ToString()
            };
        }

        public static bool CanTransitionTo(this FlightStatus current, FlightStatus target)
        {
            return SystemConstants.VALID_STATUS_TRANSITIONS.ContainsKey(current) &&
                   SystemConstants.VALID_STATUS_TRANSITIONS[current].Contains(target);
        }
    }
}
