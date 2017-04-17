using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Data.Entity;

namespace Quickipedia.Services
{
    public class BookingProcessService
    {
        public static void SaveDOMProcess(DOMBookingProcessModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var dom = db.DomesticBookingProcess.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if (dom != null)//UPDATE
                    {
                        message = "Updated";

                        dom.Value = model.Value;

                        dom.ModifiedDate = DateTime.Now;

                        dom.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        db.Entry(dom).State = EntityState.Modified;
                    }
                    else
                    {
                        message = "Saved";

                        DomesticBookingProcess newDOM = new DomesticBookingProcess
                        {
                            ID = Guid.NewGuid(),
                            ClientCode = UniversalHelpers.SelectedClient,
                            Value = model.Value,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        db.Entry(newDOM).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
        }
        
        public static void SaveINTLProcess(INTLBookingProcessModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var intl = db.InternationalBookingProcess.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(intl != null)//UPDATE
                    {
                        message = "Updated";

                        intl.Value = model.Value;

                        intl.ModifiedDate = DateTime.Now;

                        intl.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        db.Entry(intl).State = EntityState.Modified;
                    }
                    else
                    {
                        message = "Saved";

                        InternationalBookingProcess newINTL = new InternationalBookingProcess
                        {
                            ID = Guid.NewGuid(),
                            ClientCode = UniversalHelpers.SelectedClient,
                            Value = model.Value,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        db.Entry(newINTL).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static INTLBookingProcessModel GetINTL()
        {
            using (var db = new QuickipediaEntities())
            {
                var intl = from i in db.InternationalBookingProcess
                           join u in db.UserAccount on i.ModifiedBy equals u.ID into qU
                           from user in qU.DefaultIfEmpty()
                           where i.ClientCode == UniversalHelpers.SelectedClient
                           select new INTLBookingProcessModel
                           {
                               ID = i.ID,
                               Value = i.Value,
                               ModifiedBy = i.ModifiedBy,
                               ShowModifiedBy = user.FirstName + " " + user.LastName,
                               ModifiedDate = i.ModifiedDate,
                               ClientCode = i.ClientCode
                           };

                if (intl.FirstOrDefault() != null)
                    return intl.FirstOrDefault();
                else
                    return new INTLBookingProcessModel();
            }
        }

        public static DOMBookingProcessModel GetDOM()
        {
            using (var db = new QuickipediaEntities())
            {
                var intl = from i in db.DomesticBookingProcess
                           join u in db.UserAccount on i.ModifiedBy equals u.ID into qU
                           from user in qU.DefaultIfEmpty()
                           where i.ClientCode == UniversalHelpers.SelectedClient
                           select new DOMBookingProcessModel
                           {
                               ID = i.ID,
                               Value = i.Value,
                               ModifiedBy = i.ModifiedBy,
                               ShowModifiedBy = user.FirstName + " " + user.LastName,
                               ModifiedDate = i.ModifiedDate,
                               ClientCode = i.ClientCode
                           };

                if (intl.FirstOrDefault() != null)
                    return intl.FirstOrDefault();
                else
                    return new DOMBookingProcessModel();
            }
        }
    }
}