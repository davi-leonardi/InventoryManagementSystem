using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManSys.ViewModels
{
    [BindProperties]
    public class WarehouseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        [DisplayName("Current Storage")]
        public int CurrentStorage { get; set; }
        [DisplayName("Max Capacity")]
        public int MaxCapacity { get; set; }
    }
}
