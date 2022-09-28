namespace Assignment3.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Tag
{
    [Key]
    public int TagId { get; set; }

    [Required, MaxLength(50)]
    public string? Name { get; set; }

    public List<Task> Tasks { get; set; } = new();
}

// - Id : int
// - Name : string(50), required, unique
// - Tasks : many-to-many reference to *Task* entity
