// файл: Dealship.Shared/Dtos.cs
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Dealship.Shared
{
    /* ---------- спільні утиліти ---------- */

    public class QueryParams
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string? Search { get; set; }
        public string? Sort { get; set; }
    }

    public class PaginatedResult<T>
    {
        public IEnumerable<T> Items { get; }
        public int Total { get; }
        public int Page { get; }
        public int Size { get; }

        public PaginatedResult(IEnumerable<T> items, int total, int page, int size)
        {
            Items = items;
            Total = total;
            Page = page;
            Size = size;
        }
    }

    /* ---------- Cars ---------- */

    public record CarDto(
        int Id,
        string Brand,
        string Model,
        int Year,
        decimal Price,
        int Mileage,
        int EngineId,
        int TransmissionId,
        int FuelTypeId,
        int ColorId,
        string? Image);

    public class CarFormDto
    {
        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int Mileage { get; set; }
        public int EngineId { get; set; }
        public int TransmissionId { get; set; }
        public int FuelTypeId { get; set; }
        public int ColorId { get; set; }
        public IFormFile? ImageFile { get; set; }
    }

    /* ---------- Lookup ---------- */

    public record ColorDto(int Id, string Name);
    public class ColorFormDto { public string Name { get; set; } = ""; }

    public record FuelTypeDto(int Id, string Name);
    public class FuelTypeFormDto { public string Name { get; set; } = ""; }

    public record TransmissionDto(int Id, string Type);
    public class TransmissionFormDto { public string Type { get; set; } = ""; }

    public record EngineDto(int Id, string Type, double Volume, int Horsepower);
    public class EngineFormDto
    {
        public string Type { get; set; } = "";
        public double Volume { get; set; }
        public int Horsepower { get; set; }
    }

    /* ---------- Customers ---------- */

    public record CustomerDto(int Id, string FullName, string Email, string PhoneNumber);
    public class CustomerFormDto
    {
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
    }

    /* ---------- Orders ---------- */

    public record OrderDto(int Id, int CustomerId, int CarId, DateTime OrderDate, decimal TotalPrice);
    public class OrderFormDto
    {
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Today;
    }

    /* ---------- Users ---------- */

    public record UserDto(int Id, string FullName, string Email, string Role);
    public class UserFormDto
    {
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "Client";
    }
}
