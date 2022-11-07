using AutoMapper;
using InventoryManSys.Models;
using InventoryManSys.Models.ViewModels;

namespace InventoryManSys.Mappers
{
    public class ShoppingCartProfille : Profile
    {
        public ShoppingCartProfille()
        {
            CreateMap<ShoppingCartVM, ShoppingCart>(); //maps automatically the first class to the second (properties must have the same name)
            CreateMap<ShoppingCart, ShoppingCartVM>();
        }
    }
}
