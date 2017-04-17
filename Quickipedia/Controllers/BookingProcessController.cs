using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quickipedia.Models;
using Quickipedia.Services;

namespace Quickipedia.Controllers
{
    public class BookingProcessController : Controller
    {
       public ActionResult Domestic()
        {
            return View();
        }

        public ActionResult International()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetInternational()
        {
            return Json(BookingProcessService.GetINTL());
        }

        [HttpPost]
        public JsonResult GetDomestic()
        {
            return Json(BookingProcessService.GetDOM());
        }

        [HttpPost]
        public JsonResult SaveDomestic(DOMBookingProcessModel domestic)
        {
            string serverResponse = "";

            if (domestic != null)
                BookingProcessService.SaveDOMProcess(domestic, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult SaveInternational(INTLBookingProcessModel international)
        {
            string serverResponse = "";

            if (international != null)
                BookingProcessService.SaveINTLProcess(international, out serverResponse);

            return Json(serverResponse);
        }
    }
}