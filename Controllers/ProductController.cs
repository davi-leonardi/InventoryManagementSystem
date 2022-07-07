using AutoMapper;
using InventoryManSys.Data;
using InventoryManSys.Models;
using InventoryManSys.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
                    var productVM = _mapper.Map<ProductVM>(product);
                    var category = _Db.Categories.Find(product.CategoryId);
                    productVM.CategoryName = category.Name;
                    productsVM.Add(productVM);
                }

                return View(productsVM.AsEnumerable());
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

            var product = _Db.Products.Find(id);

            try
            {
                var ToBeDetailed = _mapper.Map<ProductVM>(product);
                var category = _Db.Categories.Find(product.CategoryId);
                var supplier = _Db.Suppliers.Find(product.SupplierId);

                ToBeDetailed.CategoryName = category.Name;
                ToBeDetailed.SupplierName = supplier.Name;

                return View(ToBeDetailed);

            }
            catch
            {
                return NotFound();
            }         

        }

    }
}
