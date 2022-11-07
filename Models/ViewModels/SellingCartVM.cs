using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models.ViewModels
{
    [BindProperties]
    public class SellingCartVM
    {
        [Key]
        public int Id { get; set; }
        public List<SCartProduct> Products { get; set; } = new List<SCartProduct>();
        public decimal TotalPrice { get; set; }
        public int OrderId { get; set; }
    }
}
