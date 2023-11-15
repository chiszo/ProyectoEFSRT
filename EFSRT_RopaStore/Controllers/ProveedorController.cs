using EFSRT_RopaStore.Repositorio.Interface;
using EFSRT_RopaStore.Repositorio.RepositorioSQL;
using Microsoft.AspNetCore.Mvc;
using RopaStore.Domain.Entidad;

namespace EFSRT_RopaStore.Controllers
{
    public class ProveedorController : Controller
    {
        IProducto _producto;
        ITrabajador _trabajador;
        IProveedor _proveedor;

        public ProveedorController()
        {
            _producto = new productoSQL();
            _proveedor = new proveedorSQL();
            _trabajador = new trabajadorSQL();
        }

        public async Task<IActionResult> list(string nom = "")
        {
            ViewBag.nom = nom;
            if (nom == null)
                return View(await Task.Run(() => _proveedor.GetProveedores()));
            else
            return View(await Task.Run(() => _proveedor.GetProveedores(nom)));
        }


        public async Task<IActionResult> Create()
        {
            return View(await Task.Run(() => new Proveedor()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Proveedor reg)
        {
            ViewBag.mensaje = _proveedor.InsertProveedor(reg);
            return View(await Task.Run(() => reg));
        }

        public async Task<IActionResult> Edit(string id = "")
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("list");
            Proveedor reg = _proveedor.GetProveedor(id);
            return View(await Task.Run(() => reg));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Proveedor reg)
        {
            ViewBag.mensaje = _proveedor.UpdateProveedor(reg);
            return View(await Task.Run(() => reg));

        }

        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("list");
            _proveedor.DeleteProveedor(_proveedor.GetProveedor(id));
            return RedirectToAction("list");
        }
    }
}
