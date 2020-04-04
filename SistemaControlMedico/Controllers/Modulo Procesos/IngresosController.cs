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
    public class IngresosController : Controller
    {
        private SistemaMedicoDbContext db = new SistemaMedicoDbContext();

        // GET: Ingresos
        public ActionResult Index()
        {
            var ingresos = db.Ingresos.Include(i => i.Habitaciones).Include(i => i.Medicos).Include(i => i.Pacientes);
            return View(ingresos.ToList());
        }

        [HttpPost]
        public ActionResult Index(string busqueda)
        {
            if (busqueda == string.Empty)
            {
                var ingresos = db.Ingresos.Include(i => i.Habitaciones)
                    .Include(i => i.Medicos).Include(i => i.Pacientes);
                return View(ingresos.ToList());
            }
            else
            {
                var consulta = from i in db.Ingresos
                               join h in db.Habitaciones on i.habitacion equals h.idHabitacion
                               where i.fechaIngreso.ToString().Contains(busqueda) || h.tipo.Contains(busqueda)
                               select i;


                return View(consulta);
            }
        }

        // GET: Ingresos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingresos ingresos = db.Ingresos.Find(id);
            if (ingresos == null)
            {
                return HttpNotFound();
            }
            return View(ingresos);
        }

        // GET: Ingresos/Create
        public ActionResult Create()
        {
            ViewBag.habitacion = new SelectList(db.Habitaciones, "idHabitacion", "tipo");
            ViewBag.medico = new SelectList(db.Medicos, "idMedico", "nombre");
            ViewBag.paciente = new SelectList(db.Pacientes, "idPaciente", "cedula");
            return View();
        }

        // POST: Ingresos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idIngreso,paciente,habitacion,medico,fechaIngreso")] Ingresos ingresos)
        {
            if (ModelState.IsValid)
            {
                db.Ingresos.Add(ingresos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.habitacion = new SelectList(db.Habitaciones, "idHabitacion", "tipo", ingresos.habitacion);
            ViewBag.medico = new SelectList(db.Medicos, "idMedico", "nombre", ingresos.medico);
            ViewBag.paciente = new SelectList(db.Pacientes, "idPaciente", "cedula", ingresos.paciente);
            return View(ingresos);
        }

        // GET: Ingresos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingresos ingresos = db.Ingresos.Find(id);
            if (ingresos == null)
            {
                return HttpNotFound();
            }
            ViewBag.habitacion = new SelectList(db.Habitaciones, "idHabitacion", "tipo", ingresos.habitacion);
            ViewBag.medico = new SelectList(db.Medicos, "idMedico", "nombre", ingresos.medico);
            ViewBag.paciente = new SelectList(db.Pacientes, "idPaciente", "cedula", ingresos.paciente);
            return View(ingresos);
        }

        // POST: Ingresos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idIngreso,paciente,habitacion,medico,fechaIngreso")] Ingresos ingresos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingresos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.habitacion = new SelectList(db.Habitaciones, "idHabitacion", "tipo", ingresos.habitacion);
            ViewBag.medico = new SelectList(db.Medicos, "idMedico", "nombre", ingresos.medico);
            ViewBag.paciente = new SelectList(db.Pacientes, "idPaciente", "cedula", ingresos.paciente);
            return View(ingresos);
        }

        // GET: Ingresos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingresos ingresos = db.Ingresos.Find(id);
            if (ingresos == null)
            {
                return HttpNotFound();
            }
            return View(ingresos);
        }

        // POST: Ingresos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingresos ingresos = db.Ingresos.Find(id);
            db.Ingresos.Remove(ingresos);
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
