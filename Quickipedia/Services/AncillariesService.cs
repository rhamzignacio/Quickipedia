using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Data.Entity;

namespace Quickipedia.Services
{
    public class AncillariesService
    {
        public static void SaveAncillaries(AncillariesModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    if(model.ID == null || model.ID == Guid.Empty)
                    {
                        AncillariesFees newAnci = new AncillariesFees
                        {
                            ID = Guid.NewGuid(),
                            Category = model.Category,
                            Description = model.Description,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now,
                            PHPAmount = model.PHPAmount,
                            USDAmount = model.USDAmount,
                        };

                        db.Entry(newAnci).State = EntityState.Added;

                        message = "Saved";
                    }
                    else
                    {
                        var anci = db.AncillariesFees.FirstOrDefault(r => r.ID == model.ID);

                        message = "Updated";

                        if (anci != null) {
                            if (model.Status == "U")
                            {
                                anci.Description = model.Description;

                                anci.Category = model.Category;

                                anci.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                                anci.ModifiedDate = DateTime.Now;

                                anci.PHPAmount = model.PHPAmount;

                                anci.USDAmount = model.USDAmount;

                                db.Entry(anci).State = EntityState.Modified;
                            }
                            else if (model.Status == "X")
                            {
                                db.Entry(anci).State = EntityState.Deleted;
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
        }

        public static List<AncillariesModel> GetAncillaries(out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var ancillaries = from a in db.AncillariesFees
                                      join u in db.UserAccount on a.ModifiedBy equals u.ID into qU
                                      from user in qU.DefaultIfEmpty()
                                      where a.ID != null
                                      select new AncillariesModel
                                      {
                                          ID = a.ID,
                                          Description = a.Description,
                                          Category = a.Category,
                                          PHPAmount = a.PHPAmount,
                                          USDAmount = a.USDAmount,
                                          ModifiedDate = a.ModifiedDate,
                                          ModifiedBy = a.ModifiedBy,
                                          ShowModifiedBy = user.FirstName + " " +user.LastName,
                                          Status = "Y"
                                      };

                    if (ancillaries.ToList() != null)
                        return ancillaries.ToList();
                    else
                        return new List<AncillariesModel>();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return new List<AncillariesModel>();
            }
        }
    }
}