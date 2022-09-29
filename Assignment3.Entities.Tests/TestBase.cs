using Assignment3.Core;
using Assignment3.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Task = Assignment3.Entities.Task;

public abstract class TestBase : IDisposable
{
    private SqliteConnection _connection;
    private DbContextOptions<KanbanContext> _contextOptions;
    protected KanbanContext _context;

    public TestBase()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        // These options will be used by the context instances in this test suite, including the connection opened above.
        _contextOptions = new DbContextOptionsBuilder<KanbanContext>()
            .UseSqlite(_connection)
            .Options;

        // Create the schema and seed some data
        var context = new KanbanContext(_contextOptions);

        context.Database.EnsureCreated();

        var user = new User()
        {
            Name = "Frederik Raisa",
            Email = "frai@itu.dk"
        };

        var tagInUse = new Tag("InUse")
        {
            TagId = 1,
        };

        var tasks = new List<Task>
        {
            new Task{
                TaskId = 10,
                Title = "task 1",
                AssignedTo = user,
                Description = "do this",
                State = State.Active,
                Tags = new List<Tag> {tagInUse}
            }
        };

        tagInUse.Tasks.Add(tasks[0]);

        context.Users.Add(user);
        context.Tags.AddRange(tagInUse);
        context.Tasks.AddRange(tasks);

        context.SaveChanges();

        _context = context;
    }

    public void Dispose() => _connection.Dispose();
}
