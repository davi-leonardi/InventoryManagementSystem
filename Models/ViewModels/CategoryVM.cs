using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models.ViewModels
{
    [BindProperties]
    public class CategoryVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category")]
        [StringLength(30)]
        public string Name { get; set; }
        [DisplayName("Quantity (units)")]
        public int Units { get; set; } = 0;
        [DisplayName("Select warehouse")]
        public List<SelectListItem> Warehouses { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
    }
}
