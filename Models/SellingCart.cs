using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models
{
    public class SellingCart
    {
        [Key]
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderId { get; set; }
    }
}
