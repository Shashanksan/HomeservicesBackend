using Final_Project.Datalayer;
using Final_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabourGetOrderController : ControllerBase
    {
        [Authorize(Roles ="Labour")]
        [HttpGet]
        [Route("labourorder")] 
        public  List<LabourGetOrder> LabourGetOrderController01() 
        {

           
          
                List<LabourGetOrder> LabourGetOrderlist = new List<LabourGetOrder>();
                string query = "select  r.username,r.userphonenumber,a.fulladdress ,r.userid from register r inner join AddressUser a on r.userid = a.userid";
                DataTable dt = LabourGetOrdersDataLayer.LabourGetOrders(query);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                LabourGetOrder labourgetorder = new LabourGetOrder();
                labourgetorder.UserName = Convert.ToString(dt.Rows[i]["username"]);
                labourgetorder.UserPhoneNumber = Convert.ToString(dt.Rows[i]["userphonenumber"]);
                labourgetorder.FullAddrress = Convert.ToString(dt.Rows[i]["fulladdress"]);
                labourgetorder.userid = Convert.ToString(dt.Rows[i]["userid"]);

                LabourGetOrderlist.Add(labourgetorder);

                }
                return LabourGetOrderlist;
      }
        [Authorize(Roles = "Labour")]
        [HttpDelete]
        [Route("delteLabourOrder")]
        public int DeleteLabourGetOrderController01( string userId )
        {


            
            List<LabourGetOrder> LabourGetOrderlist = new List<LabourGetOrder>();
            string query = "delete from addressuser where userid ='" + userId + "' delete from addressuser where userid ='" + userId + "'";
            SqlConnection sqlConnection = new SqlConnection("Data Source = SHASHANK\\SQLEXPRESS; Initial Catalog = student_db; Integrated Security = True; Encrypt = False");
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType= CommandType.Text;
            sqlCommand.CommandText =query ;
            int count = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();


            return count;

        }



    }
}
