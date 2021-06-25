using ingInventario.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            //se crea foreach para recorrer la lista de detalles(DetalleEntrada)
            foreach (var item in detalles)
            {
                //se añade al registro de productos(detalle) el id_orden
                item.id_orden = idDet;
                db.DetalleEntrada.Add(item);
                //actualizar stock del producto
                ActualizaStock(item.id_producto, item.cantidad);
                db.SaveChanges();
            }
            return Json("");
        }
        //método para actualizar el stock existente de producto
        public void ActualizaStock(int id_producto, int cantidad)
        {
            //verifica que el producto existe, según id
            var producto = db.Producto.Find(id_producto);
            //suma la nueva cantidad a la cantidad existente del producto
            producto.cantidad = producto.cantidad + cantidad;//producto.cantidad += cantidad;
            db.Entry(producto).State = EntityState.Modified;
        }
        [HttpPost]
        public ActionResult CodigoExiste(string codigo)
        {
            var q = db.Producto.FirstOrDefault(p => p.codigo.ToLower().Equals(codigo.ToLower()));
            if (q != null)
            {
                return Json(new { q.nombre, q.id_producto }, JsonRequestBehavior.AllowGet);
            }
            return Json("");
        }
    }
}