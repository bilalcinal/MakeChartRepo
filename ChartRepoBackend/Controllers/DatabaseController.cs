using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChartRepoBackend.Data;
using ChartRepoBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChartRepoBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly UserDatabaseService _userDatabaseService;

        public DatabaseController(UserDatabaseService userDatabaseService)
        {
            _userDatabaseService = userDatabaseService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetDatabases()
        {
            var userId = int.Parse(User.FindFirst("id").Value);
            var databases = _userDatabaseService.GetByUserId(userId);
            return Ok(databases);
        }

        [HttpPost]
        [Authorize]
        public IActionResult SaveDatabase([FromBody] UserDatabase userDatabase)
        {
            if (userDatabase == null)
            {
                return BadRequest("Invalid database details.");
            }

            var userId = int.Parse(User.FindFirst("id").Value);
            userDatabase.UserId = userId;
            var savedDatabase = _userDatabaseService.Save(userDatabase);
            return Ok(savedDatabase);
        }
    }
}
