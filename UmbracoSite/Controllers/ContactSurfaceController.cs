using Umbraco.Web.Mvc;
using System.Web.Mvc;
using UmbracoSite.Models;
using System.Net.Mail;

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
            message.Subject = string.Format("Enquiry from {0} - {1}", model.Subject, model.EmailAddress);
            message.Body = model.Message;
            SmtpClient client = new SmtpClient("127.0.0.1", 25);
            client.Send(message);
        }
    }
}