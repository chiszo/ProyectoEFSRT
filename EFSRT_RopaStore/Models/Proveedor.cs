using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RopaStore.Domain.Entidad
{
    public class Proveedor
    {
        [Required, Display(Name = "Código"), RegularExpression("^[A][0-9]{3}$")] public string idproveedor { get; set; }
        [Required, Display(Name = "Teléfono"), RegularExpression("^[9][0-9]{8}$")] public string telefono { get; set; }
        [Required, Display(Name = "Direccion Provedor")] public string direccion { get; set; }
        [Required, Display(Name = "Nombre empresa"), RegularExpression("^[A-Za-zñÑáéíóúÁÉÍÓÚ\\s]{3,20}$")] public string empresa { get; set; }
        [Required, Display(Name = "Ruc"), RegularExpression("^[2][0-9]{10}$")] public string ruc { get; set; }
        [Required, Display(Name = "Email"), DataType(DataType.EmailAddress)] public string correo { get; set; }
        [Required, Display(Name = "Nombre del representante"), RegularExpression("^[A-Za-zñÑáéíóúÁÉÍÓÚ\\s]{3,60}$")] public string representante { get; set; }
    }
}