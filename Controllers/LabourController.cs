using Final_Project.Datalayer;
using Final_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabourController : ControllerBase
    {

        List<LabourMaster> labourList = new List<LabourMaster>();
        // 1. GET: Get All Labour Records

 
        [HttpGet("getall")]
        public  IActionResult GetAllLabours()
        {
         
            labourList = LabourDataLayer.GetAllLabours();

            if (labourList.Count == 0)
                return NotFound("No labour records found.");

            return Ok(labourList);
        }

        // 2. POST: Insert a New Labour Record

        [HttpPost("insert")]
        public IActionResult InsertLabour([FromBody] LabourMaster labour)
        {
            if (labour == null )
                return BadRequest("Invalid input data.");

            bool isInserted = LabourDataLayer.InsertLabour(labour);

            if (isInserted)
                return Ok("Labour record inserted successfully.");
            else
                return StatusCode(500, "Error inserting labour record.");
        }

        // 3. PUT: Update an Existing Labour Record

        [Authorize("Labour")]
        [HttpPut("update/{labourid}")]
        public IActionResult UpdateLabour(string labourid ,[FromBody] LabourMaster labour)
        {
            if (labour == null )
                return BadRequest("Invalid input data.");

            bool isUpdated = LabourDataLayer.UpdateLabour(labourid,labour);

            if (isUpdated)
                return Ok("Labour record updated successfully.");
            else
                return NotFound("Labour record not found.");
        }

        // 4. DELETE: Delete a Labour Record by ID

        [Authorize("Labour")]
        [HttpDelete("delete/{labourid}")]
        public IActionResult DeleteLabour(string labourid)
        {
            bool isDeleted = LabourDataLayer.DeleteLabour(labourid);

            if (isDeleted)
                return Ok("Labour record deleted successfully.");
            else
                return NotFound("Labour record not found.");
        }
    }
}