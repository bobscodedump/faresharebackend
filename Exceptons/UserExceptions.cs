namespace FareShare.Exceptions;

public class UserDoesNotExistException : NotFoundException
{
    public const string Message = "User does not exist. ";

    public UserDoesNotExistException() : base(Message)
    {
    }
    public UserDoesNotExistException(string msg)
        : base(Message + msg)
    {
    }
}
