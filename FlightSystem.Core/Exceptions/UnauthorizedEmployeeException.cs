namespace FlightSystem.Core.Exceptions
{
    public class UnauthorizedEmployeeException : FlightSystemException
    {
        public UnauthorizedEmployeeException(string operation)
            : base($"Ажилтан уг үйлдлийг хийх эрхгүй байна: {operation}", "UNAUTHORIZED_EMPLOYEE")
        {
        }
    }
}
