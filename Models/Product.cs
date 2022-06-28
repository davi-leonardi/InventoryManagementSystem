using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Name should have less than 30 characters")]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        [Required]
        [Range(0, 9999.99, ErrorMessage = "Invalid Price")]
        public decimal Price { get; set; }
        [Required]
        [DisplayName("Quantity (Units)")]
        public int Quantity { get; set; } = 0;

        //Navigation Property
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

    }
}
