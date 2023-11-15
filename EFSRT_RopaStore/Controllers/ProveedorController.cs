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
        ILote _lote;
        ICargo _cargo;
        ITipoprod _tipopro;
        IArea _area;

        public ProveedorController()
        {
            _producto = new productoSQL();
            _proveedor = new proveedorSQL();
            _trabajador = new trabajadorSQL();
            _area = new areaSQL();
            _cargo = new cargoSQL();
            _lote = new loteSQL();
            _tipopro = new tipoproSQL();
        }

        public async Task<IActionResult> list(string nom = "", int p=0)
        {
            ViewBag.nom = nom;
            IEnumerable<Proveedor> temporal = _proveedor.GetProveedores();
            IEnumerable<Proveedor> temporal1 = _proveedor.GetProveedores(nom);
            int f = 7;
            int c = temporal.Count();
            int pags = c % f == 0 ? c / f : c / f + 1;
            ViewBag.p = p;
            ViewBag.pags = pags;
            if (nom == null)
                return View(await Task.Run(() => temporal.Skip(f * p).Take(f)));

            return View(await Task.Run(() => temporal1.Skip(f * p).Take(f)));

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
