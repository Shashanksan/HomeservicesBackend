using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Final_Project.Datalayer
{
    public class AddressDataLayer
    {
        public static string sqlconnectionstring = "Data Source=SHASHANK\\SQLEXPRESS;Initial Catalog=student_db;Integrated Security=True;Encrypt=False";

        public static SqlDataReader GetReaderDataFromSP(string storedProcedureName, SqlParameter[] parameters = null)
        {
            SqlConnection sqlConnection = new SqlConnection();
            SqlCommand sqlCommand = new SqlCommand();

            sqlConnection.ConnectionString = sqlconnectionstring;
            sqlConnection.Open();

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = storedProcedureName;
            sqlCommand.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                sqlCommand.Parameters.AddRange(parameters);
            }

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return sqlDataReader;
        }

        public static int SaveInformation(string query, SqlParameter[] parameters = null)
        {
            SqlConnection sqlConnection = new SqlConnection();
            SqlCommand sqlCommand = new SqlCommand();

            sqlConnection.ConnectionString = sqlconnectionstring;
            sqlConnection.Open();

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = query;
            sqlCommand.CommandType = CommandType.Text;

            if (parameters != null)
            {
                sqlCommand.Parameters.AddRange(parameters);
            }

            int count = sqlCommand.ExecuteNonQuery();
            return count;
        }
        public static DataTable GetDataTable(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            string connectionString = "your_connection_string_here";

            using (SqlConnection conn = new SqlConnection(sqlconnectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }


    }
}
