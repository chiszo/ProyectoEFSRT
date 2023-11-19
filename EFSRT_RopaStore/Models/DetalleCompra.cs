using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RopaStore.Domain.Entidad
{
    public class DetalleCompra
    {
        public string idproveedor { get; set; }
        public string idproducto { get; set; }
        public decimal preciocompra { get; set; }
        public int cantidad { get; set; }
        public decimal monto { get { return preciocompra * cantidad; } }
    }
}