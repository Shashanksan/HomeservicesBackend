using Final_Project.Datalayer;
using Final_Project.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Final_Project.Enums;
using Microsoft.AspNetCore.Authorization;


namespace Final_Project.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class RegistractionController : ControllerBase
    {

        [HttpGet]
        [Route("GetRegistractionInformation")]
        public ActionResult<List<RegistractionMaster>> GetRegistraction()
        {
            try
            {
                List<RegistractionMaster> registractionmasters = new List<RegistractionMaster>();
                string quare = "SELECT UserID,UserName,UserEmail,UserPhoneNumber,UserAddress ,Role FROM Register";
                SqlConnection sql = RegistractionDataLayer.GetAllUsersRegistraction();
                SqlDataAdapter da = new SqlDataAdapter(quare,sql);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for(int i = 0;i<dt.Rows.Count;i++)
                {
                    RegistractionMaster reg = new RegistractionMaster();
                    reg.UserEmail = Convert.ToString(dt.Rows[i]["UserEmail"]);
                    reg.UserName = Convert.ToString(dt.Rows[i]["UserName"]);
                    reg.UserAddress = Convert.ToString(dt.Rows[i]["UserAddress"]);

               reg.UserID = Convert.ToString(dt.Rows[i]["UserID"]);
                    reg.JobRole = (Role)Enum.Parse(typeof(Role), dt.Rows[i]["Role"].ToString());
                    reg.UserPhoneNumber = Convert.ToString(dt.Rows[i]["UserPhoneNumber"]);

                    registractionmasters.Add(reg);


                }
                  return registractionmasters;
            }
            catch (Exception ex)
            {
                return BadRequest("Some error occurred: " + ex.Message);
            }
        }

 
        [HttpPost]
        [Route("InsertNewUser")]
        public ActionResult RegisterUser([FromBody] RegistractionMaster registractionmaster)
        {
            try
            {
                string UserId = RegistractionDataLayer.Generate10DigitNumber();
                string hashedPassword = PasswordHasher.HashPassword(registractionmaster.Password); // Hash password

              
                string query = "INSERT INTO Register (UserID,UserEmail,Password,UserName,UserPhoneNumber,UserAddress,Role) VALUES ('"+UserId+"', '"+registractionmaster.UserEmail+ "', '"+ hashedPassword + "','"+registractionmaster.UserName+ "' ,'"+registractionmaster.UserPhoneNumber+ "' , '"+registractionmaster.UserAddress+ "','"+registractionmaster.JobRole+"')";
                string query1 = "insert into userdetails values('" + registractionmaster.UserEmail + "','" + registractionmaster.Password + "')";
                int count1 = RegistractionDataLayer.PostuserInformation(query1);

                int count = RegistractionDataLayer.PostuserInformation(query);
                if (count > 0)
                {
                    return Ok("User registered successfully!");
                }
                else
                {
                    return BadRequest("User registration failed.");
                }
              


            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
           
        }

    }

}

