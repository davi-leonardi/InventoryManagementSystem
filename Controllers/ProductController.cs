using AutoMapper;
using InventoryManSys.Data;
using InventoryManSys.Models;
using InventoryManSys.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManSys.Controllers
{
    [Authorize]
    [Route("Product")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private readonly IMapper _mapper;

        public ProductController(ApplicationDbContext db, IMapper mapper)
        {
            _Db = db;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            try
            {
                List<Product> products = _Db.Products.ToList();
                List<ProductVM> productsVM = new List<ProductVM>();

                foreach (Product product in products)
                {
                    var vm = _mapper.Map<ProductVM>(product);
                    productsVM.Add(vm);
                }

                return View(productsVM.AsEnumerable());
            }
            catch
            {
                return NotFound();
            }         
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            var productToCreate = new ProductVM();
            var CategoryNames = _Db.Categories
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }).ToList();
            productToCreate.Categories = CategoryNames;

            var SupplierNames = _Db.Suppliers
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }).ToList();
            productToCreate.Suppliers = SupplierNames;

            return View(productToCreate);
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productVM)
        {

            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var product = _mapper.Map<Product>(productVM);
                var category = _Db.Categories.Find(productVM.CategoryId);
                product.Category = category;
                product.CategoryName = category.Name;

                var supplier = _Db.Suppliers.Find(productVM.SupplierId);
                product.Supplier = supplier;
                product.SupplierName = supplier.Name;
                _Db.Products.Add(product);
                _Db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpGet("Edit")]
        public IActionResult Edit(int? id)
        {
            if(id == null) return BadRequest();

            try
            {
                var product = _Db.Products.Find(id);
                var productToEdit = _mapper.Map<ProductVM>(product);

                var CategoryNames = _Db.Categories
                        .Select(i => new SelectListItem
                        {
                            Text = i.Name,
                            Value = i.Id.ToString()
                        }).ToList();
                productToEdit.Categories = CategoryNames;

                var SupplierNames = _Db.Suppliers
                        .Select(i => new SelectListItem
                        {
                            Text = i.Name,
                            Value = i.Id.ToString()
                        }).ToList();
                productToEdit.Suppliers = SupplierNames;

                return View(productToEdit);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, ProductVM productVM)
        {

            if(!ModelState.IsValid || id == null || productVM.Id != id) return BadRequest();

            try
            {
                var product = _mapper.Map<Product>(productVM);

                var category = _Db.Categories.Find(productVM.CategoryId);
                product.Category = category;
                product.CategoryName = category.Name;

                var supplier = _Db.Suppliers.Find(productVM.SupplierId);
                product.Supplier = supplier;
                product.SupplierName = supplier.Name;

                _Db.Update(product);
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
                var product = _Db.Products.Find(id);

                var ToBeDetailed = _mapper.Map<ProductVM>(product);
                var category = _Db.Categories.Find(product.CategoryId);
                var warehouse = _Db.Warehouses.Find(category.WarehouseId);

                ViewBag.Warehouse = warehouse.Name;

                return View(ToBeDetailed);
            }
            catch
            {
                return NotFound();
            }         

        }

        [HttpGet("Delete")]
        public IActionResult Delete(int? id) 
        {
            if(id == null) return BadRequest();

            var product = _Db.Products.Find(id);
            var productToDel = _mapper.Map<ProductVM>(product);

            ViewData["IsEmpty"] = productToDel.Quantity > 0 ? "False" : "True";

            return View(productToDel);
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id, ProductVM productVM)
        {
            if(!ModelState.IsValid || id == null || productVM.Id != id) return BadRequest();

            try
            {
                var productToDel = _Db.Products.Find(id);
                _Db.Remove(productToDel);
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
