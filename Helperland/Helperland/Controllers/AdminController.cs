using Helperland.Models;
using Helperland.Models.Data;
using Helperland.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
        public ActionResult GetRescheduleRequestData(int RescheduleServiceId)
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 3 && HttpContext.Session.GetInt32("userid") != null)
            {
                ServiceRequest reqService = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == RescheduleServiceId).FirstOrDefault();
                ServiceRequestAddress reqAdd = _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == RescheduleServiceId).FirstOrDefault();
                var obj = new 
                {
                    ServiceDate = reqService.ServiceStartDate.ToString("dd/MM/yyyy"),
                    ServiceStartTime = reqService.ServiceStartDate.ToString("HH:mm"),
                    AddLine1 = reqAdd.AddressLine1,
                    AddLine2 = reqAdd.AddressLine2,
                    City = reqAdd.City,
                    State = reqAdd.State,
                    PostalCode = reqAdd.PostalCode,
                    SpId = reqService.ServiceProviderId
                };

                return Json(obj);
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpPost]
        public ActionResult PostRescheduleRequestData(AdminReschedule adminReschedule)
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 3 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                ServiceRequest serviceRequest = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == adminReschedule.Id).FirstOrDefault();
                if (serviceRequest != null)
                {
                    serviceRequest.ServiceStartDate = DateTime.ParseExact(adminReschedule.ServiceTime, "dd/MM/yyyy HH:mm", null);
                    serviceRequest.ZipCode = adminReschedule.PostalCode;
                    serviceRequest.Comments = adminReschedule.Comments;
                    serviceRequest.ModifiedBy = logedUserid;
                    serviceRequest.ModifiedDate = DateTime.Now;
                    _helperlandContext.ServiceRequests.Update(serviceRequest);
                    _helperlandContext.SaveChanges();

                    ServiceRequestAddress serviceRequestAddress = _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == adminReschedule.Id).FirstOrDefault();
                    serviceRequestAddress.AddressLine1 = adminReschedule.AddressLine1;
                    serviceRequestAddress.AddressLine2 = adminReschedule.AddressLine2;
                    serviceRequestAddress.City = adminReschedule.City;
                    serviceRequestAddress.PostalCode = adminReschedule.PostalCode;
                    serviceRequestAddress.State = adminReschedule.State;
                    _helperlandContext.ServiceRequestAddresses.Update(serviceRequestAddress);
                    _helperlandContext.SaveChanges();

                    string subject = "Service modified by admin.";
                    string mailTitle = "Helperland Service";
                    string fromEmail = "codewithravi2511@gmail.com";
                    string fromEmailPassword = "dyto qxph hvgv oslf";

                    var AvailableProvider = serviceRequest.ServiceProviderId;
                    if (AvailableProvider != null)
                    {
                        User user = _helperlandContext.Users.Where(x => x.UserId == AvailableProvider).FirstOrDefault();
                        string MailBody = "<!DOCTYPE html>" +
                                     "<html> " +
                                     "<body style=\"background -color:#ff7f26;text-align:center;\"> " +
                                     "<h1 style=\"color:#051a80;\">Welcome to Helperland.</h1> " +
                                      "<p>Dear " + user.FirstName + " " + user.LastName + " ,</p>" +
                                      "<p>Service request with reference id " + serviceRequest.ServiceRequestId + " has been modified by admin.</p>" +
                                      "<p>For more information please Login to your account</p>" +
                                      "<a style=\"background:#1d7a8c;padding:5px 10px;color:white;text-decoration:none;font-size:25px;\"  href='" + Url.Action("Index", "Helperland", new { }, "http") + "'>Login Now</a>" +
                                     "</body> " +
                                 "</html>";
                        MailMessage message = new MailMessage(new MailAddress(fromEmail, mailTitle), new MailAddress(user.Email));
                        message.Subject = subject;
                        message.Body = MailBody;
                        message.IsBodyHtml = true;

                        //Server Details
                        SmtpClient smtp = new SmtpClient();
                        //Outlook ports - 465 (SSL) or 587 (TLS)
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                        //Credentials
                        System.Net.NetworkCredential credential = new System.Net.NetworkCredential();
                        credential.UserName = fromEmail;
                        credential.Password = fromEmailPassword;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = credential;

                        smtp.Send(message);
                    }
                    User cust = _helperlandContext.Users.Where(x => x.UserId == serviceRequest.UserId).FirstOrDefault();
                    string MailBody1 = "<!DOCTYPE html>" +
                                     "<html> " +
                                     "<body style=\"background -color:#ff7f26;text-align:center;\"> " +
                                     "<h1 style=\"color:#051a80;\">Welcome to Helperland.</h1> " +
                                      "<p>Dear " + cust.FirstName + " " + cust.LastName + " ,</p>" +
                                      "<p>Service request with reference id " + serviceRequest.ServiceRequestId + " has been modified by admin.</p>" +
                                      "<p>For more information please Login to your account</p>" +
                                      "<a style=\"background:#1d7a8c;padding:5px 10px;color:white;text-decoration:none;font-size:25px;\"  href='" + Url.Action("Index", "Helperland", new { }, "http") + "'>Login Now</a>" +
                                     "</body> " +
                                 "</html>";
                    MailMessage message1 = new MailMessage(new MailAddress(fromEmail, mailTitle), new MailAddress(cust.Email));
                    message1.Subject = subject;
                    message1.Body = MailBody1;
                    message1.IsBodyHtml = true;

                    //Server Details
                    SmtpClient smtp1 = new SmtpClient();
                    //Outlook ports - 465 (SSL) or 587 (TLS)
                    smtp1.Host = "smtp.gmail.com";
                    smtp1.Port = 587;
                    smtp1.EnableSsl = true;
                    smtp1.DeliveryMethod = SmtpDeliveryMethod.Network;

                    //Credentials
                    System.Net.NetworkCredential credential1 = new System.Net.NetworkCredential();
                    credential1.UserName = fromEmail;
                    credential1.Password = fromEmailPassword;
                    smtp1.UseDefaultCredentials = false;
                    smtp1.Credentials = credential1;

                    smtp1.Send(message1);

                    return Json("UpdateSuccessfully");
                }
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpGet]
        public ActionResult CheckAvailableZip(string pinVal)
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 3 && HttpContext.Session.GetInt32("userid") != null)
            {
                var zip = _helperlandContext.Zipcodes.Where(x => x.ZipcodeValue == pinVal).FirstOrDefault();
                if (zip!=null)
                {
                    City city = _helperlandContext.Cities.Where(x => x.Id == zip.CityId).FirstOrDefault();
                    State state = _helperlandContext.States.Where(x => x.Id == city.StateId).FirstOrDefault();
                    var obj = new 
                    {
                     City = city.CityName,
                     State = state.StateName
                    };

                    return Json(obj);
                }
                else
                {
                    return Json("NotAvailable");
                }
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpPost]
        public IActionResult CancleService(int CancleClickedId)
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 3 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                ServiceRequest cancleReq = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == Convert.ToInt32(CancleClickedId)).FirstOrDefault();
                cancleReq.Status = 1;
                cancleReq.ModifiedBy = logedUserid;
                cancleReq.ModifiedDate = DateTime.Now;
                _helperlandContext.ServiceRequests.Update(cancleReq);
                _helperlandContext.SaveChanges();
                return Json("Sucessfully");
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
