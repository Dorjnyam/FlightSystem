using FlightSystem.Core.Models;
using FlightSystem.Shared.Enums;

namespace FlightSystem.Core.Extensions
{
    public static class ModelExtensions
    {
        public static string GetFullName(this Passenger passenger)
        {
            return $"{passenger.FirstName} {passenger.LastName}";
        }

        public static string GetFullName(this Employee employee)
        {
            return $"{employee.FirstName} {employee.LastName}";
        }

        public static bool IsCheckinOpen(this Flight flight)
        {
            var now = DateTime.UtcNow;
            return now >= flight.CheckinOpenTime && 
                   now <= flight.CheckinCloseTime && 
                   flight.Status == FlightStatus.CheckinOpen;
        }

        public static bool IsBoardingOpen(this Flight flight)
        {
            return flight.Status == FlightStatus.Boarding || 
                   flight.Status == FlightStatus.LastCall;
        }

        public static int GetAgeInYears(this Passenger passenger)
        {
            var today = DateTime.Today;
            var age = today.Year - passenger.DateOfBirth.Year;
            if (passenger.DateOfBirth.Date > today.AddYears(-age))
                age--;
            return age;
        }
    }
}
