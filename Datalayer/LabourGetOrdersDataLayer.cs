using Microsoft.Data.SqlClient;
using System.Data;

namespace Final_Project.Datalayer
{
    public class LabourGetOrdersDataLayer
    {
        public static string sqlconnectionstring = "Data Source = SHASHANK\\SQLEXPRESS;Initial Catalog = student_db; Integrated Security = True; Encrypt=False";
        public static DataTable LabourGetOrders(string quare)
        {


            SqlConnection sqlconnection = new SqlConnection();
            SqlCommand sqlcommand = new SqlCommand();
            sqlconnection.ConnectionString = sqlconnectionstring;
            sqlconnection.Open();
            sqlcommand.Connection = sqlconnection;

            sqlcommand.CommandText = quare;
            sqlcommand.CommandType = System.Data.CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(quare, sqlconnection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;

        }
    }
}
