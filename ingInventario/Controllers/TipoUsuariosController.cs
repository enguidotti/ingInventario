using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ingInventario.Models;

namespace ingInventario.Controllers
{
    public class TipoUsuariosController : Controller
    {
        private inventariodbEntities db = new inventariodbEntities();

        // GET: TipoUsuarios
        public ActionResult Index()
        {
            return View(db.TipoUsuario.ToList());
        }

        // GET: TipoUsuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoUsuario tipoUsuario = db.TipoUsuario.Find(id);
            if (tipoUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tipoUsuario);
        }

        // GET: TipoUsuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoUsuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tipo,nombre")] TipoUsuario tipoUsuario)
        {
            if (ModelState.IsValid)
            {
                db.TipoUsuario.Add(tipoUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoUsuario);
        }

        // GET: TipoUsuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoUsuario tipoUsuario = db.TipoUsuario.Find(id);
            if (tipoUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tipoUsuario);
        }

        // POST: TipoUsuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tipo,nombre")] TipoUsuario tipoUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoUsuario);
        }

        // GET: TipoUsuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoUsuario tipoUsuario = db.TipoUsuario.Find(id);
            if (tipoUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tipoUsuario);
        }

        // POST: TipoUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoUsuario tipoUsuario = db.TipoUsuario.Find(id);
            db.TipoUsuario.Remove(tipoUsuario);
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
