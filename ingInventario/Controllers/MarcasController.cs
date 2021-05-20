using ingInventario.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ingInventario.Controllers
{
    public class MarcasController : Controller
    {
        private inventariodbEntities db = new inventariodbEntities();
        ///método para cargar lista de marcas (get)
        public ActionResult Index()
        {
            //select * from Marca
            var marcas = db.Marca.ToList();
            return View(marcas);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Marca marca)
        {
            if (ModelState.IsValid)
            {
                //guardar en base de datos
                //insert into marca (nombre, descripcion) values ('pepsi', 'bebida')
                db.Marca.Add(marca);
                db.SaveChanges();// guardar cambios
                return View();
            }
            return View(marca);
        }
        public ActionResult Edit(int? id)
        {
            //si crea en una página aparte enviará el error 404
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //buscar el id en la marca: SELECT * FROM Marca WHERE id_marca = id;
            Marca marca = db.Marca.Find(id);//Find busca sólo el id que correspanda 
            //retornar el nombre _Edit, 
            return PartialView("_Edit",marca);
        }
        [HttpPost]
        public ActionResult Edit(Marca marca)
        {
            //edita los datos de la marca seleccionado
            //(update marca set nombre = marca.nombre, descripcion = marca.descripcion where id_marca = marca.id_marca)
            db.Entry(marca).State = EntityState.Modified;
            //guarda los cambios en la base de datos
            db.SaveChanges();
            return RedirectToAction("Index");//retorna la vista Index(lista de marcas)
        }
        public ActionResult Delete(int? id)
        {
            if(id != null)
            {
                var marca = db.Marca.Find(id);//Find, WHERE, First, FirstOrDefault
                return PartialView("_Delete", marca);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id_marca)
        {
            var marca = db.Marca.Find(id_marca);
            //eliminar con entity framework
            db.Marca.Remove(marca);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}