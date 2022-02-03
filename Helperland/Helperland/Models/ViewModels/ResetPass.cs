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
        [Display(Name ="New Password")]
        public string   password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("password",ErrorMessage ="password doesn't match")]
        public string   confirmPassword { get; set; }
    }
}
