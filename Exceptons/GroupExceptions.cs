namespace FareShare.Exceptions;

public class GroupDoesNotExistException : NotFoundException
{
    public const string Message = "Group does not exist. ";

    public GroupDoesNotExistException() : base(Message)
    {
    }
    public GroupDoesNotExistException(string msg)
        : base(Message + msg)
    {
    }
}