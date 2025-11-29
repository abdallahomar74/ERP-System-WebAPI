using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.ProductModule;
using Shared.DataTransferObjects.StockModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class StocksController(IServiceManger _serviceManger) : ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockDto>>> GetAllStocks()
        {
            var stocks = await _serviceManger.StockService.GetAllStocksAsync();
            return Ok(stocks);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<StockDto>> GetStockById(Guid id)
        {
            var stock = await _serviceManger.StockService.GetStockByIdAsync(id);
            if (stock is null)
            {
                return NotFound();
            }
            return Ok(stock);
        }
        [HttpPost]
        public async Task<ActionResult<StockDto>> CreateStock([FromBody] CreateStockDto stockForCreation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdStock = await _serviceManger.StockService.CreateStockAsync(stockForCreation);
                return CreatedAtAction(nameof(GetStockById), new { id = createdStock.Id }, createdStock);
        }
        [HttpPut("adjust")]
        public async Task<ActionResult> UpdateStock([FromBody] AdjustStockDto stockForUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _serviceManger.StockService.UpdateStockAsync(stockForUpdate);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPost("transfer")]
        public async Task<ActionResult> TransferStock([FromBody] TransferStockDto transferDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceManger.StockService.TransferStockAsync(transferDto);

            if (!result)
                return BadRequest("Cannot complete stock transfer."); 

            return NoContent();
        }

    }
}
