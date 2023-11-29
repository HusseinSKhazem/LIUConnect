using LIUConnect.Core.Models;
using LIUConnect.Core.Models.Dtos;
using LIUConnect.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LIUConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public InstructorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("AddRecommendation")] //Working
        public async Task<IActionResult> AddRecommendation(RecommnedationDto recommendation )
        {
            try { 
            var instructor = await _context.Instructors.Where(i=>i.User.Email == recommendation.InstructorEmail).FirstOrDefaultAsync();
            var student = await _context.Students.Where(i => i.User.Email == recommendation.StudentEmail).FirstOrDefaultAsync();
                if (instructor == null) 
            {
                return NotFound("Instructor NotFound");
            }

            if (student == null) 
            {
                return NotFound("Student NotFound");
            }

            var recommned = new Recommendation
            {
                Date = DateTime.Now,
                InstructorID = instructor.InstructorId,
                StudentID = student.StudentID,
                Description = recommendation.description
            };
                await _context.Recommendations.AddAsync(recommned);
                await _context.SaveChangesAsync();
                return Ok("Recommendation add succesfully");
        }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
    }
    }
}
