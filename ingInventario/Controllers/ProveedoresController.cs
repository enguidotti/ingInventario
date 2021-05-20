using ingInventario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ingInventario.Controllers
{
    public class ProveedoresController : Controller
    {
        private inventariodbEntities db = new inventariodbEntities();
        // GET: Proveedores
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Proveedor proveedor)
        {
            db.Proveedor.Add(proveedor);
            db.SaveChanges();
            return View();
        }

        public ActionResult Index()
        {
            var proveedores = db.Proveedor.ToList();
            return View(proveedores);
        }
    }
}