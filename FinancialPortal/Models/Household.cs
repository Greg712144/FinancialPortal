using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class Household
    {
        public int Id { get; set; }

        [Required, MaxLength(40), MinLength(2)]
        public string Name { get; set; }

        public virtual ICollection<MyAccount>Accounts { get; set; }
        public virtual ICollection<Budget>Budgets { get; set; }
        public virtual ICollection<ApplicationUser> Members { get; set; }
        public virtual ICollection<Invitation>Invitations { get; set; }


        public Household()
        {
            Members = new HashSet<ApplicationUser>();
            Budgets = new HashSet<Budget>();
            Accounts = new HashSet<MyAccount>();
            Invitations = new HashSet<Invitation>();

        }
    }
}