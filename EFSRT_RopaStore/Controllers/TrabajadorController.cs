using EFSRT_RopaStore.Repositorio.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EFSRT_RopaStore.Controllers
{
    public class TrabajadorController : Controller
    {
        IProducto _producto;
        ITrabajador _trabajador;
        IProveedor _proveedor;
        public async Task<IActionResult> list(string nom = "")
        {
            ViewBag.nom = nom;

            return View(await Task.Run(() => _trabajador.GetTrabajadores(nom)));
        }
    }
}
