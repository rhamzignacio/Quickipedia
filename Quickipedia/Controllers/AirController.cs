using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quickipedia.Models;
using Quickipedia.Services;

namespace Quickipedia.Controllers
{
    public class AirController : Controller
    {
        #region View
        public ActionResult SLA()
        {
            return View();
        }

        public ActionResult TripAuthorizerProcess()
        {
            return View();
        }

        public ActionResult TravelPolicy()
        {
            return View();
        }

        public ActionResult PreferredAirlines()
        {
            return View();
        }

        public ActionResult DisallowedAirlines()
        {
            return View();
        }

        public ActionResult TravelSecurity()
        {
            return View();
        }

        public ActionResult Others()
        {
            return View();
        }
        #endregion

        #region Function
        //===============OTHERS====================
        [HttpPost]
        public JsonResult GetOthers()
        {
            return Json(AirService.GetOtherAir());
        }

        [HttpPost]
        public JsonResult SaveOthers(OtherAirModel other)
        {
            string serverResponse = "";

            if (other != null)
                AirService.SaveOtherAir(other, out serverResponse);

            return Json(serverResponse);
        }

        //===============TRAVEL SECURITY========================
        [HttpPost]
        public JsonResult GetTravelSecurity()
        {
            return Json(AirService.GetTravelSecurity(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateTravelSecurity(TravelSecurityModel travelSecurity)
        {
            string serverResponse = "";

            if (travelSecurity != null)
                AirService.SaveTravelSecurity(travelSecurity, out serverResponse);

            return Json(serverResponse);
        }

        //===============DISALLOWED AIRLINES================
        [HttpPost]
        public JsonResult GetDisallowedAirlines()
        {
            return Json(AirService.GetDisallowedAirlines(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateDisallowedAirlines(PreferredAirlineModel airlines)
        {
            string serverResponse = "";

            if (airlines != null)
                AirService.SaveDisallowedAirlines(airlines, out serverResponse);

            return Json(serverResponse);
        }

        //===============PREFERRED AIRLINES=================
        [HttpPost]
        public JsonResult GetPreferredAirlines()
        {
            return Json(AirService.GetPrefferedAirlines(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdatePreferredAirlines(PreferredAirlineModel airlines)
        {
            string serverResponse = "";

            if (airlines != null)
                AirService.SavePreferredAirlines(airlines, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetAirlines()
        {
            return Json(AirService.GetAllAirlines());
        }
        
        //==============TRAVEL POLICY========================
        [HttpPost]
        public JsonResult GetTravelPolicy()
        {
            return Json(AirService.GetTravelPolicy(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateTravelPolicy(TravelPolicyModel travelPolicy)
        {
            string serverResponse = "";

            if (travelPolicy != null)
                AirService.SaveTravelPolicy(travelPolicy, out serverResponse);

            return Json(serverResponse);
        }

        //================TRIP AUTHORIZER PROCESS===================
        [HttpPost]
        public JsonResult GetTripAuthorizerProcess()
        {
            return Json(AirService.GetTripAuthorizerProcess(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateTripAuthorizerProcess(TripAuthorizerProcessModel tripAuthorizer)
        {
            string serverResponse = "";

            if (tripAuthorizer != null)
                AirService.SaveTripAuthorizer(tripAuthorizer, out serverResponse);

            return Json(serverResponse);
        }

        //================SLA=====================
        [HttpPost]
        public JsonResult GetSLA()
        {
            return Json(AirService.GetSLA(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateSLA(SLAModel sla)
        {
            string serverResonse = "";

            if (sla != null)
                AirService.SaveSLA(sla, out serverResonse);

            return Json(serverResonse);
        }
        #endregion
    }
}