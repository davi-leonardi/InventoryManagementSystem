using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models
{
    public class SCartProduct
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Total Price")]
        public decimal TotalPrice { get; set; }
        [Required]
        public int Quantity { get; set; }

        [Required]
        public int ProductId { get; set; }
        [Required]
        public Product Product { get; set; }

        public int CartId { get; set; }
        public SellingCart Cart { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
