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
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ApplicationController(ApplicationDbContext context)
        {
            _context = context;
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
    }
    }
