using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Data.Entity;

namespace Quickipedia.Services
{
    public class BasicInfoService
    {
        public static List<TravelConsultantModel> GetTCDropdown(string type)
        {
            using (var db = new QuickipediaEntities())
            {
                if(type == "INTL")
                {
                    var tc = from u in db.UserAccount
                             where u.Type == "I" || u.Type == "B"
                             orderby u.FirstName ascending
                             select new TravelConsultantModel
                             {
                                 AgentName = u.FirstName + " " + u.LastName,
                                 AgentID = u.ID
                             };

                    return tc.ToList();
                }
                else if(type == "DOM")
                {
                    var tc = from u in db.UserAccount
                             where u.Type == "D" || u.Type == "B"
                             orderby u.FirstName ascending
                             select new TravelConsultantModel
                             {
                                 AgentName = u.FirstName + " " + u.LastName,
                                 AgentID = u.ID
                             };


                    return tc.ToList();
                }
                else if(type == "ACCTG")
                {
                    var acctg = from u in db.UserAccount
                                where u.Type == "AB"
                                orderby u.FirstName ascending
                                select new TravelConsultantModel
                                {
                                    AgentName = u.FirstName + " " + u.LastName,
                                    AgentID = u.ID
                                };

                    return acctg.ToList();
                }

                return null;
            }
        }

        public static bool SaveBasicInfo(BasicInfoModel basicInfo, string type, out string errorMessage)
        {
            try
            {
                using (var db = new QuickipediaEntities())
                {
                    errorMessage = "";

                    var ifExist = db.ClientProfile.FirstOrDefault(r => r.ClientCode == basicInfo.ClientCode);

                    if (ifExist != null && type == "NewClient")
                    {
                        errorMessage = "Client Code already used";

                        return false;
                    }
                    else
                    {
                        if (type == "NewClient")
                        {
                            //=======CLIENT PROFILE===========
                            ClientProfile newClientProfile = new ClientProfile
                            {
                                ID = Guid.NewGuid(),
                                ClientCode = basicInfo.ClientCode,
                                ClientType = basicInfo.ClientType,
                                ClientName = basicInfo.ClientName,
                                Address = basicInfo.Address,
                                Biller = basicInfo.Biller,
                                AccountOfficer = basicInfo.AccountOfficerManager,
                                ModifiedBy = UniversalHelpers.CurrentUser.ID,
                                ModifiedDate  = DateTime.Now,
                                BillerID = basicInfo.BillerID,
                                ContractEndDate = basicInfo.ContractEndDate,
                                ContractStartDate = basicInfo.ContractStartDate
                            };

                            db.Entry(newClientProfile).State = EntityState.Added;
                        }
                        else
                        {
                            if(ifExist != null)
                            {
                                ifExist.ClientCode = basicInfo.ClientCode;

                                ifExist.ClientType = basicInfo.ClientType;

                                ifExist.ClientName = basicInfo.ClientName;

                                ifExist.Address = basicInfo.Address;

                                ifExist.Biller = basicInfo.Biller;

                                ifExist.AccountOfficer = basicInfo.AccountOfficerManager;

                                ifExist.ModifiedDate = DateTime.Now;

                                ifExist.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                                ifExist.BillerID = basicInfo.BillerID;

                                ifExist.ContractStartDate = basicInfo.ContractStartDate;

                                ifExist.ContractEndDate = basicInfo.ContractEndDate;

                                db.Entry(ifExist).State = EntityState.Modified;
                            }
                        }

                        //========INTERNATIONAL TC===============
                        if (basicInfo.InternationalTCs != null)
                        {
                            basicInfo.InternationalTCs.ForEach(item =>
                            {
                                if (item.ID == null || item.ID == Guid.Empty) //NEW
                            {
                                    ClientInternationalTC intlTC = new ClientInternationalTC
                                    {
                                        ID = Guid.NewGuid(),
                                        ClientCode = basicInfo.ClientCode,
                                        AgentID = item.AgentID,
                                    };

                                    db.Entry(intlTC).State = EntityState.Added;
                                }
                                else
                                {
                                    var intlTC = db.ClientInternationalTC.FirstOrDefault(r => r.ID == item.ID);

                                    if (intlTC != null)
                                    {
                                        if (item.Status == "U")//UPDATE
                                    {
                                            intlTC.AgentID = item.AgentID;

                                            db.Entry(intlTC).State = EntityState.Modified;
                                        }
                                        else if (item.Status == "X")//DELETE
                                    {
                                            db.Entry(intlTC).State = EntityState.Deleted;
                                        }
                                    }
                                }
                            });
                        }

                        //=========DOMESTIC TC=============
                        if (basicInfo.DomesticTCs != null)
                        {
                            basicInfo.DomesticTCs.ForEach(item =>
                            {
                                if (item.ID == null || item.ID == Guid.Empty)//NEW
                            {
                                    ClientDomesticTC domTC = new ClientDomesticTC
                                    {
                                        ID = Guid.NewGuid(),
                                        ClientCode = basicInfo.ClientCode,
                                        AgentID = item.AgentID
                                    };

                                    db.Entry(domTC).State = EntityState.Added;
                                }
                                else
                                {
                                    var domTC = db.ClientDomesticTC.FirstOrDefault(r => r.ID == item.ID);

                                    if (domTC != null)
                                    {
                                        if (item.Status == "U")//UPDATE
                                    {
                                            domTC.AgentID = item.AgentID;

                                            db.Entry(domTC).State = EntityState.Modified;
                                        }
                                        else if (item.Status == "X")//DELETE
                                    {
                                            db.Entry(domTC).State = EntityState.Deleted;
                                        }
                                    }
                                }
                            });
                        }

                        if (basicInfo.ClientContactPersons != null)
                        {
                            basicInfo.ClientContactPersons.ForEach(item =>
                            {
                                if (item.ID == null || item.ID == Guid.Empty)//NEW
                                {
                                    if (item.Status != "X")
                                    {
                                        ClientContactPerson newContact = new ClientContactPerson
                                        {
                                            ID = Guid.NewGuid(),
                                            ClientCode = basicInfo.ClientCode,
                                            ContactNo = item.ContactNo,
                                            Email = item.Email,
                                            Name = item.Name,
                                            Position = item.Position
                                        };

                                        db.Entry(newContact).State = EntityState.Added;
                                    }
                                }
                                else
                                {
                                    var contact = db.ClientContactPerson.FirstOrDefault(r => r.ID == item.ID);

                                    if (contact != null)
                                    {
                                        if (item.Status == "U")
                                        {
                                            contact.ContactNo = item.ContactNo;

                                            contact.Email = item.Email;

                                            contact.Name = item.Name;

                                            contact.Position = item.Position;

                                            db.Entry(contact).State = EntityState.Modified;
                                        }
                                        else if (item.Status == "X")
                                        {
                                            db.Entry(contact).State = EntityState.Deleted;
                                        }
                                    }
                                }
                            });
                        }

                        db.SaveChanges();

                        return true;
                    }
                }
            }
            catch(Exception error)
            {
                errorMessage = error.Message;

                return false;
            }
        } 

        public static BasicInfoModel GetBasicInfo(string clientCode)
        {
            BasicInfoModel basicInfo = new BasicInfoModel();

            try
            {
                using (var db = new QuickipediaEntities())
                {
                    var clientProfile = db.ClientProfile.FirstOrDefault(r => r.ClientCode == clientCode);

                    if (clientProfile != null)
                    {
                        //ClientProfile
                        basicInfo.ClientName = clientProfile.ClientName;

                        basicInfo.ClientType = clientProfile.ClientType;

                        basicInfo.ClientCode = clientProfile.ClientCode;

                        basicInfo.Biller = clientProfile.Biller;

                        basicInfo.Address = clientProfile.Address;

                        basicInfo.AccountOfficerManager = clientProfile.AccountOfficer;

                        basicInfo.ModifiedBy = clientProfile.ModifiedBy;

                        basicInfo.ModifiedDate = clientProfile.ModifiedDate;

                        basicInfo.BillerID = clientProfile.BillerID;

                        basicInfo.ContractEndDate = clientProfile.ContractEndDate;

                        basicInfo.ContractStartDate = clientProfile.ContractStartDate;

                        var userProf = db.UserAccount.FirstOrDefault(r => r.ID == clientProfile.ModifiedBy);

                        if (userProf != null)
                        {
                            basicInfo.ShowModifiedBy = userProf.FirstName + " " + userProf.LastName;
                        }
                        else
                            basicInfo.ShowModifiedBy = " ";

                        var internationalTC = from tc in db.ClientInternationalTC
                                              join user in db.UserAccount on tc.AgentID equals user.ID
                                              where tc.ClientCode == clientCode
                                              select new TravelConsultantModel
                                              {
                                                  ClientCode = tc.ClientCode,
                                                  ID = tc.ID,
                                                  AgentNo1A = user.AgentNo1A,
                                                  AgentNo1B = user.AgentNo1B,
                                                  AgentNo5J = user.AgentNo5J,
                                                  AgentID = tc.AgentID,
                                                  AgentName = user.FirstName + " " + user.LastName,
                                                  Status = "Y"
                                              };

                        basicInfo.InternationalTCs = internationalTC.ToList();

                        var domesticTC = from tc in db.ClientDomesticTC
                                         join user in db.UserAccount on tc.AgentID equals user.ID
                                         where tc.ClientCode == clientCode
                                         select new TravelConsultantModel
                                         {
                                             ClientCode = tc.ClientCode,
                                             ID = tc.ID,
                                             AgentNo1A = user.AgentNo1A,
                                             AgentNo1B = user.AgentNo1B,
                                             AgentNo5J = user.AgentNo5J,
                                             AgentID = tc.AgentID,
                                             AgentName = user.FirstName + " " + user.LastName,
                                             Status = "Y"
                                         };

                        basicInfo.DomesticTCs = domesticTC.ToList();

                        var clientContact = from cl in db.ClientContactPerson
                                            where cl.ClientCode == clientCode
                                            select new ClientContactPersonModel
                                            {
                                                ClientCode = cl.ClientCode,
                                                ContactNo = cl.ContactNo,
                                                Email = cl.Email,
                                                ID = cl.ID,
                                                Name = cl.Name,
                                                Position = cl.Position,
                                                Status = "Y"
                                            };

                        basicInfo.ClientContactPersons = clientContact.ToList();

                    }
                }
            }
            catch
            {
        
            }

            return basicInfo;
        }
    }
}