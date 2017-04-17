using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quickipedia.Services;
using Quickipedia.Models;
using System.IO;
using System.Data.Entity;

namespace Quickipedia.Controllers
{
    public class HotelController : Controller
    {
        #region View
        public ActionResult HotelPolicy()
        {
            return View();
        }

        public ActionResult HotelCorporateCode()
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
                for (int i = 0; i < uploadFiles.Count; i++)
                {
                    HttpPostedFile postedFile = uploadFiles[i];

                    ext = Path.GetExtension(postedFile.FileName);

                    newFileName = DateTime.Now.ToString("MMddyyyHHmmss");

                    var filePath = Server.MapPath(@"\FileUploads\" + newFileName + ext);

                    postedFile.SaveAs(filePath);

                    using (var db = new QuickipediaEntities())
                    {
                        HotelProgramAttachement newAttachment = new HotelProgramAttachement
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
        public JsonResult UpdateAttachment (List<HotelAttachmentModel> files)
        {
            if(files != null)
            {
                HotelService.UpdateAttachment(files);
            }

            return Json("Updated");
        }

        [HttpPost]
        public JsonResult GetHotelProgram()
        {
            string serverResponse = "";

            var links = HotelService.GetLinks(out serverResponse);

            var attachments = HotelService.GetAttachments(out serverResponse);

            return Json(new { links = links , attachment = attachments});
        }
        [HttpPost]
        public JsonResult SaveHotelProgram(List<HotelLinksModel> links)
        {
            string serverResponse = "";

            if (links != null)
                HotelService.SaveHotelLinks(links, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetHotelCorporateCode()
        {
            return Json(HotelService.GetCorporateCode(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateHotelCorporateCode(HotelCorporateCodeModel corporateCode)
        {
            string serverResponse = "";

            if (corporateCode != null)
                HotelService.SaveHotelCoporateCode(corporateCode, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetHotelPolicy()
        {
            return Json(HotelService.GetHotelPolicy(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateHotelPolicy(HotelPolicyModel hotelPolicy)
        {
            string serverResponse = "";

            if (hotelPolicy != null)
                HotelService.SaveHotelPolicy(hotelPolicy, out serverResponse);

            return Json(serverResponse);
        }
        #endregion
    }
}