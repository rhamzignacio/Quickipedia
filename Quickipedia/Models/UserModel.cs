using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quickipedia.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get;  set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string AccessLevel { get; set; }
        public string ShowAccessLevel
        {
            get
            {
                if (AccessLevel == "A")
                    return "Admin";
                else if (AccessLevel == "U")
                    return "User";
                else
                    return "";
            }
        }
        public string Type { get; set; }
        public string AgentNo1A { get; set; }
        public string AgentNo1B { get; set; }
        public string AgentNo5J { get; set; } 
        public string SessionID { get; set; }
        public string Status { get; set; }
        public string ShowStatus
        {
            get
            {
                if (Status == "Y")
                    return "Active";
                else if (Status == "N")
                    return "Inactive";
                else
                    return "";
            }
        }
        public List<string> ClientCodes { get; set; }
        public Guid ID { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string ShowModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ShowModifiedDate
        {
            get
            {
                if (ModifiedDate != null)
                    return ModifiedDate.ToString();
                else
                    return "";
            }
        }
    }

    public class UserClientModel
    {
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
    }

    public class ClientDropdownModel
    {
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
    }
}