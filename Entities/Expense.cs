using System.ComponentModel.DataAnnotations.Schema;
using Identity.Entity;

namespace FareShare.Entities;

public class Expense : BaseEntity
{
    public bool IsSettled { get; set; } = false;

    public ICollection<ExpenseParticipant> Participants { get; set; } 
    public string GroupId { get; set; }
    public Group Group { get; set; }
    public string Description { get; set; }
    
    public string Currency { get; set; }
}