using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RopaStore.Domain.Entidad
{
    public class CompraProducto
    {
        public string codcomprapro { get; set; }
        public DateTime fechapedido { get; set; }
        public string idproveedor { get; set; }
        public decimal montoT { get; set; }
    }
}