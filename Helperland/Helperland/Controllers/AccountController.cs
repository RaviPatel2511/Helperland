using Helperland.Models;
using Helperland.Models.Data;
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
        public IActionResult CustSignup(User user)
        {
            
            if (ModelState.IsValid)
            {

                user.UserTypeId = 1;
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;
                var userExists = _helperlandContext.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                if (userExists == null)
                {
                    _helperlandContext.Users.Add(user);
                    _helperlandContext.SaveChanges();
                    return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
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
        public IActionResult ProviderSignup(User user)
        {
            if (ModelState.IsValid)
            {

                user.UserTypeId = 2;
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;

           
                var userExists = _helperlandContext.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                if (userExists == null)
                {
                    _helperlandContext.Users.Add(user);
                    _helperlandContext.SaveChanges();
                    return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
                }
                else
                {
                    return RedirectToAction(nameof(ProviderSignup), new { isproviderExists = true });
                }
            }
            
                return View();
           
        }
        [HttpPost]
        public IActionResult login(User user)
        {

            if(user.Email!=null && user.Password != null)
            {
                var credentials = _helperlandContext.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
                if (credentials != null)
                {
                    
                    if (credentials.UserTypeId == 1)
                    {
                        return RedirectToAction("ServiceHistory", "Customer");
                    }
                    else if (credentials.UserTypeId == 2)
                    {
                        return RedirectToAction("UpcomingRequest", "Provider");
                    }
                }
  
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
            //return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
            ////return RedirectToAction(Url.Action("Index", "Helperland") + "?loginModal=true", new { IsLoginerror = true });
            //return RedirectToPage()
        }
    }
}
