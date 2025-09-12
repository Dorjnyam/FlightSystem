namespace FlightSystem.Core.Constants
{
    public static class ValidationRules
    {
        /// <summary>
        /// Паспортын шалгалтын доод урт
        /// </summary>
        public const int MIN_PASSPORT_LENGTH = 6;

        /// <summary>
        /// Паспортын шалгалтын дээд урт
        /// </summary>
        public const int MAX_PASSPORT_LENGTH = 15;

        /// <summary>
        /// Нэрийн шалгалтын доод урт
        /// </summary>
        public const int MIN_NAME_LENGTH = 2;

        /// <summary>
        /// Нэрийн шалгалтын дээд урт
        /// </summary>
        public const int MAX_NAME_LENGTH = 50;

        /// <summary>
        /// Нислэгийн дугаарын шалгалтын доод урт
        /// </summary>
        public const int MIN_FLIGHT_NUMBER_LENGTH = 5;

        /// <summary>
        /// Нислэгийн дугаарын шалгалтын дээд урт
        /// </summary>
        public const int MAX_FLIGHT_NUMBER_LENGTH = 8;

        /// <summary>
        /// Ажилтны кодын урт
        /// </summary>
        public const int EMPLOYEE_CODE_LENGTH = 6;

        /// <summary>
        /// Суудлын дугаарын доод урт
        /// </summary>
        public const int MIN_SEAT_NUMBER_LENGTH = 2;

        /// <summary>
        /// Суудлын дугаарын дээд урт
        /// </summary>
        public const int MAX_SEAT_NUMBER_LENGTH = 4;

        /// <summary>
        /// Паспортын regular илэрхийлэл
        /// </summary>
        public const string PASSPORT_REGEX = @"^[A-Z0-9АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ]{6,15}$";

        /// <summary>
        /// Нислэгийн дугаарын regular илэрхийлэл
        /// </summary>
        public const string FLIGHT_NUMBER_REGEX = @"^[A-Z]{2,3}[0-9]{3,4}$";

        /// <summary>
        /// Ажилтны кодын regular илэрхийлэл
        /// </summary>
        public const string EMPLOYEE_CODE_REGEX = @"^[A-Z]{3}[0-9]{3}$";

        /// <summary>
        /// Суудлын дугаарын regular илэрхийлэл
        /// </summary>
        public const string SEAT_NUMBER_REGEX = @"^[0-9]{1,2}[A-F]$";
    }
}
