using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using SistemaControlMedico.Models;

namespace SistemaControlMedico.Controllers
{
    public class CitasController : Controller
    {
        private SistemaMedicoDbContext db = new SistemaMedicoDbContext();

        // GET: Citas
        public ActionResult Index(int? i)
        {
            var citas = db.Citas.Include(c => c.Medicos).Include(c => c.Pacientes);
            return View(citas.ToList().ToPagedList(i ?? 1, 3));
        }

        [HttpPost]
        public ActionResult Index(string busqueda, string BuscarPor, int? i)
        {
            if (busqueda == string.Empty || BuscarPor == "")
            {
                var citas = db.Citas.Include(c => c.Medicos).Include(c => c.Pacientes);
                return View(citas.ToList().ToPagedList(i ?? 1, 3));
            }
            else
            {
                if (BuscarPor == "Fecha")
                {
                    var consulta = from c in db.Citas
                                   join m in db.Medicos on c.medico equals m.idMedico
                                   join p in db.Pacientes on c.paciente equals p.idPaciente
                                   where c.fecha.ToString().Contains(busqueda)
                                   select c;
                    return View(consulta.ToList().ToPagedList(i ?? 1, 3));
                }
                if (BuscarPor == "Doctor")
                {
                    var consulta = from c in db.Citas
                                   join m in db.Medicos on c.medico equals m.idMedico
                                   join p in db.Pacientes on c.paciente equals p.idPaciente
                                   where m.nombre.Contains(busqueda)
                                   select c;
                    return View(consulta.ToList().ToPagedList(i ?? 1, 3));
                }
                if (BuscarPor == "Paciente")
                {
                    var consulta = from c in db.Citas
                                   join m in db.Medicos on c.medico equals m.idMedico
                                   join p in db.Pacientes on c.paciente equals p.idPaciente
                                   where  p.nombre.Contains(busqueda)
                                   select c;
                    return View(consulta.ToList().ToPagedList(i ?? 1, 3));
                }
               
            }
            return View();
        }

        // GET: Citas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citas citas = db.Citas.Find(id);
            if (citas == null)
            {
                return HttpNotFound();
            }
            return View(citas);
        }

        // GET: Citas/Create
        public ActionResult Create()
        {
            ViewBag.medico = new SelectList(db.Medicos, "idMedico", "nombre");
            ViewBag.paciente = new SelectList(db.Pacientes, "idPaciente", "cedula");
            return View();
        }

        // POST: Citas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCita,paciente,medico,fecha")] Citas citas)
        {
            if (ModelState.IsValid)
            {
                db.Citas.Add(citas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.medico = new SelectList(db.Medicos, "idMedico", "nombre", citas.medico);
            ViewBag.paciente = new SelectList(db.Pacientes, "idPaciente", "cedula", citas.paciente);
            return View(citas);
        }

        // GET: Citas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citas citas = db.Citas.Find(id);
            if (citas == null)
            {
                return HttpNotFound();
            }
            ViewBag.medico = new SelectList(db.Medicos, "idMedico", "nombre", citas.medico);
            ViewBag.paciente = new SelectList(db.Pacientes, "idPaciente", "cedula", citas.paciente);
            return View(citas);
        }

        // POST: Citas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCita,paciente,medico,fecha")] Citas citas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(citas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.medico = new SelectList(db.Medicos, "idMedico", "nombre", citas.medico);
            ViewBag.paciente = new SelectList(db.Pacientes, "idPaciente", "cedula", citas.paciente);
            return View(citas);
        }

        // GET: Citas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citas citas = db.Citas.Find(id);
            if (citas == null)
            {
                return HttpNotFound();
            }
            return View(citas);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Citas citas = db.Citas.Find(id);
            db.Citas.Remove(citas);
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
