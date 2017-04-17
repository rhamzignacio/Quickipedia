using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Data.Entity;

namespace Quickipedia.Services
{
    public class ProfileManagementService
    {
        public static ProfileManagementModel GetProfileManagement()
        {
            using (var db = new QuickipediaEntities())
            {
                var profile = from p in db.ProfileManagement
                              join u in db.UserAccount on p.ModifiedBy equals u.ID into qU
                              from user in qU.DefaultIfEmpty()
                              where p.ClientCode == UniversalHelpers.SelectedClient
                              select new ProfileManagementModel
                              {
                                  ID = p.ID,
                                  ClientCode = p.ClientCode,
                                  ProfileType = p.ProfileType,
                                  OtherProfileType = p.OtherProfileType,
                                  SpecialInstruction = p.SpecialInstructions,
                                  ModifiedDate = p.ModifiedDate,
                                  ModifiedBy = p.ModifiedBy,
                                  ShowModifiedBy = user.FirstName + " " + user.LastName,
                                  BookingType = p.BookingType,
                                  OtherBookingType = p.OtherBookingType
                              };

                if (profile.FirstOrDefault() != null)
                    return profile.FirstOrDefault();
                else
                    return new ProfileManagementModel();
            }
        }

        public static void SaveProfileManagement(ProfileManagementModel model, out string message)
        {
            try
            {
                message = "";

                using(var db = new QuickipediaEntities())
                {
                    var profile = db.ProfileManagement.FirstOrDefault(r => r.ClientCode == UniversalHelpers.SelectedClient);

                    if(profile != null) //UPDATE
                    {
                        message = "Updated";

                        profile.ProfileType = model.ProfileType;

                        profile.OtherProfileType = model.OtherProfileType;

                        profile.SpecialInstructions = model.SpecialInstruction;

                        profile.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                        profile.ModifiedDate = DateTime.Now;

                        profile.BookingType = model.BookingType;

                        profile.OtherBookingType = model.OtherBookingType;

                        db.Entry(profile).State = EntityState.Modified;
                    }
                    else
                    {
                        message = "Saved";

                        ProfileManagement newProfile = new ProfileManagement
                        {
                            ID = Guid.NewGuid(),
                            ProfileType = model.ProfileType,
                            ClientCode = UniversalHelpers.SelectedClient,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now,
                            OtherProfileType = model.OtherProfileType,
                            SpecialInstructions = model.SpecialInstruction,
                            OtherBookingType = model.OtherBookingType
                        };

                        db.Entry(newProfile).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }
    }
}