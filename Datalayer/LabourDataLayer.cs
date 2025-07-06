
using Final_Project.Models;
using Microsoft.Data.SqlClient;
using System.Data;
namespace Final_Project.Datalayer
{
    public class LabourDataLayer
    {
        

        //string LabourId = random.Next(100000, 999999).ToString();
        public static Random random = new Random();
      public   string _connectionString = "Data Source=SHASHANK\\SQLEXPRESS;Initial Catalog=student_db;Integrated Security=True;Encrypt=False";
              // 1. GET All Labour Records
        public static  List<LabourMaster> GetAllLabours()
        {
            List<LabourMaster> labourList = new List<LabourMaster>();

            using (SqlConnection conn = new SqlConnection("Data Source=SHASHANK\\SQLEXPRESS;Initial Catalog=student_db;Integrated Security=True;Encrypt=False"))
            {
                using (SqlCommand cmd = new SqlCommand("GetLabourRegisterData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            labourList.Add(new LabourMaster
                            {
                                ID = reader.GetInt32(0),
                                Email = reader.GetString(1),
                                Pass = reader.GetString(2),
                                name = reader.GetString(3),
                                PhoneNO = reader.GetString(4),
                                work = reader.GetString(5),
                          
                                labourId = reader.GetString(6),
                                      jobrole = reader.GetString(7),
                            });
                        }
                    }
                }
            }

            return labourList;
        }

        // 2. INSERT New Labour Record
        public static  bool InsertLabour(LabourMaster labour)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=SHASHANK\\SQLEXPRESS;Initial Catalog=student_db;Integrated Security=True;Encrypt=False"))
            {
                string LabourId = GenerateRandom6Char();
            
                string hashedPassword = PasswordHasher.HashPassword(labour.Pass);

                using (SqlCommand cmd = new SqlCommand("InsertLabourDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", labour.Email);
                    cmd.Parameters.AddWithValue("@Pass", hashedPassword);
                    cmd.Parameters.AddWithValue("@Name", labour.name);
                    cmd.Parameters.AddWithValue("@PhoneNO", labour.PhoneNO);
                    cmd.Parameters.AddWithValue("@Work", labour.work);

                    cmd.Parameters.AddWithValue("@labourId", LabourId);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        public static string GenerateRandom6Char()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // 3. UPDATE Labour Record
        public  static bool UpdateLabour(string labourid,LabourMaster labour)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=SHASHANK\\SQLEXPRESS;Initial Catalog=student_db;Integrated Security=True;Encrypt=False"))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateLabourRegister", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", labour.ID);
                    cmd.Parameters.AddWithValue("@labourid", labourid);
                    cmd.Parameters.AddWithValue("@Email", labour.Email);
                    cmd.Parameters.AddWithValue("@Pass", labour.Pass);
                    cmd.Parameters.AddWithValue("@Name", labour.name);
                    cmd.Parameters.AddWithValue("@PhoneNO", labour.PhoneNO);
                    cmd.Parameters.AddWithValue("@Work", labour.work);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // 4. DELETE Labour Record
        public static  bool DeleteLabour(string labourid)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=SHASHANK\\SQLEXPRESS;Initial Catalog=student_db;Integrated Security=True;Encrypt=False"))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteLabourRegister", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@labourid", labourid);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}


