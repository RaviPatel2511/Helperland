using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModels
{
    public class ScheduleService
    {
        [Required]
        public string scheduleDate { get; set; }
        public string scheduleTime { get; set; }
        public double stayHour{ get; set; }
        public bool cabinet { get; set; }
        public bool fridge { get; set; }
        public bool oven { get; set; }
        public bool laundary { get; set; }
        public bool window { get; set; }
        //[StringLength(500, MinimumLength = 10)]
        public string comment { get; set; }
        public bool havePet { get; set; }
    }
}
