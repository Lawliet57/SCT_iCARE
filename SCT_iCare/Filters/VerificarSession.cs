using SCT_iCare.Controllers.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Filters
{
    public class VerificarSession : ActionFilterAttribute
    {
        private Usuarios oUser;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                oUser = (Usuarios)HttpContext.Current.Session["User"];
                if (oUser == null)
                {
                    if (filterContext.Controller is LoginController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Login/Inicio");
                    }
                }

            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Login/Inicio");
            }

        }
    }
}