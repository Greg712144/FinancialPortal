using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinancialPortal.Models;

namespace FinancialPortal.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users
        public ActionResult Index()
        {
            var applicationUsers = db.Users.Include(a => a.Household);
            return View(applicationUsers.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,DisplayName,AvatarPath,HouseholdId,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", applicationUser.HouseholdId);
            return View(applicationUser);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", applicationUser.HouseholdId);
            return View(applicationUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,DisplayName,AvatarPath,HouseholdId,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(applicationUser).State = EntityState.Modified;

                db.Users.Attach(applicationUser);
                applicationUser.UserName = applicationUser.Email;

                // times x for the properties that are subject to change, this allows for only for the ones to be changed instead of losing the information 
                db.Entry(applicationUser).Property(x => x.FirstName).IsModified = true;
                db.Entry(applicationUser).Property(x => x.LastName).IsModified = true;
                db.Entry(applicationUser).Property(x => x.HouseholdId).IsModified = true;
                db.Entry(applicationUser).Property(x => x.AvatarPath).IsModified = true;
                db.Entry(applicationUser).Property(x => x.Email).IsModified = true;
                db.Entry(applicationUser).Property(x => x.PhoneNumber).IsModified = true;
                db.Entry(applicationUser).Property(x => x.UserName).IsModified = true;
                db.Entry(applicationUser).Property(x => x.PasswordHash).IsModified = true;
                db.Entry(applicationUser).Property(x => x.SecurityStamp).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", applicationUser.HouseholdId);
            return View(applicationUser);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
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


        // GET: Users/Edit/5
        public ActionResult EP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", applicationUser.HouseholdId);
            return View(applicationUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EP([Bind(Include = "Id,FirstName,LastName,DisplayName,AvatarPath,HouseholdId,Email,PasswordHash,SecurityStamp,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(applicationUser).State = EntityState.Modified;
                db.Users.Attach(applicationUser);
                applicationUser.UserName = applicationUser.Email;

                // times x for the properties that are subject to change, this allows for only for the ones to be changed instead of losing the information 
                db.Entry(applicationUser).Property(x => x.FirstName).IsModified = true;
                db.Entry(applicationUser).Property(x => x.LastName).IsModified = true;
                db.Entry(applicationUser).Property(x => x.HouseholdId).IsModified = true;
                db.Entry(applicationUser).Property(x => x.AvatarPath).IsModified = true;
                db.Entry(applicationUser).Property(x => x.Email).IsModified = true;
                db.Entry(applicationUser).Property(x => x.PhoneNumber).IsModified = true;
                db.Entry(applicationUser).Property(x => x.UserName).IsModified = true;
                db.Entry(applicationUser).Property(x => x.PasswordHash).IsModified = true;
                db.Entry(applicationUser).Property(x => x.SecurityStamp).IsModified = true;


                db.SaveChanges();
                return RedirectToAction("Dashboard", "Households");
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", applicationUser.HouseholdId);
            return View(applicationUser);
        }
    }
}
