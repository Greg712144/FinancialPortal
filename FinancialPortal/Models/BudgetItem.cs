using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class BudgetItem
    {
        public int Id { get; set; }
        public int? BudgetId { get; set; }

        [MaxLength(40), MinLength(5)]
        public string Name { get; set; }

        [MaxLength(200), MinLength(5)]
        public string Description { get; set; }

        [Range(0.00, 100000.00)]
        public decimal TargetAmount { get; set; }

        [Range(0.00, 100000.00)]
        public decimal CurrentAmount { get; set; }


        public virtual Budget Budget { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        public BudgetItem()
        {
            Transactions = new HashSet<Transaction>();
        }
    }
}