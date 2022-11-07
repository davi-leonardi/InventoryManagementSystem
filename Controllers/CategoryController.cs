using AutoMapper;
using InventoryManSys.Data;
using InventoryManSys.Models;
using InventoryManSys.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManSys.Controllers
{
    [Authorize]
    [Route("Category")]
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _Db;
        private readonly IMapper _mapper;

        public CategoryController(ApplicationDbContext db, IMapper mapper)
        {
            _Db = db;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _Db.Categories;
            return View(categories);
        }

        [Route("Details")]
        public IActionResult Details(int? id)
        {
            if (id == null) return BadRequest();

            var ToBeDetailed = _Db.Categories.Find(id);

            if(ToBeDetailed == null) return NotFound();

            return View(ToBeDetailed);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            var categoryToBeCreated = new CategoryVM();
            var wareHouseNames = _Db.Warehouses
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }).ToList();
            categoryToBeCreated.Warehouses = wareHouseNames;
            return View(categoryToBeCreated);
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name, Units, WarehouseId")]CategoryVM categoryVM)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var category = _mapper.Map<Category>(categoryVM);
                var warehouse = _Db.Warehouses.Find(categoryVM.WarehouseId);

                if (warehouse == null) return NotFound();

                category.WarehouseName = warehouse.Name;
                category.Warehouse = warehouse;

                _Db.Categories.Add(category);            
                _Db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("Edit")]
        public ActionResult Edit(int? id)
        {
            if(id == null) return BadRequest();

            var category = _Db.Categories.Find(id);

            if (category == null) return NotFound();

            var categoryVM = _mapper.Map<CategoryVM>(category);

            categoryVM.Warehouses = _Db.Warehouses
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }).ToList();


            return View(categoryVM);
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, CategoryVM categoryVM)
        {

            if (!ModelState.IsValid || id == null || id != categoryVM.Id)
            {
                return BadRequest();
            }

            try
            {
                var warehouse = _Db.Warehouses.Find(categoryVM.WarehouseId);

                if (warehouse == null) return NotFound();

                categoryVM.WarehouseName = warehouse.Name;
                var category = _mapper.Map<Category>(categoryVM);
                _Db.Update(category);
                _Db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("Delete")]
        public ActionResult Delete(int? id)
        {
            if(id == null) return BadRequest();

            var ToBeDeleted = _Db.Categories.Find(id);

            ViewData["IsEmpty"] = ToBeDeleted.Units > 0 ? "False" : "True";            

            if (ToBeDeleted == null) return NotFound();

            return View(ToBeDeleted);
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id, CategoryVM categoryVM)
        {
            if (id==null || !ModelState.IsValid || id != categoryVM.Id) return BadRequest();

            try
            {
                var categoryToDelete = _Db.Categories.Find(id);
                _Db.Remove(categoryToDelete);
                _Db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
