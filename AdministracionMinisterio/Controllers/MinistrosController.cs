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
    public class MinistrosController : Controller
    {
        private Entities db = new Entities();

        // GET: Ministros
        public ActionResult Index()
        {
            var ministro = db.Ministro.Include(m => m.Funcionario);
            return View(ministro.ToList());
        }

        // GET: Ministros/Create
        public ActionResult Create()
        {
            ListaMinistros();
            return View();
        }

        // POST: Ministros/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdFuncionario,FechaDeIngreso,FechaDeEgreso,MinistroAnterior")] Ministro ministro)
        {
            if (ministro.IdFuncionario == 0 && ministro.Funcionario == null)
            {
                ViewBag.funcionarioNotFound = "Porfavor.. ingrese un funcionario..";
                return View(ministro);
            }

            if (ModelState.IsValid)
            {
                db.Ministro.Add(ministro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ListaMinistros();
            return View(ministro);
        }

        // GET: Ministros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ministro ministro = db.Ministro.Find(id);
            if (ministro == null)
            {
                return HttpNotFound();
            }
            ListaMinistros();
            return View(ministro);
        }

        // POST: Ministros/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdFuncionario,FechaDeIngreso,FechaDeEgreso,MinistroAnterior")] Ministro ministro)
        {
            if (ModelState.IsValid)
            {
               var editMinistro = db.Ministro.Find(ministro.Id);
                editMinistro.FechaDeEgreso = ministro.FechaDeEgreso;
                editMinistro.FechaDeIngreso = ministro.FechaDeIngreso;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ministro);
        }

        // GET: Ministros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ministro ministro = db.Ministro.Find(id);
            if (ministro == null)
            {
                return HttpNotFound();
            }
            return View(ministro);
        }

        // POST: Ministros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ministro ministro = db.Ministro.Find(id);
            db.Ministro.Remove(ministro);
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

        // ----------------------------

        public void ListaMinistros()
        {
            var dbFuncionarios = db.Funcionario.Where(f => f.Ministro.FirstOrDefault().FechaDeIngreso == null && f.Ministro.FirstOrDefault().FechaDeEgreso == null).ToList();
            ViewBag.IdFuncionario = new SelectList(dbFuncionarios, "Id", "Nombre");
        }
    }
}
