using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Rotativa;
using PagedList.Mvc;
using PagedList;
using SistemaControlMedico.Models;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Core.Objects;

namespace SistemaControlMedico.Controllers.Modulo_Procesos
{
    public class AltasController : Controller
    {
        private SistemaMedicoDbContext db = new SistemaMedicoDbContext();
        
        // GET: Altas
        public ActionResult Index(int? x)
        {
            var altas = db.Altas.Include(a => a.Ingresos);
            return View(altas.ToList().ToPagedList(x ?? 1 , 3));
        }

        public ActionResult PDF()
        {
            return new ActionAsPdf("Index") { FileName = "Altas.pdf" };
        }

        [HttpPost]
        public ActionResult Index(string busqueda, string BuscarPor, int? x,string operacion)
        {
            if (busqueda == string.Empty || BuscarPor=="")
            {
                var altas = db.Altas.Include(a => a.Ingresos);
                return View(altas.ToList().ToPagedList(x ?? 1, 3));
            }
            else
            {
                if(BuscarPor == "FechaIngreso")
                {
                    var consulta = from a in db.Altas
                                   join p in db.Pacientes on a.Ingresos.paciente equals p.idPaciente
                                   join i in db.Ingresos on a.ingreso equals i.idIngreso
                                   where  i.fechaIngreso.ToString().Contains(busqueda)
                                   select a;

                    if (Request["opcion"] == "count")
                    {
                        ViewBag.conteo = consulta.Count();

                    }
                    else if (Request["opcion"] == "min")
                    {
                        ViewBag.min = consulta.Select(p => p.Ingresos.fechaIngreso).Min();

                    }
                    else if (Request["opcion"] == "max")
                    {
                        ViewBag.max = consulta.Select(p => p.fechaSalida).Max();

                    }
                    else if (Request["opcion"] == "avg")
                    {
                        ViewBag.avg = consulta.Average(p => p.monto).ToString();

                    }

                    return View(consulta.ToList().ToPagedList(x ?? 1, 3));
                }

                if (BuscarPor == "FechaSalida")
                {
                    var consulta = from a in db.Altas
                                   join p in db.Pacientes on a.Ingresos.paciente equals p.idPaciente
                                   join i in db.Ingresos on a.ingreso equals i.idIngreso
                                   where a.fechaSalida.ToString().Contains(busqueda)
                                   select a;

                    if (Request["opcion"] == "count")
                    {
                        ViewBag.conteo = consulta.Count();

                    }
                    else if (Request["opcion"] == "min")
                    {
                        ViewBag.min = consulta.Select(p => p.Ingresos.fechaIngreso).Min();

                    }
                    else if (Request["opcion"] == "max")
                    {
                        ViewBag.max = consulta.Select(p => p.fechaSalida).Max();

                    }
                    else if (Request["opcion"] == "avg")
                    {
                        ViewBag.avg = consulta.Average(p => p.monto).ToString();

                    }

                    return View(consulta.ToList().ToPagedList(x ?? 1, 3));
                }

                if (BuscarPor== "Paciente")
                {
                    var consulta = from a in db.Altas
                                   join p in db.Pacientes on a.Ingresos.paciente equals p.idPaciente
                                   join i in db.Ingresos on a.ingreso equals i.idIngreso
                                   where p.nombre.Contains(busqueda) 
                                   select a;

                    if (Request["opcion"] == "count")
                    {
                        ViewBag.conteo = consulta.Count();

                    }
                    else if (Request["opcion"] == "min")
                    {
                        ViewBag.min = consulta.Select(p => p.Ingresos.fechaIngreso).Min();

                    }
                    else if (Request["opcion"] == "max")
                    {
                        ViewBag.max = consulta.Select(p => p.fechaSalida).Max();

                    }
                    else if (Request["opcion"] == "avg")
                    {
                        ViewBag.avg = consulta.Average(p => p.monto).ToString();

                    }


                    return View(consulta.ToList().ToPagedList(x ?? 1, 3));
                }
                return View(db.Altas.ToList().ToPagedList(x ?? 1, 3));
            }
            
        }
        // GET: Altas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Altas altas = db.Altas.Find(id);
            if (altas == null)
            {
                return HttpNotFound();
            }
            return View(altas);
        }

       

        // GET: Altas/Create
        public ActionResult Create()
        {

            Altas altas = new Altas();

            ViewBag.ingreso = new SelectList(db.Ingresos, "idIngreso", "idIngreso");

            return View(altas);
        }

        public JsonResult ObtenerRegistros(int code)
        {
            SistemaMedicoDbContext db = new SistemaMedicoDbContext();

            var lst = (from i in db.Ingresos
                       join h in db.Habitaciones on i.habitacion equals h.idHabitacion
                       join p in db.Pacientes on i.paciente equals p.idPaciente
                       where i.idIngreso == code
                       select new { nom = p.nombre, fe = i.fechaIngreso, hab = h.tipo, ing = i.idIngreso,
                           ced =p.cedula, med=i.Medicos.nombre });
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        // POST: Altas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAlta,ingreso,fechaI,fechaSalida,monto")] Altas altas)
        {
            if (ModelState.IsValid)
            {
                string habitacion = altas.tipoH;
                DateTime inicio = altas.fechaI;
                DateTime fin = altas.fechaSalida;

                altas.TrabajoFechas(habitacion,inicio,fin);

                db.Altas.Add(altas);
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }

            ViewBag.ingreso = new SelectList(db.Ingresos, "idIngreso", "idIngreso", altas.ingreso);
            return View(altas);
        }

        // GET: Altas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Altas altas = db.Altas.Find(id);
            if (altas == null)
            {
                return HttpNotFound();
            }
            ViewBag.ingreso = new SelectList(db.Ingresos, "idIngreso", "idIngreso", altas.ingreso);
            return View(altas);
        }

        // POST: Altas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAlta,ingreso,fechaSalida,monto")] Altas altas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(altas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ingreso = new SelectList(db.Ingresos, "idIngreso", "idIngreso", altas.ingreso);
            return View(altas);
        }

        // GET: Altas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Altas altas = db.Altas.Find(id);
            if (altas == null)
            {
                return HttpNotFound();
            }
            return View(altas);
        }

        // POST: Altas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Altas altas = db.Altas.Find(id);
            db.Altas.Remove(altas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
