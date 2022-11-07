using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }

        //Navigation Property
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Category Category { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public Supplier Supplier { get; set; }
    }
}
