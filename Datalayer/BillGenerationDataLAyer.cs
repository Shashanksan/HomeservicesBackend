using Final_Project.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Final_Project.Datalayer
{
    public class BillGenerationDataLAyer
    {

      public static string sqlconnectionstring = "Data Source = SHASHANK\\SQLEXPRESS;Initial Catalog = student_db; Integrated Security = True; Encrypt=False";
        public static  DataTable  BillGenerationDataLayer(string quare)
        {

           
            SqlConnection sqlconnection = new SqlConnection();
            SqlCommand sqlcommand = new SqlCommand();
            sqlconnection.ConnectionString = sqlconnectionstring;
            sqlconnection.Open();
            sqlcommand.Connection = sqlconnection;
           
            sqlcommand.CommandText = quare;
            sqlcommand.CommandType = System.Data.CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(quare,sqlconnection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
           
            return dt;
        
        }
    }
}
