using Microsoft.AspNetCore.Mvc;

namespace InventoryManSys.Controllers
{
    [Route("Categories")]
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
