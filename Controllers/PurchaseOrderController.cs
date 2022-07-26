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

            try
            {
                var user = _Db.Users.Find(_userManager.GetUserId(HttpContext.User));
                var cart = _Db.ShoppingCarts.Find(user.CartId);
                var product = _Db.Products.Find(Id);

                var duplicated = from p in _Db.CartProducts
                                 where p.CartId == cart.Id && p.ProductId == product.Id
                                 select p;

                if (duplicated.Any())
                {
                    var existingProduct = duplicated.First();
                    existingProduct.Quantity += quantity;
                    existingProduct.TotalPrice += (product.Price * existingProduct.Quantity);
                    _Db.SaveChanges();

                    return RedirectToAction("Index");
                }

                CartProduct cartProduct = new CartProduct();
                cartProduct.Name = product.Name;
                cartProduct.Quantity = quantity;
                decimal TotalPrice = (product.Price * quantity);
                cartProduct.TotalPrice = TotalPrice;
                cartProduct.Product = product;
                cartProduct.ProductId = product.Id;
                cartProduct.CartId = user.CartId;
                _Db.Add(cartProduct);

                cart.TotalPrice += TotalPrice;
                _Db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("ShoppingCart")]
        public IActionResult ShoppingCart()
        {
            try
            {
                var user = _Db.Users.Find(_userManager.GetUserId(HttpContext.User));
                var cart = _Db.ShoppingCarts.Find(user.CartId);

                var cartVM = _mapper.Map<ShoppingCartVM>(cart);
                var cartProducts = _Db.CartProducts.ToList();

                var cartProduct = from p in _Db.CartProducts
                                  where p.CartId == cart.Id
                                  select p;

                cartVM.Products = cartProducts;

                return View(cartVM);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            try
            {
                var cartProductToDelete = _Db.CartProducts.Find(id);
                _Db.CartProducts.Remove(cartProductToDelete);
                _Db.SaveChanges();

                return RedirectToAction("ShoppingCart");
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost("PlaceOrder")]
        [ValidateAntiForgeryToken]
        public IActionResult PlaceOrder(int? id, ShoppingCartVM shoppingCartVM)
        {
            if (id == null || id != shoppingCartVM.Id || !ModelState.IsValid) return BadRequest();

            var order = new Order();
            order.Type = Order.OrderType.Buy;
            order.TotalPrice = shoppingCartVM.TotalPrice;
            order.CreatedDate = DateTime.Now;

            var user = _Db.Users.Find(_userManager.GetUserId(HttpContext.User));
            order.EmployeeId = user.Id;
            order.EmployeeName = user.UserName;

            foreach (var product in shoppingCartVM.Products)
            {
                product.OrderId = order.Id;
            }

            _Db.Add(order);

            var newShoppingCart = new ShoppingCart();
            _Db.Add(newShoppingCart);

            user.Cart = newShoppingCart;
            user.CartId = newShoppingCart.Id;

            _Db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
