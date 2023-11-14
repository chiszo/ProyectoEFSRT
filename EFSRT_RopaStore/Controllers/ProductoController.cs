using EFSRT_RopaStore.Repositorio.Interface;
using EFSRT_RopaStore.Repositorio.RepositorioSQL;
using Microsoft.AspNetCore.Mvc;

namespace EFSRT_RopaStore.Controllers
{
    public class ProductoController : Controller
    {
        IProducto _producto;
        ITrabajador _trabajador;
        IProveedor _proveedor;

        public ProductoController()
        {
            _producto = new productoSQL();
            _proveedor = new proveedorSQL();
            _trabajador = new trabajadorSQL();
        }

        public async Task<IActionResult> list(string nom = "")
        {
            ViewBag.nom = nom;

            return View(await Task.Run(() => _producto.GetProductos(nom)));
        }
    }
}
