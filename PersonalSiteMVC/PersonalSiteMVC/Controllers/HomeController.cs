﻿using System.Web.Mvc;
using PersonalSiteMVC.Models;
using System.Net.Mail;
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
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            string message = $"You have received an email from {cvm.Name}.<br />" +
                $"Subject: {cvm.Subject}<br />" +
                $"Message: {cvm.Message}<br />" +
                $"Please reply to {cvm.Email} with your response at your earliest convenience.";

            MailMessage mm = new MailMessage("administrator@ianmcclaflin.com", "ianmcclaflin@gmail.com", cvm.Subject, message);

            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;
            mm.ReplyToList.Add(cvm.Email);

            SmtpClient client = new SmtpClient("mail.ianmcclaflin.com");

            client.Credentials = new NetworkCredential("administrator@ianmcclaflin.com", "P@ssw0rd");

            try
            {
                client.Send(mm);
            }
            catch (System.Exception ex)
            {
                ViewBag.CustomerMessage = $"We're sorry, but your request could not be completed at this time. " +
                    $"Please try again later. If the issue persists, please contact your site administrator and provide " +
                    $"the following info:<br />{ex.StackTrace}";
                return View(cvm);

                
            }

            return View("EmailConfirmation", cvm);



        }
    }
}