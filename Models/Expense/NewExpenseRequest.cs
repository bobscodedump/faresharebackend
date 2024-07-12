namespace FareShare.Models.Expense;

public class NewExpenseRequest
{
    public string GroupId { get; set; }
    public string Description { get; set; }
    public string Currency { get; set; }
}