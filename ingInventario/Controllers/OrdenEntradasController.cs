using ingInventario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ingInventario.Controllers
{
    public class OrdenEntradasController : Controller
    {
        private inventariodbEntities db = new inventariodbEntities();
        // GET: OrdenEntradas
        public ActionResult OrdenEntrada()
        {
            return View();
        }
        [HttpPost]
        public JsonResult OrdenEntrada(OrdenEntrada orden, List<DetalleEntrada> detalles)
        {
            //primer necesitamos gurdar orden de entrada
            orden.fecha = DateTime.Now;//añade la fecha de hoy a OrdenEntrada
            orden.id_user = 1;//añade el id al usuario que esta en base
            db.OrdenEntrada.Add(orden);
            db.SaveChanges();
            //capturar el id_orden autoincrementable
            var idDet = orden.id_orden;
            //continua con guardar los detalles
            return Json("");
        }
        [HttpPost]
        public ActionResult CodigoExiste(string codigo)
        {
            var q = db.Producto.FirstOrDefault(p => p.codigo.ToLower().Equals(codigo.ToLower()));
            if (q != null)
            {                
                return Json(new { q.nombre, q.id_producto },JsonRequestBehavior.AllowGet);
            }
            return Json("");
        }
    }
}