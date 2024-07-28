using ChartRepoBackend.Models;
using ChartRepoBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ChartRepoBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChartController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public ChartController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpPost("get-dataset")]
        public IActionResult GetDataSet([FromBody] DatabaseConnection dbConnection, [FromQuery] string tableName)
        {
            try
            {
                var dataSet = _databaseService.GetDataSet(dbConnection, tableName);
                return Ok(dataSet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return BadRequest("Failed to get dataset");
            }
        }

        [HttpPost("get-database-objects")]
        public IActionResult GetDatabaseObjects([FromBody] DatabaseConnection dbConnection)
        {
            try
            {
                var objects = _databaseService.GetDatabaseObjects(dbConnection);
                return Ok(objects);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return BadRequest("Failed to load database objects");
            }
        }
    }
}

