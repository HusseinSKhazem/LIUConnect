using LIUConnect.Core;
using LIUConnect.Core.Models;
using LIUConnect.Core.Models.Dtos;
using LIUConnect.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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
        public async Task<IActionResult> GetProfile(int profileId)
        {
            var existingProfile = await _context.UserDetails.FindAsync(profileId);
            if (existingProfile == null)
            {
                return NotFound($"Profile with ID {profileId} not found");
            }
            return Ok(existingProfile);
        }



        [HttpPost("UpdateProfile/{profileId}")]
        public async Task<IActionResult> UpdateProfile(int profileId, [FromForm] ProfileVM profile)
        {
            try
            {
                var existingProfile = await _context.UserDetails.FindAsync(profileId);

                if (existingProfile == null)
                {
                    return NotFound($"Profile with ID {profileId} not found");
                }

                if (profile.ProfilePicture != null && profile.ProfilePicture.Length > 0)
                {
                    byte[] fileContent;
                    using (var ms = new MemoryStream())
                    {
                        profile.ProfilePicture.CopyTo(ms);
                        fileContent = ms.ToArray();
                    }

                    string base64String = Convert.ToBase64String(fileContent);
                    existingProfile.ProfilePicture = base64String;
                    existingProfile.Bio = profile.Bio;
                    existingProfile.Links = profile.Links;

                    _context.UserDetails.Update(existingProfile);
                    await _context.SaveChangesAsync();
                }

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

    }

}



