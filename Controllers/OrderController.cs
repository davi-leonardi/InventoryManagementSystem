using AutoMapper;
using InventoryManSys.Data;
using InventoryManSys.Models;
using InventoryManSys.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManSys.Controllers
{
    [Authorize]
    [Route("Order")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private readonly IMapper _Mapper;

        public OrderController(ApplicationDbContext db, IMapper mapper)
        {
            _Db = db;   
            _Mapper = mapper;
        }

        public IActionResult Index()
        {
            try
            {
                List<Order> orders = _Db.Orders.Where(o => o.IsCompleted == true).ToList();
                List<OrderVM> ordersVM = new List<OrderVM>();

                foreach (Order order in orders)
                {
                    var map = _Mapper.Map<OrderVM>(order);
                    ordersVM.Add(map);
                }

                return View(ordersVM.AsEnumerable());
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("Details")]
        public IActionResult Details(int? id)
        {

            if (id == null) return BadRequest();

            Order order = _Db.Orders.Find(id);

            if (order.Type == Order.OrderType.Buy)
            {
                var cartProducts = from p in _Db.CartProducts
                                   where p.OrderId == id
                                   select p;

                var orderVM = _Mapper.Map<OrderVM>(order);
                orderVM.Products = cartProducts.ToList();
                ViewBag.BuyOrder = "True";

                return View(orderVM);
            }

            var scartProducts = from p in _Db.SCartProducts
                               where p.OrderId == id
                               select p;

            var SorderVM = _Mapper.Map<OrderVM>(order);
            SorderVM.SProducts = scartProducts.ToList();
            ViewBag.BuyOrder = "False";

            return View(SorderVM);

        }

        [HttpPost("ConfirmArrival")]
        public IActionResult ConfirmArrival(int? id, OrderVM orderVM)
        {
            if(id == null || !ModelState.IsValid || id != orderVM.Id) return BadRequest();

            var order = _Db.Orders.Find(orderVM.Id);
            var cartProducts = from p in _Db.CartProducts
                               where p.OrderId == order.Id
                               select p;

            cartProducts.ToList();

            foreach(var cartProduct in cartProducts.ToList<CartProduct>())
            {
                var product = _Db.Products.Find(cartProduct.ProductId);
                var category = _Db.Categories.Find(product.CategoryId);
                var warehouse = _Db.Warehouses.Find(category.WarehouseId);
                var supplier = _Db.Suppliers.Find(product.SupplierId);

                product.Quantity += cartProduct.Quantity;
                category.Units += cartProduct.Quantity;
                warehouse.CurrentStorage += cartProduct.Quantity;
                supplier.LastOrder = order.CreatedDate;
            }

            order.HasArrived = true;
            _Db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet("Delete")]
        public IActionResult Delete(int? id)
        {
            if(id == null) return BadRequest();

            try
            {
                var order = _Db.Orders.Find(id);
                var orderVM = _Mapper.Map<OrderVM>(order);

                return View(orderVM);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id, OrderVM orderVM)
        {
            if(!ModelState.IsValid || id == null || id != orderVM.Id) return BadRequest();

            try
            {
                var order = _Db.Orders.Find(orderVM.Id);
                var cartProducts = from p in _Db.CartProducts
                                   where p.OrderId == orderVM.Id
                                   select p;

                foreach (var product in cartProducts)
                {
                    _Db.CartProducts.Remove(product);
                }

                var cart = from c in _Db.ShoppingCarts
                           where c.OrderId == orderVM.Id
                           select c;

                if (cart.Any()) _Db.ShoppingCarts.Remove(cart.First());

                _Db.Orders.Remove(order);

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
