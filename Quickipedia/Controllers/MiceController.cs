using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quickipedia.Services;
using Quickipedia.Models;
using System.Data.Entity;

namespace Quickipedia.Controllers
{
    public class MiceController : Controller
    {
        #region Views
        public ActionResult MicePolicy()
        {
            return View();
        }

        public ActionResult SMMPForMSD()
        {
            return View();
        }

        public ActionResult MiceGroupBookingPolicy()
        {
            return View();
        }

        public ActionResult Pricing()
        {
            return View();
        }

        public ActionResult TicketingApproval()
        {
            return View();
        }

        public ActionResult Other()
        {
            return View();
        }

        public ActionResult Program()
        {
            return View();
        }
        #endregion

        #region  Function
        //=================PROGRAM======================
        [HttpPost]
        public ActionResult FileUpload()
        {
            var httpRequest = System.Web.HttpContext.Current.Request;
            HttpFileCollection uploadFiles = httpRequest.Files;
            var docfiles = new List<string>();
            string newFileName = "";
            string ext = "";

            if (httpRequest.Files.Count > 0)
            {
                for (int i = 0; i < uploadFiles.Count; i++)
                {
                    HttpPostedFile postedFile = uploadFiles[i];

                    ext = System.IO.Path.GetExtension(postedFile.FileName);

                    newFileName = DateTime.Now.ToString("MMddyyyHHmmss");

                    var filePath = Server.MapPath(@"\FileUploads\" + newFileName + ext);

                    postedFile.SaveAs(filePath);

                    using (var db = new QuickipediaEntities())
                    {
                        MiceProgramAttachment newAttachment = new MiceProgramAttachment
                        {
                            ID = Guid.NewGuid(),
                            ClientCode = UniversalHelpers.SelectedClient,
                            FileName = postedFile.FileName,
                            FileSize = (postedFile.ContentLength / 1024).ToString(),
                            Extension = ext,
                            Path = newFileName + ext,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        db.Entry(newAttachment).State = EntityState.Added;

                        db.SaveChanges();
                    }
                }
            }

            return Json("");
        }
        
        [HttpPost]
        public JsonResult DeleteAttachment(MiceAttachmentModel file)
        {
            string serverResponse = "";

            if(file != null)
            {
                file.Status = "X";

                MiceService.UpdateAttachment(file, out serverResponse);
            }

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetMiceProgram()
        {
            string serverResponse = "";

            var links = MiceService.GetLinks(out serverResponse);

            var attachments = MiceService.GetAttachments(out serverResponse);

            return Json(new { links = links, attachments = attachments });
        }

        [HttpPost]
        public JsonResult DeleteMiceProgram(MiceLinksModel links)
        {
            string serverReponse = "";

            links.Status = "X";

            MiceService.SaveMiceLinks(links, out serverReponse);

            return Json(serverReponse);
        }

        [HttpPost]
        public JsonResult SaveMiceProgram(MiceLinksModel links)
        {
            string serverResponse = "";

            if (links != null)
                MiceService.SaveMiceLinks(links, out serverResponse);

            return Json(serverResponse);
        }

        //==================OTHER========================
        [HttpPost]
        public JsonResult GetOther()
        {
            return Json(MiceService.GetOther());
        }

        [HttpPost]
        public JsonResult SaveOther(MiceOtherModel other)
        {
            string serverResponse = "";

            if (other != null)
                MiceService.SaveOther(other, out serverResponse);

            return Json(serverResponse);
        }

        //==================TICKETING APPROVAL=======================
        [HttpPost]
        public JsonResult GetTicketingApproval()
        {
            return Json(MiceService.GetTicketingApproval(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateTicketingApproval(MiceTicketApprovalModel ticketingApproval)
        {
            string serverResponse = "";

            if (ticketingApproval != null)
                MiceService.SaveTicketing(ticketingApproval, out serverResponse);

            return Json(serverResponse);
        }

        //====================MICE PRICING====================
        [HttpPost]
        public JsonResult GetPricing()
        {
            return Json(MiceService.GetPricing(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdatePricing(MicePricingModel pricing)
        {
            string serverReponse = "";

            if (pricing != null)
                MiceService.SavePricing(pricing, out serverReponse);

            return Json(serverReponse);
        }

        //===================GROUP BOOKING POLICY===========================
        [HttpPost]
        public JsonResult GetGroupBookingPolicy()
        {
            return Json(MiceService.GetGroupBookingPolicy(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateGroupBooking(GroupBookingPolicyModel groupBooking)
        {
            string serverResponse = "";

            if (groupBooking != null)
                MiceService.SaveGroupBooking(groupBooking, out serverResponse);

            return Json(serverResponse);
        }

        //======================SMMP FOR MSD+===============================
        [HttpPost]
        public JsonResult GetSMMPForMSD()
        {
            return Json(MiceService.GetSMMPForMSD(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateSMMP(SMMPForMSDModel smmp)
        {
            string serverResponse = "";

            if (smmp != null)
                MiceService.SaveSMMP(smmp, out serverResponse);

            return Json(serverResponse);
        }
        
        //=========================MICE POLICY=================================
        [HttpPost]
        public JsonResult GetMicePolicy()
        {
            return Json(MiceService.GetMicePolicy(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateMicePolicy(MicePolicyModel micePolicy)
        {
            string serverResponse = "";

            if (micePolicy != null)
                MiceService.SaveMicePolicy(micePolicy, out serverResponse);

            return Json(serverResponse);
        }

        #endregion
    }
} 