using FareShare.Entities;

namespace Identity.Entity;

public class Group : BaseEntity
{
    public ICollection<User> Users { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Group() : base()
    {
        this.Users = new HashSet<User>();
    }
    
    public void AddUser(User user)
    {
        this.Users.Add(user);
    }
}