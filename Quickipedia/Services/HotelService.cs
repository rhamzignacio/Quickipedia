using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Data.Entity;


namespace Quickipedia.Services
{
    public class HotelService
    {
        public static void UpdateAttachment (List<HotelAttachmentModel> files)
        {
            try
            {
                using (var db = new QuickipediaEntities())
                {
                    files.ForEach(item =>
                    {
                        if(item.Status == "X")
                        {
                            var file = db.HotelProgramAttachement.FirstOrDefault(r => r.ID == item.ID);

                            if(file != null)
                            {
                                db.Entry(file).State = EntityState.Deleted;
                            }
                        }
                    });

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {

            }
        }

        public static List<HotelAttachmentModel> GetAttachments (out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var query = from a in db.HotelProgramAttachement
                                join u in db.UserAccount on a.ModifiedBy equals u.ID into qU
                                from user in qU.DefaultIfEmpty()
                                where a.ClientCode == UniversalHelpers.SelectedClient
                                select new HotelAttachmentModel
                                {
                                    ID = a.ID,
                                    ClientCode = a.ClientCode,
                                    Extension = a.Extension,
                                    FileName = a.FileName,
                                    FileSize = a.FileSize,
                                    Path = a.Path,
                                    Status = "Y",
                                    ModifiedDate = DateTime.Now,
                                    ModifiedBy = a.ModifiedBy,
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

        public static List<HotelLinksModel> GetLinks (out string message)
        {
            try
            {

                message = "";
                using (var db = new QuickipediaEntities())
                {
                    var query = from l in db.HotelProgramLink
                                join u in db.UserAccount on l.ModifiedBy equals u.ID into qU
                                from user in qU.DefaultIfEmpty()
                                where l.ClientCode == UniversalHelpers.SelectedClient
                                select new HotelLinksModel
                                {
                                    ID = l.ID,
                                    ClientCode = l.ClientCode,
                                    Link = l.Link,
                                    Status = "Y",
                                    Title = l.Title,
                                    ModifiedBy = l.ModifiedBy,
                                    ModifiedDate = DateTime.Now,
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

        public static void SaveHotelLinks(List<HotelLinksModel> links, out string message)
        {
            try
            {
                message = "Saved";

                using (var db = new QuickipediaEntities())
                {
                    links.ForEach(item =>
                    {
                       if(item.Status == "X")
                       {
                            var link = db.HotelProgramLink.FirstOrDefault(r => r.ID == item.ID);

                            if (link != null)
                                db.Entry(link).State = EntityState.Deleted;
                       }
                       else if(item.Status == "U")
                       {
                            var link = db.HotelProgramLink.FirstOrDefault(r => r.ID == item.ID);

                            if(link != null)
                            {
                                link.Title = item.Title;
                                
                                link.Link = item.Link;

                                link.ModifiedDate = DateTime.Now;

                                link.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                                db.Entry(link).State = EntityState.Modified;
                            }
                       }
                       else if(item.Status == "Y")
                        {

                        }
                       else
                       {
                            HotelProgramLink newLink = new HotelProgramLink
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
                    });
                    
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }
        public static void SaveHotelCoporateCode(HotelCorporateCodeModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var corpCode = db.HotelCorporateCode.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(corpCode != null)
                    {
                        corpCode.CorporateCode = model.CorporateCode;

                        corpCode.ModifiedDate = DateTime.Now;

                        corpCode.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        db.Entry(corpCode).State = EntityState.Modified;

                        message = "Updated";
                    }
                    else
                    {
                        HotelCorporateCode newCorpCode = new HotelCorporateCode
                        {
                            ID = Guid.NewGuid(),
                            ClientCode = UniversalHelpers.SelectedClient,
                            CorporateCode = model.CorporateCode,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        db.Entry(newCorpCode).State = EntityState.Added;

                        message = "Saved";
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void SaveHotelPolicy(HotelPolicyModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var policy = db.HotelPolicy.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if (policy != null)//UPDATE
                    {
                        policy.Policy = model.Policy;

                        policy.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        policy.ModifiedDate = DateTime.Now;

                        db.Entry(policy).State = EntityState.Modified;

                        message = "Updated";
                    }
                    else//NEW
                    {
                        HotelPolicy newPolicy = new HotelPolicy
                        {
                            ID = Guid.NewGuid(),
                            Policy = model.Policy,
                            ClientCode = UniversalHelpers.SelectedClient,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        db.Entry(newPolicy).State = EntityState.Added;

                        message = "Saved";
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static HotelCorporateCodeModel GetCorporateCode(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var code = from hc in db.HotelCorporateCode
                           join u in db.UserAccount on hc.ModifiedBy equals u.ID into qU
                           from user in qU.DefaultIfEmpty()
                           where hc.ClientCode == clientCode
                           select new HotelCorporateCodeModel
                           {
                               ID = hc.ID,
                               ClientCode = hc.ClientCode,
                               CorporateCode = hc.CorporateCode,
                               ModifiedDate = hc.ModifiedDate,
                               ModifiedBy = hc.ModifiedBy,
                               ShowModifiedBy = user.FirstName + " " + user.LastName
                           };

                if (code.FirstOrDefault() != null)
                    return code.FirstOrDefault();
                else
                    return new HotelCorporateCodeModel();
            }
        }

        public static HotelPolicyModel GetHotelPolicy (string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var policy = from hp in db.HotelPolicy
                             join u in db.UserAccount on hp.ModifiedBy equals u.ID into qU
                             from user in qU.DefaultIfEmpty()
                             where hp.ClientCode == clientCode
                             select new HotelPolicyModel
                             {
                                 ID = hp.ID,
                                 ClientCode = hp.ClientCode,
                                 Policy = hp.Policy,
                                 ModifiedBy = hp.ModifiedBy,
                                 ModifiedDate = hp.ModifiedDate,
                                 ShowModifiedBy = user.FirstName + " " + user.LastName
                             };

                if (policy.FirstOrDefault() != null)
                    return policy.FirstOrDefault();
                else
                    return new HotelPolicyModel();
            }
        }
    }
}