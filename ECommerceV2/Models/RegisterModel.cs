using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceV2.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please enter a username!")]
        [Display(Name = "E-mail: ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your password!")]
        [Display(Name = "Password: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter confirm password!")]
        [Display(Name = "Confirm Password: ")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
