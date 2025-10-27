namespace Taskify.ApiService.Data.Entities;

public class TaskItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Status { get; set; } // "To Do", "In Progress", "In Review", "Done"
    public int? AssignedToId { get; set; }
    public int ProjectId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public User? AssignedTo { get; set; }
    public required Project Project { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
