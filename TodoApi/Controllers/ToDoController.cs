using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Application.Todo.Commands;
using Domain.Entities;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDoController : ControllerBase
{
    private readonly ChallengeTodoDbContext _context;

    public ToDoController(ChallengeTodoDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto task)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoriesIds = new List<int>();
            for (int i = 0; i < task.Categories.Count; i++)
            {
                var category = _context.Categories.FirstOrDefault(c => c.Name == task.Categories[i]);
                if (category is null)
                {
                    var newCategory = new Category()
                    {
                        Name = task.Categories[i]
                    };
                    _context.Categories.Add(newCategory);
                    await _context.SaveChangesAsync();
                    categoriesIds.Add(newCategory.Id);
                }
                else {
                    categoriesIds.Add(category.Id);
                }
            };

            var Task = new Domain.Entities.Task()
            {
                Name = task.TaskName,
                Completed = 0,
                Deadline = task.Deadline,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _context.Tasks.Add(Task);

            await _context.SaveChangesAsync();

            foreach (var categoryId in categoriesIds)
            {
                var taskCategory = new TaskCategory()
                {
                    TaskId = Task.Id,
                    CategoryId = categoryId
                };
                _context.TaskCategories.Add(taskCategory);
            }

            await _context.SaveChangesAsync();

            return Ok(Task);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error : " + ex.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        try
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound(); 
            }
            var taskCategories = _context.TaskCategories.Where(tc => tc.TaskId == id);
            _context.TaskCategories.RemoveRange(taskCategories);

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error : " + ex.Message);
        }
    }

    [HttpPut("markAsCompleted/{id}")]
    public async Task<IActionResult> MarkTaskAsCompleted(int id)
    {
        try
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound(); 
            }
            task.UpdatedAt = DateTime.Now;
            task.Completed = 1;
            await _context.SaveChangesAsync();

            return Ok(task);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error : " + ex.Message);
        }
    }

    [HttpPut("updateDeadLine/{id}")]
    public async Task<IActionResult> UpdateTaskDeadline(int id, [FromBody] DateTime newDeadline)
    {
        try
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }
            task.UpdatedAt = DateTime.Now;
            task.Deadline = newDeadline;
            await _context.SaveChangesAsync();

            return Ok(task);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno del servidor: " + ex.Message);
        }
    }

    [HttpGet]
    public IActionResult GetTasks()
    {
        try
        {
            var tasks = _context.Tasks.OrderBy(t => t.CreatedAt).ToList();

            return Ok(tasks);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno del servidor: " + ex.Message);
        }
    }

}