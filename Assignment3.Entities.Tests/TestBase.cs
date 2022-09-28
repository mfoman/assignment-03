using Assignment3.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

public abstract class TestBase : IDisposable
{
    private SqliteConnection _connection;
    private DbContextOptions<KanbanContext> _contextOptions;

    public TestBase()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        // These options will be used by the context instances in this test suite, including the connection opened above.
        _contextOptions = new DbContextOptionsBuilder<KanbanContext>()
            .UseSqlite(_connection)
            .Options;

        // Create the schema and seed some data
        using var context = new KanbanContext(_contextOptions);

        //         if (context.Database.EnsureCreated())
        //         {
        //             using var viewCommand = context.Database.GetDbConnection().CreateCommand();
        //             viewCommand.CommandText = @"
        // CREATE VIEW AllResources AS
        // SELECT Url
        // FROM Blogs;";
        //             viewCommand.ExecuteNonQuery();
        //         }

        // context.AddRange(
        //     new Blog { Name = "Blog1", Url = "http://blog1.com" },
        //     new Blog { Name = "Blog2", Url = "http://blog2.com" });
        // context.SaveChanges();
    }

    public void Dispose() => _connection.Dispose();
}
