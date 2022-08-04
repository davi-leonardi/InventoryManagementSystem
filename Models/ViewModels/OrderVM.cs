using System.Collections.Generic;
using System.ComponentModel;

namespace InventoryManSys.Models.ViewModels
{
    public class OrderVM
    {
        public enum OrderType
        {
            Buy, Sell
        }

        [DisplayName("ID")]
        public int Id { get; set; }
        public OrderType Type { get; set; }
        public bool IsCompleted { get; set; }
        [DisplayName("Delivered at Warehouse?")]
        public bool HasArrived { get; set; }
        [DisplayName("Order Total (USD$)")]
        public decimal TotalPrice { get; set; }
        [DisplayName("Date")]
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        [DisplayName("Employee")]
        public string UserName { get; set; }
        public List<CartProduct> Products { get; set; }
        public List<SCartProduct> SProducts { get; set; }
    }
}
