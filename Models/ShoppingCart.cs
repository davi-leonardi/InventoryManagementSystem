using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderId { get; set; }
    }
}
