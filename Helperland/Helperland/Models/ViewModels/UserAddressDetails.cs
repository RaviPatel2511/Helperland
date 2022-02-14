using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModels
{
    public class UserAddressDetails
    {
        public int id { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string city { get; set; }
        public string postalcode { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public bool IsDefault { get; set; }
    }
}
