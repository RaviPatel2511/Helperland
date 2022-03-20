using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModels
{
    public class CustDashboard
    {
        public int ServiceId { get; set; }
        public string ServiceDate { get; set; }
        public string ServiceStartTime { get; set; }
        public string ServiceEndTime { get; set; }
        public decimal Payment { get; set; }
        public int? Status { get; set; }
        public int? ProviderId { get; set; }
        public string Spname { get; set; }
        public string Avtar { get; set; }
        public decimal SpRatings { get; set; }
        public string Color { get; set; }
    }
}
