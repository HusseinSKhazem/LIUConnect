using LIUConnect.Core.Interface;
using LIUConnect.Core.Models.Dtos;
using LIUConnect.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LIUConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserAuthentication _UserAuthentication;
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context, IUserAuthentication UserAuthentication)
        {
            _context = context;
            _UserAuthentication = UserAuthentication;
        }


        [HttpPost("AddAdmin")]
     
        public async Task<IActionResult> AddAdmin([FromBody] adminRegister dto)
        {
            try
            {
                var admin = await _UserAuthentication.AddAdmin(dto);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AddInstructor")]
      
        public async Task<IActionResult> AddInstructor([FromBody] Register dto)
        {
            try
            {
                var Instructor = await _UserAuthentication.AddInstructor(dto);
                return Ok(Instructor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("AddStudent")]
      
        public async Task<IActionResult> AddStudent([FromBody] StudentVM dto)
        {
            try
            {
                var Student = await _UserAuthentication.AddStudent(dto);
                return Ok(Student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllVacancies")] // Updated
        public async Task<IActionResult> GetVacancies()
        {
            var vacancies = await _context.
                Vacancies.
                Include(v=>v.Major).
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
