using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quickipedia.Models;
using Quickipedia.Services;
using System.Data.Entity;

namespace Quickipedia.Controllers
{
    public class ProfileManagementController : Controller
    {
        // GET: ProfileManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProfileTemplate()
        {
            return View();
        }

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
                        ProfileTemplateAttachment newAttachment = new ProfileTemplateAttachment
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
        public JsonResult DeleteAttachment(ProfileTemplateAttachmentModel file)
        {
            string serverResponse = "";

            if(file != null)
            {
                file.Status = "X";

                ProfileManagementService.UpdateAttachment(file, out serverResponse);
            }

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetProfileTemplate()
        {
            string serverResponse = "";

            var links = ProfileManagementService.GetLinks(out serverResponse);

            var attachments = ProfileManagementService.GetAttachment(out serverResponse);

            return Json(new { errorMessage = serverResponse, links = links, attachments = attachments });
        }

        [HttpPost]
        public JsonResult DeleteTemplateLink(ProfileTemplateLinkModel link)
        {
            string serverResponse = "";

            link.Status = "X";

            ProfileManagementService.SaveTemplateLink(link, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult SaveTemplateLink(ProfileTemplateLinkModel link)
        {
            string serverResponse = "";

            if (link != null)
                ProfileManagementService.SaveTemplateLink(link, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetProfileManagement()
        {
            var profile = ProfileManagementService.GetProfileManagement();

            return Json(profile);
        }

        [HttpPost]
        public JsonResult SaveProfileMangement(ProfileManagementModel profile)
        {
            string serverResponse = "";

            if(profile != null)
            {
                ProfileManagementService.SaveProfileManagement(profile, out serverResponse);
            }

            return Json(serverResponse);
        }
    }
}