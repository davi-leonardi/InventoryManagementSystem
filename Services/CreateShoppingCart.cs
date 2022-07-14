using InventoryManSys.Data;
using InventoryManSys.Models;

namespace InventoryManSys.Services
{
    public class CreateShoppingCart
    {
        private readonly ApplicationDbContext _Db;

        public CreateShoppingCart(ApplicationDbContext db)
        {
            _Db = db;
        }

        public void CreateCart(string OwnerId)
        {
            var cart = new ShoppingCart();
            cart.OwnerId = OwnerId;

            _Db.ShoppingCarts.Add(cart);
            _Db.SaveChanges();
        }
    }
}
