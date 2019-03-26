using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialPortal.Models;
using FinancialPortal.Helpers;
using Microsoft.AspNet.Identity;

namespace FinancialPortal.Controllers
{
    public class ChartDataController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private budHelper bud = new budHelper();


        // GET: ChartData
        public JsonResult GetMorrisChartData()
        {
            //get household Id
            var userId = User.Identity.GetUserId();
            var householdId = db.Users.Find(userId).HouseholdId;

            //get budget id from household

            var budgeted = db.BudgetItems.Where(b => b.Budget.HouseholdId == householdId).Sum(b => b.TargetAmount);
            var act = db.BudgetItems.Where(b => b.Budget.HouseholdId == householdId).Sum(b => b.CurrentAmount);

            var data = new List<MorrisChartData>();

            data.Add(new MorrisChartData
            {
                    
                y = "Actual vs. Budgeted",
                a = act,
                b = budgeted

            });



            var test = data;

            return Json(data);
        }
    }
}