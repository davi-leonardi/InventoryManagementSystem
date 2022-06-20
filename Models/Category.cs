using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models
{
    public class Category
    {
        [Key]
        public int Id { get; init; }
        [Required]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        public int Units { get; set; } = 0;

        //Navigation Property
        public ICollection<Product> Products { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
