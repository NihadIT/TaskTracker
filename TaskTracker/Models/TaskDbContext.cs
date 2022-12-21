using Microsoft.EntityFrameworkCore;

namespace TaskTracker.Models
{
    public class TaskDbContext: DbContext
    {
       public DbSet<Project> Projects { get; set; }
       public DbSet<Task> Tasks { get; set; }

        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
