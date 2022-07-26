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

        public int CartId { get; set; }
        public ShoppingCart Cart { get; set; }

        public int OrderId { get;set; }
    }
}
