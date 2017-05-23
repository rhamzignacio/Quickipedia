using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Data.Entity;

namespace Quickipedia.Services
{
    public class CarService
    {
        public static void UpdateAttachments(CarAttachmentModel item, out string message)
        {
            try
            {
                message = "";
                using (var db = new QuickipediaEntities())
                {
                    if (item.Status == "X")
                    {
                        var file = db.CarProgramAttachment.FirstOrDefault(r => r.ID == item.ID);

                        if (file != null)
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

        public static List<CarAttachmentModel> GetAttachments(out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var query = from a in db.CarProgramAttachment
                                join u in db.UserAccount on a.ModifiedBy equals u.ID into qU
                                from user in qU.DefaultIfEmpty()
                                where a.ClientCode == UniversalHelpers.SelectedClient
                                select new CarAttachmentModel
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

        public static List<CarLinksModel> GetLinks(out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var query = from l in db.CarProgramLink
                                join u in db.UserAccount on l.ModifiedBy equals u.ID into qU
                                from user in qU.DefaultIfEmpty()
                                where l.ClientCode == UniversalHelpers.SelectedClient
                                select new CarLinksModel
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

        public static void SaveCarLinks(CarLinksModel item, out string message)
        {
            try
            {
                message = "Saved";

                using (var db = new QuickipediaEntities())
                {

                    if(item.Status == "X")
                    {
                        var link = db.CarProgramLink.FirstOrDefault(r => r.ID == item.ID);

                        if (link != null)
                            db.Entry(link).State = EntityState.Deleted;
                    }
                    else if(item.Status == "U")
                    {
                        var link = db.CarProgramLink.FirstOrDefault(r => r.ID == item.ID);

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
                        CarProgramLink newLink = new CarProgramLink
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
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void SaveCarPolicy(CarPolicyModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var policy = db.CarPolicy.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

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

                        CarPolicy newPolicy = new CarPolicy
                        {
                            ID = Guid.NewGuid(),
                            ClientCode = UniversalHelpers.SelectedClient,
                            Policy = model.Policy,
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

        public static void SaveCarCorporateCode(CarCorporateCodeModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var code = db.CarCorporateCode.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(code != null)//UPDATE
                    {
                        message = "Updated";

                        code.CorporateCode = model.CorporateCode;

                        code.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        code.ModifiedDate = DateTime.Now;

                        db.Entry(code).State = EntityState.Modified;
                    }
                    else//NEW
                    {
                        message = "Saved";

                        CarCorporateCode newCode = new CarCorporateCode
                        {
                            ID = Guid.NewGuid(),
                            CorporateCode = model.CorporateCode,
                            ClientCode = UniversalHelpers.SelectedClient,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        db.Entry(newCode).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static CarCorporateCodeModel GetCarCorporateCode(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var code = from cc in db.CarCorporateCode
                           join u in db.UserAccount on cc.ModifiedBy equals u.ID into qU
                           from user in qU.DefaultIfEmpty()
                           where cc.ClientCode == clientCode
                           select new CarCorporateCodeModel
                           {
                               ID = cc.ID,
                               ClientCode = cc.ClientCode,
                               CorporateCode = cc.CorporateCode,
                               ModifiedBy = cc.ModifiedBy,
                               ModifiedDate = cc.ModifiedDate,
                               ShowModifiedBy = user.FirstName + " " + user.LastName
                           };

                if (code.FirstOrDefault() != null)
                    return code.FirstOrDefault();
                else
                    return new CarCorporateCodeModel();
            }
        }

        public static CarPolicyModel GetCarPolicy(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var policy = from cp in db.CarPolicy
                             join u in db.UserAccount on cp.ModifiedBy equals u.ID into qU
                             from user in qU.DefaultIfEmpty()
                             where cp.ClientCode == clientCode
                             select new CarPolicyModel
                             {
                                 ID = cp.ID,
                                 Policy = cp.Policy,
                                 ClientCode = cp.ClientCode,
                                 ModifiedBy = cp.ModifiedBy,
                                 ModifiedDate = cp.ModifiedDate,
                                 ShowModifiedBy = user.FirstName + " " +user.LastName
                             };

                if (policy.FirstOrDefault() != null)
                    return policy.FirstOrDefault();
                else
                    return new CarPolicyModel();
            }
        }
    }
}