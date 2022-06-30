using InventoryManSys.Data;
using Microsoft.AspNetCore.Mvc;
using InventoryManSys.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using InventoryManSys.ViewModels;
using AutoMapper;

namespace InventoryManSys.Controllers
{
    [Route("Warehouse")]
    public class WarehouseController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private readonly IMapper _mapper;

        public WarehouseController(ApplicationDbContext db, IMapper mapper)
        {
            _Db = db;
            _mapper = mapper;
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
        public IActionResult Create([Bind("Name, Location, MaxCapacity")]WarehouseVM warehouseVM)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var warehouse = _mapper.Map<Warehouse>(warehouseVM);

            _Db.Add(warehouse);
            _Db.SaveChanges();

            return RedirectToAction("Index");

            //foreach (var modelState in ViewData.ModelState.Values)
            //{
            //    foreach (ModelError error in modelState.Errors)
            //    {
            //        Console.WriteLine(error.ErrorMessage);
            //    }
            //}

        }

        [HttpGet("Edit")]
        public IActionResult Edit(int? id)
        {
            if(id == null) return BadRequest();

            var warehouse = _Db.Warehouses.Find(id);

            if (warehouse == null) return NotFound();

            return View(warehouse);
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind("Id, Name, Location, MaxCapacity")]WarehouseVM warehouseVM)
        {
            if (!ModelState.IsValid || id == null || id != warehouseVM.Id)
            {
                return BadRequest();
            }

            var warehouse = _mapper.Map<Warehouse>(warehouseVM);
            _Db.Update(warehouse);
            _Db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet("Details")]
        public IActionResult Details(int? id)
        {
            if (id == null) return BadRequest();

            try
            {
                var warehouse = _Db.Warehouses.Find(id);
                var warehouseVM = _mapper.Map<WarehouseVM>(warehouse);
                return View(warehouseVM);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("Delete")]
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var warehouseToDelete = _Db.Warehouses.Find(id);

            if(warehouseToDelete == null) return NotFound();

            return View(warehouseToDelete);
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id, [Bind("Id")]WarehouseVM warehouseVM)
        {
            if(id == null || id != warehouseVM.Id || !ModelState.IsValid) return BadRequest();

            var warehouseToDelete = _Db.Warehouses.Find(id);

            if(warehouseToDelete == null) return NotFound();

            _Db.Warehouses.Remove(warehouseToDelete);           
            _Db.SaveChanges();            

            return RedirectToAction("Index");
        }
    }
}
