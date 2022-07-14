using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public List<int> ProductIds { get; set; }

        public string OwnerId { get; set; }
    }
}
