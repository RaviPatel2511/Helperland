using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModels
{
    public class CustDashboard
    {
        public int ServiceId { get; set; }
        public DateTime ServiceDate { get; set; }
        public decimal Payment { get; set; }
    }
}
