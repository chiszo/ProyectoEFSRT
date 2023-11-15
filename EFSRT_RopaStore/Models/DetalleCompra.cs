using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RopaStore.Domain.Entidad
{
    public class DetalleCompra
    {
        public string codcomprapro { get; set; }
        public string idproducto { get; set; }
        public int cantidad { get; set; }
    }
}