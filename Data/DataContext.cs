using FareShare.Entities;
using Identity.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FareShare.Data;

public class DataContext : IdentityDbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Settlement> Settlements { get; set; }
    public DbSet<ExpenseParticipant> ExpenseParticipants { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Settlement>()
            .HasOne(s => s.FromUser)
            .WithMany()
            .HasForeignKey(s => s.FromUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Settlement>()
            .HasOne(s => s.ToUser)
            .WithMany()
            .HasForeignKey(s => s.ToUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Settlement>()
            .HasOne(s => s.Group)
            .WithMany()
            .HasForeignKey(s => s.GroupId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}