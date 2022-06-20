using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models
{

    public enum Perm
    {
        User, Admin, SuperAdmin
    }
    public class Employee
    {
        [Key]
        public int Id { get; init; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string JobRole { get; set; }
        [Required]
        public Perm Permission { get; set; } = Perm.User;

    }
}
