using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModels
{
    public class BookService
    {
        public string ServiceDateTime { get; set; }
        public string ServiceZipCode { get; set; }
        public double ServiceHours { get; set; }
        public double? ExtraServiceHours { get; set; }
        public string Comments { get; set; }
        public bool HavePets { get; set; }
        public bool cabinet { get; set; }
        public bool fridge { get; set; }
        public bool oven { get; set; }
        public bool laundary { get; set; }
        public bool window { get; set; }

        public int SelectedAddressId { get; set; }

    }
}
