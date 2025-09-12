using FlightSystem.Core.Enums;

namespace FlightSystem.Shared.Constants
{
    public static class SystemConstants
    {
        /// <summary>
        /// Цагийн тогтмол утгууд
        /// </summary>
        public const int CHECKIN_OPEN_HOURS_BEFORE_DEPARTURE = 3;

        /// <summary>
        /// Нислэг хөөрөхөөс өмнөх бүртгэл хаагдах минут
        /// </summary>
        public const int CHECKIN_CLOSE_MINUTES_BEFORE_DEPARTURE = 45;

        /// <summary>
        /// Нислэг хөөрөхөөс өмнө зорчигч суух эхлэх минут
        /// </summary>
        public const int BOARDING_START_MINUTES_BEFORE_DEPARTURE = 30;

        /// <summary>
        /// Суудлын тогтмол утгууд
        /// </summary>
        public const int MAX_SEATS_PER_ROW = 6;

        /// <summary>
        /// Суудлын баганын нэрс
        /// </summary>
        public const string SEAT_COLUMNS = "ABCDEF";

        /// <summary>
        /// Суух тасалбарын тогтмол утгууд
        /// </summary>
        public const int BOARDING_PASS_CODE_LENGTH = 12;

        /// <summary>
        /// QR кодын урдчилсан тэмдэглэгээ
        /// </summary>
        public const string QR_CODE_PREFIX = "FLIGHTSYSTEM:";

        /// <summary>
        /// Ажилтны зөвшөөрлийн төрөл
        /// </summary>
        public const string PERMISSION_CHECKIN = "checkin";

        /// <summary>
        /// Нислэгийн төлөвийн зөвшөөрөл
        /// </summary>
        public const string PERMISSION_FLIGHT_STATUS = "flight_status";

        /// <summary>
        /// Админ зөвшөөрөл
        /// </summary>
        public const string PERMISSION_ADMIN = "admin";

        /// <summary>
        /// Нислэгийн төлөв өөрчлөлтийн зөвшөөрөгдөх шилжилтүүд
        /// </summary>
        public static readonly Dictionary<FlightStatus, FlightStatus[]> VALID_STATUS_TRANSITIONS = new()
        {
            [FlightStatus.Scheduled] = [FlightStatus.CheckinOpen, FlightStatus.Delayed, FlightStatus.Cancelled],
            [FlightStatus.CheckinOpen] = [FlightStatus.CheckinClosed, FlightStatus.Delayed, FlightStatus.Cancelled],
            [FlightStatus.CheckinClosed] = [FlightStatus.Boarding, FlightStatus.Delayed],
            [FlightStatus.Boarding] = [FlightStatus.LastCall, FlightStatus.Delayed],
            [FlightStatus.LastCall] = [FlightStatus.GateClosed, FlightStatus.Delayed],
            [FlightStatus.GateClosed] = [FlightStatus.Departed],
            [FlightStatus.Departed] = [],
            [FlightStatus.Delayed] = [FlightStatus.CheckinOpen, FlightStatus.CheckinClosed, FlightStatus.Boarding, FlightStatus.Cancelled],
            [FlightStatus.Cancelled] = []
        };
    }
}
