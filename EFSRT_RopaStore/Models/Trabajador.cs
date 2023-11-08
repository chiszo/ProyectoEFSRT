using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RopaStore.Domain.Entidad
{
    public class Trabajador
    {
        [Required, Display(Name = "Código"), RegularExpression("^[T][0-9]{3}$")] public string idtrabajador { get; set; }
        [Required, Display(Name = "Nombres"), RegularExpression("^[A-Za-zñÑáéíóúÁÉÍÓÚ\\s]{3,30}$")] public string nombre { get; set; }
        [Required, Display(Name = "Apellidos"), RegularExpression("^[A-Za-zñÑáéíóúÁÉÍÓÚ\\s]{3,30}$")] public string apellido { get; set; }
        [Required, Display(Name = "DNI"), RegularExpression("^[0-9]{8}$")] public string dni { get; set; }
        [Required, Display(Name = "Teléfono"), RegularExpression("^[9][0-9]{8}$")] public string telefono { get; set; }
        [Required, Display(Name = "Email"), DataType(DataType.EmailAddress)] public string correo { get; set; }
        [Required, Display(Name = "Direccion Trabajador")] public string direccion { get; set; }
        [Required, Display(Name = "Cargo")] public string idcargo { get; set; }
        [Required, Display(Name = "Area")] public string idarea { get; set; }
        [Required, Display(Name = "Contraseña")] public string contrasena { get; set; }
    }
}