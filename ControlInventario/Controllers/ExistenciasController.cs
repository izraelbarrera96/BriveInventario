using Microsoft.AspNetCore.Mvc;
using ControlInventario.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using ControlInventario.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace ControlInventario.Controllers
{
    public class ExistenciasController : Controller
    {
        private readonly InventarioContext Inventario;
        public ExistenciasController(InventarioContext context)
        {
            Inventario = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Actualizar()
        {
            ViewBag.SelectProductos = new SelectList(Inventario.Productos.AsEnumerable(), "Clave", "Nombre");
            ViewBag.SelectSucursal = new SelectList(Inventario.Sucursales.AsEnumerable(), "Clave", "Nombre");

            return View();
        }

        [HttpGet]
        public JsonResult ConsultarExistencias()
        {
            var Existencias = (
                         from existencia in Inventario.Existencias
                         join producto in Inventario.Productos on existencia.Producto equals producto.Clave
                         join sucursal in Inventario.Sucursales on existencia.Sucursal equals sucursal.Clave
                         select new
                         {
                             ClaveSucursal = existencia.Sucursal,
                             NombreSucursal = sucursal.Nombre,
                             Producto = producto.Nombre,
                             Clave = producto.Clave,
                             Cantidad = existencia.Cantidad,
                             Precio = producto.Precio
                         }).ToList();

            return Json(Existencias);
        }

        [HttpPost]
        public JsonResult ModificarExistencia(Existencia Articulo,bool Tipo)
        {
            bool Resultado = false;
            string Mensaje = "";

            try
            {
                bool existeProducto = (Inventario.Productos.Where(p => p.Clave == Articulo.Producto).ToList().Count == 1);
                bool existeSucursal = (Inventario.Sucursales.Where(s => s.Clave == Articulo.Sucursal).ToList().Count == 1);

                if (Articulo.Validar() && existeProducto && existeSucursal)
                {
                    var eProducto = Inventario.Existencias.Where(e => e.Producto == Articulo.Producto && e.Sucursal == Articulo.Sucursal).SingleOrDefault();
                    if (Tipo)// aumentar cantidad
                    {
                        eProducto.Cantidad += Articulo.Cantidad;
                    }
                    else // disminuir cantidad
                    {
                        eProducto.Cantidad -= Articulo.Cantidad;
                    }
                    Inventario.Existencias.Update(eProducto);
                    Inventario.SaveChanges();
                    Resultado = true;
                    Mensaje = "Articulo actualizado correctamente";
                }
                else {
                    Mensaje = "Validar que los datos ingresados son correctos y pertenescan a articulos en el inventario.";
                }
            }
            catch
            {
                Mensaje = "Ocurrio un error al intentar actualizar el inventario";
            }

            return Json(new { Resultado, Mensaje });
        }

    }
}
