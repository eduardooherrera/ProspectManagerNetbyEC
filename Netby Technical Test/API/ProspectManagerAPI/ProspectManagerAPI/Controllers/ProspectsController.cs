using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProspectManagerAPI.EntityFramework;
using ProspectManagerAPI.Models;

namespace ProspectManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProspectsController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prospect>>> GetProspectos()
        {
            return await _context.Prospects.ToListAsync();
        }


    }
}
