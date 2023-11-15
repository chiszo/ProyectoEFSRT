using EFSRT_RopaStore.Repositorio.Interface;
using EFSRT_RopaStore.Repositorio.RepositorioSQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RopaStore.Domain.Entidad;

namespace EFSRT_RopaStore.Controllers
{
    public class ComprarController : Controller
    {
        IProducto _producto;
        ITrabajador _trabajador;
        IProveedor _proveedor;
        ILote _lote;
        ICargo _cargo;
        ITipoprod _tipopro;
        IArea _area;
        public ComprarController()
        {
            _producto = new productoSQL();
            _proveedor = new proveedorSQL();
            _trabajador = new trabajadorSQL();
            _area = new areaSQL();
            _cargo = new cargoSQL();
            _lote = new loteSQL();
            _tipopro = new tipoproSQL();
        }
        public async Task<IActionResult> list(string idproveedor = "", int p = 0)
        {
            ViewBag.idproveedor= new SelectList(_proveedor.GetProveedores(), "idproveedor", "empresa",idproveedor);

            IEnumerable<Producto> temporal = _producto.GetProductos();
            IEnumerable<Producto> temporal1 = _producto.GetProveedor(idproveedor);
            int f = 7;
            int c = temporal.Count();

            int c1= temporal1.Count();
            int pags1 = c1 % f == 0 ? c1 / f : c1 / f + 1;

            int pags = c % f == 0 ? c / f : c / f + 1;
            ViewBag.p = p;
            ViewBag.pags = pags1;
            if (idproveedor == null)
            {
                ViewBag.pags = pags;
                return View(await Task.Run(() => temporal.Skip(f * p).Take(f)));
            }

            return View(await Task.Run(() => temporal1.Skip(f * p).Take(f)));
        }
    }
}
