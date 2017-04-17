using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quickipedia.Services;
using Quickipedia.Models;

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
        #endregion

        #region  Function
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