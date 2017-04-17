using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quickipedia.Models;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Quickipedia.Services
{
    public static class AccountService
    {
        public static ClientProfile GetClientProfile(string clientCode)
        {
            using (var db = new QuickipediaEntities())
            {
                if(clientCode != "NewClient")
                    return db.ClientProfile.FirstOrDefault(r => r.ClientCode == clientCode);
                else
                {
                    ClientProfile newProf = new ClientProfile
                    {
                        ClientName = "New Client",
                        ClientCode = "NewClient"
                    };

                    return newProf;
                }
            }
        }
        
        public static bool SelectedClientToSession(string selectedClient)
        {
            try
            {
                var currentUser = UniversalHelpers.CurrentUser;

                HttpContext.Current.Session["session_status"] = "online";

                PrincipalSerializeModel serializeModel = new PrincipalSerializeModel();

                serializeModel.SelectedClient = selectedClient;

                serializeModel.Username = currentUser.Username;

                serializeModel.Password = currentUser.Password;

                serializeModel.SessionID = HttpContext.Current.Session.SessionID;

                serializeModel.Status = currentUser.Status;

                serializeModel.ClientCodes = currentUser.ClientCodes;

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                string userData = serializer.Serialize(serializeModel);

                FormsAuthenticationTicket authenticationQuickipedia = new FormsAuthenticationTicket
                    (1, currentUser.Username, DateTime.Now, DateTime.Now.AddMinutes(30), true, userData);

                string encryptedTicket = FormsAuthentication.Encrypt(authenticationQuickipedia);

                HttpCookie authenticationCookie_quickipedia = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                HttpResponse response = HttpContext.Current.Response;

                response.Cookies.Add(authenticationCookie_quickipedia);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void LogoutFromSession(out string message)
        {
            try
            {
                message = "";

                FormsAuthentication.SignOut();
            }
            catch (Exception error)
            {
                message = error.Message;
            }
        }

        public static bool LoginToSession(UserModel userModel)
        {
            try
            {
                HttpContext.Current.Session["session_status"] = "online";

                PrincipalSerializeModel serializeModel = new PrincipalSerializeModel();

                serializeModel.Username = userModel.Username;

                serializeModel.Password = userModel.Password;

                serializeModel.SessionID = HttpContext.Current.Session.SessionID;

                serializeModel.Status = userModel.Status;

                serializeModel.ClientCodes = userModel.ClientCodes;

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                string userData = serializer.Serialize(serializeModel);

                FormsAuthenticationTicket authenticationQuickipedia = new FormsAuthenticationTicket
                    (1, userModel.Username, DateTime.Now, DateTime.Now.AddMinutes(30), true, userData);

                string encryptedTicket = FormsAuthentication.Encrypt(authenticationQuickipedia);

                HttpCookie authenticationCookie_quickipedia = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                HttpResponse response = HttpContext.Current.Response;

                response.Cookies.Add(authenticationCookie_quickipedia);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}