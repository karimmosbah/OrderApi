using AutoMapper;
using OrderApi.Dtos;
using OrderApi.Models;

namespace OrderApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderListDto>()
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.Image));
            CreateMap<EditOrderDto, Order>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<OrderDto, Order>();
        }
    }
}
