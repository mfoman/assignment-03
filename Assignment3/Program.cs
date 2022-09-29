using System.ComponentModel;
using Assignment3.Entities;
using KanbanContextFactory = Assignment3.KanbanContextFactory;
using Task = Assignment3.Entities.Task;

var factory = new KanbanContextFactory();
using var context = factory.CreateDbContext(args);

var user = new User
{
    UserId = 1,
    Name = "Frederik",
    Email = "someemail@itu.dk",
    Tasks = new List<Task> { }
};

try
{
    context.Users.Remove(user);
    context.SaveChanges();
}
catch (System.Exception) { }

context.Users.Add(user);
context.SaveChanges();

var mfoman = context.Users.Find(1);

Console.WriteLine(mfoman);

if (mfoman is not null)
{
    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(mfoman))
    {
        string name = descriptor.Name;
        object value = descriptor.GetValue(mfoman) ?? "";
        Console.WriteLine("{0}={1}", name, value);
    }
}
