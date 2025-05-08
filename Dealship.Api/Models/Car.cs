// файл: Dealship.Api/Models/Car.cs
namespace Dealship.Api.Models;
public class Car
{
    public int Id { get; set; }
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int Year { get; set; }
    public decimal Price { get; set; }
    public int Mileage { get; set; }
    public int EngineId { get; set; }
    public int TransmissionId { get; set; }
    public int FuelTypeId { get; set; }
    public int ColorId { get; set; }
    public string? Image { get; set; }

    [System.Text.Json.Serialization.JsonIgnore] public Engine Engine { get; set; } = default!;
    [System.Text.Json.Serialization.JsonIgnore] public Transmission Transmission { get; set; } = default!;
    [System.Text.Json.Serialization.JsonIgnore] public FuelType FuelType { get; set; } = default!;
    [System.Text.Json.Serialization.JsonIgnore] public Color Color { get; set; } = default!;
}
