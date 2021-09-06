using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ECommerceV2.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter username!")]
        [Display(Name = "E-mail: ")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Please enter password!")]
        [Display(Name = "Password: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
