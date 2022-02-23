using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModels
{
    public class ResetPass
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string oldPassword { get; set; }
        [Required]
        [Display(Name ="New Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,14}$", ErrorMessage = "Password must contain at least 1 capital letter, 1 small letter, 1 number and one special character. Password length must be in between 6 to 14 characters")]
        public string password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("password",ErrorMessage ="password doesn't match")]
        public string   confirmPassword { get; set; }
    }
}
