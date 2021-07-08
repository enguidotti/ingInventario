using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            ViewBag.marca = new SelectList(db.Marca.OrderBy(m => m.nombre), "id_marca", "nombre");//marcas ordenadas ascendente
            ViewBag.categoria = new SelectList(db.Categoria, "id_categoria", "nombre");
            ViewBag.proveedor = new SelectList(db.Proveedor, "id_proveedor", "nombre");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Producto producto)
        {
            try
            {
                db.Producto.Add(producto);
                db.SaveChanges();
                return Json("");
            }
            catch (Exception)
            {

                return Json("Ha ocurrido un error, intentelo más tarde.");
            }
        }
        [HttpPost]
        public JsonResult CodigoExiste(string codigo)
        {
            //select * from producto p where LOWER(p.codigo) = LOWER(codigo)
            var q = db.Producto.FirstOrDefault(p => p.codigo.ToLower().Equals(codigo.ToLower()));
            if (q != null)
            {
                string nombre = q.nombre;
                return Json(nombre);
            }
            return Json("");
        }

        public ActionResult Index()
        {
            //permite llevar al helper de razor(select) la lista de marcas 
            ViewBag.idMarca = new SelectList(db.Marca, "id_marca", "nombre");
            ViewBag.idCategoria = new SelectList(db.Categoria, "id_categoria", "nombre");
            ViewBag.idProveedor = new SelectList(db.Proveedor, "id_proveedor", "nombre");
            return View();
        }

        public ActionResult Edit(int id)
        {
            //select * from producto 
            var producto = db.Producto.Find(id);
            //lista         value          text     opción seleccionada
            ViewBag.proveedor = new SelectList(db.Proveedor, "id_proveedor", "nombre", producto.id_proveedor);
            return PartialView("_Edit", producto);
        }
        [HttpPost]
        public ActionResult Edit(Producto producto)
        {

            db.Entry(producto).State = EntityState.Modified;
            db.SaveChanges();
            return Json("");
        }
        public ActionResult Delete(int id)
        {
            var p = db.Producto.Find(id);
            return PartialView("_Delete",p);
        }
        [HttpPost]
        public ActionResult Eliminar(int id)
        {
                var p = db.Producto.Find(id);
                if (p != null)
                {
                    db.Producto.Remove(p);
                db.SaveChanges();
                    return Json("");
                }
            return Json("No se ha podido eliminar el producto");
        }
        //método para acceder a vista parcial que muestra los productos
        public ActionResult ListaProducto(int? marca, int? categoria, int? proveedor)
        {
            var productos = db.Producto.ToList();    
        
            if(marca != null)
            {
                //SELECT * FROM PRODUCTO p WHERE p.id_marca == 1 && categoria == 1 && proveedor == 1
                productos = productos.Where(p => p.id_marca == marca).ToList();
            }
            if(categoria != null)
            {
                productos = productos.Where(p => p.id_categoria == categoria).ToList();
            }
            if(proveedor != null)
            {
                productos = productos.Where(p => p.id_proveedor == proveedor).ToList();
            }
            return PartialView("_ListaProducto", productos);
        }        
    }
}