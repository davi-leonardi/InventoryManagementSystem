using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models
{
    public class Order
    {
        public enum OrderType
        {
            Buy, Sell
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Name should have less than 30 characters")]
        [DisplayName("Order Name")]
        public string Name { get; set; }
        public OrderType Type { get; set; }
        [StringLength(50, ErrorMessage = "Description should have less than 50 characters")]
        public string Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedDate { get; set; }

        //Foreign Key
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
