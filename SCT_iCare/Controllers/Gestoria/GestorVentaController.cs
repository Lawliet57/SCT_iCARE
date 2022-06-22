using SCT_iCare.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace SCT_iCare.Controllers.Gestoria
{
    public class GestorVentaController : Controller
    {
        private GMIEntities db = new GMIEntities();



        // GET: GestorVenta
        [AuthorizeUser(idOperacion: 7)]
        public ActionResult Index()
        {
            return View();
        }
    }
}