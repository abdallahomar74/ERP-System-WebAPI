using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.InventoryModule;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DataTransferObjects.ProductModule;
using Shared.DataTransferObjects.StockModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class StockService(IUnitOfWork _unitOfWork, IMapper _mapper) : IStockService
    {
        public async Task<IEnumerable<StockDto>> GetAllStocksAsync()
        {
            var spec = new StockSpecifications();
            var Repo = _unitOfWork.GetRepository<Stock>();
            var  stocks = await Repo.GetAllAsync(spec);
            return _mapper.Map<IEnumerable<StockDto>>(stocks);
        }

        public async Task<StockDto?> GetStockByIdAsync(Guid stockId)
        {
            var spec = new StockSpecifications(stockId);
            var Repo = _unitOfWork.GetRepository<Stock>();
            var stock = await Repo.GetByIdAsync(spec);
            if (stock is null)
            {
                return null;
            }
            return _mapper.Map<StockDto>(stock);
        }

        public async Task<StockDto> CreateStockAsync(CreateStockDto stockForCreation)
        {
            var Repo = _unitOfWork.GetRepository<Stock>();
            var stock = _mapper.Map<Stock>(stockForCreation);
           
            var existingStocks = await Repo.GetAllAsync();
            if (existingStocks.Any(s =>
                s.ProductId == stockForCreation.ProductId &&
                s.WarehouseId == stockForCreation.WarehouseId))
            {
                throw new InvalidOperationException("Stock already exists for this product in this warehouse.");
            }

            stock.LastUpdated = DateTime.UtcNow;
            await Repo.AddAsync(stock);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<StockDto>(stock);
        }

        public async Task<bool> UpdateStockAsync(AdjustStockDto stockForUpdate)
        {
            var Repo = _unitOfWork.GetRepository<Stock>();
            var stocks = await Repo.GetAllAsync();
            var stock = stocks.FirstOrDefault(s =>
                s.ProductId == stockForUpdate.ProductId &&
                s.WarehouseId == stockForUpdate.WarehouseId);
            if (stock is null)
                return false;

            var newQuantity = stock.Quantity + stockForUpdate.QuantityChange;
            if (newQuantity < 0)
                throw new InvalidOperationException("Insufficient stock to apply this adjustment.");

            stock.Quantity = newQuantity;
            stock.LastUpdated = DateTime.UtcNow;

            Repo.Update(stock);
            await _unitOfWork.SaveChangesAsync();
            return true;

        }

        public async Task<bool> TransferStockAsync(TransferStockDto transferDto)
        {
            var Repo = _unitOfWork.GetRepository<Stock>();
            var stocks = await Repo.GetAllAsync();
            var fromStock = stocks.FirstOrDefault(s =>
                s.ProductId == transferDto.ProductId &&
                s.WarehouseId == transferDto.FromWarehouseId);
            if (fromStock is null)
                return false;
            if (fromStock.Quantity < transferDto.Quantity)
                throw new InvalidOperationException("Insufficient stock to transfer.");

            var toStock = stocks.FirstOrDefault(s =>
                s.ProductId == transferDto.ProductId &&
                s.WarehouseId == transferDto.ToWarehouseId);
            if (toStock is null)
            {
                toStock = new Stock
                {
                    Id = Guid.NewGuid(),
                    ProductId = transferDto.ProductId,
                    WarehouseId = transferDto.ToWarehouseId,
                    Quantity = 0,
                    LastUpdated = DateTime.UtcNow
                };
                await Repo.AddAsync(toStock);
            }
                
            fromStock.Quantity -= transferDto.Quantity;
            toStock.Quantity += transferDto.Quantity;
            fromStock.LastUpdated = toStock.LastUpdated = DateTime.UtcNow;
            Repo.Update(fromStock);
            Repo.Update(toStock);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
