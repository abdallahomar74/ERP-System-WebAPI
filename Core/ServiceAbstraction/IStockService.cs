using Shared.DataTransferObjects.StockModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IStockService
    {
        public Task<IEnumerable<StockDto>> GetAllStocksAsync();
        public Task<StockDto?> GetStockByIdAsync(Guid stockId);
        public Task<StockDto> CreateStockAsync(CreateStockDto stockForCreation);
        public Task<bool> UpdateStockAsync(AdjustStockDto stockForUpdate);
        Task<bool> TransferStockAsync(TransferStockDto transferDto);

    }
}
