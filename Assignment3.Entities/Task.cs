namespace Assignment3.Entities;

public class Task
{
    [Key]
    public int TaskId { get; set; }

    [Required, MaxLength(100)]
    public string? Title { get; set; }
    public User? AssignedTo { get; set; }
    public string? Description { get; set; }

    [Required]
    public State? State { get; set; }
    public List<Tag>? Tags { get; set; } = new();
}

// - Id : int
// - Title : string(100), required
// - AssignedTo : optional reference to *User* entity
// - Description : string(max), optional
// - State : enum (New, Active, Resolved, Closed, Removed), required
// - Tags : many-to-many reference to *Tag* entity
