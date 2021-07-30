using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;

namespace SalesAndInentoryWeb_Application
{
    public class Global : HttpApplication
    {
        //bhafgigig
        void Application_Start(object sender, EventArgs e)
        {
            DashboardConfig.RegisterService(RouteTable.Routes);
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }
    }
}
