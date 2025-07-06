using Final_Project.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Final_Project.Datalayer
{
    public class HistoryDataLayer
    {
       

       

        // 1. GET All Labour Records
        public static List<HistoryMaster> GetAllHistory()
        {
            List<HistoryMaster> historyList = new List<HistoryMaster>();

            using (SqlConnection conn = new SqlConnection("Data Source=SHASHANK\\SQLEXPRESS;Initial Catalog=student_db;Integrated Security=True;Encrypt=False"))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAllHistory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            historyList.Add(new HistoryMaster
                            {
                                custName = reader.GetString(1),
                                phNo = reader.GetString(2),
                                address = reader.GetString(3),
                                labourId = reader.GetString(4)

                            });
                        }
                    }
                }
            }

            return historyList;
        }



        // By ID

        public  static List<HistoryMaster> sp_GetHistoryByID(string id)
        {
            List<HistoryMaster> historyList = new List<HistoryMaster>();

            using (SqlConnection conn = new SqlConnection("Data Source=SHASHANK\\SQLEXPRESS;Initial Catalog=student_db;Integrated Security=True;Encrypt=False"))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetHistoryByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@labourId", id); // 🔹 Pass the ID parameter

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            historyList.Add(new HistoryMaster
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                custName = reader.GetString(reader.GetOrdinal("custName")),
                                phNo = reader.GetString(reader.GetOrdinal("phNn")),
                                address = reader.GetString(reader.GetOrdinal("address")),
                                labourId = reader.GetString(reader.GetOrdinal("labourId"))
                            });
                        }
                    }
                }
            }

            return historyList;
        }


        // 2. INSERT New Labour Record
        public static bool InsertLabour(HistoryMaster Hist)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=SHASHANK\\SQLEXPRESS;Initial Catalog=student_db;Integrated Security=True;Encrypt=False"))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertHistory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@custName", Hist.custName);
                    cmd.Parameters.AddWithValue("@phNn", Hist.phNo);
                    cmd.Parameters.AddWithValue("@address", Hist.address);
                    cmd.Parameters.AddWithValue("@labourId", Hist.labourId);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // 3. UPDATE Labour Record
        public static bool UpdateLabour(HistoryMaster Hist)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=SHASHANK\\SQLEXPRESS;Initial Catalog=student_db;Integrated Security=True;Encrypt=False"))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateHistory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", Hist.ID);
                    cmd.Parameters.AddWithValue("@custName", Hist.custName);
                    cmd.Parameters.AddWithValue("@phNn", Hist.phNo);
                    cmd.Parameters.AddWithValue("@address", Hist.address);
                    cmd.Parameters.AddWithValue("@labourId", Hist.labourId);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // 4. DELETE Labour Record
        public static bool DeleteLabour(int id)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=SHASHANK\\SQLEXPRESS;Initial Catalog=student_db;Integrated Security=True;Encrypt=False"))
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteHistory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
