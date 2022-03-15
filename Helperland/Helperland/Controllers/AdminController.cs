using Helperland.Models;
using Helperland.Models.Data;
using Helperland.Models.ViewModels;
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
                ViewBag.UserName = loggeduser.FirstName + " " + loggeduser.LastName;
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
                ViewBag.UserName = loggeduser.FirstName + " " + loggeduser.LastName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpGet]
        public IActionResult getUserManagementDetails()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 3 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                List<AdminDashboard> userManagement = new List<AdminDashboard>();
                var Alldata = _helperlandContext.Users.ToList();
                foreach (var data in Alldata)
                {
                    AdminDashboard userManagementData = new AdminDashboard();
                    userManagementData.Username = data.FirstName + " " + data.LastName;
                    userManagementData.CreatedDate = data.CreatedDate.ToString("dd/MM/yyyy");
                    if(data.UserTypeId == 1)
                    {
                        userManagementData.UserType = "Customer";
                    }else if(data.UserTypeId == 2)
                    {
                        userManagementData.UserType = "Service Provider";
                    }
                    else
                    {
                        userManagementData.UserType = "Admin";
                    }
                    userManagementData.Mobile = data.Mobile;
                    if (data.ZipCode != null)
                    {
                        userManagementData.PostalCode = data.ZipCode;
                    }
                    else
                    {
                        userManagementData.PostalCode = "";
                    }
                    if (data.IsActive == true)
                    {
                        userManagementData.Status = 1;
                    }
                    else
                    {
                        userManagementData.Status = 2;
                    }
                    userManagementData.Email = data.Email;
                    userManagementData.UserId = data.UserId;
                    userManagement.Add(userManagementData);
                }
                return new JsonResult(userManagement);
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpPost]
        public IActionResult DeactivateUser(int InputDeactivateUserId)
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 3 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                User user = _helperlandContext.Users.Where(x => x.UserId == InputDeactivateUserId).FirstOrDefault();
                if (user != null)
                {
                    user.IsActive = false;
                    user.ModifiedDate = DateTime.Now;
                    user.ModifiedBy = Convert.ToInt32(logedUserid);
                    _helperlandContext.Users.Update(user);
                    _helperlandContext.SaveChanges();
                    return Json("Successfully");
                }
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpPost]
        public IActionResult ActivateUser(int InputActivateUserId)
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 3 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                User user = _helperlandContext.Users.Where(x => x.UserId == InputActivateUserId).FirstOrDefault();
                if (user != null)
                {
                    user.IsActive = true;
                    user.ModifiedDate = DateTime.Now;
                    user.ModifiedBy = Convert.ToInt32(logedUserid);
                    _helperlandContext.Users.Update(user);
                    _helperlandContext.SaveChanges();
                    return Json("Successfully");
                }
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }
    }
}
