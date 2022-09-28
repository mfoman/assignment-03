using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Assignment3.Entities;

public class KanbanContext : DbContext
{
    public KanbanContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Tag>? Tags { get; set; }
    public DbSet<Task>? Tasks { get; set; }
    public DbSet<User>? Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(@"Host=127.0.0.1,54320;Username=postgres;Password=postgrespw;Database=kanban");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Task>()
            .Property(e => e.State)
            .HasConversion(new EnumToStringConverter<State>());
    }
}
