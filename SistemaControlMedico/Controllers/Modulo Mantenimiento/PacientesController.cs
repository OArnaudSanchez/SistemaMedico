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
    public class PacientesController : Controller
    {
        private SistemaMedicoDbContext db = new SistemaMedicoDbContext();

        // GET: Pacientes
        public ActionResult Index(int? i)
        {
            return View(db.Pacientes.ToList().ToPagedList(i ?? 1, 3));
        }

        [HttpPost]
        public ActionResult Index(string busqueda, string buscarPor, int? i)
        {
            try
            {
                if (busqueda == string.Empty && buscarPor == "")
                    return View(db.Pacientes.ToList().ToPagedList(i ?? 1, 3));
                else
                {
                    if (buscarPor == "SI" || buscarPor == "NO")
                    {
                        var consulta = from p in db.Pacientes
                                       where p.asegurado.Contains(buscarPor)
                                       select p;


                        return View(consulta.ToList().ToPagedList(i ?? 1, 3));
                    }
                    if (buscarPor == "SI" || buscarPor == "NO" && busqueda == string.Empty)
                    {
                        var consulta = from p in db.Pacientes
                                       where p.asegurado.Contains(buscarPor)
                                       select p;


                        return View(consulta.ToList().ToPagedList(i ?? 1, 3));
                    }
                    if (buscarPor == "Nombre")
                    {
                        var consulta = from p in db.Pacientes
                                       where p.nombre.Contains(busqueda)
                                       select p;
                        return View(consulta.ToList().ToPagedList(i ?? 1, 3));
                    }
                    if (buscarPor == "Cedula")
                    {
                        var consulta = from p in db.Pacientes
                                       where p.cedula.Contains(busqueda)
                                       select p;
                        return View(consulta.ToList().ToPagedList(i ?? 1, 3));
                    }

                    return View(db.Pacientes.ToList().ToPagedList(i ?? 1, 3));
                }
            }
            catch (FormatException ex)
            {
                return RedirectToAction("Index");
            }
            
            
        }
        // GET: Pacientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // GET: Pacientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPaciente,cedula,nombre,asegurado")] Pacientes pacientes)
        {
            try
            {
                int id = pacientes.idPaciente;
                var opcion = db.Pacientes.Where(x => x.idPaciente == id).Count();

                if (ModelState.IsValid)
                {
                    db.Pacientes.Add(pacientes);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.opcionID = opcion;
                return View(pacientes);
            }
            catch (Exception ex)
            {
                return Content("Ya existe la Cedula que quiere agregar");
            }
            
        }

        // GET: Pacientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // POST: Pacientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPaciente,cedula,nombre,asegurado")] Pacientes pacientes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(pacientes).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(pacientes);
            }
            catch (Exception ex)
            {
                return View("Edit");
            }
            
        }

        // GET: Pacientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Pacientes pacientes = db.Pacientes.Find(id);
                db.Pacientes.Remove(pacientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Delete");
            }
            
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
