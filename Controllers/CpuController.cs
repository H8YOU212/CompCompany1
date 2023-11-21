using Microsoft.AspNetCore.Mvc;

namespace CompCompany1.Controllers
{
    public class CpuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
