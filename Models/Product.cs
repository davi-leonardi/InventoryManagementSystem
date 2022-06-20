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
        [DisplayName("Expiration Date")]
        public DateTime ExpirationDate { get; set; }

        //Navigation Property
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
