using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllStock()
        {
            var stock = await _stockService.GetAllStockAsync();
            return Ok(stock);
        }

        [Authorize]
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetStockByProductId(int productId)
        {
            var stock = await _stockService.GetStockByProductIdAsync(productId);

            if (stock == null)
                return NotFound();

            return Ok(stock);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddStock(AddStockDto dto)
        {
            var id = await _stockService.AddStockAsync(dto);

            return Ok(new
            {
                Message = "Stock added successfully.",
                StockId = id
            });
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateStock(UpdateStockDto dto)
        {
            var rowsAffected = await _stockService.UpdateStockAsync(dto);

            if (rowsAffected == 0)
                return NotFound();

            return Ok("Stock updated successfully.");
        }

        [Authorize]
        [HttpDelete("{stockId}")]
        public async Task<IActionResult> DeleteStock(int stockId)
        {
            var rowsAffected = await _stockService.DeleteStockAsync(stockId);

            if (rowsAffected == 0)
                return NotFound();

            return Ok("Stock deleted successfully.");
        }
    }
}