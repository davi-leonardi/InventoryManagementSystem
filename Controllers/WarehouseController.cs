using InventoryManSys.Data;
using Microsoft.AspNetCore.Mvc;
using InventoryManSys.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using InventoryManSys.ViewModels;

namespace InventoryManSys.Controllers
{
    [Route("Warehouse")]
    public class WarehouseController : Controller
    {
        private readonly ApplicationDbContext _Db;

        public WarehouseController(ApplicationDbContext db)
        {
            _Db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Warehouse> warehouses = _Db.Warehouses;
            return View(warehouses);
        }

        [Route("Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name, Location, MaxCapacity")]WarehouseVM warehouse)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //_Db.Add(warehouse);
            //_Db.SaveChanges();
            Console.WriteLine(warehouse.Name);
            return RedirectToAction("Index");

            //foreach (var modelState in ViewData.ModelState.Values)
            //{
            //    foreach (ModelError error in modelState.Errors)
            //    {
            //        Console.WriteLine(error.ErrorMessage);
            //    }
            //}

        }
    }
}
