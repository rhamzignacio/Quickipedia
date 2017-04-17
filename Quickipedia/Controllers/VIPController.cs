using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quickipedia.Services;
using Quickipedia.Models;

namespace Quickipedia.Controllers
{
    public class VIPController : Controller
    {
        #region Views
        public ActionResult VIPList()
        {
            return View();
        }
        #endregion

        #region Functions
        [HttpPost]
        public JsonResult GetVIPList()
        {
            return Json(VIPService.GetVIPList(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult SaveVIP(VIPModel vip)
        {
            string serverResponse = "";

            Guid ID = Guid.Empty;

            if (vip != null)
                VIPService.SaveVIPList(vip, out serverResponse, out ID);

            return Json(new { message = serverResponse, ID = ID });
        }
        #endregion
    }
}