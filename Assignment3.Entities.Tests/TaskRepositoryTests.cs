using Assignment3.Core;

namespace Assignment3.Entities.Tests;

public class TaskRepositoryTests : TestBase
{
    private TaskRepository _repo;

    public TaskRepositoryTests()
    {
        // Given
        _repo = new TaskRepository(_context);
    }

    // 1. Trying to update or delete a non-existing entity should return NotFound
    [Fact]
    public void update_delete_on_nonexisting_returns_NotFound()
    {
        // Update
        {
            var response = _repo.Update(new TaskUpdateDTO(1, "title", 1, "desc", new List<String> { }, State.Active));
            response.Should().Be(Response.NotFound);
        }

        // Delete
        {
            var response = _repo.Delete(1);
            response.Should().Be(Response.NotFound);
        }
    }

    // 2. Create, Read, and Update should return a proper Response
    [Fact]
    public void create_read_update_should_return_response()
    {
        // Create
        {
            var (response, tagId) = _repo.Create(new TaskCreateDTO("title", 1, "desc", new List<String> { }));

            response.Should().Be(Response.Created);
            tagId.Should().Be(2);
        }

        // Read
        {
            var taskDetails = _repo.Read(2);
            taskDetails.Title.Should().Be("title");
        }

        // Update
        {
            var response = _repo.Update(new TaskUpdateDTO(1, "title", 1, "desc", new List<String> { }, State.Closed));
            response.Should().Be(Response.Updated);
        }

        // Read
        {
            var taskDetails = _repo.Read(2);
            taskDetails.Title.Should().Be(null);
        }


        // Delete
        {
            var response = _repo.Delete(2);
            response.Should().Be(Response.Deleted);
        }
    }

    // 5. If a task, tag, or user is not found, return null
    [Fact]
    public void NotFound_returns_null()
    {
        // Read
        _repo.Read(2).Should().BeNull();
    }

    /* --------------------------------- Unique --------------------------------- */

    // 1. Only tasks with the state New can be deleted from the database
    [Fact]
    public void only_new_tasks_can_be_deleted()
    {
        // Given
        var res = _repo.Delete(420);

        res.Should().Be(Response.Deleted);
    }

    // 2. Deleting a task which is Active should set its state to Removed
    [Fact]
    public void delete_active_task_set_to_removed()
    {
        // Given
        var res = _repo.Delete(421);

        // Because only removed
        res.Should().Be(Response.Updated);
    }

    // 3. Deleting a task which is Resolved, Closed, or Removed should return Conflict
    [Fact]
    public void delete_task_resolved_closed_remoted_returns_conflict()
    {
        {
            // Resolved
            var res = _repo.Delete(422);
            res.Should().Be(Response.Conflict);
        }

        {
            // Closed
            var res = _repo.Delete(423);
            res.Should().Be(Response.Conflict);
        }

        {
            // Removed
            var res = _repo.Delete(424);
            res.Should().Be(Response.Conflict);
        }
    }

    // 4. Creating a task will set its state to New and Created/StateUpdated to current time in UTC
    [Fact]
    public void create_task_set_new_and_created_updated_to_current_utc()
    {
        // Given
        var (response, id) = _repo.Create(new TaskCreateDTO("new task", null, null, new List<string> { }));
        var expected = DateTime.UtcNow;

        // When
        var task = _repo.Read(id);

        // Then
        task.Created.Should().BeCloseTo(expected, precision: TimeSpan.FromSeconds(5));
        task.StateUpdated.Should().BeCloseTo(expected, precision: TimeSpan.FromSeconds(5));
    }

    // 5. Create/update task must allow for editing tags
    [Fact]
    public void create_update_task_allow_editing_tags()
    {
        // Given
        var res = _repo.Update(new TaskUpdateDTO(1, "updated", null, "desc", new List<string> { }, State.Active));

        res.Should().Be(Response.Updated);
    }

    // 6. Updating the State of a task will change the StateUpdated to current time in UTC
    [Fact]
    public void updating_state_change_updated_to_current_utc()
    {
        // Given
        var (response, id) = _repo.Create(new TaskCreateDTO("new task", null, null, new List<string> { }));
        var expected = DateTime.UtcNow;

        var res = _repo.Update(new TaskUpdateDTO(id, "updated", null, "desc", new List<string> { }, State.Active));

        // When
        var task = _repo.Read(id);

        // Then
        task.StateUpdated.Should().BeCloseTo(expected, precision: TimeSpan.FromSeconds(5));
    }

    // 7. Assigning a user which does not exist should return BadRequest
    [Fact]
    public void assign_nonexisting_user_returns_badrequest()
    {
        // Given
        var (response, id) = _repo.Create(new TaskCreateDTO("new task", -1, null, new List<string> { }));

        // Then
        response.Should().Be(Response.BadRequest);
    }
}
