using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("task_categories")]
public class TaskCategory
{
    public int ? TaskId { get; set; }
    public int  ? CategoryId { get; set; }
    public virtual Task ? Task { get; set; }

    public virtual Category ? Category { get; set; }
}
