using AutoMapper;
using InventoryManSys.Models;
using InventoryManSys.Models.ViewModels;

namespace InventoryManSys.Mappers
{ 
    public class OrderProfille : Profile
    {
        public OrderProfille()
        {
            CreateMap<Order, OrderVM>();
            CreateMap<OrderVM, Order>();
        }
    }
}
