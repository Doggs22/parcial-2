using APIConsumer.Interfaces;
using CarStockAPI.Models;

namespace CarStockAPI.Repositories;

public class CarRepository : ICarRepository
{
    private readonly IAPIConnector _apiConnector;

    public CarRepository(IAPIConnector apiConnector)
    {
        _apiConnector = apiConnector;
    }

    public async Task<IEnumerable<Car>> GetAllAsync()
    {
        var cars = await _apiConnector.GetAsync<List<Car>>("istea/api/v1/cars");
        return cars.Where(c => !c.IsDeleted);
    }
}
