﻿using LIUConnect.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LIUConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public FilterController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("LocationFilter")]
        public async Task<IActionResult> GetVacancyByLocation(string location, int MajorID)
        {
            var Vacancies = await _context.Vacancies.Where(v => v.workLocation == location && v.MajorID == MajorID).
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
                .Where(v => v.salary >= lowerSalaryBound && v.salary <= upperSalaryBound && v.MajorID == MajorID && v.workLocation == location)
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


    }
 }
