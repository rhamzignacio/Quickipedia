using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Quickipedia.Startup))]
namespace Quickipedia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}
