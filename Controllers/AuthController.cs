using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Final_Project.Models;

namespace Final_Project.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection"); // Fetch from appsettings.json
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT UserID, Password, Role FROM Register WHERE UserEmail = @UserEmail", conn);
                cmd.Parameters.AddWithValue("@UserEmail", request.UserEmail);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string storedPassword = reader["Password"].ToString();
                        string userRole = reader["Role"] != DBNull.Value ? reader["Role"].ToString() : "User";  

                        if (PasswordHasher.VerifyPassword(request.Password, storedPassword))
                        {
                            string userId = reader["UserID"].ToString();
                            string token = GenerateJwtToken(userId, userRole);
                            return Ok(new { Token = token });
                        }
                    }
                }
               


            }
            return Unauthorized("Invalid email or password.");
        }
        [HttpPost("labourlogin")]
        public IActionResult LabourLogin([FromBody] LoginRequest request)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT LabourID, Pass, work,jobrole FROM Labourregister03 WHERE Email = @Email", conn);
                cmd.Parameters.AddWithValue("@Email", request.UserEmail);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string storedPassword = reader["Pass"].ToString();
                     
                        string labourRole = reader["jobrole"] != DBNull.Value ? reader["jobrole"].ToString() : "Labour";


                        if (PasswordHasher.VerifyPassword(request.Password, storedPassword))
                        {
                            string labourId = reader["LabourID"].ToString();
                            string token = GenerateJwtToken(labourId, "Labour"); // Hardcoding "Labour" as role
                            return Ok(new { Token = token });
                        }
                    }
                }
            }

            return Unauthorized("Invalid email or password.");
        }


        private string GenerateJwtToken(string userId, string role)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var key = Encoding.UTF8.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserId", userId),
              
                    new Claim(ClaimTypes.Role, role)  

                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}



