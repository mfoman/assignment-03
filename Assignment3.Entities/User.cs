namespace Assignment3.Entities;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Key]
    public int UserId { get; set; }

    [Required, MaxLength(100)]
    public string? Name { get; set; }

    [Required, MaxLength(100)]
    public string? Email { get; set; }
    public List<Task>? Tasks { get; set; } = new();

}

// - Id : int
// - Name : string (100), required
// - Email : string (100), required, unique
// - Tasks : list of * Task* entities belonging to * User*
