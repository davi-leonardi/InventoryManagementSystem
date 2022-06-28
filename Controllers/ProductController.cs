using AutoMapper;
using InventoryManSys.Data;
using InventoryManSys.Models;
using InventoryManSys.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManSys.Controllers
{
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
            IEnumerable<Product> products = _Db.Products;
            return View(products);
        }

        [Route("Details")]
        public IActionResult Details(int? id)
        {
            if (id == null) return BadRequest();

            var product = _Db.Products.Find(id);
            Console.WriteLine(product.Category);

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
