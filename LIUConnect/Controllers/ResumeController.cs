using LIUConnect.Core.Models;
using LIUConnect.Core.Models.Dtos;
using LIUConnect.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace LIUConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ResumeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("CheckResume")]
        public async Task<IActionResult> Check(string Email)
        {
            try
            {
                var student = await _context.Students
                    .Include(s => s.User) 
                    .Where(s => s.User.Email == Email)
                    .FirstOrDefaultAsync();

                if (student == null)
                {
                    return NotFound("User NotFound");
                }

                var hasResume = _context.Resume.Any(r => r.StudentID == student.StudentID);

                if (hasResume)
                {
                    return Ok(1);
                }
                else
                {
                    return Ok(0);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("AddResume")]
        public async Task<IActionResult> AddResume([FromBody] ResumeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map ResumeDto to Resume
            var resume = new Resume
            {
                StudentID = dto.StudentID,
                location = dto.location,
                Socials = dto.Socials,
                projects = dto.projects,
                Name = dto.Name,
                Description = dto.Description,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                EducationalBackground = dto.EducationalBackground,
                WorkExperience = dto.WorkExperience,
                Skills = dto.Skills
            };

            // Add the resume to the database
            _context.Resume.Add(resume);
            await _context.SaveChangesAsync();

            return Ok("Added Resume");
        }

        [HttpGet("GetResumeByEmail")]
        public async Task<IActionResult> GetResume(string Email)
        {
            try
            {
                var student = await _context.Students
                    .Include(s => s.User)
                    .Where(s => s.User.Email == Email)
                    .FirstOrDefaultAsync();

                if (student == null)
                {
                    return NotFound("User NotFound");
                }

                var resume =await _context.Resume.Where(r => r.StudentID == student.StudentID)
                    .
                Select(v => new
                {
                   v.Name,
                   v.Description,
                   v.Email,
                   v.PhoneNumber,
                   v.EducationalBackground,
                   v.WorkExperience,
                   v.location,
                   v.Skills,
                   v.projects,
                   v.Socials
                })
        .FirstOrDefaultAsync();
                    
                return Ok(resume);
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

