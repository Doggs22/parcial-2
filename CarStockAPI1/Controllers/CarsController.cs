using CarStockAPI.Repositories;
using CarStockAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarStockAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly CarService _service;

    public CarsController(CarService service)
    {
        _service = service;
    }

    [HttpGet("added-this-year")]
    public async Task<IActionResult> GetAddedThisYear()
        => Ok(await _service.GetCountAddedThisYearAsync());

    [HttpGet("price")]
    public async Task<IActionResult> GetByPrice([FromQuery] int? min, [FromQuery] int? max)
        => Ok(await _service.GetByPriceAsync(min, max));

    [HttpGet("fuel/{type}")]
    public async Task<IActionResult> GetByFuel(string type)
        => Ok(await _service.GetByFuelAsync(type));

    [HttpGet("eco-not-sedan")]
    public async Task<IActionResult> GetEcoNotSedan()
        => Ok(await _service.GetEcoNotSedanAsync());

    [HttpGet("priority/{min}")]
    public async Task<IActionResult> GetByPriority(int min)
        => Ok(await _service.GetByPriorityAsync(min));
}
