using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaControlMedico.Models;

namespace SistemaControlMedico.Controllers
{
    public class PruebaController : Controller
    {
        // GET: Prueba
        public ActionResult Index()
        {
            SistemaMedicoDbContext bd = new SistemaMedicoDbContext();

            List<Ingresos> lst;

             lst = (from d in bd.Ingresos
                       select d).ToList();
            return View(lst);
        }

        public JsonResult ObtenerRegistros()
        {
            SistemaMedicoDbContext db = new SistemaMedicoDbContext();

            var lst = (from i in db.Ingresos
                         join h in db.Habitaciones on i.habitacion equals h.idHabitacion
                         join p in db.Pacientes on i.paciente equals p.idPaciente
                         where i.idIngreso == 0
                         select new {nom= p.nombre,fe = i.fechaIngreso,hab = h.tipo,ing = i.idIngreso}).ToList();
            
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
    }
}