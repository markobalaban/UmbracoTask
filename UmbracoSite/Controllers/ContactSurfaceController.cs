using Umbraco.Web.Mvc;
using System.Web.Mvc;
using UmbracoSite.Models;
using System.Net.Mail;
using System.Net;
using System;

namespace UmbracoSite.Controllers
{
    public class ContactSurfaceController : SurfaceController
    {
        public const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/";

        public ActionResult RenderForm()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_Contact.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                SendEmail(model);
                TempData["ContactSuccess"] = true;
                return CurrentUmbracoPage();
            }

            return CurrentUmbracoPage();
        }

        private void SendEmail(ContactModel model)
        {
            MailMessage message = new MailMessage(model.EmailAddress, "marko2@teol.net");
            message.Subject = string.Format(" {0} - Recived from: {1}", model.Subject, model.EmailAddress);
            message.Body = model.Message;
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;

            var credential = new NetworkCredential
            {                
                UserName = "e-mailAddress",
                Password = "sifra"
            };

            client.Credentials = credential;

            try { 
                client.Send(message);
            }
            catch(Exception){}            
        }
    }
}