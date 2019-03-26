using FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinancialPortal.Enumerations;

namespace FinancialPortal.Helpers
{
    public class BaHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static decimal GetCurrentBalance(int bankId)
        {
            var trans = db.Transactions.Where(t => t.AccountId == bankId);
            var deposits = trans.Where(t => t.Type == TransactionType.Deposit).Sum(t => t.Amount);
            var withdrawal = trans.Where(t => t.Type == TransactionType.Withdrawal && t.Amount > 0M).ToList();
            decimal totalWithdrawals = 0M;

            var account = db.MyAccounts.Find(bankId);
            if (withdrawal.Count() > 0)
            {
                totalWithdrawals = withdrawal.Sum(w => w.Amount);
            }
            return account.InitialBalance + deposits - totalWithdrawals;
        }
    }
}