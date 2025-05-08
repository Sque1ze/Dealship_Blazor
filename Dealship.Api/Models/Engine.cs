// файл: Dealship.Api/Models/Engine.cs
namespace Dealship.Api.Models;
public class Engine
{
    public int Id { get; set; }
    public string Type { get; set; } = default!;
    public double Volume { get; set; }
    public int Horsepower { get; set; }
}
