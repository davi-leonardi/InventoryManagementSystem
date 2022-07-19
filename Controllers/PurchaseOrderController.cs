using AutoMapper;
using InventoryManSys.Data;
using InventoryManSys.Models;
using InventoryManSys.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InventoryManSys.Controllers
{
    [Authorize]
    [Route("PurchaseOrder")]
    public class PurchaseOrderController : Controller
    {

        private readonly ApplicationDbContext _Db;
        private readonly IMapper _mapper;
        private readonly ShoppingCartVM _shoppingCartVM;
        private readonly UserManager<Models.ApplicationUser> _userManager;

        public PurchaseOrderController(ApplicationDbContext db, IMapper mapper, UserManager<Models.ApplicationUser> userManager)
        {
            _Db = db;
            _mapper = mapper;
            _shoppingCartVM = new ShoppingCartVM();
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var products = _Db.Products.ToList();
            var productsVM = new List<ProductVM>();

            foreach (var product in products)
            {
                var prouductVM = _mapper.Map<ProductVM>(product);
                productsVM.Add(prouductVM);
            }

            return View(productsVM.AsEnumerable());
        }

        [HttpPost("AddToCart")]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(ProductVM productVM)
        {

            try
            {
                //Console.WriteLine(ModelState.IsValid);
                //Console.WriteLine(productVM.Id);
                
            }
            catch
            {
                return BadRequest();
            }

            return RedirectToAction("Index");
        }

        [HttpGet("ShoppingCart")]
        public IActionResult ShoppingCart()
        {
            return View();
        }
    }
}
