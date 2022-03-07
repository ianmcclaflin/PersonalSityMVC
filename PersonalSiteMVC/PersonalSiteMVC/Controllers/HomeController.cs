using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalSiteMVC.Models;
using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace PersonalSiteMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Portfolio()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Links()
        {

            return View();
        }

        public ActionResult Resume()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);

            }

            string message = $"You have received an email from {cvm.Name}.<br />" +
                $"Subject: {cvm.Subject}<br />" +
                $"Message: {cvm.Message}<br />" +
                $"Please respond to {cvm.Email} with your reply.";

            MailMessage mm = new MailMessage(

                ConfigurationManager.AppSettings["EmailUser"].ToString(),

                ConfigurationManager.AppSettings["EmailTo"].ToString(),

                cvm.Subject,

                message

                );

            mm.IsBodyHtml = true;

            mm.Priority = MailPriority.High;

            mm.ReplyToList.Add(cvm.Email);

            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());

            client.Credentials = new NetworkCredential(

                ConfigurationManager.AppSettings["EmailUser"].ToString(),

                ConfigurationManager.AppSettings["EmailPass"].ToString()

                );

            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We're sorry, but your request cout not be " +
                    $"completed at this tie, Please try again later. <br />" +
                    $"Error Message: {ex.StackTrace}";

                return View(cvm);
            }

            return View("EmailConfirmation", cvm);

        }
    }
}