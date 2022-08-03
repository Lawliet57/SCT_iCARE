using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Controllers.Contabilidad
{
    public class ContabilidadController : Controller
    {
        GMIEntities db = new GMIEntities();
        // GET: Maqueta
        public ActionResult Index(string canal, string cuenta, string sucursal, DateTime? fechaInicio, DateTime? fechaFinal, string tipoPago, int? referido, string conciliadooo, int? id)
        {//id nos indica si es 1 son Conciliados y si es 2 son NoConciliados
            var Referido = db.Referido.Find(referido);

            if (conciliadooo == "" || conciliadooo == null)
            {
                ViewBag.Conciliado = "";
            }
            else
            {
                ViewBag.Conciliado = conciliadooo;
            }

            if (sucursal == "" || sucursal == null)
            {
                ViewBag.Sucursal = "";
            }
            else
            {
                ViewBag.Sucursal = sucursal;
            }

            if (canal == "" || canal == null)
            {
                ViewBag.Canal = "";
            }
            else
            {
                ViewBag.Canal = canal;
            }

            if (cuenta == "" || cuenta == null)
            {
                ViewBag.Cuenta = "";
            }
            else if (cuenta == "NULL")
            {
                ViewBag.Cuenta = null;
            }
            else
            {
                ViewBag.Cuenta = cuenta;
            }

            if (tipoPago == "" || tipoPago == null)
            {
                ViewBag.Pago = "";
            }
            else
            {
                ViewBag.Pago = tipoPago;
            }

            if (referido == null)
            {
                ViewBag.Referido = "";
                ViewBag.idReferido = 0;
            }
            else
            {
                ViewBag.Referido = Referido.Nombre;
                ViewBag.idReferido = Referido.idReferido;
            }

            if (id != null)
            {
                if (id == 1)
                {
                    ViewBag.Conciliados = true;
                }
                else if (id == 2)
                {
                    ViewBag.NoConciliados = true;

                }

            }

            ViewBag.FechaInicio = fechaInicio != null ? fechaInicio : null;
            ViewBag.FechaFinal = fechaFinal != null ? fechaFinal : null;

            return View();
        }

        public ActionResult Conciliados(string canal, string cuenta, string sucursal, DateTime? fechaInicio, DateTime? fechaFinal, string tipoPago, int? referido)
        {
            var Referido = db.Referido.Find(referido);

            if (sucursal == "" || sucursal == null)
            {
                ViewBag.Sucursal = "";
            }
            else
            {
                ViewBag.Sucursal = sucursal;
            }

            if (canal == "" || canal == null)
            {
                ViewBag.Canal = "";
            }
            else
            {
                ViewBag.Canal = canal;
            }

            if (cuenta == "" || cuenta == null)
            {
                ViewBag.Cuenta = "";
            }
            else
            {
                ViewBag.Cuenta = cuenta;
            }

            if (tipoPago == "" || tipoPago == null)
            {
                ViewBag.Pago = "";
            }
            else
            {
                ViewBag.Pago = tipoPago;
            }

            if (referido == null)
            {
                ViewBag.Referido = "";
                ViewBag.idReferido = 0;
            }
            else
            {
                ViewBag.Referido = Referido.Nombre;
                ViewBag.idReferido = Referido.idReferido;
            }

            ViewBag.FechaInicio = fechaInicio != null ? fechaInicio : null;
            ViewBag.FechaFinal = fechaFinal != null ? fechaFinal : null;

            return View();
        }


        ///Conciliacion
        public ActionResult Conciliacion(DateTime? fechaInicio, DateTime? fechaFinal)
        {

            ViewBag.FechaInicio = fechaInicio != null ? fechaInicio : null;
            ViewBag.FechaFinal = fechaFinal != null ? fechaFinal : null;

            return View();
        }

        public ActionResult Pagos(string canal, DateTime? fechaInicio, DateTime? fechaFinal, int? referido)
        {
            var Referido = db.Referido.Find(referido);

            if (canal == "" || canal == null)
            {
                ViewBag.Canal = "";
            }
            else
            {
                ViewBag.Canal = canal;
            }           

            if (referido == null)
            {
                ViewBag.Referido = "";
                ViewBag.idReferido = 0;
            }
            else
            {
                ViewBag.Referido = Referido.Nombre;
                ViewBag.idReferido = Referido.idReferido;
            }

            ViewBag.FechaInicio = fechaInicio != null ? fechaInicio : null;
            ViewBag.FechaFinal = fechaFinal != null ? fechaFinal : null;
                     
            return View();
        }


        public ActionResult CambiarCuenta(int? id, string cuenta, string cuenta2, string comentario, string usuario, string canal, string sucursal,
            DateTime? fechaInicio, DateTime? fechaFinal, DateTime? fechaContable, string pago, int? idGestor, string usarSaldo, string FormadePago)
        {
            var cita = db.Cita.Find(id);

            string historico = cita.CuentaComentario == null ? "" : cita.CuentaComentario + "+";
            string cuentaAnterior = cita.Cuenta == null ? "" : " PROVIENE DE " + cita.Cuenta;
            string pagoAnterior = " El tipo de pago fue modificado de " + cita.TipoPago;
            cita.CuentaComentario = historico + comentario + cuentaAnterior + pagoAnterior + " " + DateTime.Today.ToString("dd-MM-yy") + " POR " + usuario;
            cita.Cuenta = cuenta;
            cita.FechaContable = fechaContable == null ? DateTime.Now : fechaContable;
            cita.UsarSaldo = usarSaldo;
            cita.FormaPago = FormadePago;

            if (pago == null || pago == "")
            {
                cita.TipoPago = cita.TipoPago;
            }
            else
            {
                cita.TipoPago = pago;
            }

            cita.Conciliado = DateTime.Today.Day.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Year.ToString();

            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { fechaInicio = fechaInicio, fechaFinal = fechaFinal, sucursal = sucursal, cuenta = cuenta2, canal = canal, referido = idGestor });
        }

        public ActionResult CambiarCuentaALT(int? id, string cuenta, string comentario, string usuario, DateTime? fechaInicio, DateTime? fechaFinal, DateTime? fechaContable, string sucursal,
            string cuenta2, string canal, string pago, int? idGestor, string FormadePago)
        {
            var cita = db.PacienteESP.Find(id);

            string historico = cita.CuentaComentario == null ? "" : cita.CuentaComentario + "+";
            string cuentaAnterior = cita.Cuenta == null ? "" : " PROVIENE DE " + cita.Cuenta;
            cita.CuentaComentario = historico + comentario + cuentaAnterior + " " + DateTime.Today.ToString("dd-MM-yy") + " POR " + usuario;
            cita.Cuenta = cuenta;
            cita.FechaContable = fechaContable == null ? DateTime.Now : fechaContable;
            cita.FormaPago = FormadePago;

            if (pago != null || pago != "")
            {
                cita.TipoPago = pago;
            }

            cita.Conciliado = DateTime.Today.Day.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Year.ToString();


            var findPrecio = (from q in db.Referido where q.Nombre == cita.ReferidoPor && q.Tipo == cita.CanalTipo select q).FirstOrDefault();
            var precioEncontradoCI = findPrecio.PrecioNormalconIVA;
            var precioEncontradoSI = findPrecio.PrecioNormal;
            var precioEncontradoA = findPrecio.PrecioAereo;
            var precioFinal = "";

            if (cita.TipoPago == "Referencía BanBajío" || cita.TipoPago == "Transferencia vía BanBajío" || cita.TipoPago == "Credito Empresas"
          || cita.TipoPago == "Referencia BanBajío" || cita.TipoPago == "Banorte" || cita.TipoPago == "REFERENCIA OXXO" || cita.TipoPago == "Referencia OXXO"
          || cita.TipoPago == "Pago con Tarjeta")
            {

                if (cita.TipoLicencia == "AEREO")
                {
                    if (findPrecio.PrecioAereo == null || findPrecio.PrecioAereo == "0")
                    {
                        if (findPrecio.PrecioNormalconIVA == null || findPrecio.PrecioNormalconIVA == "0")
                        {
                            precioFinal = precioEncontradoSI;
                        }

                        else
                        {
                            precioFinal = precioEncontradoCI;
                        }

                    }

                    else
                    {

                        precioFinal = precioEncontradoA;

                    }

                }
                else
                {
                    if (findPrecio.PrecioNormalconIVA == null || findPrecio.PrecioNormalconIVA == "0")
                    {

                        precioFinal = precioEncontradoSI;

                    }

                    else
                    {

                        precioFinal = precioEncontradoCI;

                    }

                }
            }

            else
            {
                if (cita.TipoLicencia == "AEREO")
                {
                    if (findPrecio.PrecioAereo == null || findPrecio.PrecioAereo == "0")
                    {
                        precioFinal = precioEncontradoSI;
                    }

                    else
                    {
                        precioFinal = precioEncontradoA;
                    }

                }
                else
                {
                    if (precioEncontradoSI == null || precioEncontradoSI == "0")
                    {
                        precioFinal = precioEncontradoCI;
                    }
                    else
                    {
                        precioFinal = precioEncontradoSI;
                    }
                }
            }

            cita.PrecioEpi = precioFinal;

            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { fechaInicio = fechaInicio, fechaFinal = fechaFinal, sucursal = sucursal, cuenta = cuenta2, canal = canal, referido = idGestor });
        }

        public ActionResult AbrirTicket(int? idTicket, string cuenta, string canal, string sucursal, string tiporeferido, string tipopago,
            DateTime? fechaInicio, DateTime? fechaFinal, int? idTicketSeguro)
        {
            if (idTicket != 0)
            {
                TempData["ID"] = idTicket;

                if (idTicketSeguro != 0)
                {
                    TempData["IDTICKETSEGURO"] = idTicketSeguro;
                }
                return RedirectToAction("Index", new { fechaInicio = fechaInicio, fechaFinal = fechaFinal, sucursal = sucursal, cuenta = cuenta, canal = canal, referido = tiporeferido, tipopago = tipopago });
            }
            else
            {
                return View("FaltaTicket");
            }
            //else
            //{
            //    TempData["ID"] = null;
            //    return RedirectToAction("Index", new { fechaInicio = fechaInicio, fechaFinal = fechaFinal, sucursal = sucursal, cuenta = cuenta, canal = canal, referido = tiporeferido, tipopago = tipopago });
            //}


        }
        //Abrir ticket vista conciliados
        public ActionResult AbrirTicketC(int? idTicket, string cuenta, string canal, string sucursal, string tiporeferido, string tipopago,
        DateTime? fechaInicio, DateTime? fechaFinal)
        {
            if (idTicket != null)
            {
                TempData["ID"] = idTicket;
                return RedirectToAction("Conciliados", new { fechaInicio = fechaInicio, fechaFinal = fechaFinal, sucursal = sucursal, cuenta = cuenta, canal = canal, referido = tiporeferido, tipopago = tipopago });
            }
            else
            {
                TempData["ID"] = null;
                return RedirectToAction("Conciliados", new { fechaInicio = fechaInicio, fechaFinal = fechaFinal, sucursal = sucursal, cuenta = cuenta, canal = canal, referido = tiporeferido, tipopago = tipopago });
            }


        }

        public ActionResult AbrirTicket2(int id)
        {
            Tickets ticket = db.Tickets.Find(id);

                var bytesBinary = ticket.Ticket;
                TempData["ID"] = null;
                return File(bytesBinary, "DownloadName.pdf");
                //return File(bytesBinary, "application/pdf");
                //return File(bytesBinary, "application/vnd.visio");
            
        }

        public ActionResult AbrirTicketSeguro(int id)
        {
            TicketSeguro ticket = db.TicketSeguro.Find(id);

            var bytesBinary = ticket.FileTicketSeguro;
            TempData["IDTICKETSEGURO"] = null;
            return File(bytesBinary, "DownloadName.pdf");
            //return File(bytesBinary, "application/pdf");
            //return File(bytesBinary, "application/vnd.visio");

        }

        public ActionResult Deuda(int? id, string deuda, string canal)
        {
            var gestor = db.Referido.Find(id);

            gestor.Deuda = deuda != null ? deuda : null;

            //variables para filtro
            if (canal == "" || canal == null)
            {
                ViewBag.Canal = "";
            }
            else
            {
                ViewBag.Canal = canal;
            }



            if (ModelState.IsValid)
            {
                db.Entry(gestor).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Redirect("Pagos");
        }




        //METODO DE INGRESO DE PAGOS EN LA VISTA DE PAGOS
        public ActionResult EditarEfectivo(int? id, int efectivo, string usuario, DateTime fechaAhora, DateTime? fecha1, DateTime? fecha2, DateTime fechadepago)
        {

            ViewBag.FechaInicio = fecha1 != null ? fecha1 : null;
            ViewBag.FechaFinal = fecha2 != null ? fecha2 : null;

            DateTime fechaInicio = ViewBag.FechaInicio;
            DateTime fechaFinal = ViewBag.FechaFinal;

            Referido referido = db.Referido.Find(id);
            PagosGestores pagosGestores = new PagosGestores();

            int sumaEfectivos = 0;

            var pG = (from p in db.PagosGestores where p.idReferido == id select p);

            foreach (var pagosGes in pG)
            {

                sumaEfectivos += Convert.ToInt32(pagosGes.PagoIngresado);

            }

            sumaEfectivos = sumaEfectivos + efectivo;

            referido.Efectivo = Convert.ToString(sumaEfectivos);

            pagosGestores.Gestor = referido.Nombre;
            pagosGestores.idReferido = referido.idReferido;
            pagosGestores.PagoIngresado = Convert.ToString(efectivo);
            pagosGestores.Fecha = fechadepago;

            if (ModelState.IsValid)
            {
                db.Entry(referido).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (ModelState.IsValid)
            {
                db.PagosGestores.Add(pagosGestores);
                db.SaveChanges();
            }


            return RedirectToAction("Pagos", new { fechaInicio = fechaInicio, fechaFinal = fechaFinal });
        }



        //METODO PARA CONCILIAR CON EL INGRESO DE PAGOS EN LA VISTA DE PAGOS
        public ActionResult ConciliarIngresosNom(int? id, string usuario, DateTime? fecha1, DateTime? fecha2, string metododepago, string fechadepago)
        {
            ViewBag.FechaInicio = fecha1 != null ? fecha1 : null;
            ViewBag.FechaFinal = fecha2 != null ? fecha2 : null;

            DateTime fechaInicio = ViewBag.FechaInicio;
            DateTime fechaFinal = ViewBag.FechaFinal;

            Referido referido = db.Referido.Find(id);
            PagosGestores pagosGestores = new PagosGestores();

            var modelonNormal = db.Paciente.Join(db.Cita, n => n.idPaciente, m => m.idPaciente, (n, m) => new { N = n, M = m }).
Join(db.Captura, a => a.M.idPaciente, b => b.idPaciente, (a, b) => new { A = a, B = b }).
Where(s => s.B.EstatusCaptura == "Terminado" && s.A.M.Asistencia == null && s.A.M.CanalTipo.Contains(referido.Tipo) && s.A.M.ConciliarPago == null && s.A.M.TipoPago == "Pendiente de pago"
&& s.A.M.ReferidoPor.Contains(referido.Nombre) && s.A.M.TipoTramite != "REVALORACIÓN" && s.A.M.Cuenta == "CUENTAS X COBRAR" && s.A.M.FechaCita >= fecha1 && s.A.M.FechaCita <= fecha2).OrderBy(o => o.A.M.FechaCita);

            var modelonNormalCount = db.Paciente.Join(db.Cita, n => n.idPaciente, m => m.idPaciente, (n, m) => new { N = n, M = m }).
Join(db.Captura, a => a.M.idPaciente, b => b.idPaciente, (a, b) => new { A = a, B = b }).
Where(s => s.B.EstatusCaptura == "Terminado" && s.A.M.Asistencia == null && s.A.M.CanalTipo.Contains(referido.Tipo) && s.A.M.ConciliarPago == null && s.A.M.TipoPago == "Pendiente de pago"
&& s.A.M.ReferidoPor.Contains(referido.Nombre) && s.A.M.TipoTramite != "REVALORACIÓN" && s.A.M.Cuenta == "CUENTAS X COBRAR" && s.A.M.FechaCita >= fecha1 && s.A.M.FechaCita <= fecha2).Count();

            var pG = (from i in db.PagosGestores where i.idReferido == referido.idReferido && i.Fecha >= fechaInicio && i.Fecha < fechaFinal && i.EfectivoUsado == null select i);
            var saldoTotal = 0;
            var deudaTotal = 0;
            var conteoNormal = 0;
            int conteo = (from i in db.PagosGestores where i.idReferido == referido.idReferido && i.Fecha >= fechaInicio && i.Fecha < fechaFinal && i.EfectivoUsado == null select i).OrderBy(o => o.Fecha).Count();
            int[] idPagos = new int[conteo];
            int[] idPacientes = new int[1000];
            var aumento = 0;
            var deudaSumatoria = 0;
            var aumentoPaciente = 0;
            string HASH = "";

            foreach (var pagosGes in pG)
            {
                saldoTotal += Convert.ToInt32(pagosGes.PagoIngresado);
                idPagos[aumento] = pagosGes.idPagoGestor;
                aumento++;
            }

            for (int n = 0; n < aumento;)
            {
                var pilin = idPagos[n];
                var editarEfectivo = (from i in db.PagosGestores where i.idPagoGestor == pilin select i).FirstOrDefault();

                editarEfectivo.EfectivoUsado = "Si";

                if (HASH != "")
                {
                    HASH += "+" + pilin;
                }
                else
                {
                    HASH += pilin;
                }

                if (ModelState.IsValid)
                {
                    db.Entry(editarEfectivo).State = EntityState.Modified;
                    db.SaveChanges();
                }
                n++;
            }

                if (referido.Residual != "" && referido.Residual != null)
                {
                    saldoTotal += Convert.ToInt32(referido.Residual);
                }

                //NORMALES
                if (modelonNormalCount != 0)
                {
                    foreach (var pacientes in modelonNormal)
                    {
                        deudaTotal += Convert.ToInt32(pacientes.A.M.PrecioEpi);
                        idPacientes[aumentoPaciente] = pacientes.A.M.idCita;
                        aumentoPaciente++;

                        if (deudaTotal <= saldoTotal)
                        {
                            deudaSumatoria = deudaTotal;
                            conteoNormal++;
                        }
                        else
                        {
                            break;
                        }            
                    }

                    for (int n = 0; n < conteoNormal;)
                    {
                        DateTime Ahora = DateTime.Now;
                        DateTime fechadePagoController = Convert.ToDateTime(fechadepago);
                        var fechaContable = Convert.ToDateTime(fechadePagoController);
                        var fechaHoy = Convert.ToDateTime(Ahora).ToString("d-M-yy");
                        var pilin = idPacientes[n];
                        var editarPaciente = (from i in db.Cita where i.idCita == pilin select i).FirstOrDefault();
                        string Historico = editarPaciente.CuentaComentario;

                        editarPaciente.ConciliarPago = "Si";
                        editarPaciente.Conciliado = fechaHoy;
                        editarPaciente.FechaContable = fechaContable;
                        editarPaciente.TipoPago = metododepago;
                        editarPaciente.Cuenta = "BANCOS";
                        editarPaciente.RastreoPagos = HASH;


                    if (Historico == null || Historico == "")
                        {
                            editarPaciente.CuentaComentario = DateTime.Today.ToString("dd-MM-yy") + " por " + usuario + " CONCILIADO DESDE EL MODULO DE PAGOS";
                        }
                        else
                        {
                            editarPaciente.CuentaComentario = Historico + "+" + DateTime.Today.ToString("dd-MM-yy") + " por " + usuario + " CONCILIADO DESDE EL MODULO DE PAGOS";
                        }

                    if (ModelState.IsValid)
                        {
                            db.Entry(editarPaciente).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        n++;
                    }

                    if (conteoNormal > 0)
                    {
                                var residuo = saldoTotal - deudaSumatoria;
                                referido.Residual = Convert.ToString(residuo);
                                if (ModelState.IsValid)
                                {
                                    db.Entry(referido).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                    }                      
                    else
                    {
                        referido.Residual = Convert.ToString(saldoTotal);

                        if (ModelState.IsValid)
                        {
                            db.Entry(referido).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }        

                return RedirectToAction("Pagos", new { fechaInicio = fechaInicio, fechaFinal = fechaFinal });        
        }
        


        //METODO PARA CONCILIAR CON EL INGRESO DE PAGOS EN LA VISTA DE PAGOS
        public ActionResult ConciliarIngresosAlt(int? id, string usuario, DateTime? fecha1, DateTime? fecha2, string metododepago, string fechadepago)
        {
            ViewBag.FechaInicio = fecha1 != null ? fecha1 : null;
            ViewBag.FechaFinal = fecha2 != null ? fecha2 : null;

            DateTime fechaInicio = ViewBag.FechaInicio;
            DateTime fechaFinal = ViewBag.FechaFinal;

            Referido referido = db.Referido.Find(id);
            PagosGestores pagosGestores = new PagosGestores();


            var modelonAlternativo = db.PacienteESP.Where(s => s.EstatusCaptura == "Terminado" && s.Asistencia == null && s.CanalTipo.Contains(referido.Tipo) && s.ConciliarPago == null
&& s.ReferidoPor.Contains(referido.Nombre) && s.Conciliado == null && s.TipoTramite != "REVALORACIÓN" && s.FechaCita >= fecha1 && s.FechaCita <= fecha2).OrderBy(o => o.FechaCita);

            var modelonAlternativoCount = db.PacienteESP.Where(s => s.EstatusCaptura == "Terminado" && s.Asistencia == null && s.CanalTipo.Contains(referido.Tipo) && s.ConciliarPago == null
&& s.ReferidoPor.Contains(referido.Nombre) && s.Conciliado == null && s.TipoTramite != "REVALORACIÓN" && s.FechaCita >= fecha1 && s.FechaCita <= fecha2).Count();

            var pG = (from i in db.PagosGestores where i.idReferido == referido.idReferido && i.Fecha >= fechaInicio && i.Fecha < fechaFinal && i.EfectivoUsado == null select i);
            var saldoTotal = 0;
            var deudaTotal = 0;
            var conteoNormal = 0;
            int conteo = (from i in db.PagosGestores where i.idReferido == referido.idReferido && i.Fecha >= fechaInicio && i.Fecha < fechaFinal && i.EfectivoUsado == null select i).OrderBy(o => o.Fecha).Count();
            int[] idPagos = new int[conteo];
            int[] idPacientes = new int[1000];
            var aumento = 0;
            var deudaSumatoria = 0;
            var aumentoPaciente = 0;
            string HASH = "";


            foreach (var pagosGes in pG)
            {
                saldoTotal += Convert.ToInt32(pagosGes.PagoIngresado);
                idPagos[aumento] = pagosGes.idPagoGestor;
                aumento++;
            }

            for (int n = 0; n < aumento;)
            {
                var pilin = idPagos[n];
                var editarEfectivo = (from i in db.PagosGestores where i.idPagoGestor == pilin select i).FirstOrDefault();

                if(HASH != "" )
                {
                    HASH += "+" + pilin;
                }
                else
                {
                    HASH += pilin;
                }

                editarEfectivo.EfectivoUsado = "Si";

                if (ModelState.IsValid)
                {
                    db.Entry(editarEfectivo).State = EntityState.Modified;
                    db.SaveChanges();
                }
                n++;
            }

            if (referido.Residual != "" && referido.Residual != null)
            {
                saldoTotal += Convert.ToInt32(referido.Residual);
            }

            //ALTERATIVOS
            if (modelonAlternativoCount != 0)
            {
                foreach (var pacientes in modelonAlternativo)
                {
                    deudaTotal += Convert.ToInt32(pacientes.PrecioEpi);
                    idPacientes[aumentoPaciente] = pacientes.idPacienteESP;
                    aumentoPaciente++;

                    if (deudaTotal <= saldoTotal)
                    {
                        deudaSumatoria = deudaTotal;
                        conteoNormal++;
                    }
                    else
                    {
                        break;
                    }
                }

                for (int n = 0; n < conteoNormal;)
                {
                    DateTime Ahora = DateTime.Now;
                    DateTime fechadePagoController = Convert.ToDateTime(fechadepago);
                    var fechaContable = Convert.ToDateTime(fechadePagoController);
                    var fechaHoy = Convert.ToDateTime(Ahora).ToString("dd-M-yy");
                    var pilin = idPacientes[n];
                    var editarPaciente = (from i in db.PacienteESP where i.idPacienteESP == pilin select i).FirstOrDefault();
                    string Historico = editarPaciente.CuentaComentario;

                    editarPaciente.ConciliarPago = "Si";
                    editarPaciente.Conciliado = fechaHoy;
                    editarPaciente.FechaContable = fechaContable;
                    editarPaciente.TipoPago = metododepago;
                    editarPaciente.Cuenta = "BANCOS";
                    editarPaciente.RastreoPagos = HASH;

                    if (Historico == null || Historico == "")
                    {
                        editarPaciente.CuentaComentario = DateTime.Today.ToString("dd-MM-yy") + " por " + usuario + " CONCILIADO DESDE EL MODULO DE PAGOS";
                    }
                    else
                    {
                        editarPaciente.CuentaComentario = Historico + "+" + DateTime.Today.ToString("dd-MM-yy") + " por " + usuario + " CONCILIADO DESDE EL MODULO DE PAGOS";
                    }

                    if (ModelState.IsValid)
                    {
                        db.Entry(editarPaciente).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    n++;
                }

                if (conteoNormal > 0)
                {
                    var residuo = saldoTotal - deudaSumatoria;
                    referido.Residual = Convert.ToString(residuo);
                    if (ModelState.IsValid)
                    {
                        db.Entry(referido).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    referido.Residual = Convert.ToString(saldoTotal);

                    if (ModelState.IsValid)
                    {
                        db.Entry(referido).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Pagos", new { fechaInicio = fechaInicio, fechaFinal = fechaFinal });
        }



        //METODO ESTADO DE CUENTA
        public ActionResult EstadoCuenta(string canal, string cuenta, DateTime? fechaInicio, DateTime? fechaFinal, string tipoPago, string sucursal, int? referido, string conciliadooo, int? id)
        {
            var Referido = db.Referido.Find(referido);

            if (conciliadooo == "" || conciliadooo == null)
            {
                ViewBag.Conciliado = "";
            }
            else
            {
                ViewBag.Conciliado = conciliadooo;
            }

            if (sucursal == "" || sucursal == null)
            {
                ViewBag.Sucursal = "";
            }
            else
            {
                ViewBag.Sucursal = sucursal;
            }

            if (canal == "" || canal == null)
            {
                ViewBag.Canal = "";
            }
            else
            {
                ViewBag.Canal = canal;
            }

            if (cuenta == "" || cuenta == null)
            {
                ViewBag.Cuenta = "";
            }
            else
            {
                ViewBag.Cuenta = cuenta;
            }

            if (tipoPago == "" || tipoPago == null)
            {
                ViewBag.Pago = "";
            }
            else
            {
                ViewBag.Pago = tipoPago;
            }

            if (referido == null)
            {
                ViewBag.Referido = "";
                ViewBag.idReferido = 0;
            }
            else
            {
                ViewBag.Referido = Referido.Nombre;
                ViewBag.idReferido = Referido.idReferido;
            }

            if (id != null)
            {
                if (id == 1)
                {
                    ViewBag.Conciliados = true;
                }
                else if (id == 2)
                {
                    ViewBag.NoConciliados = true;
                }
            }

            ViewBag.FechaInicio = fechaInicio != null ? fechaInicio : null;
            ViewBag.FechaFinal = fechaFinal != null ? fechaFinal : null;

            return View();
        }
    }
}
