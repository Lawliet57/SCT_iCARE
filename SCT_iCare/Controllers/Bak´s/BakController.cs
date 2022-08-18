using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCT_iCare;

namespace SCT_iCare.Controllers.Bak_s
{
    public class BakController : Controller
    {
        private QA_bakEntities db = new QA_bakEntities();
        private GMIEntities bd = new GMIEntities();

        // GET: Bak
        public ActionResult Index(int id)
        {

            Paciente pacienteGMI = bd.Paciente.Find(id);
            Paciente pacienteQA = db.Paciente.Find(id);

            return View();
        }
    }
}
