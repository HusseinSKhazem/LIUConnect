using LIUConnect.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LIUConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MajorController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetMajors")]
        public async Task<IActionResult> GetMajors()
        {
            var major = await _context.Majors.ToListAsync();
            if (major == null) { return NotFound(); }
            return Ok(major);
        }
    }
}
