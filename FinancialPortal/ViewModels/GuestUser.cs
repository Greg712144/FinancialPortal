using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.ViewModels
{
    public class GuestUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string AvatarPath { get; set; }
    }
}