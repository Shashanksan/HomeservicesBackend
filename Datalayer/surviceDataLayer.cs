using Final_Project.Models;

using Microsoft.Data.SqlClient;
using System.Data;

namespace FinalYearProject.DataLayer
{
    public class surviceDataLayer
    {
        public static string sqlconnectionstring = "Data Source=SHASHANK\\SQLEXPRESS;Initial Catalog=student_db;Integrated Security=True;Encrypt=False";
        #region Get All Survices
        public static List<SurvicesMaster> GetReaderDataFromQuery()
        {
            List<SurvicesMaster> survicesMasters = new List<SurvicesMaster>();
            string strQuery = "SELECT [ServiceID],[ServiceName],[ServiceImage],[ServiceCost],[ServiceDiscount],[type] FROM [dbo].[Survices]";

            using (SqlConnection sqlConnection = new SqlConnection(sqlconnectionstring))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(strQuery, sqlConnection))
                {
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            survicesMasters.Add(new SurvicesMaster
                            {  
                                Type=reader.GetString(5),
                                ServiceID = reader.GetString(0),
                                ServiceName = reader.GetString(1),
                                ServiceImage = reader.GetString(2),
                                ServiceCost = reader.GetInt32(3),
                                ServiceDiscount = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }

            return survicesMasters; 
        }
        #endregion



        #region Get Survice By ID
        public static List<SurvicesMaster> GetSurviceInformationById(string ServiceID)
        {
            List<SurvicesMaster> survicesmaster = new List<SurvicesMaster>();
            string strQuery = "SELECT [ServiceID],[ServiceName],[ServiceImage],[ServiceCost],[ServiceDiscount], [type]FROM [dbo].[Survices] WHERE ServiceID = @ServiceID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlconnectionstring))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(strQuery, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ServiceID", ServiceID); // Using parameterized query

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            survicesmaster.Add(new SurvicesMaster
                            {
                                Type = reader.GetString(5),
                                ServiceID = reader.GetString(0),
                                ServiceName = reader.GetString(1),
                                ServiceImage = reader.GetString(2),
                                ServiceCost = reader.GetInt32(3),
                                ServiceDiscount = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }

            return survicesmaster; // Return the correct type
        }
        #endregion



        #region Delete survices
        public static int DeleteSurviceInformation01(string quare)
        {
            using (SqlConnection sqlConnection = new SqlConnection(sqlconnectionstring))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(quare, sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    int count = sqlCommand.ExecuteNonQuery();
                    return count;
                }
            }
        }
        #endregion

        #region Insert survices
        public static int PostSurviceInformation(SurvicesMaster newService)
        {
            string guid = Guid.NewGuid().ToString("N"); // Generate GUID without hyphens
            string SurvicesID = guid.Substring(guid.Length - 4); // Extract last 4 characters

            string query = "INSERT INTO [dbo].[Survices] ([ServiceID],[ServiceName],[ServiceImage],[ServiceCost],[ServiceDiscount] ,[type]) VALUES" + " ('" + SurvicesID + "' ,'" + newService.ServiceName + "' ,'" + newService.ServiceImage + "', " + newService.ServiceCost + "," + newService.ServiceDiscount + ")";

            using (SqlConnection sqlConnection = new SqlConnection(sqlconnectionstring))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    int count = sqlCommand.ExecuteNonQuery();
                    return count;
                }
            }
        }
        #endregion



    }
}
