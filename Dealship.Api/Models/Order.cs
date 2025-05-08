// файл: Dealship.Api/Models/Order.cs
using System;

namespace Dealship.Api.Models;
public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int CarId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }

    [System.Text.Json.Serialization.JsonIgnore] public Customer Customer { get; set; } = default!;
    [System.Text.Json.Serialization.JsonIgnore] public Car Car { get; set; } = default!;
}
