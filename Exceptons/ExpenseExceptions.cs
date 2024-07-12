namespace FareShare.Exceptions;

public class ExpenseDoesNotExistException : NotFoundException
{
    public const string Message = "Expense does not exist. ";

    public ExpenseDoesNotExistException() : base(Message)
    {
    }
    public ExpenseDoesNotExistException(string msg)
        : base(Message + msg)
    {
    }
}