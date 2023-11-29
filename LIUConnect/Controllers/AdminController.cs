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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAdmin([FromBody] Register dto)
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        [HttpGet("GetAllVacancies")]
        public async Task<IActionResult> GetVacancies()
        {
            var vacancies = await _context.
                Vacancies.
                Include(v=>v.Major).
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
            }).
            ToListAsync();
            if(vacancies.Count == 0) 
            {
                return NotFound("No vacancies Found");
            }
            return Ok(vacancies);
        }
    }
}
