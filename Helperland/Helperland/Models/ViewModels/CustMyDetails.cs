using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModels
{
    public class CustMyDetails
    {
        //languageId
        //1 for gujrati
        //2 for english

        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public DateTime? dob { get; set; }
        public int? languageid { get; set; }
    }
}
