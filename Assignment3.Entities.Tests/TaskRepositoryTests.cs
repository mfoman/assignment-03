namespace Assignment3.Entities.Tests;

public class TaskRepositoryTests
{
    // 1. Trying to update or delete a non-existing entity should return NotFound
    [Fact]
    public void update_delete_on_nonexisting_returns_NotFound()
    {
        // Given

        // When

        // Then
    }

    // 2. Create, Read, and Update should return a proper Response
    [Fact]
    public void create_read_update_should_return_response()
    {
        // Given

        // When

        // Then
    }

    // 5. If a task, tag, or user is not found, return null
    [Fact]
    public void NotFound_returns_null()
    {
        // Given

        // When

        // Then
    }

    /* --------------------------------- Unique --------------------------------- */

    // 1. Only tasks with the state New can be deleted from the database
    [Fact]
    public void only_new_tasks_can_be_deleted()
    {
        // Given

        // When

        // Then
    }

    // 2. Deleting a task which is Active should set its state to Removed
    [Fact]
    public void delete_active_task_set_to_removed()
    {
        // Given

        // When

        // Then
    }

    // 3. Deleting a task which is Resolved, Closed, or Removed should return Conflict
    [Fact]
    public void delete_task_resolved_closed_remoted_returns_conflict()
    {
        // Given

        // When

        // Then
    }

    // 4. Creating a task will set its state to New and Created/StateUpdated to current time in UTC
    [Fact]
    public void create_task_set_new_and_created_updated_to_current_utc()
    {
        // Given

        // When

        // Then
    }

    // 5. Create/update task must allow for editing tags
    [Fact]
    public void create_update_task_allow_editing_tags()
    {
        // Given

        // When

        // Then
    }

    // 6. Updating the State of a task will change the StateUpdated to current time in UTC
    [Fact]
    public void updating_state_change_updated_to_current_utc()
    {
        // Given

        // When

        // Then
    }

    // 7. Assigning a user which does not exist should return BadRequest
    [Fact]
    public void assign_nonexisting_user_returns_badrequest()
    {
        // Given

        // When

        // Then
    }
}
