using AutoMapper;
using InventoryManSys.Models;
using InventoryManSys.Models.ViewModels;

namespace InventoryManSys.Mappers
{
    public class ProductProfille : Profile
    {
        public ProductProfille()
        {
            CreateMap<ProductVM, Product>();
            CreateMap<Product, ProductVM>();
        }
    }
}
