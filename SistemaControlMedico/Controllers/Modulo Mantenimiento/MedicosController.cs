using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using SistemaControlMedico.Models;

namespace SistemaControlMedico.Controllers
{
    public class MedicosController : Controller
    {
        private SistemaMedicoDbContext db = new SistemaMedicoDbContext();

        // GET: Medicos
        public ActionResult Index(int ?i)
        {
            return View(db.Medicos.ToList().ToPagedList(i ?? 1 , 3));
        }

        [HttpPost]
        public ActionResult Index(string busqueda, string BuscarPor, int? i)
        {
            if(busqueda == string.Empty || BuscarPor=="")
            return View(db.Medicos.ToList().ToPagedList(i ?? 1, 3));
            else
            {
                if (BuscarPor == "Nombre")
                {
                    var consulta = from m in db.Medicos
                                   where m.nombre.Contains(busqueda)
                                   select m;
                    return View(consulta.ToList().ToPagedList(i ?? 1, 3));
                }
                if (BuscarPor=="Especialidad")
                {
                    var consulta = from m in db.Medicos
                                   where m.especialidad.Contains(busqueda)
                                   select m;
                    return View(consulta.ToList().ToPagedList(i ?? 1, 3));
                }
            }
            return View();
        }


        // GET: Medicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicos medicos = db.Medicos.Find(id);
            if (medicos == null)
            {
                return HttpNotFound();
            }
            return View(medicos);
        }

        // GET: Medicos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medicos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMedico,nombre,exequatur,especialidad")] Medicos medicos)
        {
            if (ModelState.IsValid)
            {
                db.Medicos.Add(medicos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medicos);
        }

        // GET: Medicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicos medicos = db.Medicos.Find(id);
            if (medicos == null)
            {
                return HttpNotFound();
            }
            return View(medicos);
        }

        // POST: Medicos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMedico,nombre,exequatur,especialidad")] Medicos medicos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medicos);
        }

        // GET: Medicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicos medicos = db.Medicos.Find(id);
            if (medicos == null)
            {
                return HttpNotFound();
            }
            return View(medicos);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medicos medicos = db.Medicos.Find(id);
            db.Medicos.Remove(medicos);
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
