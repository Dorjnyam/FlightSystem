namespace FlightSystem.Core.Exceptions
{
    public class InvalidCredentialsException : FlightSystemException
    {
        public InvalidCredentialsException()
            : base("Ажилтны нэвтрэх мэдээлэл буруу байна", "INVALID_CREDENTIALS")
        {
        }
    }
}
