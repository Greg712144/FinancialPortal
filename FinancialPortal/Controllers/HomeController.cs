using FinancialPortal.Models;
using FinancialPortal.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FinancialPortal.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Landing()
        {
            return View();
        }

        public ActionResult Lobby()
        {
            var guest = db.Users.Where(u => u.HouseholdId == null).Select(guestUser => new GuestUser
            {
                Name = guestUser.FirstName + " " + guestUser.LastName,
                Email = guestUser.Email,
                DisplayName = guestUser.DisplayName,
                AvatarPath = ""
            }).ToList();

            return View(guest);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            EmailModel model = new EmailModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model)
        {
          if(ModelState.IsValid)
            {
                try
                {

                    var body = "<p>Email From : <bold>{0}</bold>({1})</p><p>Message:</p><p>{2}</p>";
                    var emailto = ConfigurationManager.AppSettings["emailto"];
                    var from = string.Format("FinancialPortal<{0}>", emailto);

                    model.Body = "This is a message from you Financial Portal site. The name and the email of the contacting person is above. " + model.Body;

                    var email = new MailMessage(from, emailto)
                    {
                        Subject = model.Subject,
                        Body = string.Format(body, model.FromName, model.FromEmail, model.Body),
                        IsBodyHtml = true
                    };

                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);

                    return View(new EmailModel());
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
          }

            return View(model);
        }
    }
}