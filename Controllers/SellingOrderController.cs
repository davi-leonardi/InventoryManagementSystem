using AutoMapper;
using InventoryManSys.Data;
using InventoryManSys.Models;
using InventoryManSys.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManSys.Controllers
{
    [Authorize]
    [Route("SellingOrder")]
    public class SellingOrderController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private readonly IMapper _mapper;
        private readonly UserManager<Models.ApplicationUser> _userManager;

        public SellingOrderController(ApplicationDbContext db, IMapper mapper, UserManager<Models.ApplicationUser> userManager)
        {
            _Db = db;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var products = from p in _Db.Products
                           where p.Quantity > 0
                           select p;

            var productsVM = new List<ProductVM>();

            foreach (var product in products.ToList())
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
                var cart = _Db.SellingCarts.Find(user.SCartId);
                var product = _Db.Products.Find(Id);

                var duplicated = from p in _Db.SCartProducts
                                 where p.CartId == cart.Id && p.ProductId == product.Id
                                 select p;

                if (duplicated.Any())
                {
                    var existingProduct = duplicated.First();
                    existingProduct.Quantity += quantity;
                    existingProduct.TotalPrice += (product.Price * existingProduct.Quantity);
                    product.Quantity -= quantity;
                    _Db.Update(product);
                    _Db.SaveChanges();

                    return RedirectToAction("Index");
                }

                SCartProduct cartProduct = new SCartProduct();
                cartProduct.Name = product.Name;
                cartProduct.Quantity = quantity;
                decimal TotalPrice = (product.Price * quantity);
                cartProduct.TotalPrice = TotalPrice;
                cartProduct.Product = product;
                cartProduct.ProductId = product.Id;
                cartProduct.Cart = cart;
                cartProduct.CartId = user.SCartId;

                product.Quantity -= quantity;
                _Db.Update(product);

                var order = _Db.Orders.Find(cart.OrderId);

                cartProduct.Order = order;
                cartProduct.OrderId = order.Id;
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

        [HttpGet("SellingCart")]
        public IActionResult SellingCart()
        {
            try
            {
                var user = _Db.Users.Find(_userManager.GetUserId(HttpContext.User));
                var cart = _Db.SellingCarts.Find(user.SCartId);

                var cartVM = _mapper.Map<SellingCartVM>(cart);
                var scartProducts = _Db.SCartProducts.ToList();

                var scartProduct = from p in scartProducts
                                  where p.CartId == cart.Id
                                  select p;

                cartVM.Products = scartProduct.ToList();

                return View(cartVM);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? Id)
        {
            try
            {
                var cartProductToDelete = _Db.SCartProducts.Find(Id);
                var product = _Db.Products.Find(cartProductToDelete.ProductId);

                product.Quantity += cartProductToDelete.Quantity;

                _Db.SCartProducts.Remove(cartProductToDelete);
                _Db.SaveChanges();

                return RedirectToAction("SellingCart");
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("ConcludeOrder")]
        [ValidateAntiForgeryToken]
        public IActionResult ConcludeOrder(int? id, SellingCartVM sellingCartVM)
        {
            if (id == null || id != sellingCartVM.Id || !ModelState.IsValid) return BadRequest();
            try
            {

                var order = _Db.Orders.Find(sellingCartVM.OrderId);
                order.Type = Order.OrderType.Sell;
                order.TotalPrice = sellingCartVM.TotalPrice;
                order.CreatedDate = DateTime.Now;

                var user = _Db.Users.Find(_userManager.GetUserId(HttpContext.User));
                order.User = user;
                order.UserId = user.Id;
                order.UserName = user.UserName;
                order.IsCompleted = true;
                order.HasArrived = true;
                _Db.Update(order);

                var scartProducts = from p in _Db.SCartProducts
                                   where p.OrderId == order.Id
                                   select p;

                foreach(var p in scartProducts.ToList<SCartProduct>())
                {
                    var product = _Db.Products.Find(p.ProductId);
                    var category = _Db.Categories.Find(product.CategoryId);
                    var warehouse = _Db.Warehouses.Find(category.WarehouseId);

                    category.Units -= p.Quantity;
                    warehouse.CurrentStorage -= p.Quantity;
                    Console.WriteLine($"|{p.Quantity} - {category.Units} - {warehouse.CurrentStorage}|");
                }

                var newSellingCart = new SellingCart();
                var newOrder = new Order();
                _Db.Add(newOrder);
                _Db.Add(newSellingCart);

                _Db.SaveChanges();

                user.SCart = newSellingCart;
                user.SCartId = newSellingCart.Id;
                newSellingCart.OrderId = newOrder.Id;

                _Db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
