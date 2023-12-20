using LIUConnect.Core.Models;
using LIUConnect.Core.Models.Dtos;
using LIUConnect.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LIUConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public InstructorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("AddRecommendation")] //Working
        public async Task<IActionResult> AddRecommendation(RecommnedationDto recommendation)
        {
            try
            {
                var instructor = await _context.Instructors
                    .Where(i => i.User.Email == recommendation.InstructorEmail)
                    .FirstOrDefaultAsync();

                var student = await _context.Students
                    .Where(i => i.User.Email == recommendation.StudentEmail)
                    .FirstOrDefaultAsync();

                if (instructor == null)
                {
                    return NotFound("Instructor Not Found");
                }

                if (student == null)
                {
                    return NotFound("Student Not Found");
                }

                var existingRecommendation = await _context.Recommendations
                    .FirstOrDefaultAsync(r =>
                        r.InstructorID == instructor.InstructorId &&
                        r.StudentID == student.StudentID);

                if (existingRecommendation != null)
                {
                    return BadRequest("Recommendation already exists for this instructor and student");
                }

                var recommned = new Recommendation
                {
                    Date = DateTime.Now,
                    InstructorID = instructor.InstructorId,
                    StudentID = student.StudentID,
                    Description = recommendation.description
                };

                await _context.Recommendations.AddAsync(recommned);
                await _context.SaveChangesAsync();

                return Ok("Recommendation added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Refer")]
        public async Task<IActionResult> Refer(ReferalVM dto)
        {
            try
            {
                var existingReferral = await _context.Referral
                    .Where(r => r.VacancyId == dto.vacancyID && r.Instructor.User.Email == dto.InstructorEmail)
                    .FirstOrDefaultAsync();

                if (existingReferral != null)
                {
                    // Instructor has already referred a student for this vacancy
                    return BadRequest("Instructor has already referred a student for this vacancy.");
                }

                var vacancy = await _context.Vacancies
                    .Where(v => v.VacancyId == dto.vacancyID)
                    .FirstOrDefaultAsync();

                var student = await _context.Students
                    .Where(s => s.User.Email == dto.StudentEmail)
                    .FirstOrDefaultAsync();

                var instructor = await _context.Instructors
                    .Where(i => i.User.Email == dto.InstructorEmail)
                    .FirstOrDefaultAsync();

                if (vacancy == null)
                {
                    return NotFound();
                }

                if (student == null)
                {
                    return BadRequest("Student not found.");
                }

                if (instructor == null)
                {
                    return BadRequest("Instructor not found.");
                }

                var referal = new Referral
                {
                    VacancyId = dto.vacancyID,
                    StudentId = student.StudentID,
                    InstructorId = instructor.InstructorId,
                    ReferralDescription = dto.Description
                };

                await _context.Referral.AddAsync(referal);
                await _context.SaveChangesAsync();

                return Ok("refferal Added successfully!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
