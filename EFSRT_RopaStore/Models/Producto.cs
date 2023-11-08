using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RopaStore.Domain.Entidad
{
    public class Producto
    {
        [Required, Display(Name = "Código"), RegularExpression("^[P][0-9]{3}$")] public string idproducto { get; set; }
        [Required, Display(Name = "Tipo")] public string idtipopro { get; set; }
        [Required, Display(Name = "Proveedor"), RegularExpression("^[A][0-9]{3}$")] public string idproveedor { get; set; }
        [Required, Display(Name = "Nombre"), RegularExpression("^[A-Za-zñÑáéíóúÁÉÍÓÚ\\s]{3,40}$")] public string nombre { get; set; }
        [Required, Display(Name = "Cant. actual")] public int cantidad { get; set; }
        [Required, Display(Name = "Precio"), RegularExpression("^[0-9.]+[^,]$")] public decimal precio { get; set; }
        [Required, Display(Name = "Stock min.")] public int stockmin { get; set; }
        [Required, Display(Name = "Stock max.")] public int stockmax { get; set; }
        [Required, Display(Name = "Lote")] public string idlote { get; set; }
        [Required, Display(Name = "Estado")] public Boolean estado { get; set; }
    }
}