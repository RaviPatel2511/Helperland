using Helperland.Models;
using Helperland.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class AdminController : Controller
    {
        private readonly HelperlandContext _helperlandContext;
        public AdminController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }

        public IActionResult ServiceRequest()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 3 && HttpContext.Session.GetInt32("userid") != null)
            {
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        public IActionResult UserManagement()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 3 && HttpContext.Session.GetInt32("userid") != null)
            {
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }
    }
}
