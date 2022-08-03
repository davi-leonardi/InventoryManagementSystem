using AutoMapper;
using InventoryManSys.Models;
using InventoryManSys.Models.ViewModels;

namespace InventoryManSys.Mappers
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, SupplierVM>();
            CreateMap<SupplierVM, Supplier>();
        }
    }
}
