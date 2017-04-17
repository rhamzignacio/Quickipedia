using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Data.Entity;

namespace Quickipedia.Services
{
    public class MiceService
    {
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