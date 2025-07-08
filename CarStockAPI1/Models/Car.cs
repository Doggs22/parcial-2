namespace CarStockAPI.Models;

public class Car
{
    public string Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Type { get; set; }
    public string Fuel { get; set; }
    public int Price { get; set; }
    public int Priority { get; set; }
    public string CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
