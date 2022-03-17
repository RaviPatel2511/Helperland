using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModels
{
    public class AdminRefund
    {
        public int? Id { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? RefundAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public string Comment { get; set; }
    }
}
