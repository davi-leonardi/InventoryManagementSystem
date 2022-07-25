using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models
{
    public class CartProduct
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public Product Product { get; set; }
        public int cartId { get; set; }
        public ShoppingCart cart { get; set; }

    }
}
