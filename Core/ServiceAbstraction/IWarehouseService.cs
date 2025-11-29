using Shared.DataTransferObjects.WarehouseModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IWarehouseService
    {
        Task<IEnumerable<WarehouseDto>> GetAllWarehousesAsync();
        Task<WarehouseDto?> GetWarehouseByIdAsync(Guid warehouseId);
        Task<WarehouseDto> CreateWarehouseAsync(CreateWarehouseDto createWarehouseDto);
        Task<bool> UpdateWarehouseAsync(Guid warehouseId, UpdateWarehouseDto updateWarehouseDto);
        Task<bool> DeleteWarehouseAsync(Guid warehouseId);
    }
}
