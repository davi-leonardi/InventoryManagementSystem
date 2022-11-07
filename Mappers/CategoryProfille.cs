using AutoMapper;
using InventoryManSys.Models;
using InventoryManSys.Models.ViewModels;

namespace InventoryManSys.Mappers
{
    public class CategoryProfille : Profile
    {
        public CategoryProfille()
        {
            CreateMap<CategoryVM, Category>();
            CreateMap<Category, CategoryVM>();
        }
    }
}
