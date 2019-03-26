using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string RecipientId { get; set; }

        [MaxLength(200), MinLength(10)]
        public string Desciption { get; set; }

        public DateTime Created { get; set; }

        public bool Read { get; set; }

        public virtual ApplicationUser Recipient { get; set; }

    }
}