using AutoMapper;
using InventoryManSys.Data;
using InventoryManSys.Models;
using InventoryManSys.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace InventoryManSys.Controllers
{
    [Authorize]
    [Route("Supplier")]
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private readonly IMapper _Mapper;

        public SupplierController(ApplicationDbContext db, IMapper mapper)
        {
            _Db = db;
            _Mapper = mapper;
        }

        public IActionResult Index()
        {
            try
            {
                List<Supplier> suppliers = _Db.Suppliers.ToList();
                List<SupplierVM> suppliersVM = new List<SupplierVM>();

                foreach (Supplier supplier in suppliers)
                {
                    var vm = _Mapper.Map<SupplierVM>(supplier);
                    suppliersVM.Add(vm);
                }

                return View(suppliersVM.AsEnumerable());
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SupplierVM supplierVM)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var supplier = _Mapper.Map<Supplier>(supplierVM);
                _Db.Suppliers.Add(supplier);
                _Db.SaveChanges();
            }
            catch
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpGet("Edit")]
        public IActionResult Edit(int? id)
        {
            if (id == null) return BadRequest();

            try
            {
                var supplier = _Db.Suppliers.Find(id);
                var supplierVM = _Mapper.Map<SupplierVM>(supplier);
                return View(supplierVM);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, SupplierVM supplierVM)
        {
            if (!ModelState.IsValid || id == null || supplierVM.Id != id) return BadRequest();

            try
            {
                var supplier = _Mapper.Map<Supplier>(supplierVM);
                _Db.Update(supplier);
                _Db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return NotFound();
            }

        }

        [Route("Details")]
        public IActionResult Details(int? id)
        {
            if (id == null) return BadRequest();

            try
            {
                var supplier = _Db.Suppliers.Find(id);
                var supplierVM = _Mapper.Map<SupplierVM>(supplier);

                var hasProducts = from p in _Db.Products
                                  where p.SupplierId == supplier.Id
                                  select p;

                ViewBag.IsEmpty = (hasProducts.Any()) ? "True" : "False"; 

                return View(supplierVM);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpGet("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null) return BadRequest();

            try
            {
                var supplier = _Db.Suppliers.Find(id);
                var supplierVM = _Mapper.Map<SupplierVM>(supplier);
                return View(supplierVM);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id, SupplierVM supplierVM)
        {

            if (!ModelState.IsValid || id == null || supplierVM.Id != id) return BadRequest();

            try
            {
                var supplier = _Db.Suppliers.Find(id);
                _Db.Suppliers.Remove(supplier);
                _Db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
