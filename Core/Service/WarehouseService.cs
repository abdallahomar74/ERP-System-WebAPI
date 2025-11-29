using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.InventoryModule;
using ServiceAbstraction;
using Shared.DataTransferObjects.ProductModule;
using Shared.DataTransferObjects.WarehouseModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class WarehouseService(IUnitOfWork _unitOfWork, IMapper _mapper) : IWarehouseService
    {
        public async Task<IEnumerable<WarehouseDto>> GetAllWarehousesAsync()
        {
            var Repo = _unitOfWork.GetRepository<Warehouse>();
            var warehouses = await Repo.GetAllAsync();
            var warehouseDtos =  _mapper.Map<IEnumerable<WarehouseDto>>(warehouses);
            return warehouseDtos;
        }

        public async Task<WarehouseDto?> GetWarehouseByIdAsync(Guid warehouseId)
        {
            var Repo = _unitOfWork.GetRepository<Warehouse>();
            var warehouse = await Repo.GetByIdAsync(warehouseId);
            if (warehouse is null)
                return null;

            return _mapper.Map<WarehouseDto>(warehouse);
        }

        public async Task<WarehouseDto> CreateWarehouseAsync(CreateWarehouseDto createWarehouseDto)
        {
            var Repo = _unitOfWork.GetRepository<Warehouse>();

            var existingWarehouses = await Repo.GetAllAsync();
            if (existingWarehouses.Any(p => p.Code == createWarehouseDto.Code))
                throw new InvalidOperationException("Code already exists.");

            var newWarehouse = _mapper.Map<Warehouse>(createWarehouseDto);
            await Repo.AddAsync(newWarehouse);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<WarehouseDto>(newWarehouse);
        }

        public async Task<bool> UpdateWarehouseAsync(Guid warehouseId, UpdateWarehouseDto updateWarehouseDto)
        {
            var Repo = _unitOfWork.GetRepository<Warehouse>();
            var warehouse = await Repo.GetByIdAsync(warehouseId);
            if (warehouse is null)
                return false;

            _mapper.Map(updateWarehouseDto, warehouse);
            warehouse.UpdatedAt = DateTime.UtcNow;

            Repo.Update(warehouse);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async  Task<bool> DeleteWarehouseAsync(Guid warehouseId)
        {
            var Repo = _unitOfWork.GetRepository<Warehouse>();
            var warehouse = await Repo.GetByIdAsync(warehouseId);
            if (warehouse is null)
                return false;

            Repo.Delete(warehouse);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
