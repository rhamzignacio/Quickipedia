using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Data.Entity;

namespace Quickipedia.Services
{
    public class AirService
    {
        public static void SaveOtherAir(OtherAirModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var other = db.OtherAir.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(other != null)//UPDATE
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

                        OtherAir newOther = new OtherAir
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

        public static void SaveTravelSecurity(TravelSecurityModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var travSecurity = db.TravelSecurity.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(travSecurity != null)//UPDATE
                    {
                        message = "Updated";

                        travSecurity.TravelSecurity1 = model.TravelSecurity;

                        travSecurity.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        travSecurity.ModifiedDate = DateTime.Now;

                        db.Entry(travSecurity).State = EntityState.Modified;
                    }
                    else//NEW
                    {
                        message = "Saved";

                        TravelSecurity newTravSecurity = new TravelSecurity
                        {
                            ID = Guid.NewGuid(),
                            TravelSecurity1 = model.TravelSecurity,
                            ClientCode = UniversalHelpers.SelectedClient,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                            
                        };

                        db.Entry(newTravSecurity).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void SaveDisallowedAirlines(PreferredAirlineModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var temp = db.DisallowedAirlines.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient && r.AirlineCode == model.AirlineCode);

                    if (temp != null)
                    {
                        message = "Updated";

                        if (model.Status == "X")//DELETE
                        {
                            db.Entry(temp).State = EntityState.Deleted;
                        }
                        else
                        {
                            temp.CorporateCodes = model.CorporateCodes;

                            temp.ContractEnd = model.ContractEnd;

                            temp.ContractStart = model.ContractStart;

                            db.Entry(temp).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        message = "Saved";

                        DisallowedAirlines newDisallowedAirline = new DisallowedAirlines
                        {
                            ID = Guid.NewGuid(),
                            AirlineCode = model.AirlineCode,
                            ClientCode = UniversalHelpers.SelectedClient,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now,
                            CorporateCodes = model.CorporateCodes,
                            ContractEnd = model.ContractEnd,
                            ContractStart = model.ContractStart
                        };

                        db.Entry(newDisallowedAirline).State = EntityState.Added;
                    }


                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void SavePreferredAirlines(PreferredAirlineModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var temp = db.PreferredAirlines.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient && r.AirlineCode == model.AirlineCode);

                    if (temp != null)
                    {
                        message = "Updated";

                        if (model.Status == "X")//DELETE
                        {
                            db.Entry(temp).State = EntityState.Deleted;
                        }
                        else
                        {
                            temp.AirlineCode = model.AirlineCode;

                            temp.CorporateCodes = model.CorporateCodes;

                            temp.ContractStart = model.ContractStart;

                            temp.ContractEnd = model.ContractEnd;

                            db.Entry(temp).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        message = "Saved";

                            PreferredAirlines newPreferred = new PreferredAirlines
                            {
                                ID = Guid.NewGuid(),
                                AirlineCode = model.AirlineCode,
                                ClientCode = UniversalHelpers.SelectedClient,
                                ModifiedDate = DateTime.Now,
                                ModifiedBy = UniversalHelpers.CurrentUser.ID,
                                CorporateCodes = model.CorporateCodes,
                                ContractEnd = model.ContractEnd,
                                ContractStart = model.ContractStart
                            };

                            db.Entry(newPreferred).State = EntityState.Added;
                        }
                   
                        db.SaveChanges();                   
                }

            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void SaveTravelPolicy(TravelPolicyModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var policy = db.TravelPolicy.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(policy != null)//UPDATE
                    {
                        message = "Updated";

                        policy.AdvancePurchase = model.AdvancePurchase;

                        policy.BaggageAllowance = model.BaggageAllowance;

                        policy.ClassOfService = model.ClassOfService;

                        policy.CompanionHCPPersonalTravel = model.CompanionHCPersonalTravel;

                        policy.FlightWindow = model.FlightWindow;

                        policy.GroupBookingPolicy = model.GroupBookingPolicy;

                        policy.LCCCondition = model.LCCCondition;

                        policy.LLF = model.LLF;

                        policy.SeatSelection = model.SeatSelection;

                        policy.TravelInsurance = model.TravelInsurance;

                        policy.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        policy.ModifiedDate = DateTime.Now;

                        db.Entry(policy).State = EntityState.Modified;
                    }
                    else//NEW
                    {
                        message = "Saved";

                        TravelPolicy newPolicy = new TravelPolicy
                        {
                            ID = Guid.NewGuid(),
                            AdvancePurchase = model.AdvancePurchase,
                            BaggageAllowance = model.BaggageAllowance,
                            ClassOfService = model.ClassOfService,
                            CompanionHCPPersonalTravel = model.CompanionHCPersonalTravel,
                            FlightWindow = model.FlightWindow,
                            GroupBookingPolicy = model.GroupBookingPolicy,
                            LCCCondition = model.LCCCondition,
                            LLF = model.LLF,
                            SeatSelection = model.SeatSelection,
                            TravelInsurance = model.TravelInsurance,
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

        public static void SaveTripAuthorizer(TripAuthorizerProcessModel model, out string message)
        {
            try
            {
                using (var db = new QuickipediaEntities())
                {
                    message = "";

                    var trip = db.TripAuthorizerProcess.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(trip != null)//UPDATE
                    {
                        message = "Updated";

                        trip.TripAuthorizer = model.TripAuthorizer;

                        trip.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        trip.ModifiedDate = DateTime.Now;

                        db.Entry(trip).State = EntityState.Modified;
                    }
                    else//NEW
                    {
                        message = "Saved";

                        TripAuthorizerProcess newTrip = new TripAuthorizerProcess
                        {
                            ID = Guid.NewGuid(),
                            TripAuthorizer = model.TripAuthorizer,
                            ClientCode = UniversalHelpers.SelectedClient,
                            ModifiedDate = DateTime.Now,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID
                        };

                        db.Entry(newTrip).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void SaveSLA(SLAModel slaModel, out string message)
        {
            try
            {
                using (var db = new QuickipediaEntities())
                {
                    message = "";

                    var sla = db.SLA.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(sla != null)//UPDATE
                    {
                        message = "Updated";

                        sla.NoOfQuotationOptions = slaModel.NoOfQuotationOptions;

                        sla.QuotationFormat = slaModel.QuotationFormat;

                        sla.ResponseTime = slaModel.ResponseTime;

                        sla.FareAudit = slaModel.FareAudit;

                        sla.AuthorityToTicket = slaModel.AuthorityToTicket;

                        sla.AOHReminder = slaModel.AOHReminder;

                        sla.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        sla.ModifiedDate = DateTime.Now;

                        sla.QuotationFormat_Other = slaModel.QuotationFormat_Other;

                        sla.Remarks = slaModel.Remarks;

                        db.Entry(sla).State = EntityState.Modified;
                    }
                    else//NEW
                    {
                        message = "Saved";

                        SLA newSLA = new SLA
                        {
                            ID = Guid.NewGuid(),
                            ClientCode = UniversalHelpers.SelectedClient,
                            NoOfQuotationOptions = slaModel.NoOfQuotationOptions,
                            QuotationFormat = slaModel.QuotationFormat,
                            ResponseTime = slaModel.ResponseTime,
                            FareAudit = slaModel.FareAudit,
                            AuthorityToTicket = slaModel.AuthorityToTicket,
                            AOHReminder = slaModel.AOHReminder,
                            ModifiedDate = DateTime.Now,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            QuotationFormat_Other = slaModel.QuotationFormat_Other,
                            Remarks = slaModel.Remarks
                        };

                        db.Entry(newSLA).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static OtherAirModel GetOtherAir()
        {
            using (var db = new QuickipediaEntities())
            {
                var other = from o in db.OtherAir
                            join u in db.UserAccount on o.ModifiedBy equals u.ID into qU
                            from user in qU.DefaultIfEmpty()
                            where o.ClientCode == UniversalHelpers.SelectedClient
                            select new OtherAirModel
                            {
                                ID = o.ID,
                                ClientCode = o.ClientCode,
                                ModifiedBy = o.ModifiedBy,
                                ShowModifiedBy = user.FirstName + " " + user.LastName,
                                ModifiedDate = o.ModifiedDate,
                                Value = o.Value
                            };

                if (other.FirstOrDefault() != null)
                    return other.FirstOrDefault();
                else
                    return new OtherAirModel();
            }
        }

        public static TravelSecurityModel GetTravelSecurity(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var trav = from ts in db.TravelSecurity
                           join u in db.UserAccount on ts.ModifiedBy equals u.ID into qU
                           from user in qU.DefaultIfEmpty()
                           where ts.ClientCode == clientCode
                           select new TravelSecurityModel
                           {
                               ID = ts.ID,
                               TravelSecurity = ts.TravelSecurity1,
                               ClientCode = ts.ClientCode,
                               ModifiedBy = ts.ModifiedBy,
                               ModifiedDate = ts.ModifiedDate,
                               ShowModifiedBy = user.FirstName + " " + user.LastName
                           };

                if (trav.FirstOrDefault() != null)
                    return trav.FirstOrDefault();
                else
                    return new TravelSecurityModel();
            }
        }

        public static List<PreferredAirlineModel> GetDisallowedAirlines(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var air = from da in db.DisallowedAirlines
                          join a in db.Airlines on da.AirlineCode equals a.AirlineCode
                          join u in db.UserAccount on da.ModifiedBy equals u.ID into qU
                          from user in qU.DefaultIfEmpty()
                          where da.ClientCode == clientCode
                          select new PreferredAirlineModel
                          {
                              ID = da.ID,
                              AirlineCode = da.AirlineCode,
                              ClientCode = da.ClientCode,
                              AirlineName = a.AirlineName,
                              AirlineNo = a.AirlineNo,
                              Status = "Y",
                              ModifiedBy = da.ModifiedBy,
                              ModifiedDate = da.ModifiedDate,
                              ShowModifiedBy = user.FirstName + " " + user.LastName,
                              CorporateCodes = da.CorporateCodes,
                              ContractStart = da.ContractStart,
                              ContractEnd = da.ContractEnd
                          };

                if (air.ToList() != null)
                    return air.ToList();
                else
                    return new List<PreferredAirlineModel>();
            }
        }
        public static List<PreferredAirlineModel> GetPrefferedAirlines(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var air = from pa in db.PreferredAirlines
                          join a in db.Airlines on pa.AirlineCode equals a.AirlineCode
                          join u in db.UserAccount on pa.ModifiedBy equals u.ID into qU
                          from user in qU.DefaultIfEmpty()
                          where pa.ClientCode == clientCode
                          select new PreferredAirlineModel
                          {
                              ID = pa.ID,
                              AirlineCode = pa.AirlineCode,
                              AirlineName = a.AirlineName,
                              ClientCode = pa.ClientCode,
                              AirlineNo = a.AirlineNo,
                              Status = "Y",
                              ModifiedBy = pa.ModifiedBy,
                              ModifiedDate = pa.ModifiedDate,
                              ShowModifiedBy = user.FirstName + " " +user.LastName,
                              ContractEnd = pa.ContractEnd,
                              ContractStart = pa.ContractStart,
                              CorporateCodes = pa.CorporateCodes
                          };

                if (air.ToList() != null)
                    return air.ToList();
                else
                    return new List<PreferredAirlineModel>();
            }
        }

        public static TravelPolicyModel GetTravelPolicy (string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var trav = from t in db.TravelPolicy
                           join c in db.ClientProfile on t.ClientCode equals c.ClientCode
                           join u in db.UserAccount on t.ModifiedBy equals u.ID into qU
                           from user in qU.DefaultIfEmpty()
                           where t.ClientCode == clientCode
                           select new TravelPolicyModel
                           {
                               ID = t.ID,
                               ClientCode = t.ClientCode,
                               ClientName = c.ClientName,
                               LLF = t.LLF,
                               ClassOfService = t.ClassOfService,
                               FlightWindow = t.FlightWindow,
                               AdvancePurchase = t.AdvancePurchase,
                               LCCCondition = t.LCCCondition,
                               SeatSelection = t.SeatSelection,
                               BaggageAllowance = t.BaggageAllowance,
                               GroupBookingPolicy = t.GroupBookingPolicy,
                               CompanionHCPersonalTravel = t.CompanionHCPPersonalTravel,
                               TravelInsurance = t.TravelInsurance,
                               ModifiedBy = t.ModifiedBy,
                               ModifiedDate = t.ModifiedDate,
                               ShowModifiedBy = user.FirstName + " " + user.LastName
                           };

                if (trav.FirstOrDefault() != null)
                    return trav.FirstOrDefault();
                else
                    return new TravelPolicyModel();
            }
        }

        public static TripAuthorizerProcessModel GetTripAuthorizerProcess(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var trip = from t in db.TripAuthorizerProcess
                           join c in db.ClientProfile on t.ClientCode equals c.ClientCode
                           join u in db.UserAccount on t.ModifiedBy equals u.ID into qU
                           from user in qU.DefaultIfEmpty()
                           where t.ClientCode == clientCode
                           select new TripAuthorizerProcessModel
                           {
                               ID = t.ID,
                               ClientCode = t.ClientCode,
                               ClientName = c.ClientName,
                               TripAuthorizer = t.TripAuthorizer,
                               ModifiedBy = t.ModifiedBy,
                               ModifiedDate = t.ModifiedDate,
                               ShowModifiedBy = user.FirstName + " " + user.LastName
                           };

                if (trip.FirstOrDefault() != null)
                    return trip.FirstOrDefault();
                else
                    return new TripAuthorizerProcessModel();
            }
        }

        public static SLAModel GetSLA(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var sla = from s in db.SLA
                          join c in db.ClientProfile on s.ClientCode equals c.ClientCode
                          join u in db.UserAccount on s.ModifiedBy equals u.ID into qU
                          from user in qU.DefaultIfEmpty()
                          where s.ClientCode == clientCode
                          select new SLAModel
                          {
                              ID = s.ID,
                              ClientCode = s.ClientCode,
                              ClientName = c.ClientName,
                              ResponseTime = s.ResponseTime,
                              NoOfQuotationOptions = s.NoOfQuotationOptions,
                              QuotationFormat = s.QuotationFormat,
                              AuthorityToTicket = s.AuthorityToTicket,
                              FareAudit = s.FareAudit,
                              AOHReminder = s.AOHReminder,
                              ModifiedBy = s.ModifiedBy,
                              ModifiedDate = s.ModifiedDate,
                              ShowModifiedBy = user.FirstName + " " + user.LastName,
                              QuotationFormat_Other = s.QuotationFormat_Other,
                              Remarks = s.Remarks
                          };

                if (sla.FirstOrDefault() != null)
                    return sla.FirstOrDefault();
                else
                    return new SLAModel();
            }
        }

        public static List<Airlines> GetAllAirlines()
        {
            using (var db = new QuickipediaEntities())
            {
                return db.Airlines.Where(r => r.AirlineCode != "").ToList();
            }
        }
    }
}