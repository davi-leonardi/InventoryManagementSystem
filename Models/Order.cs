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
        public OrderType Type { get; set; }
        public bool IsCompleted { get; set; } = false;
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }

    }
}
