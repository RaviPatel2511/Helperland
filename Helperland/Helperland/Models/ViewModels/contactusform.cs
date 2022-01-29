using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModels
{
    public class contactusform
    {
        [Required(ErrorMessage ="The First Name field is Required")]
        [StringLength (15,MinimumLength =2)]

        public string fname{ get; set; }
        [Required(ErrorMessage = "The Last Name field is Required")]
        [StringLength(15, MinimumLength = 2)]
        public string lname {get; set; }
        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Enter Valid Email")]
        public string Email{ get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [StringLength(10,MinimumLength =5)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 10,ErrorMessage ="Message should contain have 10 to 500 character")]
        public string Message { get; set; }
        public IFormFile UploadFileName { get; set; }
        public string UploadFilePath{ get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        
    }
}
