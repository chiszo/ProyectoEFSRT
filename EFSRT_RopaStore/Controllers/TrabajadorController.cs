using EFSRT_RopaStore.Repositorio.Interface;
using Microsoft.AspNetCore.Mvc;
using RopaStore.Domain.Entidad;

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
            ViewBag.nom = nom;

            if (nom == null)
                return View(await Task.Run(() => _trabajador.GetTrabajadores()));

            return View(await Task.Run(() => _trabajador.GetTrabajadores(nom)));
        }

        public async Task<IActionResult> Create()
        {
            return View(await Task.Run(() => new Trabajador()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Trabajador reg)
        {
            ViewBag.mensaje = _trabajador.InsertTrabajador(reg);
            return View(await Task.Run(() => reg));
        }

        public async Task<IActionResult> Edit(string id = "")
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("list");
            Trabajador reg = _trabajador.GetTrabajador(id);
            return View(await Task.Run(() => reg));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Trabajador reg)
        {
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
