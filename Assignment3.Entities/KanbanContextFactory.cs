using Microsoft.EntityFrameworkCore.Design;

namespace Assignment3.Entities;

public class KanbanContextFactory : IDesignTimeDbContextFactory<KanbanContext>
{
    public KanbanContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>();
        optionsBuilder.UseNpgsql(@"Host=127.0.0.1:54320;Username=postgres;Password=postgrespw;Database=kanban");

        return new KanbanContext(optionsBuilder.Options);
    }
}
