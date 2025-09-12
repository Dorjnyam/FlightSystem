namespace FlightSystem.Shared.Constants;

public static class ApiEndpoints
{
    public const string BaseUrl = "https://localhost:7001";
    
    public static class Flights
    {
        public const string GetAll = "/api/flights";
        public const string GetById = "/api/flights/{0}";
        public const string GetByNumber = "/api/flights/number/{0}";
        public const string Create = "/api/flights";
        public const string Update = "/api/flights/{0}";
        public const string Delete = "/api/flights/{0}";
        public const string UpdateStatus = "/api/flights/{0}/status";
        public const string GetByDate = "/api/flights/date/{0:yyyy-MM-dd}";
        public const string GetActive = "/api/flights/active";
        public const string GetDetails = "/api/flights/{0}/details";
    }
    
    public static class Checkin
    {
        public const string CheckinPassenger = "/api/checkin/passenger";
        public const string GetStatus = "/api/checkin/status/{0}/{1}";
        public const string GetEligibility = "/api/checkin/eligibility/{0}/{1}";
        public const string CancelCheckin = "/api/checkin/cancel/{0}";
        public const string AssignSeat = "/api/checkin/assign-seat";
        public const string ReleaseSeat = "/api/checkin/release-seat/{0}";
        public const string GetAvailableSeats = "/api/checkin/seats/{0}";
        public const string GetSeatMap = "/api/checkin/seatmap/{0}";
    }

    public static class Passengers
    {
        public const string GetAll = "/api/passengers";
        public const string GetById = "/api/passengers/{0}";
        public const string GetByPassport = "/api/passengers/passport/{0}";
        public const string Create = "/api/passengers";
        public const string Update = "/api/passengers/{0}";
        public const string Delete = "/api/passengers/{0}";
        public const string Search = "/api/passengers/search?term={0}";
        public const string GetFlightPassenger = "/api/passengers/flight/{0}/passport/{1}";
    }

    public static class Employees
    {
        public const string Authenticate = "/api/employees/authenticate";
        public const string GetAll = "/api/employees";
        public const string GetById = "/api/employees/{0}";
        public const string Create = "/api/employees";
        public const string Update = "/api/employees/{0}";
        public const string Delete = "/api/employees/{0}";
        public const string ChangePassword = "/api/employees/{0}/password";
        public const string Deactivate = "/api/employees/{0}/deactivate";
        public const string Activate = "/api/employees/{0}/activate";
    }

    public static class BoardingPass
    {
        public const string Generate = "/api/boardingpass/generate";
        public const string GetById = "/api/boardingpass/{0}";
        public const string GetByCode = "/api/boardingpass/code/{0}";
        public const string Update = "/api/boardingpass/{0}";
        public const string Delete = "/api/boardingpass/{0}";
        public const string ProcessBoarding = "/api/boardingpass/process/{0}";
        public const string Validate = "/api/boardingpass/validate/{0}";
        public const string CancelBoarding = "/api/boardingpass/cancel/{0}";
    }

    public static class Aircraft
    {
        public const string GetAll = "/api/aircraft";
        public const string GetById = "/api/aircraft/{0}";
        public const string Create = "/api/aircraft";
        public const string Update = "/api/aircraft/{0}";
        public const string Delete = "/api/aircraft/{0}";
        public const string GetActive = "/api/aircraft/active";
        public const string GetWithSeats = "/api/aircraft/{0}/seats";
        public const string Deactivate = "/api/aircraft/{0}/deactivate";
        public const string Activate = "/api/aircraft/{0}/activate";
    }

    public static class Seats
    {
        public const string GetAll = "/api/seats";
        public const string GetById = "/api/seats/{0}";
        public const string Create = "/api/seats";
        public const string Update = "/api/seats/{0}";
        public const string Delete = "/api/seats/{0}";
        public const string GetByAircraft = "/api/seats/aircraft/{0}";
        public const string GetAvailable = "/api/seats/available/{0}";
        public const string GetByClass = "/api/seats/class/{0}/{1}";
        public const string GetWindowSeats = "/api/seats/window/{0}";
        public const string GetAisleSeats = "/api/seats/aisle/{0}";
    }

    public static class SeatAssignments
    {
        public const string GetAll = "/api/seatassignments";
        public const string GetById = "/api/seatassignments/{0}";
        public const string Create = "/api/seatassignments";
        public const string Update = "/api/seatassignments/{0}";
        public const string Delete = "/api/seatassignments/{0}";
        public const string GetByFlight = "/api/seatassignments/flight/{0}";
        public const string GetByPassenger = "/api/seatassignments/passenger/{0}";
        public const string GetByEmployee = "/api/seatassignments/employee/{0}";
        public const string ReleaseSeat = "/api/seatassignments/release/{0}";
    }

    public static class FlightPassengers
    {
        public const string GetAll = "/api/flightpassengers";
        public const string GetById = "/api/flightpassengers/{0}";
        public const string Create = "/api/flightpassengers";
        public const string Update = "/api/flightpassengers/{0}";
        public const string Delete = "/api/flightpassengers/{0}";
        public const string GetByFlight = "/api/flightpassengers/flight/{0}";
        public const string GetByPassenger = "/api/flightpassengers/passenger/{0}";
        public const string GetByBookingReference = "/api/flightpassengers/booking/{0}";
        public const string GetCheckedIn = "/api/flightpassengers/checkedin/{0}";
        public const string CheckinPassenger = "/api/flightpassengers/checkin/{0}";
        public const string CancelCheckin = "/api/flightpassengers/cancel-checkin/{0}";
    }
}
