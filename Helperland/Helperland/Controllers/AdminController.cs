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

        [HttpGet]
        public IActionResult GetServiceRequestData()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 3 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                List<AdminServiceRequest> ServiceRequest = new List<AdminServiceRequest>();
                var Alldata = _helperlandContext.ServiceRequests.ToList();
                if (Alldata != null)
                {
                foreach (var data in Alldata)
                {
                    AdminServiceRequest serviceRequestData = new AdminServiceRequest();
                    serviceRequestData.ServiceId = data.ServiceRequestId;
                    serviceRequestData.ServiceDate = data.ServiceStartDate.ToString("dd/MM/yyyy");
                    serviceRequestData.ServiceStartTime = data.ServiceStartDate.ToString("HH:mm");
                    serviceRequestData.ServiceEndTime = data.ServiceStartDate.AddHours((double)data.SubTotal).ToString("HH:mm");

                    User user = _helperlandContext.Users.Where(x => x.UserId == data.UserId).FirstOrDefault();
                    serviceRequestData.CustName = user.FirstName + " " + user.LastName;
                    serviceRequestData.CustEmail = user.Email;
                    ServiceRequestAddress serviceRequestAddress = _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == data.ServiceRequestId).FirstOrDefault();
                    serviceRequestData.add1 = serviceRequestAddress.AddressLine1;
                    serviceRequestData.add2 = serviceRequestAddress.AddressLine2;
                    serviceRequestData.city = serviceRequestAddress.City;
                    serviceRequestData.pincode = serviceRequestAddress.PostalCode;

                    serviceRequestData.ProviderId = data.ServiceProviderId;
                        if (data.ServiceProviderId != null)
                        {
                            User prouser = _helperlandContext.Users.Where(x => x.UserId == data.ServiceProviderId).FirstOrDefault();
                                serviceRequestData.Spname = prouser.FirstName + " " + prouser.LastName;
                            serviceRequestData.ProEmail = prouser.Email;
                            if (prouser.UserProfilePicture != null)
                            {
                                    serviceRequestData.Avtar = prouser.UserProfilePicture;
                            }
                            else
                            {
                                serviceRequestData.Avtar = "cap";
                            }
                            var rating = _helperlandContext.Ratings.Where(x => x.RatingTo == data.ServiceProviderId);
                            if (rating.Count() > 0)
                            {
                                    serviceRequestData.SpRatings = Math.Round(rating.Average(x => x.Ratings), 1);
                            }
                            else
                            {
                                    serviceRequestData.SpRatings = 0;
                            }
                        }

                        serviceRequestData.Payment = data.TotalCost;

                        if(data.ServiceProviderId == null && data.Status == null)
                        {
                            serviceRequestData.Status = 1; // new
                        }else if(data.ServiceProviderId != null && data.Status == null)
                        {
                            serviceRequestData.Status = 2; //pending
                        }else if(data.Status == 1)
                        {
                            serviceRequestData.Status = 3; //cancle
                        }else if (data.Status == 2)
                        {
                            serviceRequestData.Status = 4; //Complete
                        }
                        ServiceRequest.Add(serviceRequestData);
                    }
                return new JsonResult(ServiceRequest);
                }
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpGet]
        public ActionResult GetServiceSummaryData(int ReqServiceId)
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

            //ServiceRequestExtra serviceRequestExtra = _helperlandContext.ServiceRequestExtras.Where(x => x.ServiceRequestId == ReqServiceId).FirstOrDefault();
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
                List<AdminUserManagement> userManagement = new List<AdminUserManagement>();
                var Alldata = _helperlandContext.Users.ToList();
                foreach (var data in Alldata)
                {
                    AdminUserManagement userManagementData = new AdminUserManagement();
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
