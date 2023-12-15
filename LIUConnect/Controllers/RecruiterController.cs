using LIUConnect.Core;
using LIUConnect.Core.Interface;
using LIUConnect.Core.Models;
using LIUConnect.Core.Models.Dtos;
using LIUConnect.EF;
using LIUConnect.EF.Repository;
using LIUConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LIUConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Recruiter")]
    public class RecruiterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RecruiterController(ApplicationDbContext context)
        {
            _context = context;
        }





        [HttpPost("PostVacancy")]
        public async Task<IActionResult> AddVacancy(VacancyDto dto)
        {
            try
            {
                var recruiter = _context.Recruiters
                .Include(r => r.User)
                .FirstOrDefault(r => r.User.Email == dto.RecruiterEmail);


                if (recruiter == null)
                {
                    return NotFound("Recruiter not found");
                }

                if (recruiter.isApproved == false)
                {
                    return BadRequest("Contact the administrator for approval");
                }

                var major = await _context.Majors.FindAsync(dto.MajorID);
                if (major == null)
                {
                    return NotFound("Major not found");
                }

                var vacancy = new Vacancy
                {
                    RecruiterID = recruiter.RecruiterID,
                    Status = dto.Status,
                    Description = dto.Description,
                    Requirements = dto.Requirements,
                    WorkingHours = dto.WorkingHours,
                    workLocation = dto.workLocation,
                    salary = dto.salary,
                    experience = dto.experience,
                    Responsibility = dto.Responsibility,
                    JobOffer = dto.JobOffer,
                    MajorID = dto.MajorID,
                    Recruiter = recruiter,
                    Major = major,
                };

                await _context.Vacancies.AddAsync(vacancy);
                await _context.SaveChangesAsync();

                return Ok("Vacancy Added");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }




        [HttpGet("VacancyList/{Email}")]
        public async Task<IActionResult> ListVacancy(string Email)
        {
            var vacancies = _context.Vacancies
                .Where(v => v.Recruiter.User.Email == Email)
                .Include(v => v.Major)
                .Include(v => v.Recruiter)
                .Select(v => new
                {
                    v.VacancyId,
                    v.Status,
                    v.Description,
                    v.Requirements,
                    v.WorkingHours,
                    v.JobOffer,
                    majorName = v.Major.MajorName,
                    RecruiterUsername = v.Recruiter.User.Username,
                    companyName = v.Recruiter.CompanyName

                })
                .ToList();
            if (vacancies.Any())
            {
                return Ok(vacancies);
            }
            else
            {
                return NotFound("No vacancies found for the given MajorID.");
            }
        }




        [HttpDelete("DeleteVacancy/{vacancyId}")]
        public async Task<IActionResult> DeleteVacancy(int vacancyId)
        {
            var vacancy = await _context.Vacancies.FindAsync(vacancyId);

            if (vacancy == null)
            {
                return NotFound($"Vacancy with ID {vacancyId} not found.");
            }
            _context.Vacancies.Remove(vacancy);
            await _context.SaveChangesAsync();

            return Ok($"Vacancy with ID {vacancyId} has been deleted.");
        }
        [HttpPost("GetApplicationsByRecommendations")]
        public async Task<IActionResult> GetApplications(int vacancyID)
        {
            var applications = await _context.Applications
                .Where(a => a.VacancyID == vacancyID)
                .Select(a => new
                {
                    a.ApplicationId,
                    a.VacancyID,
                    a.StudentID,
                    a.status,
                    a.Datetime,
                    Student = new
                    {
                        a.Student.User.Email,
                        a.Student.User.Username,
                        NumberOfRecommendations = a.Student.Recommendations.Count()
                    }
                })
                .OrderByDescending(a => a.Student.NumberOfRecommendations) // Sort by NumberOfRecommendations in descending order
                .ToListAsync();

            if (applications.Count == 0)
            {
                return NotFound("No applications were found for the specified vacancy");
            }

            return Ok(applications);
        }

        [HttpPost("AcceptApplication")]
        public async Task<IActionResult> Accept(int applicationID)
        {
            var application = await _context.Applications.Where(a => a.ApplicationId == applicationID).FirstOrDefaultAsync();
            if (application == null)
            {
                return NotFound("Application NotFound");
            }

            var student = await _context.Students.Where(s => s.StudentID == application.StudentID).Include(v => v.User).FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound("Student not found");
            }

            var vacancy = await _context.Vacancies.Where(v => v.VacancyId == application.VacancyID).Include(v => v.Recruiter).ThenInclude(r => r.User).FirstOrDefaultAsync();
            if (vacancy == null)
            {
                return NotFound("vacancy NotFound");
            }

            application.status = "Accepted";
            await _context.SaveChangesAsync();

            var StudentEmail = student.User.Email;
            var Body = $"<h2>Hello Dear {student.User.Username}</h2><br> We are thrilled to extend a warm welcome as we joyfully accept your application for the job vacancy. Please reach out with your preferred contact details in the upcoming week to schedule an interview. We look forward to getting to know you better and exploring the potential for a successful collaboration. Thank you for considering this opportunity with us.<br> {vacancy.Recruiter.User.Username}<br>{vacancy.Recruiter.CompanyName}";

            Email emailService = new Email();
            emailService.SendEmail(StudentEmail, Body);

            return Ok(1);
        }

        [HttpGet("GetRefferalDetail")]
        public async Task<IActionResult> getRefferalDetails(string email, int VacancyID)
        {
            var recruiter = await _context.Recruiters.Where(r => r.User.Email == email).FirstOrDefaultAsync();
            if (recruiter == null)
            {
                return NotFound("No recruiter With this Email");
            }
            var vacany = await _context.Vacancies.Where(v => v.VacancyId == VacancyID && v.RecruiterID == recruiter.RecruiterID).FirstOrDefaultAsync();
            if (vacany == null)
            {
                return NotFound("No refferals for this vacancy");
            }

            var refferals = await _context.Referral.Where(r => r.VacancyId == vacany.VacancyId).Select(v => new
            {
                v.Instructor.User.Username,
                v.ReferralDescription,
                StudentUsername = v.Student.User.Username,
                StudentEmail = v.Student.User.Email,
            })
                .ToListAsync();
            if (refferals == null)
            {
                return NotFound("No refferals for this vacancy");
            }
            return Ok(refferals);

        }
        [HttpGet("GetRefferals")]
        public async Task<IActionResult> GetRefferals(string email, int VacancyID)
        {
            var recruiter = await _context.Recruiters.Where(r => r.User.Email == email).FirstOrDefaultAsync();
            if (recruiter == null)
            {
                return NotFound("No recruiter With this Email");
            }
            var vacany = await _context.Vacancies.Where(v => v.VacancyId == VacancyID && v.RecruiterID == recruiter.RecruiterID).FirstOrDefaultAsync();
            if (vacany == null)
            {
                return NotFound("No refferals for this vacancy");
            }

            var refferals = await _context.Referral.Where(r => r.VacancyId == vacany.VacancyId).Select(v => new
            {
                v.Instructor.User.Username,
                StudentUsername = v.Student.User.Username,
                StudentEmail = v.Student.User.Email,
            })
                .ToListAsync();
            if (refferals.Count()==0)
            {
                return NotFound("No refferals for this vacancy");
            }
            return Ok(refferals);
        }
    }
}
    
