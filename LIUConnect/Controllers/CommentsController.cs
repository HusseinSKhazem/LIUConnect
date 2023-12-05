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
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CommentsController(ApplicationDbContext context)
        {
            _context=context;   
        }
        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment(int vacancyID, CommentDto comment)
        {
            try
            {
                var user = await _context.Users.Where(u => u.Email == comment.UserEmail).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound("User Not Found");
                }

                var Comment = new Comment
                {
                    UserID = user.UserId,
                    dateTime = DateTime.Now,
                    Content = comment.content,
                    VacancyId = vacancyID
                };

                await _context.Comments.AddAsync(Comment);
                await _context.SaveChangesAsync();

                return Ok("Comment added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding comment: {ex.Message}. InnerException: {ex.InnerException?.Message}");
            }
        }

        [HttpGet("GetComments")]
        public async Task<IActionResult> GetComments(int vacancyID)
        {
            
            var Commments = await _context.Comments.Where(c=>c.VacancyId==vacancyID).Select(
                c => new
            {
                c.Content,
                c.ID,
                c.dateTime,
                username = c.User.Username,
                profilePicture = _context.UserDetails.Where(u=>u.User.UserId == c.User.UserId).Select(j=> new
                {
                    j.ProfilePicture
                }).FirstOrDefault()     
            })
                
                .ToListAsync();
            if(Commments.Count==0) { return NotFound("no Comments"); }
            return Ok(Commments);
        }
    }
    }

