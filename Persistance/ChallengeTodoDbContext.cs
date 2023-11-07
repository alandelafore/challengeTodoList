using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ChallengeTodoDbContext : DbContext
{
    public ChallengeTodoDbContext(DbContextOptions<ChallengeTodoDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Entities.Task> Tasks { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<TaskCategory> TaskCategories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Domain.Entities.Task>()
            .Property(t => t.Name)
            .HasColumnName("name");

        modelBuilder.Entity<Domain.Entities.Task>()
            .Property(t => t.Completed)
            .HasColumnName("completed");

        modelBuilder.Entity<Domain.Entities.Task>()
            .Property(t => t.Deadline)
            .HasColumnName("deadline");

        modelBuilder.Entity<Domain.Entities.Task>()
            .Property(t => t.CreatedAt)
            .HasColumnName("created_at");

        modelBuilder.Entity<Domain.Entities.Task>()
            .Property(t => t.UpdatedAt)
            .HasColumnName("updated_at");


        modelBuilder.Entity<Category>()
            .Property(t => t.Id)
            .HasColumnName("id");

        modelBuilder.Entity<Category>()
            .Property(t => t.Name)
            .HasColumnName("name");

        modelBuilder.Entity<TaskCategory>()
            .HasKey(tc => new { tc.TaskId, tc.CategoryId });

        modelBuilder.Entity<TaskCategory>()
            .Property(t => t.TaskId)
            .HasColumnName("task_id");

        modelBuilder.Entity<TaskCategory>()
            .Property(t => t.CategoryId)
            .HasColumnName("category_id");


        base.OnModelCreating(modelBuilder);
    }
}
