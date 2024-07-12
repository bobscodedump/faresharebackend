using FareShare.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Entity;

public class User : IdentityUser
{
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public ICollection<Group> Groups { get; set; }

    public User() : base()
    {
        this.Groups = new HashSet<Group>();
    }
    public void AddGroup(Group group)
    {
        this.Groups.Add(group);
    }
}