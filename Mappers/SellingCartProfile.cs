using AutoMapper;
using InventoryManSys.Models;
using InventoryManSys.Models.ViewModels;

namespace InventoryManSys.Mappers
{
    public class SellingCartProfile : Profile
    {
        public SellingCartProfile()
        {
            CreateMap<SellingCartVM, SellingCart>();
            CreateMap<SellingCart, SellingCartVM>();
        }
    }
}
