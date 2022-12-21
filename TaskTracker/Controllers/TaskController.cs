using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;

namespace TaskTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        TaskDbContext db;
        public TaskController(TaskDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> ShowAll()
        {
            return await db.Tasks.ToListAsync();
        }

        [HttpGet("id")]
        public async Task<ActionResult<Models.Task>> GetById(int id)
        {
            Models.Task task = await db.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (task == null)
                return NotFound();
            return new ObjectResult(task);
        }

        [HttpPost]
        public async Task<ActionResult<Models.Task>> Post(Models.Task task)
        {
            if(db.Projects.FirstOrDefault(x =>
            x.Id == task.Project_id) == null || task == null)
            {
                return BadRequest();
            }

            db.Tasks.Add(task);
            await db.SaveChangesAsync();
            return Ok(task);
        }

        [HttpPut]
        public async Task<ActionResult<Models.Task>> Put(Models.Task task)
        {
            if (task == null)
            {
                return BadRequest();
            }
            if (!db.Tasks.Any(x => x.Id == task.Id))
            {
                return NotFound();
            }

            db.Update(task);
            await db.SaveChangesAsync();
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Task>> Delete(int id)
        {
            Models.Task task = db.Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            db.Tasks.Remove(task);
            await db.SaveChangesAsync();
            return Ok(task);
        }

        // Sorting 
        [HttpGet("priority/ascending")]
        public async Task<ActionResult<IEnumerable<Models.Task>>> Ascending()
        {
            var order = await db.Tasks.OrderBy(x => x.Priority).ToListAsync();

            if (order == null)
                return NotFound();
            return new ObjectResult(order);
        }

        [HttpGet("priority/descending")]
        public async Task<ActionResult<IEnumerable<Models.Task>>> Descending()
        {
            var order = await db.Tasks.OrderByDescending(x => x.Priority).ToListAsync();

            if (order == null)
                return NotFound();
            return new ObjectResult(order);
        }

    }
}
