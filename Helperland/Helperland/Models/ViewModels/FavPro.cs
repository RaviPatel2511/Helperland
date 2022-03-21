using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModels
{
    public class FavPro
    {
        public int? ProId { get; set; }
        public string Avtar { get; set; }
        public string Name { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsFavourite { get; set; }
        public int MyProperty { get; set; }
        public decimal SpRating { get; set; }
        public int TotalCleaning { get; set; }
    }
}
