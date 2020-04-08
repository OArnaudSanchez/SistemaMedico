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
    public class HabitacionesController : Controller
    {
        private SistemaMedicoDbContext db = new SistemaMedicoDbContext();

        // GET: Habitaciones
        public ActionResult Index(int? i)
        {
            return View(db.Habitaciones.ToList().ToPagedList(i ?? 1, 3));
        }

        [HttpPost]
        public ActionResult Index(string BuscarPor, int? i)
        {
            if ( BuscarPor=="")
                return View(db.Habitaciones.ToList().ToPagedList(i ?? 1, 3));
            else
            {
                if (BuscarPor == "DOBLE")
                {
                    var consulta = (from h in db.Habitaciones
                                    where h.tipo.Contains(BuscarPor)
                                    select h);
                    return View(consulta.ToList().ToPagedList(i ?? 1, 6));
                }
                if (BuscarPor== "PRIVADA")
                {
                    var consulta = (from h in db.Habitaciones
                                    where h.tipo.Contains(BuscarPor)
                                    select h);
                    return View(consulta.ToList().ToPagedList(i ?? 1, 6));
                }
                if (BuscarPor== "SUITE")
                {
                    var consulta = (from h in db.Habitaciones
                                    where h.tipo.Contains(BuscarPor)
                                    select h);
                    return View(consulta.ToList().ToPagedList(i ?? 1, 6));
                }
                return View(db.Habitaciones.ToList().ToPagedList(i ?? 1, 3));
            }
            
        }

        // GET: Habitaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitaciones habitaciones = db.Habitaciones.Find(id);
            if (habitaciones == null)
            {
                return HttpNotFound();
            }
            return View(habitaciones);
        }

        // GET: Habitaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Habitaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idHabitacion,numero,tipo,precioDia")] Habitaciones habitaciones)
        {
            if (ModelState.IsValid)
            {
                db.Habitaciones.Add(habitaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(habitaciones);
        }

        // GET: Habitaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitaciones habitaciones = db.Habitaciones.Find(id);
            if (habitaciones == null)
            {
                return HttpNotFound();
            }
            return View(habitaciones);
        }

        // POST: Habitaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idHabitacion,numero,tipo,precioDia")] Habitaciones habitaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(habitaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(habitaciones);
        }

        // GET: Habitaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitaciones habitaciones = db.Habitaciones.Find(id);
            if (habitaciones == null)
            {
                return HttpNotFound();
            }
            return View(habitaciones);
        }

        // POST: Habitaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Habitaciones habitaciones = db.Habitaciones.Find(id);
            db.Habitaciones.Remove(habitaciones);
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
