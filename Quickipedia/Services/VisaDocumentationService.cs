using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Data.Entity;

namespace Quickipedia.Services
{
    public class VisaDocumentationService
    {
        public static void SaveVisaDocumentation(VisaDocumentationModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var visa = db.VisaAndDocumentation.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(visa != null) //UPDATE
                    {
                        message = "Updated";

                        visa.ProcessFlow = model.ProcessFlow;

                        visa.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        visa.ModifiedDate = DateTime.Now;

                        db.Entry(visa).State = EntityState.Modified;
                    }
                    else //NEW
                    {
                        message = "Saved";

                        VisaAndDocumentation newVisa = new VisaAndDocumentation
                        {
                            ID = Guid.NewGuid(),
                            ClientCode = UniversalHelpers.SelectedClient,
                            ProcessFlow = model.ProcessFlow,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        db.Entry(newVisa).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static VisaDocumentationModel GetVisaDocumentation(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var visa = from vd in db.VisaAndDocumentation
                           join u in db.UserAccount on vd.ModifiedBy equals u.ID into qU
                           from user in qU.DefaultIfEmpty()
                           where vd.ClientCode == clientCode
                           select new VisaDocumentationModel
                           {
                               ID = vd.ID,
                               ClientCode = vd.ClientCode,
                               ProcessFlow = vd.ProcessFlow,
                               ModifiedBy = vd.ModifiedBy,
                               ModifiedDate = vd.ModifiedDate,
                               ShowModifiedBy = user.FirstName + " " + user.LastName
                           };

                if (visa.FirstOrDefault() != null)
                    return visa.FirstOrDefault();
                else
                    return new VisaDocumentationModel();
            }
        }
    }
}