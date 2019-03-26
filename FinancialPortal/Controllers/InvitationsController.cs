using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FinancialPortal.Enumerations;
using FinancialPortal.Helpers;
using FinancialPortal.Models;
using Microsoft.AspNet.Identity;

namespace FinancialPortal.Controllers
{
    public class InvitationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UsersRoleHelper rolehelper = new UsersRoleHelper();
        

        // GET: Invitations
        public ActionResult Index()
        {
            var invitations = db.Invitations.Include(i => i.Household);
            return View(invitations.ToList());
        }

        // GET: Invitations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // GET: Invitations/Create
        public ActionResult Create()
        {
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(string email, string subject, [ModelBinder(typeof(AllowHtmlHelper))] string body)
        {
            //Collect information to create the invitation

            var invitation = new Invitation()
            {
                KeyCode = Guid.NewGuid(),
                Subject = subject,
                Email = email,
                Body = body,
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(2),
                HouseholdId = db.Users.Find(User.Identity.GetUserId()).HouseholdId.GetValueOrDefault()
            };

             db.Invitations.Add(invitation);
            db.SaveChanges();

            //Do Email work
            var callbackUrl = Url.Action("AcceptInvite", "Invitations", new { email = invitation.Email, keyCode = invitation.KeyCode }, protocol: Request.Url.Scheme);

            var acceptLink = "You can accept your invitation by clicking <a href=\"" + callbackUrl + "\">here</a>";
            var from = string.Format("FinancialPortal<FinancialPortal@gmail.com>");

            var emailMessage = new MailMessage(from, email)
            {
                Subject = invitation.Subject,
                Body = $"{invitation.Body} <hr /><br /> {acceptLink}",
                IsBodyHtml = true
            };

            var svc = new PersonalEmail();
            await svc.SendAsync(emailMessage);


            return RedirectToAction("Dashboard", "Households");
        }


       
        public ActionResult AcceptInvite(string email, string keyCode, int? householdId)
        {
            var invite = db.Invitations.FirstOrDefault(i => i.KeyCode.ToString() == keyCode);

            if (invite.Expired == true)
            {
                TempData["Exp"] = "The invitation is Expired! Please request new invitation.";
                return RedirectToAction("Landing","Home");
            }
            if(invite.Accepted < DateTime.Now)
            {
                TempData["Accepted"] = "This invitation has already been accepted! Please request new invitation.";
                return RedirectToAction("Landing","Home");
            }
                return View();
        }

       
        // GET: Invitations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", invitation.HouseholdId);
            return View(invitation);
        }

        // POST: Invitations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HouseholdId,KeyCode,Email,Body,Created,Expires,Expired")] Invitation invitation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invitation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", invitation.HouseholdId);
            return View(invitation);
        }

        // GET: Invitations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // POST: Invitations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invitation invitation = db.Invitations.Find(id);
            db.Invitations.Remove(invitation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
