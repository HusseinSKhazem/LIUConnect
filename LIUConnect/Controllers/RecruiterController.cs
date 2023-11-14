using LIUConnect.Core;
using LIUConnect.Core.Interface;
using LIUConnect.Core.Models;
using LIUConnect.Core.Models.Dtos;
using LIUConnect.EF;
using LIUConnect.EF.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LIUConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                .FirstOrDefault(r => r.RecruiterID == dto.RecruiterID);


                if (recruiter == null)
                {
                    return NotFound("Recruiter not found");
                }

                var major = await _context.Majors.FindAsync(dto.MajorID);
                if (major == null)
                {
                    return NotFound("Major not found");
                }

                var vacancy = new Vacancy
                {
                    RecruiterID = dto.RecruiterID,
                    Status = dto.Status,
                    Description = dto.Description,
                    Requirements = dto.Requirements,
                    WorkingHours = dto.WorkingHours,
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




        [HttpGet("VacancyList")]
        public async Task<IActionResult> ListVacancy(int MajorID)
        {
            var vacancies = _context.Vacancies
                .Where(v => v.MajorID == MajorID)
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
                    RecruiterUsername = v.Recruiter.User.Username
                    
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
        public async Task<IActionResult> DeleteVacancy(int vacancyId,int RecruiterID)
        {
            var vacancy = await _context.Vacancies.FindAsync(vacancyId);

            if (vacancy == null)
            {
                return NotFound($"Vacancy with ID {vacancyId} not found.");
            }

            if (vacancy.RecruiterID! == RecruiterID)
            {
                return BadRequest("You aren't the Recruiter of this Vacancy.");
            }

            _context.Vacancies.Remove(vacancy);
            await _context.SaveChangesAsync();

            return Ok($"Vacancy with ID {vacancyId} has been deleted.");
        }




        [HttpPut("UpdateVacancy/{vacancyId}")]
        public async Task<IActionResult> UpdateVacancy(int vacancyId, VacancyDto updatedVacancyDto)
        {
            try
            {
                var vacancy = await _context.Vacancies.FindAsync(vacancyId);

                if (vacancy == null)
                {
                    return NotFound($"Vacancy with ID {vacancyId} not found.");
                }

                var recruiter = await _context.Recruiters
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(r => r.RecruiterID == updatedVacancyDto.RecruiterID);

                if (recruiter == null)
                {
                    return NotFound("Recruiter not found");
                }

                var major = await _context.Majors.FindAsync(updatedVacancyDto.MajorID);
                if (major == null)
                {
                    return NotFound("Major not found");
                }
                if (vacancy.RecruiterID != recruiter.RecruiterID)
                {
                    return BadRequest("You aren't the recruiter of this Vacancy");
                }

                vacancy.Status = updatedVacancyDto.Status;
                vacancy.Description = updatedVacancyDto.Description;
                vacancy.Requirements = updatedVacancyDto.Requirements;
                vacancy.WorkingHours = updatedVacancyDto.WorkingHours;
                vacancy.JobOffer = updatedVacancyDto.JobOffer;
                vacancy.MajorID = updatedVacancyDto.MajorID;
                vacancy.Recruiter = recruiter;
                vacancy.Major = major;

                await _context.SaveChangesAsync();

                return Ok($"Vacancy with ID {vacancyId} has been updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


    }
}
    
