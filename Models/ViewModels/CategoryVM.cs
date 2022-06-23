using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [DisplayName("Category Name")]
        [StringLength(30)]
        public string Name { get; set; }
        public int Units { get; set; } = 0;
        public string SelectedWarehouse { get; set; }
        //public List<string> Warehouses { get; set; }
        public List<SelectList> Warehouses { get; set; }
        public int WarehouseId { get; set; }
    }
}
