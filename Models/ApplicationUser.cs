using Microsoft.AspNetCore.Identity;

namespace InventoryManSys.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int ShoppingCart { get; set; }
        public ShoppingCart Cart { get; set; }
    }
}
