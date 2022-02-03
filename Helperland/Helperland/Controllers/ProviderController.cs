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
            
            if (Convert.ToInt32(HttpContext.Session.GetString("usertypeid")) == 2 && HttpContext.Session.GetString("userid") != null)
            {
                ViewBag.Title = "UpcomingRequest";
                ViewBag.loginUserTypeId = 2;
                var userid = Convert.ToInt32(HttpContext.Session.GetString("userid"));
                var loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }
    }
}
