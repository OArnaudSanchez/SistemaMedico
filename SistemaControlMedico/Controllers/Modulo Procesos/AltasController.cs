using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaControlMedico.Models;

namespace SistemaControlMedico.Controllers.Modulo_Procesos
{
    public class AltasController : Controller
    {
        private SistemaMedicoDbContext db = new SistemaMedicoDbContext();
        
        // GET: Altas
        public ActionResult Index()
        {
            var altas = db.Altas.Include(a => a.Ingresos);
            return View(altas.ToList());
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
