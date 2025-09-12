namespace FlightSystem.Core.Exceptions
{
    public class PassengerAlreadyCheckedInException : FlightSystemException
    {
        public PassengerAlreadyCheckedInException(string passportNumber)
            : base($"Зорчигч {passportNumber} аль хэдийн бүртгэгдсэн байна", "PASSENGER_ALREADY_CHECKED_IN")
        {
        }
    }
}
