using System;
using Microsoft.Owin;
using Owin;
using Payroll.UI.Web;

[assembly: OwinStartupAttribute(typeof(Startup))]
namespace Payroll.UI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }       
    }
}
