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
        public int CurrentStorage { get; set; } = 0;
        public bool IsFull { get; set; }
        [Required]
        [DisplayName("Max Capacity (units)")]
        public int MaxCapacity { get; set; }

        //Navigation Property
        [BindNever]
        public ICollection<Category> Categories { get; set; }
    }
}
