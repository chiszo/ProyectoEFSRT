using EFSRT_RopaStore.Repositorio.Interface;
using EFSRT_RopaStore.Repositorio.RepositorioSQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RopaStore.Domain.Entidad;

namespace EFSRT_RopaStore.Controllers
{
    public class TrabajadorController : Controller
    {
        IProducto _producto;
        ITrabajador _trabajador;
        IProveedor _proveedor;
        ILote _lote;
        ICargo _cargo;
        ITipoprod _tipopro;
        IArea _area;

        public TrabajadorController()
        {
            _producto = new productoSQL();
            _proveedor = new proveedorSQL();
            _trabajador = new trabajadorSQL();
            _area = new areaSQL();
            _cargo = new cargoSQL();
            _lote = new loteSQL();
            _tipopro = new tipoproSQL();
        }

        public async Task<IActionResult> list(string nom = "")
        {
            ViewBag.nom = nom;

            if (nom == null)
                return View(await Task.Run(() => _trabajador.GetTrabajadores()));

            return View(await Task.Run(() => _trabajador.GetTrabajadores(nom)));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.area = new SelectList(_area.listado(), "idarea", "descripcion");
            ViewBag.cargo = new SelectList(_cargo.listado(), "idcargo", "descripcion");
            return View(await Task.Run(() => new Trabajador()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Trabajador reg)
        {
            ViewBag.area = new SelectList(_area.listado(), "idarea", "descripcion");
            ViewBag.cargo = new SelectList(_cargo.listado(), "idcargo", "descripcion");
            ViewBag.mensaje = _trabajador.InsertTrabajador(reg);
            return View(await Task.Run(() => reg));
        }

        public async Task<IActionResult> Edit(string id = "")
        {
            ViewBag.area = new SelectList(_area.listado(), "idarea", "descripcion");
            ViewBag.cargo = new SelectList(_cargo.listado(), "idcargo", "descripcion");
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("list");
            Trabajador reg = _trabajador.GetTrabajador(id);
            return View(await Task.Run(() => reg));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Trabajador reg)
        {
            ViewBag.area = new SelectList(_area.listado(), "idarea", "descripcion");
            ViewBag.cargo = new SelectList(_cargo.listado(), "idcargo", "descripcion");
            ViewBag.mensaje = _trabajador.UpdateTrabajador(reg);
            return View(await Task.Run(() => reg));

        }

        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("list");
            _trabajador.DeleteTrabajador(_trabajador.GetTrabajador(id));
            return RedirectToAction("list");
        }
    }
}
