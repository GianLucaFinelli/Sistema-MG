using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdministracionMinisterio.Models;

namespace AdministracionMinisterio.Controllers
{
    public class MinisteriosController : Controller
    {
        private Entities db = new Entities();

        // GET: Ministerios
        public ActionResult Index()
        {
            return View(db.Ministerio.ToList());
        }

        // GET: Ministerios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ministerio ministerio = db.Ministerio.Find(id);
            if (ministerio == null)
            {
                return HttpNotFound();
            }
            return View(ministerio);
        }

        // GET: Ministerios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ministerios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Localidad,Direccion")] Ministerio ministerio)
        {
            if (ModelState.IsValid)
            {
                db.Ministerio.Add(ministerio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ministerio);
        }

        // GET: Ministerios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ministerio ministerio = db.Ministerio.Find(id);
            if (ministerio == null)
            {
                return HttpNotFound();
            }
            return View(ministerio);
        }

        // POST: Ministerios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Localidad,Direccion")] Ministerio ministerio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ministerio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ministerio);
        }

        // GET: Ministerios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ministerio ministerio = db.Ministerio.Find(id);
            if (ministerio == null)
            {
                return HttpNotFound();
            }
            return View(ministerio);
        }

        // POST: Ministerios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ministerio ministerio = db.Ministerio.Find(id);

            var FuncionariosExisten = db.Funcionario.Where(f => f.IdMinisterio == id).Count();
            if(FuncionariosExisten > 0)
            {
                ViewBag.FuncionariosExisten = "Todavia existen funcionarios en el ministerio.. no pdora borrarlo hasta deshabilitar todos.";
                return View(ministerio);
            }

            db.Ministerio.Remove(ministerio);
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
