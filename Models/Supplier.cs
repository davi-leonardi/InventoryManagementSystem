using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime LastOrder { get; set; }
        public string PhoneNumber { get; set; }
    }
}
