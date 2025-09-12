namespace FlightSystem.Core.Exceptions
{
    public class PassengerNotFoundException : FlightSystemException
    {
        public PassengerNotFoundException(string passportNumber)
            : base($"Паспортын дугаар {passportNumber} бүхий зорчигч олдсонгүй", "PASSENGER_NOT_FOUND")
        {
        }
    }
}
