using LIUConnect.Core.Interface;
using LIUConnect.Core.Models;
using LIUConnect.Core.Models.Dtos;
using LIUConnect.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LIUConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthentication _UserAuthentication;
        private readonly ApplicationDbContext _Context;
        public AuthController(ApplicationDbContext context,IUserAuthentication UserAuthentication)
        {
            _Context = context; 
            _UserAuthentication = UserAuthentication;
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> Register(Register dto)
        {
            try
            {
                var recruiter = await _UserAuthentication.Register(dto);
                return Ok(recruiter);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Userlogin([FromBody] UserDto userLogin)
        {
            try
            {
                var ClientToken = await _UserAuthentication.Login(userLogin);
                var history = new LoginHistory
                {
                    dateTime = DateTime.Now,
                    Email = userLogin.Email,
                };
                await _Context.loginIndex.AddAsync(history);
                await _Context.SaveChangesAsync();
                return Ok(ClientToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest("Incorrect email or password");
            }
        }
    }
}
