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
        public async Task<IActionResult> AddResume(string Email,[FromBody] ResumeDto dto)
        {
          var student = await _context.Students.Where(s=>s.User.Email == Email).FirstOrDefaultAsync();
          if (student == null)
            {
                return NotFound("You aren't a Student");
            }

            // Map ResumeDto to Resume
            var resume = new Resume
            {
                StudentID = student.StudentID,
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

        [HttpPut("UpdateResume")]
        public async Task<IActionResult> UpdateResume(string Email, [FromBody] ResumeDto dto)
        {
            var student = await _context.Students
                .Include(s => s.User) // Make sure to include User for comparison
                .Where(s => s.User.Email == Email)
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound("You aren't a Student");
            }

            // Retrieve the existing resume
            var existingResume = await _context.Resume
                .Where(r => r.StudentID == student.StudentID)
                .FirstOrDefaultAsync();

            if (existingResume == null)
            {
                return NotFound("Resume not found");
            }

            // Update properties with new values from dto
            existingResume.location = dto.location;
            existingResume.Socials = dto.Socials;
            existingResume.projects = dto.projects;
            existingResume.Name = dto.Name;
            existingResume.Description = dto.Description;
            existingResume.Email = dto.Email;
            existingResume.PhoneNumber = dto.PhoneNumber;
            existingResume.EducationalBackground = dto.EducationalBackground;
            existingResume.WorkExperience = dto.WorkExperience;
            existingResume.Skills = dto.Skills;

            // Update the resume in the database
            _context.Resume.Update(existingResume);
            await _context.SaveChangesAsync();

            return Ok("Updated Resume");
        }
    }
}

