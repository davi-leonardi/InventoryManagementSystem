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
        private readonly UserManager<Models.ApplicationUser> _userManager;

        public PurchaseOrderController(ApplicationDbContext db, IMapper mapper, UserManager<Models.ApplicationUser> userManager)
        {
            _Db = db;
            _mapper = mapper;
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

            return View(productsVM);
        }

        [HttpPost("AddToCart")]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int Id, int quantity)
        {

            //try
            //{
            var product = _Db.Products.Find(Id);
            CartProduct cartProduct = new CartProduct();
            cartProduct.Name = product.Name;
            cartProduct.Quantity = quantity;
            decimal TotalPrice = (product.Price * quantity);
            cartProduct.TotalPrice = TotalPrice;
            cartProduct.Product = product;
            cartProduct.ProductId = product.Id;
            _Db.Add(cartProduct);

                var user = _Db.Users.Find(_userManager.GetUserId(HttpContext.User));
                var cart = _Db.ShoppingCarts.Find(user.CartId);
                cart.Products.Add(cartProduct);
                cart.TotalPrice += TotalPrice;
                _Db.SaveChanges();
            
            //}
            //catch
            //{
            //    return BadRequest();
            //}

            return RedirectToAction("Index");
        }

        [HttpGet("ShoppingCart")]
        public IActionResult ShoppingCart()
        {
            return View();
        }
    }
}
