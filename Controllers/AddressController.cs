using Final_Project.Datalayer;
using Final_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 public class AddressController : ControllerBase
    {
        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("SaveFull")]
        public IActionResult SaveLocation([FromBody] AddressMaster location)
        {
          try
            {
                Random random = new Random();
                string AddressID = random.Next(10000000, 99999999).ToString();

                Console.WriteLine($"Received Location: UserID={location.UserId}, City={location.City}, Latitude={location.Latitude}, Longitude={location.Longitude}");

                string storedProcedure = "InsertLocationData";
                SqlParameter[] parameters = {
                    new SqlParameter("Addressid", AddressID),
                    new SqlParameter("@UserID", location.UserId),
                    new SqlParameter("@Latitude", location.Latitude),
                    new SqlParameter("@Longitude", location.Longitude),
                    new SqlParameter("@City", location.City ?? (object)DBNull.Value),
                    new SqlParameter("@Area", location.Area ?? (object)DBNull.Value),
                    new SqlParameter("@Ward", location.Ward ?? (object)DBNull.Value),
                    new SqlParameter("@Landmark", location.Landmark ?? (object)DBNull.Value),
                    new SqlParameter("@Street", location.Street ?? (object)DBNull.Value),
                    new SqlParameter("@FullAddress", location.FullAddress)
                };

                AddressDataLayer.GetReaderDataFromSP(storedProcedure, parameters);

                return Ok(new { message = "Location saved successfully!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("SaveSimple")]
        public IActionResult SaveSimpleLocation([FromBody] AddressMaster location)
        {
           

            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@UserID", location.UserId),
                    new SqlParameter("@FullAddress", location.FullAddress)
                };

                // Calling the stored procedure using AdoData
                int rowsAffected = AddressDataLayer.SaveInformation("EXEC InsertShaLocation @UserID, @FullAddress", parameters);

                if (rowsAffected > 0)
                {
                    return Ok(new { message = "Location saved successfully!" });
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to save location." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }

        [Authorize(Roles ="User")]
        [HttpGet]
        [Route("GetAllLocations")]
        public IActionResult GetAllLocations()
        {
            List<AddressMaster> addressList = new List<AddressMaster>();
            string query = "SELECT * FROM addressuser";

            DataTable dt = AddressDataLayer.GetDataTable(query);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AddressMaster address = new AddressMaster();
                address.AddressId = Convert.ToString(dt.Rows[i]["AddressId"]);
                address.City = Convert.ToString(dt.Rows[i]["city"]);
                address.Street = Convert.ToString(dt.Rows[i]["street"]);
                address.Latitude = Convert.ToDouble(dt.Rows[i]["latitude"]);
                address.Longitude = Convert.ToDouble(dt.Rows[i]["longitude"]);
                address.FullAddress = Convert.ToString(dt.Rows[i]["FullAddress"]);
                address.UserId = Convert.ToString(dt.Rows[i]["userId"]);
                address.Area = Convert.ToString(dt.Rows[i]["area"]);
                address.Ward = Convert.ToString(dt.Rows[i]["ward"]);
                address.Landmark = Convert.ToString(dt.Rows[i]["LandMark"]);

                addressList.Add(address);
            }

            return Ok(addressList);
        }
        [Authorize("User")]
        [HttpGet]
        [Route("GetLocationById/{addressId}")]
        public IActionResult GetLocationById(int addressId)
        {
            List<AddressMaster> addressList = new List<AddressMaster>();
            string query = "SELECT * FROM addressuser WHERE id = @AddressId";

            SqlParameter[] parameters = { new SqlParameter("@AddressId", addressId) };
            DataTable dt = AddressDataLayer.GetDataTable(query, parameters);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AddressMaster address = new AddressMaster();
                address.AddressId = Convert.ToString(dt.Rows[i]["AddressId"]);
                address.City = Convert.ToString(dt.Rows[i]["city"]);
                address.Street = Convert.ToString(dt.Rows[i]["street"]);
                address.Latitude = Convert.ToDouble(dt.Rows[i]["latitude"]);
                address.Longitude = Convert.ToDouble(dt.Rows[i]["longitude"]);
                address.FullAddress = Convert.ToString(dt.Rows[i]["FullAddress"]);
                address.UserId = Convert.ToString(dt.Rows[i]["userId"]);
                address.Area = Convert.ToString(dt.Rows[i]["area"]);
                address.Ward = Convert.ToString(dt.Rows[i]["ward"]);
                address.Landmark = Convert.ToString(dt.Rows[i]["LandMark"]);

                addressList.Add(address);
            }

            return Ok(addressList);
        }



    }

}
