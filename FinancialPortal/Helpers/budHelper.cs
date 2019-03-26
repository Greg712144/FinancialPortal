using FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinancialPortal.Enumerations;

namespace FinancialPortal.Helpers
{
    public class budHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        //public static int GetBudgetId()
        //{

        //    var budId = db.BudgetItems.Select(b => b.Id);

        //    return
        //}


        public static decimal GetBudgetTarget(int budId)
        {
            return db.BudgetItems.Where(b => b.BudgetId == budId).DefaultIfEmpty().Sum(b => b.TargetAmount);
        }

        public static decimal GetBudgetActual(int budId)
        {
            decimal? actual = 0M;
            var budItems = db.BudgetItems.Where(b => b.BudgetId == budId);

            foreach(var bud in budItems.ToList())
            {
                var transactions = db.Transactions.Where(t => t.BudgetItemId == bud.Id && t.Type == TransactionType.Withdrawal && t.Amount > 0M).ToList();
                if(transactions.Count > 0)
                {
                    actual += transactions.Sum(t => t.Amount);
                }
            }
            return (decimal)(actual);
         
        }
    }
}