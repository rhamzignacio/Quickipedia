using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using Quickipedia.Models;

namespace Quickipedia
{
    public class UniversalHelpers
    {
        public static List<string> UserClients
        {
            get
            {
                List<string> userClients = new List<string>(); ;

                HttpCookie authCookie_quickipedia = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie_quickipedia != null)
                {
                    FormsAuthenticationTicket authTicket_quikipedia = FormsAuthentication.Decrypt(authCookie_quickipedia.Value);

                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    PrincipalSerializeModel serializeModel = serializer.Deserialize<PrincipalSerializeModel>(authTicket_quikipedia.UserData);

                    userClients = serializeModel.ClientCodes;
                }

                return userClients;
            }
            set
            {

            }
        }

        public static string SelectedClient
        {
            get
            {
                HttpCookie authCookie_quickipedia = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                if(authCookie_quickipedia != null)
                {
                    FormsAuthenticationTicket authTicket_quikipedia = FormsAuthentication.Decrypt(authCookie_quickipedia.Value);

                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    PrincipalSelectedClientModel serializeModel = serializer.Deserialize<PrincipalSelectedClientModel>(authTicket_quikipedia.UserData);

                    return serializeModel.SelectedClient;
                }

                return "";
            }
            set
            {

            }
        }

        public static UserModel CurrentUser
        {
            get
            {
                UserModel user = null;

                HttpCookie authCookie_quickipedia = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie_quickipedia != null)
                {
                    FormsAuthenticationTicket authTicket_quikipedia = FormsAuthentication.Decrypt(authCookie_quickipedia.Value);

                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    PrincipalSerializeModel serializeModel = serializer.Deserialize<PrincipalSerializeModel>(authTicket_quikipedia.UserData);

                    Principal newUser = new Principal(authTicket_quikipedia.Name);

                    newUser.Username = serializeModel.Username;

                    newUser.SessionID = serializeModel.SessionID;

                    newUser.Status = serializeModel.Status;

                    HttpContext.Current.User = newUser;

                    using (var db = new QuickipediaEntities())
                    {
                        var query = from a in db.UserAccount
                                    where a.Username == newUser.Username
                                    select new UserModel
                                    {
                                        Username = a.Username,
                                        FirstName = a.FirstName,
                                        LastName = a.LastName,
                                        MiddleInitial = a.MiddleInitial,
                                        AccessLevel = a.AccessLevel,
                                        AgentNo1A = a.AgentNo1A,
                                        AgentNo1B = a.AgentNo1B,
                                        AgentNo5J = a.AgentNo5J,
                                        Status = a.Status,
                                        Type = a.Type,
                                        ID = a.ID
                                    };
                        user = query.FirstOrDefault();

                        return user;
                    }
                }
                else
                {
                    return user;
                }
            }
            set
            {

            }
        }
    }
}