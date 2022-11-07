using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models.ViewModels
{
    public class SupplierVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [DisplayName("Last Order")]
        public DateTime LastOrder { get; set; }
        [Required]
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
