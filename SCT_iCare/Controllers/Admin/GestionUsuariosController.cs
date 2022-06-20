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


namespace SCT_iCare.Controllers.Admin
{
    public class GestionUsuariosController : Controller
    {
        private GMIEntities db = new GMIEntities();

        // GET: GestionUsuarios
        [AuthorizeUser(idOperacion: 15)]
        public ViewResult GestionUsuarios(string searchString)
        {
            var usuarios = db.Usuarios.Include(u => u.Roles);
            if (!String.IsNullOrEmpty(searchString))
            {
                usuarios = usuarios.Where(s => s.Nombre.ToUpper().Contains(searchString.ToUpper())
                                       || s.Email.ToUpper().Contains(searchString.ToUpper())
                                       || s.Nombre.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(usuarios.ToList());
        }


        public ActionResult GestoresU(string searchString)
        {
            var usuarios = db.Usuarios.Where(w => w.idRol == 10).Include(u => u.Roles);
            if (!String.IsNullOrEmpty(searchString))
            {
                usuarios = usuarios.Where(s => s.Nombre.ToUpper().Contains(searchString.ToUpper())
                                       || s.Email.ToUpper().Contains(searchString.ToUpper())
                                       || s.Nombre.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(usuarios.ToList());
        }

        // GET: GestionUsuarios/Details/5
        public ActionResult DetailsU(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: GestionUsuarios/Create
        public ActionResult CreateU()
        {
            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre");
            return View();
        }

        // POST: GestionUsuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateU([Bind(Include = "idUsuario,Nombre,Email,Password,idRol")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                usuarios.Password = Encrypt.GetSHA256(usuarios.Password);

                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("GestionUsuarios");
            }

            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre", usuarios.idRol);
            return View(usuarios);
        }

        [HttpPost]
        public ActionResult CreateGestor(string email, string password, string nombre)
        {
            Usuarios usuarios = new Usuarios();

            usuarios.Nombre = nombre;
            usuarios.Email = email;
            usuarios.Password = Encrypt.GetSHA256(password);
            usuarios.idRol = 10;

            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("GestionUsuarios");
            }

            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre", usuarios.idRol);
            return View("GestionUsuarios");
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
        public ActionResult EditU(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound("GestionUsuarios");
            }
            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre", usuarios.idRol);
            return View(usuarios);
        }

        // POST: GestionUsuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditU([Bind(Include = "idUsuario,Nombre,Email,Password,idRol")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                usuarios.Password = Encrypt.GetSHA256(usuarios.Password);
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GestionUsuarios");
            }
            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre", usuarios.idRol);
            return View(usuarios);
        }

        // GET: GestionUsuarios/Delete/5
        public ActionResult DeleteU(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: GestionUsuarios/Delete/5
        [HttpPost, ActionName("DeleteU")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
            db.SaveChanges();
            return RedirectToAction("GestionUsuarios");
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
