using ingInventario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ingInventario.Controllers
{
    public class HomeController : Controller
    {
        private inventariodbEntities db = new inventariodbEntities();
        // GET: Home
        public ActionResult Home()
        {
            ViewBag.ordenes= db.OrdenEntrada.Count();
            ViewBag.totalProductos = db.DetalleEntrada.Sum(d => d.cantidad);
            //ViewBag.productos = db.Producto.Select(p => new SelectListItem {
            //    p.nombre,
            //    p.cantidad
            //});
            ViewBag.productos = (from p in db.Producto select new { 
                p.nombre,
                p.cantidad
            }).ToList();
            return View();
        }
    }
}