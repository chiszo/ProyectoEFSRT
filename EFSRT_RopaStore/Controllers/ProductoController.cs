using EFSRT_RopaStore.Repositorio.Interface;
using EFSRT_RopaStore.Repositorio.RepositorioSQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RopaStore.Domain.Entidad;

namespace EFSRT_RopaStore.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        IProducto _producto;
        ITrabajador _trabajador;
        IProveedor _proveedor;
        ILote _lote;
        ICargo _cargo;
        ITipoprod _tipopro;
        IArea _area;
        public ProductoController()
        {
            _producto = new productoSQL();
            _proveedor = new proveedorSQL();
            _trabajador = new trabajadorSQL();
            _area = new areaSQL();
            _cargo = new cargoSQL();
            _lote = new loteSQL();
            _tipopro = new tipoproSQL();
        }

        public async Task<IActionResult> list(string nom = "", int p =0)
        {
            ViewBag.nom = nom;

            IEnumerable<Producto> temporal = _producto.GetProductos();
            IEnumerable<Producto> temporal1 = _producto.GetProductos(nom);
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
            ViewBag.lote = new SelectList(_lote.listado(), "idlote", "descripcion");
            ViewBag.tipopro = new SelectList(_tipopro.listado(),"idtipopro","descripcion");
            ViewBag.proveedor = new SelectList(_proveedor.GetProveedores(), "idproveedor", "empresa");
            return View(await Task.Run(() => new Producto()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producto reg)
        {
            ViewBag.mensaje = _producto.InsertProductos(reg);
            ViewBag.lote = new SelectList(_lote.listado(), "idlote", "descripcion");
            ViewBag.tipopro = new SelectList(_tipopro.listado(), "idtipopro", "descripcion");
            ViewBag.proveedor = new SelectList(_proveedor.GetProveedores(), "idproveedor", "empresa");
            return View(await Task.Run(() => reg));
        }

        public async Task<IActionResult> Edit(string id = "")
        {
            ViewBag.lote = new SelectList(_lote.listado(), "idlote", "descripcion");
            ViewBag.tipopro = new SelectList(_tipopro.listado(), "idtipopro", "descripcion");
            ViewBag.proveedor = new SelectList(_proveedor.GetProveedores(), "idproveedor", "empresa");
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("list");
            Producto reg = _producto.GetProducto(id);
            return View(await Task.Run(() => reg));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Producto reg)
        {
            ViewBag.lote = new SelectList(_lote.listado(), "idlote", "descripcion");
            ViewBag.tipopro = new SelectList(_tipopro.listado(), "idtipopro", "descripcion");
            ViewBag.proveedor = new SelectList(_proveedor.GetProveedores(), "idproveedor", "empresa");
            ViewBag.mensaje = _producto.UpdateProductos(reg);
            return View(await Task.Run(() => reg));

        }

        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("list");
            _producto.DeleteProductos(_producto.GetProducto(id));
            return RedirectToAction("list");
        }
    }
}
