using Helperland.Models;
using Helperland.Models.Data;
using Helperland.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class AccountController : Controller
    {
    private readonly HelperlandContext _helperlandContext;
        public AccountController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }
        public IActionResult CustSignup()
        {
            ViewBag.Title = "Signup";
            return View();
        }
        [HttpPost]
        public IActionResult CustSignup(userVM uservm)
        {
            
            if (ModelState.IsValid)
            {
                var cust = new User()
                {
                    UserTypeId = 1,
                    FirstName = uservm.FirstName,
                    LastName = uservm.LastName,
                    Email = uservm.Email,
                    Mobile = uservm.Mobile,
                    Password = uservm.Password,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now

                };
            _helperlandContext.Users.Add(cust);
            _helperlandContext.SaveChanges();
            return RedirectToAction("Index","Helperland");
            }
            else
            {
                return View();
            }
        }
        public IActionResult ProviderSignup()
        {
            ViewBag.Title = "Signup";
            return View();
        }
        [HttpPost]
        public IActionResult ProviderSignup(userVM uservm)
        {
            if (ModelState.IsValid)
            {
                var provider = new User()
                {
                    UserTypeId = 2,
                    FirstName = uservm.FirstName,
                    LastName = uservm.LastName,
                    Email = uservm.Email,
                    Mobile = uservm.Mobile,
                    Password = uservm.Password,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now

                };
                _helperlandContext.Users.Add(provider);
                _helperlandContext.SaveChanges();
                return RedirectToAction("Index", "Helperland");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult login(userVM uservm)
        {

            if(uservm.Email!=null && uservm.Password != null)
            {
                var umail = _helperlandContext.Users.Where(x => x.Email == uservm.Email && x.Password == uservm.Password).FirstOrDefault();
                if (umail != null)
                {
                    if (umail.UserTypeId == 1)
                    {
                        return RedirectToAction("ServiceHistory", "Customer");
                    }
                    else if (umail.UserTypeId == 2)
                    {
                        return RedirectToAction("UpcomingRequest", "Provider");
                    }
                }
                else
                {
                    return RedirectToAction("Index","Helperland");
                }
            }
            
                return View();
 
        }
    }
}
