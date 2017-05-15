using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Data.Entity;

namespace Quickipedia.Services
{
    public class UserService
    {
        public static void ChangePassword(ChangePasswordModel _pass ,out string message)
        {
            message = "";

            try
            {
                using (var db = new QuickipediaEntities())
                {
                    var user = db.UserAccount.FirstOrDefault(r => r.ID == UniversalHelpers.CurrentUser.ID && r.Password == _pass.CurrentPassword);

                    if(user != null)
                    {
                        user.Password = _pass.NewPassword;

                        db.Entry(user).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                    else
                    {
                        message = "Incorrect Password";
                    }
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static List<UserModel> GetUsers(out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var users = from u in db.UserAccount
                                join m in db.UserAccount on u.ID equals m.ModifiedBy into qM
                                from mU in qM.DefaultIfEmpty()
                                where u.ID != Guid.Empty
                                select new UserModel
                                {
                                    ID = u.ID,
                                    AccessLevel = u.AccessLevel,
                                    AgentNo1A = u.AgentNo1A,
                                    AgentNo1B = u.AgentNo1B,
                                    AgentNo5J = u.AgentNo5J,
                                    FirstName = u.FirstName,
                                    LastName = u.LastName,
                                    MiddleInitial = u.MiddleInitial,
                                    ModifiedBy = u.ModifiedBy,
                                    ModifiedDate = u.ModifiedDate,
                                    ShowModifiedBy = mU.FirstName + " " + mU.LastName,
                                    Status = u.Status,
                                    Type = u.Type,
                                    Username = u.Username
                                };

                    return users.OrderBy(r=>r.Username).ToList();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static void SaveUser(UserModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new QuickipediaEntities())
                {
                    var user = db.UserAccount.FirstOrDefault(r => r.Username == model.Username);

                    if (user != null && (model.ID == Guid.Empty || model.ID == null))
                        message = "Username already exist";
                    else
                    {
                        message = "Saved";

                        if(model.ID == Guid.Empty || model.ID == null)//NEW
                        {
                            UserAccount newUser = new UserAccount
                            {
                                ID = Guid.NewGuid(),
                                AccessLevel = model.AccessLevel,
                                AgentNo1A = model.AgentNo1A,
                                AgentNo1B = model.AgentNo1B,
                                AgentNo5J = model.AgentNo5J,
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                MiddleInitial = model.MiddleInitial,
                                ModifiedBy = UniversalHelpers.CurrentUser.ID,
                                ModifiedDate = DateTime.Now,
                                Password = model.Password,
                                Status = "Y",
                                Type = model.Type,
                                Username = model.Username
                            };

                            db.Entry(newUser).State = EntityState.Added;
                        }
                        else //UPDATE
                        {
                            message = "Updated";

                            user.AccessLevel = model.AccessLevel;

                            user.AgentNo1A = model.AgentNo1A;

                            user.AgentNo1B = model.AgentNo1B;

                            user.AgentNo5J = model.AgentNo5J;

                            user.FirstName = model.FirstName;

                            user.LastName = model.LastName;

                            user.MiddleInitial = model.MiddleInitial;

                            user.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                            user.ModifiedDate = DateTime.Now;

                            user.Password = model.Password;

                            user.Status = model.Status;

                            user.Type = model.Type;

                            db.Entry(user).State = EntityState.Modified;
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

        public static List<ClientDropdownModel> GetClientDropDown()
        {
            using (var db = new QuickipediaEntities())
            {
                var user = UniversalHelpers.CurrentUser;

                //if(user.AccessLevel == "A")
                //{
                    var clients = from c in db.ClientProfile
                                  orderby c.ClientName ascending
                                  select new ClientDropdownModel
                                  {
                                      ClientCode = c.ClientCode,
                                      ClientName = c.ClientName
                                  };

                    return clients.ToList();
                //}
                //else
                //{
                //    var clients = from c in db.ClientProfile
                //                  join d in db.ClientDomesticTC on c.ClientCode equals d.ClientCode into qD
                //                  from dom in qD.DefaultIfEmpty()
                //                  join i in db.ClientInternationalTC on c.ClientCode equals i.ClientCode into qI
                //                  from intl in qI.DefaultIfEmpty()
                //                  where dom.AgentID == user.ID && intl.AgentID == user.ID
                //                  select new ClientDropdownModel
                //                  {
                //                      ClientCode = c.ClientCode,
                //                      ClientName = c.ClientName
                //                  };

                //    return clients.ToList();
                //}
            }
        }

        public static UserClientModel GetUserClients(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                var client = from c in db.ClientProfile
                             where c.ClientCode == clientCode
                             select new UserClientModel
                             {
                                 ClientCode = c.ClientCode,
                                 ClientName = c.ClientName
                             };

                return client.FirstOrDefault();
            }
        }

        public static UserModel ValidateUserLogin(string _username, string _password, out string returnString)
        {
            returnString = "";

            UserModel userModel = null;

            try
            {
                using (var db = new QuickipediaEntities())
                {
                    var user = db.UserAccount.FirstOrDefault(r => r.Username == _username);

                    if (user != null)
                    {
                        if (user.Password == _password)
                        {
                            if (user.Status == "N")
                                returnString = "User has been deactivated";
                            else
                            {
                                userModel = new UserModel
                                {
                                    Username = user.Username,
                                    SessionID = null,
                                    Status = user.Status,
                                    FirstName = user.FirstName,
                                    MiddleInitial = user.MiddleInitial,
                                    LastName = user.LastName,
                                    AgentNo1A = user.AgentNo1A,
                                    AgentNo1B = user.AgentNo1B,
                                    AgentNo5J = user.AgentNo5J,
                                    AccessLevel = user.AccessLevel,
                                    Type = user.Type
                                };

                                userModel.ClientCodes = new List<string>();

                                var clients = db.UserClient.Where(r => r.Username == _username).ToList();

                                clients.ForEach(item =>
                                {
                                    userModel.ClientCodes.Add(item.ClientCode);
                                });
                            }
                        }
                        else
                            returnString = "Invalid Password";
                    }
                    else
                        returnString = "Invalid Username";
                }
            }
            catch(Exception error)
            {
                returnString = "ERROR " + error.Message;
            }

            return userModel;
        }
    }
}