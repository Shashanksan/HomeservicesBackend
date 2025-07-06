using Final_Project.Datalayer;
using Final_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {


 
        [HttpGet("getall")]

        public IActionResult GetAllLabours()
        {
            var Hist = HistoryDataLayer.GetAllHistory();

            if (Hist.Count == 0)
                return NotFound("No labour records found.");

            return Ok(Hist);
        }

        //by ID

        // 5. GET: Get Labour Record by ID
        [Authorize("Labour")]
        [HttpGet("getbyid/{labourid}")]
        public IActionResult GetLabourById(string labourid)
        {
            var hist = HistoryDataLayer.sp_GetHistoryByID(labourid);

            if (hist == null)
                return NotFound("Labour record not found.");

            return Ok(hist);
        }
        // 2. POST: Insert a New Labour Record
        [Authorize(Roles ="Labour")]
        [HttpPost("insert")]

        public IActionResult InsertLabour([FromBody] HistoryMaster hist)
        {
            if (hist == null)
                return BadRequest("Invalid input data.");

            bool isInserted = HistoryDataLayer.InsertLabour(hist);

            if (isInserted)
                return Ok("Labour record inserted successfully.");
            else
                return StatusCode(500, "Error inserting labour record.");
        }

        // 3. PUT: Update an Existing Labour Record
        [HttpPut("update/{id}")]
        [Authorize("Labour")]
        public IActionResult UpdateLabour(int id, [FromBody] HistoryMaster hist)
        {
            if (hist == null || id != hist.ID)
                return BadRequest("Invalid input data.");

            bool isUpdated = HistoryDataLayer.UpdateLabour(hist);

            if (isUpdated)
                return Ok("Labour record updated successfully.");
            else
                return NotFound("Labour record not found.");
        }

        // 4. DELETE: Delete a Labour Record by ID
        [HttpDelete("delete/{id}")]
        [Authorize("Labour")]
        public IActionResult DeleteLabour(int id)
        {
            bool isDeleted = HistoryDataLayer.DeleteLabour(id);

            if (isDeleted)
                return Ok("Labour record deleted successfully.");
            else
                return NotFound("Labour record not found.");
        }
    }
}
