namespace BlazorAppNetwork.Data;

public class CarData
{
    public CarData()
    {

    }
    public CarData(int id, string? brand, string? model, string? year, string? price)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
    }

    public int Id { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? Year { get; set; }
    public string? Price { get; set; }
}
