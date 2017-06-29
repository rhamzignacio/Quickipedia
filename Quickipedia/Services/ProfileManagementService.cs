using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Data.Entity;

namespace Quickipedia.Services
{
    public class ProfileManagementService
    {
        public static List<ProfileTemplateLinkModel> GetLinks(out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var query = from l in db.ProfileTemplateLink
                                join u in db.UserAccount on l.ModifiedBy equals u.ID into qU
                                from user in qU.DefaultIfEmpty()
                                where l.ClientCode == UniversalHelpers.SelectedClient
                                select new ProfileTemplateLinkModel
                                {
                                    ID = l.ID,
                                    ClientCode = l.ClientCode,
                                    Link = l.Link,
                                    Status = "Y",
                                    Title = l.Title,
                                    ModifiedBy = l.ModifiedBy,
                                    ModifiedDate = l.ModifiedDate,
                                    ShowModifiedBy = user.FirstName + " " + user.LastName
                                };

                    return query.ToList();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static void SaveTemplateLink(ProfileTemplateLinkModel item, out string message)
        {
            try
            {
                message = "Saved";

                using (var db = new QuickipediaEntities())
                {
                    if(item.Status == "X")
                    {
                        var link = db.ProfileTemplateLink.FirstOrDefault(r => r.ID == item.ID);

                        if (link != null)
                            db.Entry(link).State = EntityState.Deleted;
                    }
                    else if(item.Status == "U")
                    {
                        var link = db.ProfileTemplateLink.FirstOrDefault(r => r.ID == item.ID);

                        if(link != null)
                        {
                            link.Title = item.Title;

                            link.Link = item.Link;

                            link.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                            link.ModifiedDate = DateTime.Now;

                            db.Entry(link).State = EntityState.Modified;
                        }
                    }
                    else if(item.Status == "Y")
                    {

                    }
                    else
                    {
                        ProfileTemplateLink newLink = new ProfileTemplateLink
                        {
                            ID = Guid.NewGuid(),
                            ClientCode = UniversalHelpers.SelectedClient,
                            Link = item.Link,
                            Title = item.Title,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        db.Entry(newLink).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static List<ProfileTemplateAttachmentModel> GetAttachment(out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var query = from p in db.ProfileTemplateAttachment
                                join u in db.UserAccount on p.ModifiedBy equals u.ID into qU
                                from user in qU.DefaultIfEmpty()
                                where p.ClientCode == UniversalHelpers.SelectedClient
                                select new ProfileTemplateAttachmentModel
                                {
                                    ID = p.ID,
                                    ClientCode = p.ClientCode,
                                    Extension = p.Extension,
                                    FileName = p.FileName,
                                    FileSize = p.FileSize,
                                    Path = p.Path,
                                    Status = "Y",
                                    ModifiedBy = p.ModifiedBy,
                                    ModifiedDate = p.ModifiedDate,
                                    ShowModifiedBy = user.FirstName + " " + user.LastName
                                };

                    return query.ToList();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static void UpdateAttachment (ProfileTemplateAttachmentModel link, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    if(link.Status == "X")
                    {
                        var file = db.ProfileTemplateAttachment.FirstOrDefault(r => r.ID == link.ID);

                        if(file != null)
                        {
                            db.Entry(file).State = EntityState.Deleted;
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }
        public static ProfileManagementModel GetProfileManagement()
        {
            using (var db = new QuickipediaEntities())
            {
                var profile = from p in db.ProfileManagement
                              join u in db.UserAccount on p.ModifiedBy equals u.ID into qU
                              from user in qU.DefaultIfEmpty()
                              where p.ClientCode == UniversalHelpers.SelectedClient
                              select new ProfileManagementModel
                              {
                                  ID = p.ID,
                                  ClientCode = p.ClientCode,
                                  ProfileType = p.ProfileType,
                                  OtherProfileType = p.OtherProfileType,
                                  SpecialInstruction = p.SpecialInstructions,
                                  ModifiedDate = p.ModifiedDate,
                                  ModifiedBy = p.ModifiedBy,
                                  ShowModifiedBy = user.FirstName + " " + user.LastName,
                                  BookingType = p.BookingType,
                                  OtherBookingType = p.OtherBookingType
                              };

                if (profile.FirstOrDefault() != null)
                    return profile.FirstOrDefault();
                else
                    return new ProfileManagementModel();
            }
        }

        public static void SaveProfileManagement(ProfileManagementModel model, out string message)
        {
            try
            {
                message = "";

                using(var db = new QuickipediaEntities())
                {
                    var profile = db.ProfileManagement.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(profile != null) //UPDATE
                    {
                        message = "Updated";

                        profile.ProfileType = model.ProfileType;

                        profile.OtherProfileType = model.OtherProfileType;

                        profile.SpecialInstructions = model.SpecialInstruction;

                        profile.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        profile.ModifiedDate = DateTime.Now;

                        profile.BookingType = model.BookingType;

                        profile.OtherBookingType = model.OtherBookingType;

                        db.Entry(profile).State = EntityState.Modified;
                    }
                    else
                    {
                        message = "Saved";

                        ProfileManagement newProfile = new ProfileManagement
                        {
                            ID = Guid.NewGuid(),
                            ProfileType = model.ProfileType,
                            ClientCode = UniversalHelpers.SelectedClient,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now,
                            OtherProfileType = model.OtherProfileType,
                            SpecialInstructions = model.SpecialInstruction,
                            OtherBookingType = model.OtherBookingType
                        };

                        db.Entry(newProfile).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }
    }
}