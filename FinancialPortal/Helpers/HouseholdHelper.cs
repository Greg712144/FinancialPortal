using FinancialPortal.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Helpers
{
    public class HouseholdHelper
    {
        //Same as "new ApplicationDbContext();"
        private ApplicationDbContext db = new ApplicationDbContext();

        public ICollection<Household> ListUserHousehold()
        {
            //Collect user data
            var userId = HttpContext.Current.User.Identity.GetUserId();

            //Collect user householdId
            var householdId = db.Users.Find(userId).HouseholdId;

            //Collect household where Id is the same as user householdId and put into a list
            var household = db.Households.Where(t => t.Id == householdId).ToList();

            return (household);
        }

        public ICollection<Budget> ListUserBudget()
        {
            //Collect user data
            var userId = HttpContext.Current.User.Identity.GetUserId();

            //Collect user householdId
            var householdId = db.Users.Find(userId).HouseholdId;

            //Collect budget where Id is the same as user budgetId and put into a list
            var budget = db.Budgets.Where(b => b.HouseholdId == householdId).ToList();

            return (budget);
        }

        public ICollection<BudgetItem> ListUserBudgetItem ()
        {
            //Collect user data
            var userId = HttpContext.Current.User.Identity.GetUserId();

            //Collect user householdId
            var householdId = db.Users.Find(userId).HouseholdId;

            //Collect budgetItem where Id is the same as user householdId and put into a list
            var budgetItem = db.BudgetItems.Where(b => b.Budget.HouseholdId == householdId).ToList();

            return (budgetItem);
        }

        public ICollection<MyAccount> ListUserAccounts()
        {
            //Collect user data
            var userId = HttpContext.Current.User.Identity.GetUserId();

            //Collect user householdId
            var householdId = db.Users.Find(userId).HouseholdId;

            //Collect account where Id is the same as user householdId and put into a list
            var myAcc = db.MyAccounts.Where(m => m.HouseholdId == householdId).ToList();

            return (myAcc);
        }
        
        public ICollection<ApplicationUser> ListMemberOnTrans()
        {
            //Collect user data
            var userId = HttpContext.Current.User.Identity.GetUserId();

            //Collect user householdId
            var householdId = db.Users.Find(userId).HouseholdId;

            //Collect tran user where Id is the same as user householdId and put into a list
            var myAccU = db.Users.Where(m => m.HouseholdId == householdId).ToList();

            return (myAccU);
        }
    }
}