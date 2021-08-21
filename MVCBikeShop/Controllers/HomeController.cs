using Microsoft.AspNetCore.Mvc;
using MVCBikeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MVCBikeShop.Controllers
{
    public class HomeController : Controller
    {
        BikeContext context;
        public List<Bike> DisplayedArticles { get; set; }

        //private string Text { get; set; }
        public HomeController(BikeContext context)
        {
            this.context = context;
        }
        public IActionResult Index(string text,int id = 1)
        {
            
            ViewBag.Text = text;
            int postsAmount = 10;
            if (context.Bikes.Count() % postsAmount == 0)
            {
                ViewBag.PagesCount = (context.Bikes.Count() / postsAmount);
            }
            else
            {
                ViewBag.PagesCount = (context.Bikes.Count() / postsAmount) + 1;
            }
            if (id == 0 || id>ViewBag.PagesCount)
            {
                id = 1;
            }
            ViewBag.CurrentPage = id;
            DisplayedArticles = context.Bikes.Skip((id - 1) * postsAmount).Take(postsAmount).ToList();
            return View(DisplayedArticles);
        }

        public IActionResult Buy(int? id)
        {
            if(id==null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.BikeId = id;
            //ViewBag.Bike = context.Bikes.Find(id);
            return View(context.Bikes.Find(id));
        }
        [HttpPost]
        public IActionResult Buy(int? id,Order order)
        {
            if (ModelState.IsValid)
            {
                context.Orders.Add(order);
                context.SaveChanges();
                if (!string.IsNullOrEmpty(order.Email))
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress("messagess40@gmail.com");
                        mail.To.Add(order.Email);
                        mail.Subject = "Order";
                        mail.Body = $"<h2>{order.Name}, thanks for purchase!</h2>";
                        mail.IsBodyHtml = true;

                        using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                        {
                            smtp.Credentials = new NetworkCredential("messagess40@gmail.com", "MessageForYou");
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                        }
                    }
                }
                return RedirectToAction("Index", new { text = $"{order.Name}, thanks for order" });
            }
            else
            {
                return View(context.Bikes.Find(id));
            }
        }
    }
}
