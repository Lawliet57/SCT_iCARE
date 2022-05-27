using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCT_iCare;

namespace SCT_iCare.Controllers.Admin
{
    public class MetasIndividualesController : Controller
    {

        GMIEntities db = new GMIEntities();


        //METODO QUE NECESITA LAS REFERENCIAS

        public ActionResult MetasIndividuales(string canalInd)
        {
            if (canalInd == "" || canalInd == null)
            {
                ViewBag.Canal = "";
            }
            else
            {
                ViewBag.Canal = canalInd;
            }

            return View();
        }


        //METODO ACTUAL SIN REFRENCIAS
        public ActionResult EditarMetasIndividuales(int? id, int setMeta, string canalInd)
        {
            if (canalInd == "" || canalInd == null)
            {
                ViewBag.Canal = "";
            }
            else
            {
                ViewBag.Canal = canalInd;
            }

            Referido referido = db.Referido.Find(id);

            referido.Meta = Convert.ToString(setMeta);

            if (ModelState.IsValid)
            {
                db.Entry(referido).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("MetasIndividuales", new { canalInd = canalInd});
        }
    }
}
