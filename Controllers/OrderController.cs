using Final_Project.Datalayer;
using Final_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        [Authorize(Roles= ("Admin"))]
        [HttpGet]
        [Route("GetOrders")]
        public ActionResult<List<OrderMaster>> GetOrderLists()
        {
            try
            {
                List<OrderMaster> ordermaster = new List<OrderMaster>();
                string quare = "SELECT [OrderID],[UserID],[LabourID],[ServiceID],[AddressId],[Discount],[SurviceCost],[TotalCost] FROM [dbo].[OrdersList]";
                SqlDataReader reader = OrderDatalayer.GetAllOrdersList(quare);
                while (reader.Read())
                {
                    ordermaster.Add(new OrderMaster
                    {
                        OrderID = reader.GetInt32(0),
                        UserID = reader.GetString(1),
                        LabourID = reader.GetString(2),
                        ServiceID = reader.GetString(3),
                        AddressId = reader.GetString(4),
                        Discount = reader.GetDecimal(5),
                        SurviceCost = reader.GetDecimal(6),
                        TotalCost = reader.GetDecimal(7),
                    });
                }
                return ordermaster;
            }
            catch (Exception ex)
            {
                return BadRequest("Some error occured :m " + ex.Message);
            }
        }



        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("InsertNewOrder")]
        public ActionResult InsertLabourInformation([FromBody] OrderMaster ordermaster)
        {
            try
            {
                decimal discountAmount = (ordermaster.SurviceCost * ordermaster.Discount) / 100;
                decimal totalCost = ordermaster.SurviceCost - discountAmount;
                string quare = "INSERT INTO [dbo].[OrdersList] ( [UserID], [LabourID], [ServiceID], [AddressId], [Discount], [SurviceCost], [TotalCost])" + "VALUES" + "('" + ordermaster.UserID + "','" + ordermaster.LabourID + "','" + ordermaster.ServiceID + "','" + ordermaster.AddressId + "'," + discountAmount + "," + ordermaster.SurviceCost + "," + totalCost + ")";
                int count = OrderDatalayer.PostOrderInformation(quare);
                if (count > 0)

                {
                    return Ok("inserted successfully");
                }
                else
                {
                    return BadRequest("Labour is not Inserted");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Somne error occured : " + ex.Message);
            }
        }




        [Authorize("Admin")]
        [HttpGet]
        [Route("GetOrderByID")]
        public IActionResult GetOrderByID([FromQuery] int OrderID)
        {

            try
            {
                List<OrderMaster> ordermaster = new List<OrderMaster>();
                string quare = "SELECT [OrderID],[UserID],[LabourID],[ServiceID],[AddressId],[Discount],[SurviceCost],[TotalCost] FROM [dbo].[OrdersList] where OrderID='" + OrderID + "'";
                SqlDataReader reader = OrderDatalayer.GetOrderINformationById(quare);
                while (reader.Read())
                {
                    OrderMaster order = new OrderMaster
                    {
                        OrderID = reader.GetInt32(0),
                        UserID = reader.GetString(1),
                        LabourID = reader.GetString(2),
                        ServiceID = reader.GetString(3),
                        AddressId = reader.GetString(4),
                        Discount = reader.GetDecimal(5),
                        SurviceCost = reader.GetDecimal(6),
                        TotalCost = reader.GetDecimal(7),
                    };
                    ordermaster.Add(order);

                }
                if (ordermaster.Count > 0)
                {
                    return Ok(ordermaster);
                }
                else
                {
                    return BadRequest("Somoe error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Somne error occured : " + ex.Message);
            }


        }
    }
}

