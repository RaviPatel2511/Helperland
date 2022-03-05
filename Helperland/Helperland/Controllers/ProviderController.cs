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
    public class ProviderController : Controller
    {
        private readonly HelperlandContext _helperlandContext;
        public ProviderController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "Dashboard";
                ViewBag.IsloggedIn = "success";
                ViewBag.UType = 2;
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }
        public IActionResult MySetting()
        {
            //if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            //{
                //ViewBag.Title = "My Setting";
                //ViewBag.IsloggedIn = "success";
                //ViewBag.UType = 2;
                //var userid = HttpContext.Session.GetInt32("userid");
                //User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                //ViewBag.UserName = loggeduser.FirstName;
                return View();
            //}
            //return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }
        public IActionResult NewService()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "New Request";
                ViewBag.IsloggedIn = "success";
                ViewBag.UType = 2;
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
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

        public IActionResult ServiceHistory()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "Service History";
                ViewBag.IsloggedIn = "success";
                ViewBag.UType = 2;
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }
        [HttpGet]
        public IActionResult GetServiceHistoryDetails()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                List<ProviderDashboard> serviceHistory = new List<ProviderDashboard>();
                var Alldata = _helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == logedUserid && x.Status == 2).ToList();
                foreach (var data in Alldata)
                {
                    ProviderDashboard ServiceHistoryData = new ProviderDashboard();
                    ServiceHistoryData.ServiceId = data.ServiceRequestId;
                    ServiceHistoryData.ServiceDate = data.ServiceStartDate.ToString("dd/MM/yyyy");
                    ServiceHistoryData.ServiceStartTime = data.ServiceStartDate.ToString("HH:mm");
                    ServiceHistoryData.ServiceEndTime = data.ServiceStartDate.AddHours((double)data.SubTotal).ToString("HH:mm");

                    User user = _helperlandContext.Users.Where(x => x.UserId == data.UserId).FirstOrDefault();
                    ServiceHistoryData.CustName = user.FirstName + " " + user.LastName;
                    
                    ServiceRequestAddress serviceRequestAddress = _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == data.ServiceRequestId).FirstOrDefault();
                    ServiceHistoryData.CustAddress = serviceRequestAddress.AddressLine1 + " " + serviceRequestAddress.AddressLine2 + " " + serviceRequestAddress.PostalCode + " " + serviceRequestAddress.City + " " + serviceRequestAddress.State;
                    serviceHistory.Add(ServiceHistoryData);
                }
                return new JsonResult(serviceHistory);
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpGet]
        public ActionResult GetServiceSummaryData(int ReqServiceId)
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                ServiceRequest reqService = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == ReqServiceId).FirstOrDefault();
                CustDashServiceSummary custDashServiceSummary = new CustDashServiceSummary();
                custDashServiceSummary.id = ReqServiceId;
                custDashServiceSummary.ServiceDate = reqService.ServiceStartDate.ToString("dd/MM/yyyy");
                custDashServiceSummary.ServiceStartTime = reqService.ServiceStartDate.ToString("HH:mm");
                custDashServiceSummary.ServiceEndTime = reqService.ServiceStartDate.AddHours((double)reqService.SubTotal).ToString("HH:mm");
                custDashServiceSummary.Duration = reqService.SubTotal;
                custDashServiceSummary.Payment = reqService.TotalCost;
                custDashServiceSummary.Comments = reqService.Comments;
                custDashServiceSummary.HavePets = reqService.HasPets;

                ServiceRequestAddress serviceRequestAddress = _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == ReqServiceId).FirstOrDefault();
                custDashServiceSummary.AddressLine1 = serviceRequestAddress.AddressLine1;
                custDashServiceSummary.AddressLine2 = serviceRequestAddress.AddressLine2;
                custDashServiceSummary.City = serviceRequestAddress.City;
                custDashServiceSummary.PostalCode = serviceRequestAddress.PostalCode;
                custDashServiceSummary.Mobile = serviceRequestAddress.Mobile;
                custDashServiceSummary.Email = serviceRequestAddress.Email;

                List<ServiceRequestExtra> serviceRequestExtra = _helperlandContext.ServiceRequestExtras.Where(x => x.ServiceRequestId == ReqServiceId).ToList();
                foreach (ServiceRequestExtra row in serviceRequestExtra)
                {
                    if (row.ServiceExtraId == 1)
                    {
                        custDashServiceSummary.cabinet = true;
                    }
                    else if (row.ServiceExtraId == 2)
                    {
                        custDashServiceSummary.fridge = true;
                    }
                    else if (row.ServiceExtraId == 3)
                    {
                        custDashServiceSummary.oven = true;
                    }
                    else if (row.ServiceExtraId == 4)
                    {
                        custDashServiceSummary.laundary = true;
                    }
                    else if (row.ServiceExtraId == 5)
                    {
                        custDashServiceSummary.window = true;
                    }
                }


                return Json(custDashServiceSummary);

            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }
        public IActionResult MyRating()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "My Rating";
                ViewBag.IsloggedIn = "success";
                ViewBag.UType = 2;
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpGet]
        public ActionResult GetMyRatingData()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                List<ProviderDashboard> MyRating = new List<ProviderDashboard>();
                var RatingData = _helperlandContext.Ratings.Where(x=>x.RatingTo == logedUserid).ToList();
                foreach (var data in RatingData)
                {
                    ProviderDashboard ratingTbldata = new ProviderDashboard();
                    ratingTbldata.ServiceId = data.ServiceRequestId;

                    ServiceRequest serviceRequest = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == data.ServiceRequestId).FirstOrDefault();
                    ratingTbldata.ServiceDate = serviceRequest.ServiceStartDate.ToString("dd/MM/yyyy");
                    ratingTbldata.ServiceStartTime = serviceRequest.ServiceStartDate.ToString("HH:mm");
                    ratingTbldata.ServiceEndTime = serviceRequest.ServiceStartDate.AddHours((double)serviceRequest.SubTotal).ToString("HH:mm");
                    ratingTbldata.comment = serviceRequest.Comments;


                    User user = _helperlandContext.Users.Where(x => x.UserId == data.RatingFrom).FirstOrDefault();
                    ratingTbldata.CustName = user.FirstName + " " + user.LastName;

                    Rating rating = _helperlandContext.Ratings.Where(x => x.ServiceRequestId == data.ServiceRequestId).FirstOrDefault();
                    ratingTbldata.SpRatings = rating.Ratings;



                    MyRating.Add(ratingTbldata);
                }
                return new JsonResult(MyRating);
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        public IActionResult BlockCustomer()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "Block Customer";
                ViewBag.IsloggedIn = "success";
                ViewBag.UType = 2;
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

    }
}
