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
    public class AccountController : Controller
    {
    private readonly HelperlandContext _helperlandContext;
        public AccountController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }
        
        public IActionResult CustSignup(bool isuserExists= false)
        {
            if(HttpContext.Session.GetString("userid") == null)
            {
                    ViewBag.Title = "Signup";
                    ViewBag.IsuserExists = isuserExists;
                    return View();
            }
            return RedirectToAction("Error", "Helperland");
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
            if (HttpContext.Session.GetString("userid") == null)
            {
                ViewBag.Title = "Signup";
                ViewBag.IsproviderExists = isproviderExists;
                return View();
            }
            return RedirectToAction("Error", "Helperland");
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

                    HttpContext.Session.SetString("userid", credentials.UserId.ToString());
                    HttpContext.Session.SetString("usertypeid", credentials.UserTypeId.ToString());
                    if (credentials.UserTypeId == 1)
                    {
                        return RedirectToAction("ServiceHistory", "Customer");
                    }   
                    else if (credentials.UserTypeId == 2)
                    {
                        return RedirectToAction("UpcomingService", "Provider");
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
