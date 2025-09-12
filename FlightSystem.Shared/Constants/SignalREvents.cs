namespace FlightSystem.Shared.Constants;

public static class SignalREvents
{
    public const string FlightStatusChanged = "FlightStatusChanged";
    public const string SeatAssigned = "SeatAssigned";
    public const string SeatReleased = "SeatReleased";
    public const string PassengerCheckedIn = "PassengerCheckedIn";
    public const string BoardingPassIssued = "BoardingPassIssued";
    public const string SystemMessage = "SystemMessage";
    public const string FlightUpdated = "FlightUpdated";
    
    public const string JoinFlightGroup = "JoinFlightGroup";
    public const string LeaveFlightGroup = "LeaveFlightGroup";
    public const string JoinEmployeeGroup = "JoinEmployeeGroup";
    public const string RequestFlightUpdate = "RequestFlightUpdate";
}
