namespace FlightSystem.Core.Exceptions
{
    public class FlightSystemException : Exception
    {
        public string ErrorCode { get; }
        
        public FlightSystemException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        
        public FlightSystemException(string message, string errorCode, Exception innerException) 
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
