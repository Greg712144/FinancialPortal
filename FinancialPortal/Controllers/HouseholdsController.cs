using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinancialPortal.Models;
using FinancialPortal.Helpers;
using FinancialPortal.Enumerations;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using FinancialPortal.ViewModels;

namespace FinancialPortal.Controllers
{
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UsersRoleHelper roleHelper = new UsersRoleHelper();
        private AuthorizerHelper authHelper = new AuthorizerHelper();


        // GET: Households
        public ActionResult Index()
        {
            return View(db.Households.ToList());
        }

        [Authorize(Roles ="HOH, Member")]
        public ActionResult DashBoard()
        {

            var userId = User.Identity.GetUserId();
            var householdId = db.Users.Find(userId).HouseholdId;
            var house = db.Households.Find(householdId);



            var data = new DataVM();
            data.Accounts = db.MyAccounts.ToList();
            data.Budgets = db.Budgets.ToList();
            data.Households = house;
            data.Invitations = db.Invitations.ToList();
            data.Members = db.Users.ToList();
            data.Transactions = db.Transactions.ToList();
            data.BudgetItems = db.BudgetItems.ToList();



            return View(data);
        }

        // GET: Households/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // GET: Households/Create
        public ActionResult CreateInvitation(int houseId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateInvitation(int houseId)
        {
            return RedirectToAction("Dashboard","Households", new { id = houseId});
        }

        // GET: Households/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Households.Add(household);
                db.SaveChanges();

                //#1 Update user created to reference household
                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                user.HouseholdId = household.Id;

                //#2 whoever creates HH inherits role of HOH
                roleHelper.AddUserToRole(userId, RoleName.HOH);
                db.SaveChanges();

                await AuthorizerHelper.ReauthorizeUserAsync(userId);
                
                return RedirectToAction("DashBoard");
            }

           

            return View(household);
        }

        // GET: Households/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Entry(household).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(household);
        }

        // GET: Households/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }

            return View(household);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            Household household = db.Households.Find(id);

            if (User.IsInRole("Member"))
            {
                user.HouseholdId = null;
                db.SaveChanges();

                roleHelper.RemoveUserFromRole(userId, RoleName.Member);
                roleHelper.AddUserToRole(userId, RoleName.Guest);

                db.SaveChanges();
            }
            else User.IsInRole("HOH");
            {
                user.HouseholdId = null;
                db.Households.Remove(household);
                db.SaveChanges();

                roleHelper.RemoveUserFromRole(userId, RoleName.HOH);
                roleHelper.AddUserToRole(userId, RoleName.Guest);
                db.SaveChanges();
            }
            

            return RedirectToAction("Lobby", "Home");
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
