using LIUConnect.Core.Models;
using LIUConnect.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LIUConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        public MobileController(ApplicationDbContext context)
        {
            _context = context;                
        }

        [HttpGet("JobOfferings")]
        public async Task<IActionResult> JobOfferings()
        {
            var vacancies = await _context.
               Vacancies.
               Include(v => v.Major).
               Include(v => v.Recruiter).
               Select(v => new
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
                   v.Recruiter.CompanyName,
                   majorName = v.Major.MajorName,
                   RecruiterUsername = v.Recruiter.User.Username,
                   Comments = v.Comments.Select(c => new
                   {
                       c.ID,
                       c.Content,
                       c.User.Username 
                   }).ToList()
               })
        .ToListAsync();
            if (vacancies.Count == 0)
            {
                return NotFound("No vacancies Found");
            }
            return Ok(vacancies);
        }


        [HttpGet("GetVacancyByMajor/{MajorID}")]
        public async Task<IActionResult> GetVacancyByMajor(int MajorID)
        {
            var vacancies = await _context.Vacancies.
                Where(v => v.MajorID == MajorID && v.isActive == true).
                Include(v => v.Major).
                Include(v => v.Recruiter).
                Select(v => new
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
                    Comments = v.Comments.Select(c => new
                    {
                        c.ID,
                        c.Content,
                        c.User.Username
                    }).ToList()
                })
                .ToListAsync();
            if (vacancies.Count == 0)
            {
                return NotFound("No vacancies were found");
            }
            return Ok(vacancies);
        }

        [HttpPost("Apply")]
        public async Task<IActionResult> Apply(string Email, int vacancyID)
        {
            try
            {
                var student = await _context.Students
                    .Where(s => s.User.Email == Email)
                    .FirstOrDefaultAsync();

                if (student == null)
                {
                    return NotFound("The Student is not found");
                }
                var existingApplication = await _context.Applications
                          .Where(a => a.StudentID == student.StudentID && a.VacancyID == vacancyID)
                        .FirstOrDefaultAsync();

                if (existingApplication != null)
                {
                    return BadRequest("The student has already applied to this vacancy.");
                }


                var applicationEntity = new Application
                {
                    Datetime = DateTime.Now,
                    VacancyID = vacancyID,
                    StudentID = student.StudentID,
                    status = "pending",
                };

                await _context.Applications.AddAsync(applicationEntity);
                await _context.SaveChangesAsync();

                return Ok("The Application has been successfully added");
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return BadRequest($"An error occurred while processing the application: {ex.Message}");
            }
        }

        [HttpGet("LocationFilter")]
        public async Task<IActionResult> GetVacancyByLocation(string location, int MajorID)
        {
            var Vacancies = await _context.Vacancies.Where(v => v.workLocation == location && v.MajorID == MajorID && v.isActive == true).
                Include(v => v.Major).
                Include(v => v.Recruiter).
                Select(v => new
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
                    Comments = v.Comments.Select(c => new
                    {
                        c.ID,
                        c.Content,
                        c.User.Username
                    }).ToList()
                })
        .ToListAsync();
            if (Vacancies.Count == 0)
            {
                return NotFound("No Vacancies Found");
            }
            return Ok(Vacancies);
        }
        [HttpGet("SalaryFilter")]
        public async Task<IActionResult> GetVacancyBySalary(int salary, int MajorID, string location)
        {
            var lowerSalaryBound = salary - 500;
            var upperSalaryBound = salary + 500;

            var Vacancies = await _context.Vacancies
                .Where(v => v.salary >= lowerSalaryBound && v.salary <= upperSalaryBound && v.MajorID == MajorID && v.workLocation == location && v.isActive == true)
                .Include(v => v.Major)
                .Include(v => v.Recruiter)
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
                    Comments = v.Comments.Select(c => new
                    {
                        c.ID,
                        c.Content,
                        c.User.Username
                    }).ToList()
                })
                .ToListAsync();

            if (Vacancies.Count == 0)
            {
                return NotFound("No Vacancies Found");
            }

            return Ok(Vacancies);
        }

        [HttpGet("GetAllVacancies")] // Updated
        public async Task<IActionResult> GetVacancies()
        {
            var vacancies = await _context.
                Vacancies.
                Where(v=>v.isActive == true).
                Include(v => v.Major).
                Include(v => v.Recruiter).
                Select(v => new
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
                    Comments = v.Comments.Select(c => new
                    {
                        c.ID,
                        c.Content,
                        c.User.Username
                    }).ToList()
                })
        .ToListAsync();
            if (vacancies.Count == 0)
            {
                return NotFound("No vacancies Found");
            }
            return Ok(vacancies);
        }
        [HttpGet("GetLast4Vacancies")] // Updated endpoint
        public async Task<IActionResult> GetLast4Vacancies()
        {
            var last4Vacancies = await _context
                .Vacancies
                .Where(v => v.isActive == true)
                .Include(v => v.Major)
                .Include(v => v.Recruiter)
                .OrderByDescending(v => v.VacancyId) // Order by VacancyId in descending order
                .Take(4) // Take the first 4 records
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
                    Comments = v.Comments.Select(c => new
                    {
                        c.ID,
                        c.Content,
                        c.User.Username
                    }).ToList()
                })
                .ToListAsync();

            if (last4Vacancies.Count == 0)
            {
                return NotFound("No vacancies Found");
            }

            return Ok(last4Vacancies);
        }

    }
    }

