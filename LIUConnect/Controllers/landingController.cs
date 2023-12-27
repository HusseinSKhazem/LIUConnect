using LIUConnect.Core.Models;
using LIUConnect.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LIUConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class landingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public landingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetLast4byMajor")]
        public async Task<IActionResult> GetLast4(string Email)
        {
            var student = await _context.Students.Where(s=>s.User.Email==Email).FirstOrDefaultAsync();  
            if (student == null) 
            {
                return NotFound("StudentNotFound");
            }
            var vacancy = await _context.Vacancies.Where(v=>v.MajorID == student.MajorID && v.isActive == true).Include(v => v.Major)
                .Include(v => v.Recruiter)
                .OrderByDescending(v => v.VacancyId) 
                .Take(4)
                .Select(v => new
                {
                    v.VacancyId,
                    v.JobOffer,
                    v.Description,
                    v.Requirements,
                    v.WorkingHours,
                    v.Status,
                    v.Responsibility,
                    v.salary,
                    v.experience,
                    v.workLocation,
                    v.Recruiter.CompanyName,
                    majorName = v.Major.MajorName,
                    RecruiterUsername = v.Recruiter.User.Username,
                })
                .ToListAsync();

            if (vacancy.Count == 0)
            {
                return NotFound("No vacancies Found");
            }

            return Ok(vacancy);
        }

         [HttpGet("most-applied")]
        public async Task<ActionResult<IEnumerable<Vacancy>>> GetMostAppliedVacancies()
        {
            var mostAppliedVacancies = await _context.Vacancies.Where(v=>v.isActive == true)
                .OrderByDescending(v => v.Applications.Count)
                .Take(4)
                 .Include(v => v.Major)
                .Include(v => v.Recruiter)
                .Select(v => new
                {
                    v.VacancyId,
                    v.Status,
                    v.Description,
                    v.salary,
                    v.Requirements,
                    v.WorkingHours,
                    v.JobOffer,
                    majorName = v.Major.MajorName,
                    RecruiterUsername = v.Recruiter.User.Username,
                    companyName = v.Recruiter.CompanyName

                }).ToListAsync();

            if (mostAppliedVacancies == null || mostAppliedVacancies.Count == 0)
            {
                return NotFound();
            }

            return Ok(mostAppliedVacancies);
        }
    }
}
