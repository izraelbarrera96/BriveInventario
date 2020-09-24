using Microsoft.AspNetCore.Mvc;
using ControlInventario.Data;
using System.Linq;
using ControlInventario.Models;

namespace ControlInventario.Controllers
{
    public class ProductosController : Controller
    {
        private readonly InventarioContext Inventario;
        public ProductosController(InventarioContext context)
        {
            Inventario = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Actualizar()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ConsultarProductos()
        {
            return Json(Inventario.Productos.ToList());
        }

        [HttpPost]
        public JsonResult ModificarProductos(Producto Producto,bool Tipo)
        {
            bool Resultado = false;
            string Mensaje = "";

            try
            {
                bool existeProducto = (Inventario.Productos.Where(p => p.Clave == Producto.Clave).ToList().Count == 1);

                if (Producto.Validar())
                {
                    if (Tipo && !existeProducto)// agregar producto
                    {
                        Inventario.Productos.Add(Producto);
                    }
                    else if(existeProducto) // quitar producto
                    {
                        var pEliminar = Inventario.Productos.Where(p=>p.Clave == Producto.Clave).FirstOrDefault();
                        Inventario.Productos.Remove(pEliminar);
                    }
                    Inventario.SaveChanges();
                    Resultado = true;
                    Mensaje = "Productos actualizados correctamente";
                }
                else
                {
                    Mensaje = "Validar que los datos ingresados son correctos.";
                }
            }
            catch
            {
                Mensaje = "Ocurrio un error al intentar actualizar los productos";
            }

            return Json(new { Resultado, Mensaje });
        }
    }
}

