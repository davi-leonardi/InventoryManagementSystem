using Microsoft.AspNetCore.Identity;

namespace InventoryManSys.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int CartId { get; set; }
        public ShoppingCart Cart { get; set; }
        public int SCartId { get; set; }
        public SellingCart SCart { get; set; }
    }
}
