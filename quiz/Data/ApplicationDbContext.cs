using System.Reflection;
using Microsoft.EntityFrameworkCore;
using quiz.Entities;

namespace quiz.Data;
public class ApplicationDbContext : DbContext
{
    public DbSet<Quiz>? Quizzes { get; set; }
    public DbSet<Topic>? Topics { get; set; }
    public DbSet<Question>? Questions { get; set; }
    public DbSet<McqOption>? McqOptions { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) 
        :base(option) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
        SetDates();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetDates();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetDates()
    {
        foreach(var entry in ChangeTracker.Entries<EntityBase>())
        {
            if(entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.Now;
                entry.Entity.UpdatedAt = DateTime.Now;
            }
            if(entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.Now;
            }
        }
    }
}