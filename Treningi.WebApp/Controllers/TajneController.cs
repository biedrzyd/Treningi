using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Treningi.WebApp.Controllers
{
    [Authorize(Roles = "wazny, zarzadca")]
    public class TajneController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
