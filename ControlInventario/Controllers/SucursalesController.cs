using Microsoft.AspNetCore.Mvc;
using ControlInventario.Data;
using ControlInventario.Models;
using System.Linq;

namespace ControlInventario.Controllers
{
    public class SucursalesController : Controller
    {
        private readonly InventarioContext Inventario;
        public SucursalesController(InventarioContext context)
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
        public JsonResult ConsultarSucursales()
        {
            return Json(Inventario.Sucursales.ToList());
        }

        [HttpPost]
        public JsonResult ModificarSucursales(Sucursal Sucursal,bool Tipo)
        {
            bool Resultado = false;
            string Mensaje = "";

            try
            {
                bool existeSucursal = (Inventario.Sucursales.Where(s => s.Clave == Sucursal.Clave).ToList().Count == 1);

                if (Sucursal.Validar())
                {
                    if (Tipo && !existeSucursal)// agragar sucursal
                    {
                        Inventario.Sucursales.Add(Sucursal);
                    }
                    else if(existeSucursal) // eliminar sucursal
                    {
                        var sEliminar = Inventario.Sucursales.Where(s => s.Clave == Sucursal.Clave).FirstOrDefault();
                        Inventario.Sucursales.Remove(sEliminar);
                    }
                    Inventario.SaveChanges();
                    Resultado = true;
                    Mensaje = "Sucursales actualizadas correctamente";
                }
                else
                {
                    Mensaje = "Validar que los datos ingresados son correctos.";
                }
            }
            catch
            {
                Mensaje = "Ocurrio un error al intentar actualizar las Sucursales";
            }

            return Json(new { Resultado, Mensaje });
        }
    }
}

