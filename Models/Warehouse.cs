using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Name should have less than 30 characters")]
        [DisplayName("Warehouse Name")]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [DisplayName("Current Storage")]
        public int CurrentStorage { get; set; } = 0;
        public bool IsFull { get; set; } = false;
        [Required]
        [DisplayName("Max Capacity (units)")]
        public int MaxCapacity { get; set; }
    }
}
