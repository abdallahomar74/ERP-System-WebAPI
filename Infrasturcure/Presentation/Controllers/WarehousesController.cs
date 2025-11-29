using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.WarehouseModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class WarehousesController(IServiceManger _serviceManger) : ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarehouseDto>>> GetAllWarehouses()
        {
            var warehouses = await _serviceManger.WarehouseService.GetAllWarehousesAsync();
            return Ok(warehouses);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<WarehouseDto>> GetWarehouseById(Guid id)
        {
            var warehouse = await _serviceManger.WarehouseService.GetWarehouseByIdAsync(id);
            if (warehouse is null)
            {
                return NotFound();
            }
            return Ok(warehouse);
        }
        [HttpPost]
        public async Task<ActionResult<WarehouseDto>> CreateWarehouse([FromBody] CreateWarehouseDto createWarehouseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdWarehouse = await _serviceManger.WarehouseService.CreateWarehouseAsync(createWarehouseDto);
            return CreatedAtAction(nameof(GetWarehouseById),
                new { id = createdWarehouse.Id }, createdWarehouse);
        }
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateWarehouse(Guid id, [FromBody] UpdateWarehouseDto updateWarehouseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _serviceManger.WarehouseService.UpdateWarehouseAsync(id, updateWarehouseDto);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteWarehouse(Guid id)
        {
            var result = await _serviceManger.WarehouseService.DeleteWarehouseAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
