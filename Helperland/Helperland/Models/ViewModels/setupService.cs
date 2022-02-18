using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModels
{
    public class setupService
    {
        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string Zipcode { get; set; }
    }
}
