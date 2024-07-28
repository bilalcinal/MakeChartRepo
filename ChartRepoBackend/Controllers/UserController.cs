using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ChartRepoBackend.Data;
using ChartRepoBackend.Services;

namespace ChartRepoBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(UserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            var existingUser = _userService.GetByUsername(user.Username);
            if (existingUser != null)
            {
                return BadRequest("Username already exists");
            }

            var registeredUser = _userService.Register(user);
            return Ok(registeredUser);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var existingUser = _userService.GetByUsername(user.Username);
            if (existingUser == null || existingUser.Password != user.Password)
            {
                return Unauthorized("Invalid username or password");
            }

            var token = JwtHelper.GenerateJwtToken(existingUser, _configuration);
            return Ok(new { Token = token });
        }
    }
}
