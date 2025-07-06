using Final_Project.Datalayer;
using Final_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillGenerationController : ControllerBase
    {
        [Authorize(Roles = "User,Labour")]
        [HttpGet]
        [Route("GetBillForm")]
        public IActionResult GetBillGeneration()
        {
            List<BillGenerationClass> billgenerationlist = new List<BillGenerationClass>();
            string query = "SELECT   o.TotalCost, o.orderid,   s.servicename,  s.servicecost,   s.servicediscount,   r.username,   r.UserPhoneNumber,   a.FullAddress FROM OrdersList o INNER JOIN Survices s ON o.serviceid = s.serviceid INNER JOIN register r ON o.userid = r.userid INNER JOIN AddressUser a ON r.userid = a.userid;";
            DataTable dt = BillGenerationDataLAyer.BillGenerationDataLayer(query);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BillGenerationClass billGenerationClass = new BillGenerationClass();
                billGenerationClass.OrderId = Convert.ToInt32(dt.Rows[i]["orderid"]);
                billGenerationClass.Discount = Convert.ToInt32(dt.Rows[i]["servicediscount"]);
                billGenerationClass.ServiceName = Convert.ToString(dt.Rows[i]["servicename"]);
                    billGenerationClass.ServiceCost = Convert.ToInt32(dt.Rows[i]["servicecost"]);
                    billGenerationClass.UserName = Convert.ToString(dt.Rows[i]["username"]);
                    billGenerationClass.UserPhoneNumber = Convert.ToString(dt.Rows[i]["UserPhoneNumber"]);
                    billGenerationClass.Label = Convert.ToString(dt.Rows[i]["fulladdress"]);
                billGenerationClass.toatalcost = Convert.ToInt32(dt.Rows[i]["TotalCost"]);
                billgenerationlist.Add(billGenerationClass);

                }
            return Ok(billgenerationlist);
            }

           

        }



    }

