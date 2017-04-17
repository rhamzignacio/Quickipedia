using System.Web.Mvc;
using Quickipedia.Services;
using Quickipedia.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Data.Entity;

namespace Quickipedia.Controllers
{
    public class RailController : Controller
    {
        #region Views
        public ActionResult RailPolicy()
        {
            return View();
        }

        public ActionResult RailCorporateCode()
        {
            return View();
        }

        public ActionResult Program()
        {
            return View();
        }
        #endregion

        #region Function
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
                int i;
                for (i = 0; i < uploadFiles.Count; i++)
                {
                    HttpPostedFile postedFile = uploadFiles[i];

                    ext = Path.GetExtension(postedFile.FileName);

                    newFileName = DateTime.Now.ToString("MMddyyyHHmmss");

                    var filePath = Server.MapPath(@"\FileUploads\" + newFileName + ext);

                    postedFile.SaveAs(filePath);

                    using (var db = new QuickipediaEntities())
                    {
                        RailProgramAttachment newAttachment = new RailProgramAttachment
                        {
                            ID = Guid.NewGuid(),
                            ClientCode = UniversalHelpers.SelectedClient,
                            FileName = postedFile.FileName,
                            FileSize = (postedFile.ContentLength / 1024).ToString(),
                            Extension = ext,
                            Path = newFileName + ext
                        };

                        db.Entry(newAttachment).State = EntityState.Added;

                        db.SaveChanges();
                    }
                }
            }

            return Json("");
        }

        [HttpPost]
        public JsonResult UpdateAttachment(List<RailAttachmentModel> files)
        {
            if(files != null)
            {
                RailService.UpdateAttachments(files);
            }

            return Json("Updated");
        }

        [HttpPost]
        public JsonResult SaveRailProgram(List<RailLinksModel> links)
        {

            string serverResponse = "";

            if (links != null)
                RailService.SaveRailLinks(links, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetRailProgram()
        {
            string serverResponse = "";

            var links = RailService.GetLinks(out serverResponse);

            var attachments = RailService.GetAttachments(out serverResponse);

            return Json(new { links = links, attachments = attachments });
        }

        [HttpPost]
        public JsonResult GetRailCorporateCode()
        {
            return Json(RailService.GetRailCorporateCode(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateRailCorporateCode(RailCorporateCodeModel railCorporateCode)
        {
            string serverResponse = "";

            if (railCorporateCode != null)
                RailService.SaveRailCorporateCode(railCorporateCode, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetRailPolicy()
        {
            return Json(RailService.GetRailPolicy(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateRailPolicy (RailPolicyModel railPolicy)
        {
            string serverResponse = "";

            if (railPolicy != null)
                RailService.SaveRailPolicy(railPolicy, out serverResponse);

            return Json(serverResponse);
        }
        #endregion
    }
}