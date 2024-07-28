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
        public IActionResult GetDataSet([FromBody] DatabaseConnection dbConnection, [FromQuery] string tableName, [FromQuery] string columns)
        {
            try
            {
                if (string.IsNullOrEmpty(columns))
                {
                    return BadRequest("The columns field is required.");
                }

                var dataSet = _databaseService.GetDataSet(dbConnection, tableName, columns);
                return Ok(dataSet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return BadRequest("Failed to get dataset");
            }
        }




        [HttpPost("get-columns-with-types")]
        public IActionResult GetColumnsWithTypes([FromBody] DatabaseConnection dbConnection, [FromQuery] string tableName)
        {
            try
            {
                var columns = _databaseService.GetColumnsWithTypes(dbConnection, tableName);
                return Ok(columns);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to get columns with types");
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
                return BadRequest("Failed to load database objects");
            }
        }

        [HttpPost("get-columns")]
        public IActionResult GetColumns([FromBody] DatabaseConnection dbConnection, [FromQuery] string tableName)
        {
            try
            {
                var columns = _databaseService.GetColumns(dbConnection, tableName);
                return Ok(columns);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to get columns");
            }
        }
    }
}
