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
    public class ProviderController : Controller
    {
        private readonly HelperlandContext _helperlandContext;
        public ProviderController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }
        public IActionResult UpcomingService()
        {
            
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "UpcomingRequest";
                ViewBag.UType = 2;
                ViewBag.IsloggedIn = "success";
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }
    }
}
