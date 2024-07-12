using Identity.Entity;

namespace FareShare.Entities;

public class Settlement : BaseEntity
{
    public string? FromUserId { get; set; }
    public User FromUser { get; set; }
    
    public string? ToUserId { get; set; }
    public User ToUser { get; set; }
    
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public DateTime Timestamp { get; set; }
    
    public string GroupId { get; set; }
    public Group Group { get; set; }
}