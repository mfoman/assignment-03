namespace Assignment3.Entities;

public class TaskRepository : ITaskRepository
{
    private readonly KanbanContext _context;

    public TaskRepository(KanbanContext context)
    {
        _context = context;
    }

    public (Response Response, int TaskId) Create(TaskCreateDTO task)
    {
        var entity = _context.Tasks.FirstOrDefault(c => c.Title == task.Title);
        var user = _context.Users.Find(task.AssignedToId);

        Response status;

        if (user is null && task.AssignedToId != null)
        {
            entity = new Task
            {
                Title = task.Title,
                AssignedTo = null,
                Description = task.Description,
                State = State.Removed,
                Tags = null
            };

            status = BadRequest;
        }
        else if (entity is null)
        {
            var tags = from c in _context.Tags
                       where task.Tags.Contains(c.Name)
                       select c;

            entity = new Task
            {
                Title = task.Title,
                AssignedTo = user,
                Description = task.Description,
                State = State.New,
                Tags = tags.ToList()
            };

            _context.Tasks.Add(entity);
            _context.SaveChanges();

            status = Created;
        }
        else
        {
            status = Conflict;
        }

        var created = new TaskDTO(
            entity.TaskId,
            entity.Title ?? "",
            entity.AssignedTo?.Name ?? "",
            entity.Tags as IReadOnlyCollection<string> ?? new List<string> { },
            entity.State
        );

        return (status, created.Id);
    }

    public Response Delete(int taskId)
    {
        var task = _context.Tasks.FirstOrDefault(c => c.TaskId == taskId);
        Response response;

        if (task is null)
        {
            response = NotFound;
        }
        else if ((new[] { State.Closed, State.Removed, State.Resolved }).Contains(task.State))
        {
            response = Conflict;
        }
        else if (task.State == State.Active)
        {
            task.State = State.Removed;
            _context.Tasks.Update(task);

            response = Updated;
        }
        else
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();

            response = Deleted;
        }

        return response;
    }

    public TaskDetailsDTO Read(int taskId)
    {
        var tasks = from c in _context.Tasks
                    where c.TaskId == taskId
                    select c;

        var t = tasks.FirstOrDefault();

        if (t is null)
        {
            return new TaskDetailsDTO(-1, "", "", DateTime.Now, "", new List<string> { }, State.Closed, DateTime.Now);
        }

        return new TaskDetailsDTO(
            t.TaskId,
            t.Title ?? "",
            t.Description ?? "",
            t.CreatedDate,
            t.AssignedTo?.Name ?? "",
            t.Tags as IReadOnlyCollection<string> ?? new List<string> { },
            t.State,
            t.UpdatedDate ?? DateTime.UtcNow
        );
    }

    public IReadOnlyCollection<TaskDTO> ReadAll()
    {
        var Tasks = from c in _context.Tasks
                    orderby c.Title
                    select new TaskDTO(
                        c.TaskId,
                        c.Title ?? "",
                        (c.AssignedTo == null ? "" : c.AssignedTo.Name ?? ""),
                        c.Tags as IReadOnlyCollection<string> ?? new List<string> { },
                        c.State
                    );

        return Tasks.ToArray();
    }

    public IReadOnlyCollection<TaskDTO> ReadAllByState(State state)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<TaskDTO> ReadAllByTag(string tag)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<TaskDTO> ReadAllByUser(int userId)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<TaskDTO> ReadAllRemoved()
    {
        throw new NotImplementedException();
    }

    public Response Update(TaskUpdateDTO task)
    {
        var entity = _context.Tasks.Find(task.Id);

        Response response;

        if (entity is null)
        {
            response = NotFound;
        }
        else if (_context.Tasks.FirstOrDefault(c => c.TaskId != task.Id && c.Title == task.Title) != null)
        {
            response = Conflict;
        }
        else
        {
            entity.Title = task.Title;
            entity.Description = task.Description;
            entity.State = task.State;
            entity.Tags = task.Tags as List<Tag>;

            if (entity.AssignedTo?.UserId != task.AssignedToId)
            {
                var user = _context.Users.Find(task.AssignedToId);
                entity.AssignedTo = user;
            }

            _context.SaveChanges();
            response = Updated;
        }

        return response;
    }
}
