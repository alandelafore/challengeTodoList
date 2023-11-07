using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("tasks")]
public class Task 
{
    public int ? Id { get; set; }
    public string Name { get; set; }
    public int Completed { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
