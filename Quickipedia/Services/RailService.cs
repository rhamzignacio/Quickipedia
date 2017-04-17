using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Data.Entity;

namespace Quickipedia.Services
{
    public class RailService
    {
        public static void UpdateAttachments (List<RailAttachmentModel> files)
        {
            try
            {
                using (var db = new QuickipediaEntities())
                {
                    files.ForEach(item =>
                    {
                        if (item.Status == "X")
                        {

                            var file = db.RailProgramAttachment.FirstOrDefault(r => r.ID == item.ID);

                            if (file != null)
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

        public static List<RailAttachmentModel> GetAttachments (out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var query = from a in db.RailProgramAttachment
                                join u in db.UserAccount on a.ModifiedBy equals u.ID into qU
                                from user in qU.DefaultIfEmpty()
                                where a.ClientCode == UniversalHelpers.SelectedClient
                                select new RailAttachmentModel
                                {
                                    ID = a.ID,
                                    Extension = a.Extension,
                                    FileName = a.FileName,
                                    FileSize = a.FileSize,
                                    Path = a.Path,
                                    Status = "Y",
                                    ModifiedBy = a.ModifiedBy,
                                    ModifiedDate = a.ModifiedDate,
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

        public static List<RailLinksModel> GetLinks (out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var query = from l in db.RailProgramLink
                                join u in db.UserAccount on l.ModifiedBy equals u.ID into qU
                                from user in qU.DefaultIfEmpty()
                                where l.ClientCode == UniversalHelpers.SelectedClient
                                select new RailLinksModel
                                {
                                    ID = l.ID,
                                    ClientCode = l.ClientCode,
                                    Link = l.Link,
                                    Status = "Y",
                                    Title = l.Title,
                                    ModifiedDate = l.ModifiedDate,
                                    ModifiedBy = l.ModifiedBy,
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

        public static void SaveRailLinks(List<RailLinksModel> links, out string message)
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
                            var link = db.RailProgramLink.FirstOrDefault(r => r.ID == item.ID);

                            if (link != null)
                                db.Entry(link).State = EntityState.Deleted;
                        }
                        else if(item.Status == "U")
                        {
                            var link = db.RailProgramLink.FirstOrDefault(r => r.ID == item.ID);

                            if(link != null)
                            {
                                link.Title = item.Title;

                                link.Link = item.Link;

                                link.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                                link.ModifiedDate = DateTime.Now;

                                db.Entry(link).State = EntityState.Modified;
                            }
                        }
                        else if (item.Status == "Y")
                        {

                        }
                        else
                        {
                            RailProgramLink newLink = new RailProgramLink
                            {
                                ID = Guid.NewGuid(),
                                ClientCode = UniversalHelpers.SelectedClient,
                                Link = item.Link,
                                Title = item.Title,
                                ModifiedDate = DateTime.Now,
                                ModifiedBy = UniversalHelpers.CurrentUser.ID
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

        public static void SaveRailPolicy(RailPolicyModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var policy = db.RailPolicy.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(policy != null)
                    {
                        message = "Updated";

                        policy.Policy = model.Policy;

                        policy.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        policy.ModifiedDate = DateTime.Now;

                        db.Entry(policy).State = EntityState.Modified;
                    }
                    else
                    {
                        message = "Saved";

                        RailPolicy newPolicy = new RailPolicy
                        {
                            ID = Guid.NewGuid(),
                            Policy = model.Policy,
                            ClientCode = UniversalHelpers.SelectedClient,
                            ModifiedDate = DateTime.Now,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID
                        };

                        db.Entry(newPolicy).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }

            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void SaveRailCorporateCode(RailCorporateCodeModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var corp = db.RailCorporateCode.FirstOrDefault(r => r.CorporateCode == UniversalHelpers.SelectedClient);

                    if(corp != null)//UPDATE
                    {
                        message = "Updated";

                        corp.CorporateCode = model.CorporateCode;

                        corp.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        corp.ModifiedDate = DateTime.Now;

                        db.Entry(corp).State = EntityState.Modified;
                    }
                    else//NEW
                    {
                        message = "Saved";

                        RailCorporateCode newCorp = new RailCorporateCode
                        {
                            ID = Guid.NewGuid(),
                            CorporateCode = model.CorporateCode,
                            ClientCode = UniversalHelpers.SelectedClient,
                            ModifiedDate = DateTime.Now,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID
                        };

                        db.Entry(newCorp).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static RailCorporateCodeModel GetRailCorporateCode(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var corp = from rc in db.RailCorporateCode
                           join u in db.UserAccount on rc.ModifiedBy equals u.ID into qU
                           from user in qU.DefaultIfEmpty()
                           where rc.ClientCode == clientCode
                           select new RailCorporateCodeModel
                           {
                               ID = rc.ID,
                               ClientCode = rc.ClientCode,
                               CorporateCode = rc.CorporateCode,
                               ModifiedBy = rc.ModifiedBy,
                               ModifiedDate = rc.ModifiedDate,
                               ShowModifiedBy = user.FirstName + " " + user.LastName
                           };

                if (corp.FirstOrDefault() != null)
                    return corp.FirstOrDefault();
                else
                    return new RailCorporateCodeModel();
            }
        }

        public static RailPolicyModel GetRailPolicy(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var policy = from rp in db.RailPolicy
                             join u in db.UserAccount on rp.ModifiedBy equals u.ID into qU
                             from user in qU.DefaultIfEmpty()
                             where rp.ClientCode == clientCode
                             select new RailPolicyModel
                             {
                                 ID = rp.ID,
                                 Policy = rp.Policy,
                                 ClientCode = rp.ClientCode,
                                 ModifiedDate = rp.ModifiedDate,
                                 ModifiedBy = rp.ModifiedBy,
                                 ShowModifiedBy = user.FirstName + " " + user.LastName
                             };

                if (policy.FirstOrDefault() != null)
                    return policy.FirstOrDefault();
                else
                    return new RailPolicyModel();
            }
        }
    }
}