using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quickipedia.Services;
using Quickipedia.Models;

namespace Quickipedia.Controllers
{
    public class BasicInfoController : Controller
    {
        // GET: BasicInfo
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetTCDropdown()
        {
            return Json(new { intlTC = BasicInfoService.GetTCDropdown("INTL"),
                domTC = BasicInfoService.GetTCDropdown("DOM"),
                biller = BasicInfoService.GetTCDropdown("ACCTG") });
        }

        [HttpPost]
        public JsonResult GetBasicInfo()
        {
            return Json(BasicInfoService.GetBasicInfo(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public ActionResult AddUpdateBasicInfo(BasicInfoModel basicInfo, string type)
        {
            string serverResponse = "";

            if (basicInfo != null)
            {
                if (BasicInfoService.SaveBasicInfo(basicInfo, type, out serverResponse))
                {

                }
            }

            if (serverResponse == "")
            {
                if (type == "NewClient")
                {
                    serverResponse = "Saved";

                    AccountService.SelectedClientToSession(basicInfo.ClientCode);
                }
                else
                    serverResponse = "Updated";
            }

            return Json(serverResponse);
        }
    }
}