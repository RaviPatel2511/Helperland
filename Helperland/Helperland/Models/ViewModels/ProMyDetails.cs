using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModels
{
    public class ProMyDetails
    {
        //Gender => 1 for male, 2 for female,3 for other
        //NationalityId => 1 for india, 2 for german,3 for pakistan

        public bool? IsActive { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public DateTime? dob { get; set; }
        public int? nationnalityId { get; set; }
        public int? gender { get; set; }
        public string avtar { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalcode { get; set; }
    }
}
