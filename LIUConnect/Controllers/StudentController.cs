using LIUConnect.Core.Models;
using LIUConnect.Core.Models.Dtos;
using LIUConnect.EF;
using LIUConnect.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LIUConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetVacancyByMajor/{MajorID}")]
        public async Task<IActionResult> GetVacancyByMajor(int MajorID)
        {
            var vacancies = await _context.Vacancies.
                Where(v => v.MajorID == MajorID).
                Include(v => v.Major).
                Include(v => v.Recruiter).
                Select(v => new
                {
                    v.VacancyId,
                    v.Status,
                    v.Description,
                    v.Requirements,
                    v.WorkingHours,
                    v.JobOffer,
                    majorName = v.Major.MajorName,
                    RecruiterUsername = v.Recruiter.User.Username
                })
                .ToListAsync();
            if (vacancies.Count == 0)
            {
                return NotFound("No vacancies were found");
            }
            return Ok(vacancies);
        }

        [HttpPost("Apply")]
        public async Task<IActionResult> Apply(string Email, [FromForm] ApplicationDto application)
        {
            try
            {
                var student = await _context.Students.Where(s => s.User.Email == Email).FirstOrDefaultAsync();
                if (student == null)
                {
                    return NotFound("The Student is not found");
                }
                Files fileService = new Files();
                var application1 = new Application
                {
                    Datetime = DateTime.Now,
                    File = fileService.WriteFile(application.CvFile),
                    VacancyID = application.VacancyId,
                    StudentID = student.StudentID,
                    status = "pending",
                };
                await _context.Applications.AddAsync(application1);
                await _context.SaveChangesAsync();
                return Ok("The Application has been successfully added");
            }
            catch (Exception ex)
            {
                // Get the innermost exception
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return BadRequest($"An error occurred while processing the application: {ex.Message}");
            }
        }
    }
}
