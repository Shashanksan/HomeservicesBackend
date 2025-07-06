namespace Final_Project.Datalayer;
using Microsoft.Data.SqlClient;

public class OrderDatalayer
{
    public static string sqlconnectionstring = "Data Source=SHASHANK\\SQLEXPRESS;Initial Catalog=student_db;Integrated Security=True;Encrypt=False";
    public static SqlDataReader GetAllOrdersList(string quare)
    {
        SqlConnection sqlconnection = new SqlConnection();
        SqlCommand sqlcommand = new SqlCommand();
        sqlconnection.ConnectionString = sqlconnectionstring;
        sqlconnection.Open();
        sqlcommand.Connection = sqlconnection;
        sqlcommand.CommandText = quare;
        sqlcommand.CommandType = System.Data.CommandType.Text;
        SqlDataReader sqldatareader = sqlcommand.ExecuteReader();
        return sqldatareader;
    }


    public static int PostOrderInformation(string quare)
    {
        SqlConnection sqlconnection = new SqlConnection();
        SqlCommand sqlcommand = new SqlCommand();
        sqlconnection.ConnectionString = sqlconnectionstring;
        sqlconnection.Open();
        sqlcommand.Connection = sqlconnection;
        sqlcommand.CommandText = quare;
        sqlcommand.CommandType = System.Data.CommandType.Text;
        int count = sqlcommand.ExecuteNonQuery();
        return count;
    }

    public static SqlDataReader GetOrderINformationById(string quare)
    {
        SqlConnection sqlconnection = new SqlConnection();
        SqlCommand sqlcommand = new SqlCommand();
        sqlconnection.ConnectionString = sqlconnectionstring;
        sqlconnection.Open();
        sqlcommand.Connection = sqlconnection;
        sqlcommand.CommandText = quare;
        sqlcommand.CommandType = System.Data.CommandType.Text;
        SqlDataReader sqldatareader = sqlcommand.ExecuteReader();
        return sqldatareader;
    }
}
