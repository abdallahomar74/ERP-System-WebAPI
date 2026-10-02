using AutoMapper;
using DomainLayer.Models.SalesModule;
using Shared.DataTransferObjects.SalesModule;

namespace Service.Mapping
{
    public class SalesProfile : Profile
    {
        public SalesProfile()
        {
            CreateMap<SalesOrder, SalesOrderDto>()
                .ForMember(dest => dest.OrderItems, opt => opt
                .MapFrom(src => src.SalesOrderItems))
                .ForMember(dest => dest.Status, opt => opt
                .MapFrom(src => src.Status.ToString()));
            CreateMap<SalesOrderItem, SalesOrderItemDto>();
        }
    }
}
