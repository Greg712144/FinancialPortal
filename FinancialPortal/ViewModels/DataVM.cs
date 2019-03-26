using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinancialPortal.Models;

namespace FinancialPortal.ViewModels
{
    public class DataVM
    {
        public virtual ICollection<MyAccount> Accounts { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<ApplicationUser> Members { get; set; }
        public virtual ICollection<BudgetItem> BudgetItems { get; set; }
        public virtual Household Households { get; set; }


        public DataVM()
        {
            Accounts = new HashSet<MyAccount>();
            Budgets = new HashSet<Budget>();
            Invitations = new HashSet<Invitation>();
            Transactions = new HashSet<Transaction>();
            Members = new HashSet<ApplicationUser>();
            BudgetItems = new HashSet<BudgetItem>();

        }

    }
}