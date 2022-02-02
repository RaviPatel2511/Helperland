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
        public IActionResult CustSignup(bool isuserExists= false)
        {
            ViewBag.IsuserExists = isuserExists;
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
                var userExists = _helperlandContext.Users.Where(x => x.Email == cust.Email).FirstOrDefault();
                if (userExists == null)
                {
                    _helperlandContext.Users.Add(cust);
                    _helperlandContext.SaveChanges();
                    return RedirectToAction("Index","Helperland");
                }
                else
                {
                    return RedirectToAction(nameof(CustSignup), new { isuserExists = true });
                }
            }
           
                return View();
            
        }
        public IActionResult ProviderSignup(bool isproviderExists = false)
        {
            ViewBag.IsproviderExists = isproviderExists;
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
                var userExists = _helperlandContext.Users.Where(x => x.Email == provider.Email).FirstOrDefault();
                if (userExists == null)
                {
                    _helperlandContext.Users.Add(provider);
                    _helperlandContext.SaveChanges();
                    return RedirectToAction("Index", "Helperland");
                }
                else
                {
                    return RedirectToAction(nameof(ProviderSignup), new { isproviderExists = true });
                }
            }
            
                return View();
           
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
  
            }

            return RedirectToAction("Index", "Helperland");

        }
    }
}
