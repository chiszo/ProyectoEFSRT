using EFSRT_RopaStore.Repositorio.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EFSRT_RopaStore.Controllers
{
    public class ProveedorController : Controller
    {
        IProducto _producto;
        ITrabajador _trabajador;
        IProveedor _proveedor;

        public async Task<IActionResult> list(string nom = "")
        {
            ViewBag.nom = nom;

            return View(await Task.Run(() => _proveedor.GetProveedores(nom)));
        }
    }
}
