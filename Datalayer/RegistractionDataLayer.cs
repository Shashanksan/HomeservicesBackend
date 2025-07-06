namespace Final_Project.Datalayer;
using Microsoft.Data.SqlClient;

public class RegistractionDataLayer
{

    public static string sqlconnectionstring = "Data Source=SHASHANK\\SQLEXPRESS;Initial Catalog=student_db;Integrated Security=True;Encrypt=False";
    public static SqlConnection GetAllUsersRegistraction()
    {
        SqlConnection sqlconnection = new SqlConnection();
       
        sqlconnection.ConnectionString = sqlconnectionstring;
        sqlconnection.Open();
      
        return sqlconnection;
    }
    public static int PostuserInformation(string quare)
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
    public static string Generate10DigitNumber()
    {
        Random random = new Random();
        string number = random.Next(100000, 999999).ToString() + random.Next(100000, 999999).ToString().Substring(0, 4);
        return number;
    }
}
