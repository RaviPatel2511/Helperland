using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModels
{
    public class ProviderDashboard
    {
        public int ServiceId { get; set; }
        public string ServiceDate { get; set; }
        public string ServiceStartTime { get; set; }
        public string ServiceEndTime { get; set; }
        public int? CustId { get; set; }
        public string CustName { get; set; }
        public string add1 { get; set; }
        public string add2 { get; set; }
        public string city { get; set; }
        public string pincode { get; set; }
        public decimal? Payment { get; set; }
        public decimal? SpRatings { get; set; }
        public string comment { get; set; }
    }
}
