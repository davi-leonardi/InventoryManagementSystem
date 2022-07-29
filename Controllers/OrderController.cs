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
            OrderVM orderVM = _Mapper.Map<OrderVM>(order);

            var cartProducts = from p in _Db.CartProducts
                               where p.OrderId == id
                               select p;

            orderVM.Products = cartProducts.ToList();

            return View(orderVM);
        }
    }
}
