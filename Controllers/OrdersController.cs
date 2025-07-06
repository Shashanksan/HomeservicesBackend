using Final_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ResendEmailService _emailService;

        public OrdersController()
        {
            _emailService = new ResendEmailService();
        }

        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderDto order)
        {
            // Save order to database here if you want...

            // Email content for User
            string userEmailContent = $@"
            <h2>Thank you for your order, {order.UserName}!</h2>
            <p>Service: {order.ServiceName}</p>
            <p>Address: {order.Address}</p>
            <p>We will contact you shortly at {order.ContactNumber}.</p>";

            // Email content for Labour
            string labourEmailContent = $@"
            <h2>New Job Assigned!</h2>
            <p>Customer: {order.UserName}</p>
            <p>Service: {order.ServiceName}</p>
            <p>Contact Number: {order.ContactNumber}</p>
            <p>Address: {order.Address}</p>";

            // Send confirmation email to User
            await _emailService.SendEmailAsync(new EmailRequest
            {
                From = "Home Services <onboarding@resend.dev>",
                To = order.UserEmail,
                Subject = "Order Confirmation",
                Html = userEmailContent
            });

            // Send job assignment email to Labour
            await _emailService.SendEmailAsync(new EmailRequest
            {
                From = "Home Services <onboarding@resend.dev>",
                To = order.LabourEmail,
                Subject = "New Work Assigned",
                Html = labourEmailContent
            });

            return Ok(new { message = "Order placed successfully and emails sent!" });
        }
    }
}
