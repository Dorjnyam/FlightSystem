namespace FlightSystem.Shared.Constants
{
    public static class ErrorMessages
    {
        /// <summary>
        /// Нислэгийн алдаа
        /// </summary>
        public const string FLIGHT_NOT_FOUND = "Нислэг олдсонгүй";
        public const string INVALID_FLIGHT_STATUS_TRANSITION = "Нислэгийн төлөвийг буруу өөрчилсөн";

        /// <summary>
        /// Зорчигчийн алдаа
        /// </summary>
        public const string PASSENGER_NOT_FOUND = "Зорчигч олдсонгүй";
        public const string PASSENGER_NOT_BOOKED = "Энэ нислэгт зорчигч захиалга хийгдээгүй";
        public const string PASSENGER_ALREADY_CHECKED_IN = "Зорчигч аль хэдийн бүртгэгдсэн байна";

        /// <summary>
        /// Суудлын алдаа
        /// </summary>
        public const string SEAT_NOT_AVAILABLE = "Суудал боломжгүй байна";
        public const string SEAT_ALREADY_ASSIGNED = "Суудал аль хэдийн оноогдсон байна";

        /// <summary>
        /// Бүртгэлийн (Check-in) алдаа
        /// </summary>
        public const string CHECKIN_NOT_OPEN = "Бүртгэл хараахан нээгдээгүй байна";
        public const string CHECKIN_CLOSED = "Бүртгэл хаагдсан байна";
        public const string FLIGHT_CANCELLED = "Нислэг цуцлагдсан байна";

        /// <summary>
        /// Ажилтны алдаа
        /// </summary>
        public const string EMPLOYEE_NOT_FOUND = "Ажилтан олдсонгүй";
        public const string INVALID_CREDENTIALS = "Нэвтрэх мэдээлэл буруу байна";
        public const string INSUFFICIENT_PERMISSIONS = "Зөвшөөрөл хүрэлцэхгүй байна";
    }
}
