using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RopaStore.Domain.Entidad
{
    public class CompraProducto
    {
        [Required, Display(Name = "Código")]public string codcomprapro { get; set; }
        [Required, Display(Name = "Fecha de compra")] public DateTime fechapedido { get; set; }
        [Required, Display(Name = "Proveedor")] public string idproveedor { get; set; }
        [Required, Display(Name = "Monto total")] public decimal montoT { get; set; }
    }
}