using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SCT_iCare
{
    public class MvcApplication : System.Web.HttpApplication

    {
        //metodos tiempo de espea y validacion de sesiones 
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //inicio de sesion y tiempo de sesion admitido
        protected void Session_Start(object sender, EventArgs e)
        {
            Session["username"] = "username";

            Session.Timeout = 90;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }
        //cierres de sesion por usuario redirecciona a login
        protected void Session_End(object sender, EventArgs e)
        {


        }

        protected void Application_End(object sender, EventArgs e)
        {
            Session.Abandon();
        }

        ////metodo tiempo de sesion
        public void Session_OnEnd()
        {
            // do your desired task when the session expires
        }

    }



}


