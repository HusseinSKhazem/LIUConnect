﻿using LIUConnect.Core;
using LIUConnect.Core.Models;
using LIUConnect.Core.Models.Dtos;
using LIUConnect.EF;
using LIUConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using static System.Net.Mime.MediaTypeNames;

namespace LIUConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public UserController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetUsersByRole/{role}")]
        public async Task<IActionResult> GetUsersByRole(int role)
        {
            var users = await _context.Users.Where(u => u.UserRole == role).ToListAsync();

            return Ok(users);
        }

        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile(string Email)
        {
            var existingProfile = await _context.UserDetails
                .Include(u => u.User).
                Where(u => u.User.Email == Email)
                  .Select(u => new
                  {
                   u.Id,
                   u.ProfilePicture,
                   u.Links,
                   u.Bio,
                   u.User.Username,
                   u.User.Email,
                   
                  }).
                FirstOrDefaultAsync();
            if (existingProfile == null)
            {
                return NotFound($"Profile  not found");
            }
            return Ok(existingProfile);
        }

        [HttpPut("UpdateProfilePicture")]
        public async Task<IActionResult> UpdateProfilePicture(string Email, [FromForm] ProfilePictureVM Pic)
        {
            try
            {
                var existingProfile = await _context.UserDetails.Where(u=>u.User.Email == Email).FirstOrDefaultAsync(); 

                if (existingProfile == null)
                {
                    return NotFound($"Profile  not found");
                }

                if (Pic.ProfilePicture != null)
                {
                   
                    Files fileService = new Files();
                    existingProfile.ProfilePicture = fileService.WriteFile(Pic.ProfilePicture);

                    _context.UserDetails.Update(existingProfile);
                    await _context.SaveChangesAsync();

                    return Ok("Profile picture updated successfully");
                }
                else
                {
                    return BadRequest("Profile picture file is required");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(string Email, [FromForm] ProfileVM profile)
        {
            try
            {
                var existingProfile = await _context.UserDetails.Where(u=>u.User.Email==Email).FirstOrDefaultAsync();

                if (existingProfile == null)
                {
                    return NotFound($"Profile  not found");
                }

               
                   
                    existingProfile.Bio = profile.Bio;
                    existingProfile.Links = profile.Links;

                    _context.UserDetails.Update(existingProfile);
                    await _context.SaveChangesAsync();
                

                return Ok("Profile updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("ChangeEmail/{userID}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmail(int userID, [FromBody] string Email)
        {
            try
            {
                var user = await _context.Users.Where(u => u.UserId == userID).FirstAsync();
                user.Email = Email;
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("DeleteUsers/{userID}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int userID)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userID);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Ok("User Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("ApproveRecruiters/{RecruiterID}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveRecruiter(int RecruiterID)
        {
            try
            {
                var Recruiter = await _context.Recruiters.FirstOrDefaultAsync(r => r.RecruiterID == RecruiterID);
               
                if (Recruiter == null)
                {
                    return NotFound("User not found");
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == Recruiter.UserID);

                if (user == null)
                {
                    return NotFound("User not found");
                }   
                var RecruiterEmail = user.Email;
                var Body = "<h2>Hello Dear Recruiter</h2><br> Your Account has been approved, you can proceed and good luck!";
                Recruiter.isApproved = true;
                Email emailService = new Email();
                emailService.SendEmail(RecruiterEmail, Body);
                await _context.SaveChangesAsync();

                return Ok("Approved");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetRecruiterApprove")]
        public async Task<IActionResult> GetRecruiterApprove()
        {
            var recruiters = await _context.Recruiters
                .Where(r => r.isApproved == false)
                .Select(r => new
                {
                    r.RecruiterID,
                    r.officialFiles,
                    r.isApproved,
                    Username = r.User.Username
                })
                .ToListAsync();

            return Ok(recruiters);
        }



        [HttpGet("GetProfilePicture/{profileId}")]
        public IActionResult GetProfilePicture(int profileId)
        {
            try
            {
                var existingProfile = _context.UserDetails.Find(profileId);

                if (existingProfile == null || string.IsNullOrEmpty(existingProfile.ProfilePicture))
                {
                    return NotFound($"Profile with ID {profileId} or profile picture not found");
                }

                var imagePath = Path.Combine("C:\\Users\\Dark\\source\\repos\\LIUConnect\\LIUConnect\\Upload\\Files", existingProfile.ProfilePicture);


                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);


                string contentType = "image/jpeg"; 

 
                return File(imageBytes, contentType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}



