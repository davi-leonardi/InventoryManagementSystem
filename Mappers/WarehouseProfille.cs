using AutoMapper;
using InventoryManSys.Models;
using InventoryManSys.ViewModels;

namespace InventoryManSys.Mappers
{
    public class WarehouseProfille : Profile
    {
        public WarehouseProfille()
        {
            CreateMap<WarehouseVM, Warehouse>(); //maps automatically the first class to the second (properties must have the same name)
            CreateMap<Warehouse, WarehouseVM>();
        }
    }
}
