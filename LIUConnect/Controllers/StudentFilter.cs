using LIUConnect.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LIUConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentFilter : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentFilter(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("StudentMajorFilter")]
        public async Task<IActionResult> FilterByMajor(int majorID)
        {
            var students = await _context.Students.Where(s => s.MajorID == majorID).Include(u => u.User).Include(m => m.Major)
                .
                Select(v => new
                {
                    v.StudentID,
                    v.Major.MajorName,
                    v.User.Username,
                    v.User.Email,
                })
        .ToListAsync();
            if (students.Count() == 0)
            {
                return NotFound("No Students in this Major");
            }
            return Ok(students);
        }

        [HttpGet("StudentEmailFilter")]
        public async Task<IActionResult> FilterByEmail(string Email)
        {
            var students = await _context.Students.Where(s => s.User.Email == Email).Include(u => u.User).Include(m => m.Major)
                .
                Select(v => new
                {
                    v.StudentID,
                    v.Major.MajorName,
                    v.User.Username,
                    v.User.Email,
                })
        .FirstOrDefaultAsync();
            if (students == null)
            {
                return NotFound("No Students with this Email");
            }
            return Ok(students);
        }

        [HttpGet("GetStudentBySkills")]
        public async Task<IActionResult> GetSkills(string skills)
        {
            var resume = await _context.Resume.Where(r => r.Skills.Contains(skills)).
                Select(v => new
                {
                   v.Student.User.Email,
                   v.Student.User.Username,
                   v.Skills,
                }).ToListAsync();

            if (resume.Count() == 0) 
            {
                return NotFound("No Student with such skills");
            }

            return Ok(resume);
        }

       
    }
}