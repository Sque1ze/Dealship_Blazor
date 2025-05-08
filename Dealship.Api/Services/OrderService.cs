using Dealship.Api.Data;
using Dealship.Api.Models;
using Dealship.Api.Services.Interfaces;
using Dealship.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dealship.Api.Services
{
    public sealed class OrderService : ICrudService<OrderDto, OrderFormDto>
    {
        private readonly AppDbContext _db;

        public OrderService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<PaginatedResult<OrderDto>> GetAsync(QueryParams qp)
        {
            var q = _db.Orders
                       .AsNoTracking()
                       .OrderByDescending(o => o.OrderDate);

            var total = await q.CountAsync();

            var items = await q
                .Skip((qp.Page - 1) * qp.Size)
                .Take(qp.Size)
                .Select(o => new OrderDto(
                    o.Id,
                    o.CustomerId,
                    o.CarId,
                    o.OrderDate,
                    o.TotalPrice))
                .ToListAsync();

            return new PaginatedResult<OrderDto>(items, total, qp.Page, qp.Size);
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            return await _db.Orders
                .AsNoTracking()
                .Where(o => o.Id == id)
                .Select(o => new OrderDto(
                    o.Id,
                    o.CustomerId,
                    o.CarId,
                    o.OrderDate,
                    o.TotalPrice))
                .FirstOrDefaultAsync();
        }

        public async Task<OrderDto> CreateAsync(OrderFormDto f)
        {
            var car = await _db.Cars.FindAsync(f.CarId)
                      ?? throw new ArgumentException($"Car {f.CarId} not found", nameof(f.CarId));

            var ent = new Order
            {
                CustomerId = f.CustomerId,
                CarId = f.CarId,
                OrderDate = f.OrderDate == default ? DateTime.UtcNow : f.OrderDate,
                TotalPrice = car.Price
            };

            _db.Orders.Add(ent);
            await _db.SaveChangesAsync();

            return new OrderDto(ent.Id, ent.CustomerId, ent.CarId, ent.OrderDate, ent.TotalPrice);
        }

        public async Task<bool> UpdateAsync(int id, OrderFormDto f)
        {
            var ent = await _db.Orders.FindAsync(id);
            if (ent is null) return false;

            if (ent.CarId != f.CarId)
            {
                var car = await _db.Cars.FindAsync(f.CarId)
                          ?? throw new ArgumentException($"Car {f.CarId} not found", nameof(f.CarId));
                ent.CarId = f.CarId;
                ent.TotalPrice = car.Price;
            }

            ent.CustomerId = f.CustomerId;
            ent.OrderDate = f.OrderDate == default ? ent.OrderDate : f.OrderDate;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ent = await _db.Orders.FindAsync(id);
            if (ent is null) return false;
            _db.Orders.Remove(ent);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
