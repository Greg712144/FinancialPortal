using FinancialPortal.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        public int BudgetItemId { get; set; }

        public string EnteredById { get; set; }

        [MaxLength(200), MinLength(5)]
        public string Description { get; set; }
        public DateTime Date { get; set; }

        [Range(0.00, 1000000.00)]
        public Decimal Amount { get; set; }
        public TransactionType Type { get; set; }

        public bool Reconciled { get; set; }

        [Range(0.00, 1000000.00)]
        public decimal? ReconciledAmount { get; set; } 

        public virtual MyAccount Account { get; set; }
        
        public virtual BudgetItem BudgetItem { get; set; }
        public virtual ApplicationUser EnteredBy { get; set; }


    }
}