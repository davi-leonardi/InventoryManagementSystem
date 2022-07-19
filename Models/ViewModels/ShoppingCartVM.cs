using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.Models.ViewModels
{
    [BindProperties]
    [Keyless]
    public class ShoppingCartVM
    {
        public int Id { get; set; }

        public string Description  { get; set; }

        public List<int> ProductIds { get; set; }

        public List<ProductVM> Products { get; set; }

        public int OwnerId { get; set; }
    }
}
