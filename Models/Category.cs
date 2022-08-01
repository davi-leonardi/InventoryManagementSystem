using Microsoft.AspNetCore.Mvc.Rendering;
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
        [StringLength(30)]
        public string Name { get; set; }
        public int Units { get; set; } = 0;

        //Navigation Property
        [Required]
        public int WarehouseId { get; set; }
        [DisplayName("Warehouse Name")]
        public string WarehouseName { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
