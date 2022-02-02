﻿using Helperland.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HelperlandContext _helperlandContext;
        public CustomerController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }
        public IActionResult ServiceHistory()
        {
            if(Convert.ToInt32(HttpContext.Session.GetString("usertypeid")) == 1 && HttpContext.Session.GetString("userid") != null)
            {
                ViewBag.Title = "ServiceHistory";
                ViewBag.loginUserType = "customer";
                var userid = Convert.ToInt32(HttpContext.Session.GetString("userid"));
                var loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
           return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }
    }
}
