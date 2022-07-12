using InventoryManSys.Data;
using InventoryManSys.Models;
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

        public SupplierController(ApplicationDbContext db)
        {
            _Db = db;
        }

        public IActionResult Index()
        {
            var suppliers = _Db.Suppliers.ToList();

            if (suppliers == null) return NotFound();

            return View(suppliers);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Supplier supplier)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
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
                return View(supplier);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, Supplier supplier)
        {
            if (!ModelState.IsValid || id == null || supplier.Id != id) return BadRequest();

            try
            {
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
                return View(supplier);
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
                return View(supplier);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id, Supplier supplier)
        {

            if (!ModelState.IsValid || id == null || supplier.Id != id) return BadRequest();

            try
            {
                var supplierToDelete = _Db.Suppliers.Find(id);
                _Db.Suppliers.Remove(supplierToDelete);
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
