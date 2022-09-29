using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.Entities;

public class Task
{
    // public Task(int taskId, string? title, User? assignedTo, string? description, State? state, List<Tag>? tags)
    // {
    //     TaskId = taskId;
    //     Title = title;
    //     AssignedTo = assignedTo;
    //     Description = description;
    //     State = state;
    //     Tags = tags;
    // }

    [Key]
    public int TaskId { get; set; }

    [Required, MaxLength(100)]
    public string? Title { get; set; }
    public User? AssignedTo { get; set; } = new();
    public string? Description { get; set; }

    [Required]
    public State State { get; set; }
    public List<Tag>? Tags { get; set; } = new();

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? UpdatedDate { get; set; }
}

// - Id : int
// - Title : string(100), required
// - AssignedTo : optional reference to *User* entity
// - Description : string(max), optional
// - State : enum (New, Active, Resolved, Closed, Removed), required
// - Tags : many-to-many reference to *Tag* entity
