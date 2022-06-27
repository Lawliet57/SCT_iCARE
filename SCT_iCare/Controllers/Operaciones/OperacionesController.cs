using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SCT_iCare;
using SCT_iCare.Filters;


namespace SCT_iCare.Controllers.Operaciones
{
    public class OperacionesController : Controller
    {
        private GMIEntities db = new GMIEntities();
        // GET: Operaciones

        //[AuthorizeUser(idOperacion: 15)]
        public ViewResult GestionOperaciones(string searchString)
        {
            var roles = db.Roles.Include(u => u.operaciones);
            var operaciones = db.operaciones.Include(a => a.rol_operacion);
            if (!String.IsNullOrEmpty(searchString))
            {
                roles = roles.Where(s => s.Nombre.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(roles.ToList());
        }


        public ActionResult GestionR(string searchString)
        {
            var roles = db.Roles.Where(w => w.idRol == 10).Include(u => u.operaciones);
            if (!String.IsNullOrEmpty(searchString))
            {
                roles = roles.Where(s => s.Nombre.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(roles.ToList());
        }

        // GET: GestionUsuarios/Details/5
        //public ActionResult DetailsR(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Usuarios usuarios = db.Usuarios.Find(id);
        //    if (usuarios == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(usuarios);
        //}

        // GET: GestionUsuarios/Create
        public ActionResult CreateR()
        {
            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre", "idOperacion");
            return View();
        }

        // POST: GestionUsuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateR([Bind(Include = "idRol,Nombre,idOperacion")] Roles roles)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(roles);
                db.SaveChanges();
                return RedirectToAction("GestionOperaciones");
            }

            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre", roles.idRol);
            return View(roles);
        }

        //[HttpPost]
        //public ActionResult CreateRolOperacion(int idRol, int idOperacion)
        //{
        //    rol_operacion rol_Operacion = new rol_operacion();

        //    rol_operacion. = nombre;
        //    rol_operacion.Email = email;
         
        //    rol_operacion.idRol = 10;

        //    if (ModelState.IsValid)
        //    {
        //        db.Usuarios.Add(usuarios);
        //        db.SaveChanges();
        //        return RedirectToAction("GestionUsuarios");
        //    }

        //    ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre", usuarios.idRol);
        //    return View("GestionUsuarios");
        }

        [HttpPost]
        public ActionResult CreateCallCenter(string email, string password, string nombre)
        {
            Usuarios usuarios = new Usuarios();

            usuarios.Nombre = nombre;
            usuarios.Email = email;
            usuarios.Password = Encrypt.GetSHA256(password);
            usuarios.idRol = 1;

            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return View("GestionUsuarios", "CallCenter");
            }

            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre", usuarios.idRol);
            return View("GestionUsuarios", "CallCenter");
        }




        // GET: GestionUsuarios/Edit/5
        public ActionResult EditR(int id)
        {
            try
            {
                GMIEntities db = new GMIEntities();
                Usuarios vd = db.Usuarios.Find(id);
                List<Roles> lsG = db.Roles.ToList();
                ViewBag.idRol = new SelectList(lsG, "idRol", "Nombre", vd.idRol);
                db.Dispose();
                return View(vd);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("GestionUsuarios");
            }
        }

        // POST: GestionUsuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditR(Usuarios v)
        {
            try
            {
                using (GMIEntities db = new GMIEntities())
                {
                    Usuarios vd = db.Usuarios.Find(v.idUsuario);
                    vd.Nombre = v.Nombre;
                    vd.Email = v.Email;
                    vd.Password = v.Password;
                    vd.idRol = v.idRol;

                    db.SaveChanges();
                }
                TempData["mensaje"] = "Se edito a " + v.Nombre;

                return RedirectToAction("GestionUsuarios");
            }
            catch
            {
                return View();
            }
        }

        // GET: GestionUsuarios/Delete/5
        public ActionResult DeleteR(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles roles = db.Roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }

        // POST: GestionUsuarios/Delete/5
        [HttpPost, ActionName("DeleteR")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Roles roles = db.Roles.Find(id);
            db.Roles.Remove(roles);
            db.SaveChanges();
            return RedirectToAction("GestionOperaciones");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsignarSucursal(int id, int sucursal)
        {
            var recepcion = (from r in db.Recepcionista where r.idUsuario == id select r).FirstOrDefault();
            var idSucursal = (from i in db.Sucursales where i.idSucursal == sucursal select i.idSucursal).FirstOrDefault();

            if (recepcion != null)
            {
                Recepcionista recepcionista = db.Recepcionista.Find(id);
                recepcionista.idUsuario = id;
                recepcionista.idSucursal = idSucursal;

                if (ModelState.IsValid)
                {
                    db.Entry(recepcionista).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("GestionUsuarios");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Recepcionista recepcionista = new Recepcionista();
                    recepcionista.idUsuario = id;
                    recepcionista.idSucursal = idSucursal;

                    db.Recepcionista.Add(recepcionista);
                    db.SaveChanges();
                    return RedirectToAction("GestionUsuarios");
                }
            }

            return RedirectToAction("GestionUsuarios");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult CarruselMedicoU()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AsignarModulo(int modulo, int sucursal, int doctor)
        {
            SCT_iCare.DoctorModulo dm = new SCT_iCare.DoctorModulo();

            dm.idUsuario = doctor;
            dm.idModulo = modulo;
            dm.idSucursal = sucursal;

            if (ModelState.IsValid)
            {
                db.DoctorModulo.Add(dm);
                db.SaveChanges();
            }

            return Redirect("CarruselMedico");
        }


    }
}
