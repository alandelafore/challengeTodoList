using AutoMapper;

namespace Application.Todo.Commands;

public class CreateTaskDto
{
    public string TaskName { get; set; }
    public DateTime Deadline { get; set; }

    public List<string> Categories { get; set; }

}
