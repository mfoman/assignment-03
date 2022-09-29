using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Assignment3.Entities;

public class KanbanContext : DbContext
{
    public KanbanContext(DbContextOptions options) : base(options) { }

    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Task> Tasks => Set<Task>();
    public DbSet<User> Users => Set<User>();

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseNpgsql(@"Host=127.0.0.1:54320;Username=postgres;Password=postgrespw;Database=kanban");

    static KanbanContext OpenPostgreSQL()
    {
        var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>();
        optionsBuilder.UseNpgsql(@"Host=127.0.0.1:54320;Username=postgres;Password=postgrespw;Database=kanban");

        return new KanbanContext(optionsBuilder.Options);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Task>()
            .Property(e => e.State)
            .HasConversion(new EnumToStringConverter<State>());
    }
}
