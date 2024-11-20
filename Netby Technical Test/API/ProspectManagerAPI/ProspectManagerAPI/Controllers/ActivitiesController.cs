using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProspectManagerAPI.Dto;
using ProspectManagerAPI.EntityFramework;
using ProspectManagerAPI.Models;

namespace ProspectManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class ActivitiesController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private static readonly string[] sourceArray = ["llamada", "mensaje", "correo"];

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities(Guid prospectId)
        {

            var prospect = await _context.Prospects
                .Include(p => p.Activities)
                .FirstOrDefaultAsync(p => p.Id == prospectId);

            if (prospect == null)
                return NotFound();

            var response = new ProspectDto
            {
                Id = prospect.Id,
                Name = prospect.Name,
                Email = prospect.Email,
                CellPhoneNumber = prospect.CellPhoneNumber,
                Activities = prospect.Activities.Select(a => new ActivityDto
                {
                    Id = a.Id,
                    Description = a.Description,
                    Date = a.Date.ToString("dd/MM/yyyy hh:mm:ss"),
                    Type = a.Type,
                    Rating = a.Rating
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Activity>> CreateActivity(Activity activity)
        {
            if (!await _context.Prospects.AnyAsync(p => p.Id == activity.ProspectId))
                return NotFound();

            if (!sourceArray.Contains(activity.Type.ToLower()) ||
                activity.Rating < 1 || activity.Rating > 5)
            {
                return BadRequest("El tipo o la calificación son inválidos.");
            }
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetActivities), activity);
        }

        [HttpPut]
        public async Task<ActionResult<Activity>> UpdateActivity(ActivityDto activity)
        {

            var existing = await _context.Activities.FindAsync(activity.Id);
            if (existing == null)
                return NotFound();

            if (!sourceArray.Contains(activity.Type.ToLower()) ||
                activity.Rating < 1 || activity.Rating > 5)
            {
                return BadRequest("El tipo o la calificación son inválidos.");
            }

            existing.Date = DateTime.Parse(activity.Date);
            existing.Description = activity.Description;
            existing.Type = activity.Type;
            existing.Rating = activity.Rating;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var activity = await _context.Activities
                .FirstOrDefaultAsync(a => a.Id == id);

            if (activity == null)
                return NotFound();

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return Ok("Eliminado");
        }
    }
}
