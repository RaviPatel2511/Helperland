using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModels
{
    public class AdminUserManagement
    {
        public int UserId { get; set; }

        public string Username { get; set; }
        public string CreatedDate { get; set; }
        public string UserType { get; set; }
        public string Mobile { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public int? Status { get; set; }
    }
}
