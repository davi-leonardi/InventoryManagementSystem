using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models.ViewModels
{
    [BindProperties]
    public class ProductVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Name should have less than 30 characters")]
        public string Name { get; set; }
        [Required]
        [Range(0, 99999.99, ErrorMessage = "Invalid Price (0 - 99999.99)")]
        [DisplayName("Price (USD$)")]
        public decimal Price { get; set; }
        [Range(0, 10000, ErrorMessage = "Invalid Quantity (0 - 10000)")]
        [DisplayName("Quantity")]
        public int Quantity { get; set; } = 0;
        public List<SelectListItem> Categories { get; set; }
        public int CategoryId { get; set; }
        [DisplayName("Category")]
        public string CategoryName { get; set; }
        public List<SelectListItem> Suppliers { get; set; }
        public int SupplierId { get; set; }
        [DisplayName("Supplier")]
        public string SupplierName { get; set; }
    }
}
