using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ingInventario.Models;

namespace ingInventario.Controllers
{
    public class ProductosController : Controller
    {
        private inventariodbEntities db = new inventariodbEntities();
        // GET: Productos
        public ActionResult Create()
        {
            //lista para mostar en el select
            ViewBag.marca = new SelectList(db.Marca,"id_marca","nombre");
            ViewBag.categoria = new SelectList(db.Categoria, "id_categoria", "nombre");
            ViewBag.proveedor = new SelectList(db.Proveedor, "id_proveedor", "nombre");
            return View();
        }
    }
}