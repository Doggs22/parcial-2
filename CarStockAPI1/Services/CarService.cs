using CarStockAPI.Models;
using CarStockAPI.Repositories;

namespace CarStockAPI.Services;

public class CarService
{
    private readonly ICarRepository _repository;

    public CarService(ICarRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> GetCountAddedThisYearAsync()
    {
        var cars = await _repository.GetAllAsync();
        return cars.Count(c => DateTime.Parse(c.CreatedAt).Year == DateTime.Now.Year);
    }

    public async Task<IEnumerable<Car>> GetByPriceAsync(int? minPrice = null, int? maxPrice = null)
    {
        var cars = await _repository.GetAllAsync();
        return cars.Where(c =>
            (!minPrice.HasValue || c.Price >= minPrice.Value) &&
            (!maxPrice.HasValue || c.Price <= maxPrice.Value));
    }

    public async Task<IEnumerable<Car>> GetByFuelAsync(string fuelType)
    {
        var cars = await _repository.GetAllAsync();
        return cars.Where(c => string.Equals(c.Fuel, fuelType, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<IEnumerable<Car>> GetEcoNotSedanAsync()
    {
        var cars = await _repository.GetAllAsync();
        return cars.Where(c =>
            (c.Fuel == "electric" || c.Fuel == "hybrid") &&
            c.Type.ToLower() != "sedan");
    }

    public async Task<IEnumerable<Car>> GetByPriorityAsync(int minPriority)
    {
        var cars = await _repository.GetAllAsync();
        return cars.Where(c => c.Priority > minPriority);
    }
}
