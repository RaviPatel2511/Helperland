using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModels
{
    public class CustDashServiceSummary
    {
        public int id { get; set; }
        public string ServiceDate { get; set; }
        public string ServiceStartTime { get; set; }
        public string ServiceEndTime { get; set; }
        public decimal Duration { get; set; }
        public decimal Payment { get; set; }
        public bool oven { get; set; }
        public bool cabinet { get; set; }
        public bool fridge { get; set; }
        public bool laundary { get; set; }
        public bool window { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
        public bool HavePets { get; set; }

        public string CustomerName { get; set; }
    }
}
