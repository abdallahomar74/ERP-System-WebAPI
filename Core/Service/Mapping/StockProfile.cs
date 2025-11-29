using AutoMapper;
using DomainLayer.Models.InventoryModule;
using Shared.DataTransferObjects.StockModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            CreateMap<Stock, StockDto>()
                .ForMember(d => d.ProductName,
                    opt => opt.MapFrom(s => s.Product != null ? s.Product.Name : null))
                .ForMember(d => d.WarehouseName,
                    opt => opt.MapFrom(s => s.Warehouse != null ? s.Warehouse.Name : null));

            CreateMap<CreateStockDto, Stock>();
        }
    }
}
