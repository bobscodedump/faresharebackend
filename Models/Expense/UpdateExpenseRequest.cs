namespace FareShare.Models.Expense;

public class UpdateExpenseRequest
{
    public string Id { get; set; }
    public string Description { get; set; }
    public Dictionary<string, string> Map { get; set; }
}