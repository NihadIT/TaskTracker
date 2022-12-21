using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;

namespace TaskTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        // Database context
        TaskDbContext db;

        public ProjectController(TaskDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> ShowAll()
        {
            return await db.Projects.ToListAsync();
        }

        [HttpGet("id")]
        public async Task<ActionResult<Project>> GetById(int id)
        {
            Project project = await db.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (project == null)
                return NotFound();
            return new ObjectResult(project);
        }

        [HttpPost]
        public async Task<ActionResult<Project>> Post(Project project)
        {
            if (project == null)
            {
                return BadRequest();
            }

            db.Projects.Add(project);
            await db.SaveChangesAsync();
            return Ok(project);
        }

        [HttpPut]
        public async Task<ActionResult<Project>> Put(Project project)
        {
            if (project == null)
            {
                return BadRequest();
            }
            if (!db.Projects.Any(x => x.Id == project.Id))
            {
                return NotFound();
            }

            db.Update(project);
            await db.SaveChangesAsync();
            return Ok(project);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> Delete(int id)
        {
            Project project = db.Projects.FirstOrDefault(x => x.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            db.Projects.Remove(project);
            await db.SaveChangesAsync();
            return Ok(project);
        }

        // Sorting 
        [HttpGet("priority/ascending")]
        public async Task<ActionResult<IEnumerable<Project>>> Ascending()
        {
            var order = await db.Projects.OrderBy(x => x.Priority).ToListAsync();

            if (order == null)
                return NotFound();
            return new ObjectResult(order);
        }

        [HttpGet("priority/descending")]
        public async Task<ActionResult<IEnumerable<Project>>> Descending()
        {
            var order = await db.Projects.OrderByDescending(x => x.Priority).ToListAsync();

            if (order == null)
                return NotFound();
            return new ObjectResult(order);
        }

        [HttpGet("date/search")]
        public async Task<ActionResult<Project>> GetByDate(DateTime date)
        {
            // date format yyyy-mm-dd
            var order = await db.Projects.FirstOrDefaultAsync(x => x.StartDate == date);
            if (order == null)
                return NotFound();
            return new ObjectResult(order);
        }
    }
}
