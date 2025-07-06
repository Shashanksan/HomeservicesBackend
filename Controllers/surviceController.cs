using Final_Project.Datalayer;
using Final_Project.Models;
using FinalYearProject.DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;

namespace Final_Project.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
public class surviceController : ControllerBase
    {
        [Authorize(Roles ="User,Admin")]
        [HttpGet]                                                                                                                                                                                                                      
        [Route("GetSurvices")]
      
        public ActionResult<List<SurvicesMaster>> getSurvicesDetails()
        {
            try
            {
                List<SurvicesMaster> survicesMasters = new List<SurvicesMaster>();

                survicesMasters = surviceDataLayer.GetReaderDataFromQuery();


                return survicesMasters;
            }
            catch (Exception ex)
            {
                return BadRequest("Some error occured : " + ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteServices")]
        public  IActionResult DeleteSurviceInformation([FromQuery]string id)
        {

            
            string query = "delete from Labour where labourid = '"+id+ "'delete from survices where serviceid = '"+id+"'";
            int count = surviceDataLayer.DeleteSurviceInformation01(query);
            if(count>0)
            {
                return Ok("succes deleted");
            }
            else
            {
                return BadRequest("erorr");
            }




        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("UpdateSurvice")]
        public IActionResult UpdateTheSurvices([FromQuery] string ServiceID, [FromBody] SurvicesMaster survicemaster)
        {
            string sqlconnectionstring = "Data Source=SHASHANK\\SQLEXPRESS;Initial Catalog=student_db;Integrated Security=True;Encrypt=False";
            try
            {
                using (SqlConnection sqlconnecton = new SqlConnection(sqlconnectionstring))
                {
                    sqlconnecton.Open();

                    string quare = "UPDATE Survices SET ServiceName = '" + survicemaster.ServiceName + "',ServiceImage = '" + survicemaster.ServiceImage + "', ServiceCost = " + survicemaster.ServiceCost + ",ServiceDiscount = "+survicemaster.ServiceDiscount+" ,type = '"+survicemaster.Type+"' WHERE ServiceID = '"+ServiceID+"'";


                    using (SqlCommand sqlcommand = new SqlCommand(quare, sqlconnecton))
                    {
                        sqlcommand.CommandType = System.Data.CommandType.Text;

                        int count = sqlcommand.ExecuteNonQuery();
                        if (count > 0)
                        {
                            return Ok("Updated successfully");
                        }
                        else
                        {
                            return BadRequest("Something went wrong");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Some error occurred: " + ex.Message);
            }
        }
        // Allows Admin & User roles

        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Route("InsertServices")]
        public  IActionResult PostSurviceInformation(SurvicesMaster newService)
        {
            string conn = "Data Source=SHASHANK\\SQLEXPRESS;Initial Catalog=student_db;Integrated Security=True;Encrypt=False";
            string guid = Guid.NewGuid().ToString("N"); 
            string SurvicesID = guid.Substring(guid.Length - 4);
            string  labour = SurvicesID;

            string query = "INSERT INTO [dbo].[Survices] ([ServiceID],[ServiceName],[ServiceImage],[ServiceCost],[ServiceDiscount],[type]) VALUES" + " ('" + SurvicesID + "' ,'" + newService.ServiceName + "' ,'" + newService.ServiceImage + "', " + newService.ServiceCost + "," + newService.ServiceDiscount + ",'"+newService.Type+"')";

            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = conn;
            SqlCommand cmd = new SqlCommand();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
                
                    sqlCommand.CommandType = CommandType.Text;
                    int count = sqlCommand.ExecuteNonQuery();
                    if (count > 0)
                    {
                        return Ok("inserted");
                    }
                    else
                    {
                        return BadRequest("erorr");
                    }
            sqlConnection.Close();


                   
                
            
        }
    }
}
