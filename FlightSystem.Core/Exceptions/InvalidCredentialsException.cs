namespace FlightSystem.Core.Exceptions
{
    public class InvalidCredentialsException : FlightSystemException
    {
        public InvalidCredentialsException()
            : base("������� ������� �������� ����� �����", "INVALID_CREDENTIALS")
        {
        }
    }
}
