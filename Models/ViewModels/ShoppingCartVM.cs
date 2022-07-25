using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models.ViewModels
{
    [BindProperties]
    public class ShoppingCartVM
    {
        [Key]
        public int Id { get; set; }
        public List<CartProduct> Products { get; set; } = new List<CartProduct>();
        public decimal TotalPrice { get; set; }
    }
}
