using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Data.Entity;

namespace Quickipedia.Services
{
    public class VIPService
    {
        public static void SaveVIPList(VIPModel model, out string message, out Guid ID)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var vip = db.VIP.FirstOrDefault(r=>r.ID == model.ID);

                    if(vip != null)
                    {
                        if (model.Status != "X")
                        {
                            message = "Updated";

                            vip.VIPList = model.VIPList;

                            vip.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                            vip.ModifiedDate = DateTime.Now;

                            vip.Name = model.Name;

                            vip.Position = model.Position;

                            vip.Remarks = model.Remarks;

                            vip.Email = model.Email;

                            vip.ContactNo = model.ContactNo;                        

                            ID = vip.ID;

                            db.Entry(vip).State = EntityState.Modified;
                        }
                        else
                        {
                            message = "Deleted";

                            ID = vip.ID;

                            db.Entry(vip).State = EntityState.Deleted;
                        }
                    }
                    else
                    {
                        message = "Saved";

                        VIP newVIP = new VIP
                        {
                            ID = Guid.NewGuid(),
                            VIPList = model.VIPList,
                            ClientCode = UniversalHelpers.SelectedClient,
                            ModifiedDate = DateTime.Now,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ContactNo = model.ContactNo,
                            Email = model.Email,
                            Name = model.Name,
                            Position = model.Position,
                            Remarks = model.Remarks
                        };

                        ID = newVIP.ID;

                        db.Entry(newVIP).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                ID = Guid.Empty;
            }
        }

        public static List<VIPModel> GetVIPList(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var vip = from vl in db.VIP
                          join u in db.UserAccount on vl.ModifiedBy equals u.ID into qU
                          from user in qU.DefaultIfEmpty()
                          where vl.ClientCode == clientCode
                          select new VIPModel
                          {
                              ID = vl.ID,
                              ClientCode = vl.ClientCode,
                              VIPList = vl.VIPList,
                              ModifiedBy = vl.ModifiedBy,
                              ModifiedDate = vl.ModifiedDate,
                              ShowModifiedBy = user.FirstName + " " + user.LastName,
                              ContactNo = vl.ContactNo,
                              Email = vl.Email,
                              Name = vl.Name,
                              Position = vl.Position,
                              Remarks = vl.Remarks,
                              Status = "Y"
                          };

                    return vip.ToList();
            }
        }
    }
}