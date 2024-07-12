using Identity.Entity;

namespace FareShare.Entities;

public class ExpenseParticipant : BaseEntity
{
    public string ExpenseId { get; set; }
    public Expense Expense { get; set; }
    
    public string UserId { get; set; }
    public User User { get; set; }
    
    public decimal Amount { get; set; }
    
    public string Currency { get; set; }
}