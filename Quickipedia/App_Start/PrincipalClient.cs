using System.Collections.Generic;
using clPrincipal = System.Security.Principal;


namespace Quickipedia
{
    public class PrincipalClient  : IClientPrincipal
    {
        public clPrincipal.IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return true;
        }

        public void Principal(string name)
        {
            Identity = new clPrincipal.GenericIdentity(name);
        }

        public string SelectedClient { get; set; }
    }

    interface IClientPrincipal : clPrincipal.IPrincipal
    {
        string SelectedClient { get; set; }
    }

    public class PrincipalSelectedClientModel
    {
        public string SelectedClient { get; set; }
    }
}