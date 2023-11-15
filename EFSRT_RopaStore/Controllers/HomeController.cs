using EFSRT_RopaStore.Models;
using EFSRT_RopaStore.Repositorio.Interface;
using EFSRT_RopaStore.Repositorio.RepositorioSQL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace EFSRT_RopaStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IProducto _producto;
        ITrabajador _trabajador;
        IProveedor _proveedor;
        ILote _lote;
        ICargo _cargo;
        ITipoprod _tipopro;
        IArea _area;

        public HomeController()
        {
            _producto = new productoSQL();
            _proveedor = new proveedorSQL();
            _trabajador = new trabajadorSQL();
            _area = new areaSQL();
            _cargo = new cargoSQL();
            _lote = new loteSQL();
            _tipopro = new tipoproSQL();
        }

        public IActionResult Index()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            string correo = "";
            if (claimuser.Identity.IsAuthenticated) 
            {
                correo = claimuser.Claims.Where(C=>C.Type==ClaimTypes.Name).
                    Select(c=>c.Value).SingleOrDefault();
            }
            ViewBag.msj=_trabajador.GetUsuario(correo).nombre+" "+ _trabajador.GetUsuario(correo).apellido;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Salir()
        { 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("iniciarsesion","Usuario");
        }
    }
}