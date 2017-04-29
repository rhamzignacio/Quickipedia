using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Data.Entity;

namespace Quickipedia.Services
{
    public class AdvisoryService
    {
       
        public static List<AdvisoryModel> GetAdvisory(out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var query = from a in db.Advisory
                                join u in db.UserAccount on a.ModifiedBy equals u.ID into qU
                                from user in qU.DefaultIfEmpty()
                                select new AdvisoryModel
                                {
                                    ID = a.ID,
                                    Message = a.Message,
                                    ModifiedBy = a.ModifiedBy,
                                    ModifiedDate = a.ModifiedDate,
                                    ShowModifiedBy = user.FirstName + " " + user.LastName,
                                    Status = "Y",
                                    Title = a.Title
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

        public static void SaveAdvisory(AdvisoryModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    if(model.ID == Guid.Empty || model.ID == null)//NEW
                    {
                        message = "Saved";
                        Advisory newAd = new Advisory
                        {
                            ID = Guid.NewGuid(),
                            Title = model.Title,
                            Message = model.Message,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now,
                            Status = "Y"
                        };

                        db.Entry(newAd).State = EntityState.Added;
                    }
                    else
                    {
                        var advisory = db.Advisory.FirstOrDefault(r => r.ID == model.ID);

                        if(advisory != null)
                        {
                            message = "Updated";

                            if (model.Status == "X")
                                db.Entry(advisory).State = EntityState.Deleted;
                            else
                            {
                                advisory.Message = model.Message;

                                advisory.Title = model.Title;

                                advisory.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                                advisory.ModifiedDate = DateTime.Now;

                                db.Entry(advisory).State = EntityState.Modified;
                            }
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }//SaveAdvisory
    }
}