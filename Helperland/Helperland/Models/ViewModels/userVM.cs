using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModels
{
    public class userVM
    {
        [Required(ErrorMessage = "First name is must")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Min 2 and Max 15 Characters  allow")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is must")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Min 2 and Max 15 Characters  allow")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is must")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Enter Valid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobile is must")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Enter Valid Mobile")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Password is must")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Confirm Pass is must")]
        [Compare("Password",ErrorMessage ="Password doesn't match")]
        public string ConfirmPass { get; set; }

    }
}
