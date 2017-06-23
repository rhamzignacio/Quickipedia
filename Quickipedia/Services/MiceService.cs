using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Data.Entity;
using System.IO;

namespace Quickipedia.Services
{
    public class MiceService
    {
        public static void SaveMiceLinks (MiceLinksModel item, out string message)
        {
            try
            {
                message = "Saved";

                using (var db = new QuickipediaEntities())
                {
                    if(item.Status == "X")
                    {
                        var link = db.MiceProgramLink.FirstOrDefault(r => r.ID == item.ID);

                        if (link != null)
                            db.Entry(link).State = EntityState.Deleted;
                    }
                    else if(item.Status == "U")
                    {
                        var link = db.MiceProgramLink.FirstOrDefault(r => r.ID == item.ID);

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
                        MiceProgramLink newLink = new MiceProgramLink
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

        public static List<MiceLinksModel> GetLinks(out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var query = from l in db.MiceProgramLink
                                join u in db.UserAccount on l.ModifiedBy equals u.ID into qU
                                from user in qU.DefaultIfEmpty()
                                where l.ClientCode == UniversalHelpers.SelectedClient
                                select new MiceLinksModel
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

        public static List<MiceAttachmentModel> GetAttachments(out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var query = from a in db.MiceProgramAttachment
                                join u in db.UserAccount on a.ModifiedBy equals u.ID into qU
                                from user in qU.DefaultIfEmpty()
                                where a.ClientCode == UniversalHelpers.SelectedClient
                                select new MiceAttachmentModel
                                {
                                    ID = a.ID,
                                    ClientCode = a.ClientCode,
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

        public static void UpdateAttachment (MiceAttachmentModel item, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    if(item.Status == "X")
                    {
                        var file = db.MiceProgramAttachment.FirstOrDefault(r => r.ID == item.ID);

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

        public static MiceOtherModel GetOther()
        {
            try
            {

                using (var db = new QuickipediaEntities())
                {
                    var other = from o in db.OtherMice
                                join u in db.UserAccount on o.ModifiedBy equals u.ID into qU
                                from user in qU.DefaultIfEmpty()
                                where o.ClientCode == UniversalHelpers.SelectedClient
                                select new MiceOtherModel
                                {
                                    ID = o.ID,
                                    ClientCode = o.ClientCode,
                                    ModifiedBy = o.ModifiedBy,
                                    ModifiedDate = o.ModifiedDate,
                                    Value = o.Value,
                                    ShowModifiedBy = user.FirstName + " " + user.LastName
                                };

                    if (other.FirstOrDefault() != null)
                        return other.FirstOrDefault();
                    else
                        return new MiceOtherModel();
                }
            }
            catch (Exception error)
            {
                return new MiceOtherModel();
            }
        }

        public  static void SaveOther(MiceOtherModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var other = db.OtherMice.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if (other != null)
                    {
                        message = "Updated";

                        other.Value = model.Value;

                        other.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        other.ModifiedDate = DateTime.Now;

                        db.Entry(other).State = EntityState.Modified;
                    }
                    else
                    {
                        message = "Saved";

                        OtherMice newOther = new OtherMice
                        {
                            ID = Guid.NewGuid(),
                            Value = model.Value,
                            ClientCode = UniversalHelpers.SelectedClient,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        db.Entry(newOther).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
        }

        public static void SaveTicketing(MiceTicketApprovalModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var tkt = db.MICETicketingApproval.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(tkt != null)
                    {
                        message = "Updated";

                        tkt.TicketingApproval = model.TicketingApproval;

                        tkt.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        tkt.ModifiedDate = DateTime.Now;

                        db.Entry(tkt).State = EntityState.Modified;
                    }
                    else
                    {
                        message = "Saved";

                        MICETicketingApproval newTkt = new MICETicketingApproval
                        {
                            ID = Guid.NewGuid(),
                            TicketingApproval = model.TicketingApproval,
                            ClientCode = UniversalHelpers.SelectedClient,
                            ModifiedDate = DateTime.Now,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID
                        };

                        db.Entry(newTkt).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void SavePricing(MicePricingModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var pricing = db.MICEPricing.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(pricing != null)
                    {
                        message = "Updated";

                        pricing.Pricing = model.Pricing;

                        pricing.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        pricing.ModifiedDate = DateTime.Now;

                        db.Entry(pricing).State = EntityState.Modified;
                    }
                    else
                    {
                        message = "Saved";

                        MICEPricing newPricing = new MICEPricing
                        {
                            ID = Guid.NewGuid(),
                            Pricing = model.Pricing,
                            ClientCode = UniversalHelpers.SelectedClient,
                            ModifiedDate = DateTime.Now,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID
                        };

                        db.Entry(newPricing).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }

            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void SaveGroupBooking (GroupBookingPolicyModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var group = db.MICEGroupBookingPolicy.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(group != null)
                    {
                        message = "Updated";

                        group.GroupPolicy = model.GroupPolicy;

                        group.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        group.ModifiedDate = DateTime.Now;

                        db.Entry(group).State = EntityState.Modified;
                    }
                    else
                    {
                        message = "Saved";

                        MICEGroupBookingPolicy newGroup = new MICEGroupBookingPolicy
                        {
                            ID = Guid.NewGuid(),
                            GroupPolicy = model.GroupPolicy,
                            ClientCode = UniversalHelpers.SelectedClient,
                            ModifiedDate= DateTime.Now,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID
                        };

                        db.Entry(newGroup).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void SaveSMMP(SMMPForMSDModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var smmp = db.SMMPForMSD.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(smmp != null)
                    {
                        message = "Updated";

                        smmp.SMMPForMSD1 = model.SMMPForMSD;

                        smmp.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        smmp.ModifiedDate = DateTime.Now;

                        db.Entry(smmp).State = EntityState.Modified;
                    }
                    else
                    {
                        message = "Saved";

                        SMMPForMSD newSmmp = new SMMPForMSD
                        {
                            ID = Guid.NewGuid(),
                            SMMPForMSD1 = model.SMMPForMSD,
                            ClientCode = UniversalHelpers.SelectedClient,
                            ModifiedDate = DateTime.Now,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID
                        };

                        db.Entry(newSmmp).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void SaveMicePolicy(MicePolicyModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var policy = db.MICEPolicy.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

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

                        MICEPolicy newPolicy = new MICEPolicy
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

        public static MiceTicketApprovalModel GetTicketingApproval(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var ticket = from ta in db.MICETicketingApproval
                             join u in db.UserAccount on ta.ModifiedBy equals u.ID into qU
                             from user in qU.DefaultIfEmpty()
                             where ta.ClientCode == clientCode
                             select new MiceTicketApprovalModel
                             {
                                 ID = ta.ID,
                                 ClientCode = ta.ClientCode,
                                 TicketingApproval = ta.TicketingApproval,
                                 ModifiedBy = ta.ModifiedBy,
                                 ModifiedDate = ta.ModifiedDate,
                                 ShowModifiedBy = user.FirstName + " " + user.LastName
                             };

                if (ticket.FirstOrDefault() != null)
                    return ticket.FirstOrDefault();
                else
                    return new MiceTicketApprovalModel();
            }
        }

        public static MicePricingModel GetPricing(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var pricing = from mp in db.MICEPricing
                              join u in db.UserAccount on mp.ModifiedBy equals u.ID into qU
                              from user in qU.DefaultIfEmpty()
                              where mp.ClientCode == clientCode
                              select new MicePricingModel
                              {
                                  ID = mp.ID,
                                  ClientCode = mp.ClientCode,
                                  Pricing = mp.Pricing,
                                  ModifiedDate = mp.ModifiedDate,
                                  ModifiedBy = mp.ModifiedBy,
                                  ShowModifiedBy = user.FirstName + " " + user.LastName
                              };

                if (pricing.FirstOrDefault() != null)
                    return pricing.FirstOrDefault();
                else
                    return new MicePricingModel();
            }
        }

        public static GroupBookingPolicyModel GetGroupBookingPolicy(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var group = from gp in db.MICEGroupBookingPolicy
                            join u in db.UserAccount on gp.ModifiedBy equals u.ID into qU
                            from user in qU.DefaultIfEmpty()
                            where gp.ClientCode == clientCode
                            select new GroupBookingPolicyModel
                            {
                                ID = gp.ID,
                                ClientCode = gp.ClientCode,
                                GroupPolicy = gp.GroupPolicy,
                                ModifiedBy = gp.ModifiedBy,
                                ModifiedDate = gp.ModifiedDate,
                                ShowModifiedBy = user.FirstName + " " + user.LastName
                            };

                if (group.FirstOrDefault() != null)
                    return group.FirstOrDefault();
                else
                    return new GroupBookingPolicyModel();
            }
        }

        public static SMMPForMSDModel GetSMMPForMSD(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var smmp = from sm in db.SMMPForMSD
                           join u in db.UserAccount on sm.ModifiedBy equals u.ID into qU
                           from user in qU.DefaultIfEmpty()
                           where sm.ClientCode == clientCode
                           select new SMMPForMSDModel
                           {
                               ID = sm.ID,
                               ClientCode = sm.ClientCode,
                               SMMPForMSD = sm.SMMPForMSD1,
                               ModifiedDate = sm.ModifiedDate,
                               ModifiedBy = sm.ModifiedBy,
                               ShowModifiedBy = user.FirstName + " " + user.LastName
                           };

                if (smmp.FirstOrDefault() != null)
                    return smmp.FirstOrDefault();
                else
                    return new SMMPForMSDModel();
            }
        }

        public static MicePolicyModel GetMicePolicy(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var policy = from mp in db.MICEPolicy
                             join u in db.UserAccount on mp.ModifiedBy equals u.ID into qU
                             from user in qU.DefaultIfEmpty()
                             where mp.ClientCode == clientCode
                             select new MicePolicyModel
                             {
                                 ID = mp.ID,
                                 ClientCode = mp.ClientCode,
                                 Policy = mp.Policy,
                                 ModifiedBy = mp.ModifiedBy,
                                 ModifiedDate = mp.ModifiedDate,
                                 ShowModifiedBy = user.FirstName + " " + user.LastName
                             };

                if (policy.FirstOrDefault() != null)
                    return policy.FirstOrDefault();
                else
                    return new MicePolicyModel();
            }
        }
    }
}