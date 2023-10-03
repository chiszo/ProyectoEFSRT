using Microsoft.AspNetCore.Mvc;

namespace EFSRT_RopaStore.Controllers
{
    public class RopaStoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
