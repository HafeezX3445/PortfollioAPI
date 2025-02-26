using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortofollioAPI.DTOs;
using PortofollioAPI.Helpers;
using PortofollioAPI.Models;
using PortofollioAPI.Repositories.Interfaces;

namespace PortofollioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(JwtHelper jwtHelper, IUser userRepo) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userRepo.VerifyLogin(loginData);

            if (user == null)
            {
                return BadRequest("Oops! Invalid SignIn credentials!!");
            }
            var token = jwtHelper.GenerateToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(CreateUserDTO userInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = new User
                {
                    Name = userInfo.Name,
                    Email = userInfo.Email,
                    Password = userInfo.Password,
                    DOB = userInfo.DOB,
                };

                await userRepo.CreateUserAsync(user);

                return Created();//CreatedAtAction("GetUserById", new { id = userInfo.UserId }, userInfo);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }

        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userRepo.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
