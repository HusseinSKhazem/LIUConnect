using LIUConnect.Core.Interface;
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
        public AuthController(IUserAuthentication UserAuthentication)
        {
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
        public async Task<string> Userlogin(UserDto userLogin)
        {
            try
            {
                var ClientToken = await _UserAuthentication.Login(userLogin);

                return ClientToken;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
