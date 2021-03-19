using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SendmailWithMemoryStreamApplication.Helpers;
using SendmailWithMemoryStreamApplication.Models;

namespace SendmailWithMemoryStreamApplication.Controllers
{
    [Route("checkout")]
    public class CheckoutController : Controller
    {
        private IConfiguration configuration;
        public CheckoutController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            return View();
        }

 
        [Route("send")]
        public IActionResult Send()
        {
            var order = new Order
            {
                Id = "Or01",
                Name = "order 1",
                Created = DateTime.Now,
                Payment = "Cash",
                Products = new List<Product>
                {
                    new Product{Id="p01",Name="name 1",Price=4,Description="good"},
                    new Product{Id="p02",Name="name 2",Price=4,Description="good"},
                    new Product{Id="p03",Name="name 3",Price=4,Description="good"},
                    new Product{Id="p04",Name="name 4",Price=5,Description="good"}
                }
            };

            var mailHelper = new MailHelper(configuration);
            if (mailHelper.send("faysalahamed3859@gmail.com", configuration["Gmail:Username"],
                "Order Info", "Order Info and Product List", convertOrderToHTMLString
                (order)))
            {
                ViewBag.msg = "Sent";
            }
            else
            {
                ViewBag.msg = "Failed";
            }

            return View("Index");
        }
        private string convertOrderToHTMLString(Order order)
        {
            var result = "<h3>Order Info</h3>";
            result += "Id: " + order.Id;
            result += "Name: " + order.Name +"<br>";
            result += "Payment: " + order.Payment + "<br>";
            result += "Created: " + order.Created.ToString("MM/dd/yyyyy");
            result += "<h3>Product List</h3>";
            result += "<table border='1'>";
            result += "<tr>";
            result += "<th>Id</th>";
            result += "<th>Name</th>";
            result += "<th>Payment</th>";
            result += "<th>Created</th>";
            result += "</tr>";
            foreach (var product in order.Products)
            {
                result += "<tr>";
                result += "<td>"+product.Id+"</td>";
                result += "<td>" + product.Name + "</td>";
                result += "<td>" + product.Price + "</td>";
                result += "<td>" + product.Description + "</td>";
                result += "</tr>";
            }
            result += "</table>";
            return result;
        }
    }
}
