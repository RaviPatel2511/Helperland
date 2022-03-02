using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModels
{
    public class SpRating
    {
        public int ServiceRequestId { get; set; }
        public string Comments { get; set; }
        public decimal OnTimeArrival { get; set; }
        public decimal Friendly { get; set; }
        public decimal QualityOfService { get; set; }
    }
}
