using CarStockAPI.Models;

namespace CarStockAPI.Repositories;

public interface ICarRepository
{
    Task<IEnumerable<Car>> GetAllAsync();
}
