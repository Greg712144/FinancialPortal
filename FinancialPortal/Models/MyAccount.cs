using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class MyAccount
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }

        [MaxLength(40), MinLength(5)]
        public string Name { get; set; }

        [Range(0.00, 100000.00)]
        public decimal InitialBalance { get; set; }

        [Range(0.00, 100000.00)]
        public decimal CurrentBalance { get; set; }

        [Range(0.00, 100000.00)]
        public decimal ReconciledBalance { get; set; }

        [Range(0.00, 100000.00)]
        public decimal LowBalanceLevel { get; set; }

        public virtual Household Household { get; set; }
        public virtual ICollection<Transaction>Transactions { get; set; }

        public MyAccount()
        {
            Transactions = new HashSet<Transaction>();
        }
    }
}