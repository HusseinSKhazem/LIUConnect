using LIUConnect.Core.Interface;
using LIUConnect.Core.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LIUConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserAuthentication _UserAuthentication;
        public AdminController(IUserAuthentication UserAuthentication)
        {
            _UserAuthentication = UserAuthentication;
        }


        [HttpPost("AddAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAdmin(Register dto)
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
        public async Task<IActionResult> AddInstructor(Register dto)
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
        public async Task<IActionResult> AddStudent(StudentVM dto)
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
    }

}
