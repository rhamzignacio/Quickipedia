using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Data.Entity;

namespace Quickipedia.Services
{
    public class ClientBookerApproverService
    {
        public static ClientBookerApproverModel GetClientBooker()
        {
            using (var db = new QuickipediaEntities())
            {
                var booker = from c in db.ClientBookerApprover
                             join u in db.UserAccount on c.ModifiedBy equals u.ID into qU
                             from user in qU.DefaultIfEmpty()
                             where c.ClientCode == UniversalHelpers.SelectedClient
                             select new ClientBookerApproverModel
                             {
                                 ID = c.ID,
                                 ClientCode = c.ClientCode,
                                 Value = c.Value,
                                 ModifiedBy = c.ModifiedBy,
                                 ModifiedDate = c.ModifiedDate,
                                 ShowModifiedBy = user.FirstName + " " + user.LastName
                             };

                if (booker.FirstOrDefault() != null)
                    return booker.FirstOrDefault();
                else
                    return new ClientBookerApproverModel();
            }
        }

        public static void SaveClientBooker(ClientBookerApproverModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var booker = db.ClientBookerApprover.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(booker != null)//UPDATE
                    {
                        message = "Updated";

                        booker.Value = model.Value;

                        booker.ModifiedDate = DateTime.Now;

                        booker.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        db.Entry(booker).State = EntityState.Modified;
                    }
                    else//NEW
                    {
                        message = "Saved";

                        ClientBookerApprover newBooker = new ClientBookerApprover
                        {
                            ID = Guid.NewGuid(),
                            Value = model.Value,
                            ClientCode = UniversalHelpers.SelectedClient,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        db.Entry(newBooker).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
        }
    }
}