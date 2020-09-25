using ControlInventario.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlInventario.Models
{
    public class Producto
    {
        [Key]
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        public bool Validar()
        {
            bool bClave = false;
            bool bNombre = false;
            bool bPrecio = false;

            if (!String.IsNullOrEmpty(this.Clave))
            {
                if (this.Clave.Length == 6)
                {
                    bClave = true;
                }
            }

            if (!String.IsNullOrEmpty(this.Nombre))
            {
                bNombre = true;
            }

            bPrecio = (this.Precio >= 0);

            return (bClave && bNombre && bPrecio);
        }
    }

    public class Sucursal
    { 
        [Key]
        public string Clave { get; set; }
        public string Nombre { get; set; }

        public bool Validar()
        {
            bool bClave = false;
            bool bNombre = false;

            if (!String.IsNullOrEmpty(this.Clave))
            {
                if (this.Clave.Length == 3)
                {
                    bClave = true;
                }
            }

            if (!String.IsNullOrEmpty(this.Nombre))
            {
                bNombre = true;
            }

            return (bClave && bNombre);
        }
    }

    public class Existencia
    { 
        [Key]
        public int ID { get; set; } 
        public string Sucursal { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public bool Validar()
        {
            bool bSucursal = false;
            bool bProducto = false;
            bool bCantidad = false;

            if (!String.IsNullOrEmpty(this.Sucursal)) 
            {            
                if (this.Sucursal.Length == 3) 
                {
                    bSucursal = true;
                }
            }

            if (!String.IsNullOrEmpty(this.Producto))
            {
                if (this.Producto.Length == 6)
                {
                    bProducto = true;
                }
            }

            bCantidad = (this.Cantidad > 0);

            return (bSucursal && bProducto && bCantidad);
        }
    }
}
