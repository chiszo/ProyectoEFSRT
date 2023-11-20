using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RopaStore.Domain.Entidad
{
    public class DetalleCompra
    {
        [Required, Display(Name = "Código")]public string codcomprapro { get; set; }
        [Required, Display(Name = "ID Proveedor")]public string idproveedor { get; set; }
        [Required, Display(Name = "ID Producto")]public string idproducto { get; set; }
        [Required, Display(Name = "Precio")]public decimal preciocompra { get; set; }
        [Required, Display(Name = "Cantidad")]public int cantidad { get; set; }
        [Required, Display(Name = "Monto")]public decimal monto { get { return preciocompra * cantidad; } set { } }
    }
}