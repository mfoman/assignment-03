namespace Assignment3.Entities.Tests;

public class UserRepositoryTests
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

    // 1. Users who are assigned to a task may only be deleted using the force
    [Fact]
    public void users_assigned_to_task_only_delete_with_force()
    {
        // Given

        // When

        // Then
    }

    // 2. Trying to delete a user in use without the force should return Conflict
    [Fact]
    public void delete_active_user_without_force_returns_conflict()
    {
        // Given

        // When

        // Then
    }

    // 3. Trying to create a user which exists already (same email) should return Conflict.
    [Fact]
    public void create_user_that_exists_email_returns_conflict()
    {
        // Given

        // When

        // Then
    }
}
