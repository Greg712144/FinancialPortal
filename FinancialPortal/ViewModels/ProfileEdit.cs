using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortal.ViewModels
{
    public class ProfileEdit
    {
        [Display(Name = "First Name")]
        [Required, MaxLength(20), MinLength(2)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required, MaxLength(20), MinLength(2)]
        public string LastName { get; set; }

        [Display(Name = "Display Name")]
        [Required, MaxLength(20), MinLength(2)]
        public string DisplayName { get; set; }

        public string AvatarPath { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }

  
}