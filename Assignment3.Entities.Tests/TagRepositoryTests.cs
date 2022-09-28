namespace Assignment3.Entities.Tests;

public class TagRepositoryTests : TestBase
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

    // 1. Tags which are assigned to a task may only be deleted using the force.
    [Fact]
    public void tag_assigned_to_task_only_delete_using_force()
    {
        // Given

        // When

        // Then
    }

    // 2. Trying to delete a tag in use without the force should return Conflict
    [Fact]
    public void delete_tag_assigned_without_force_returns_conflict()
    {
        // Given

        // When

        // Then
    }

    // 3. Trying to create a tag which exists already should return Conflict
    [Fact]
    public void create_tag_already_exists_returns_conflict()
    {
        // Given

        // When

        // Then
    }
}
