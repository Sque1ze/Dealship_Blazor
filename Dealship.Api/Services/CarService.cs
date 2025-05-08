using Dealship.Api.Data;
using Dealship.Api.Models;
using Dealship.Shared;

namespace Dealship.Api.Services;

public sealed class CarService(AppDbContext db, IWebHostEnvironment env)
    : CrudServiceBase<Car, CarDto, CarFormDto>(
        db,
        c => new(c.Id, c.Brand, c.Model, c.Year, c.Price, c.Mileage,
                 c.EngineId, c.TransmissionId, c.FuelTypeId, c.ColorId, c.Image),
        (f, e) =>
        {
            e.Brand = f.Brand; e.Model = f.Model; e.Year = f.Year; e.Price = f.Price;
            e.Mileage = f.Mileage; e.EngineId = f.EngineId; e.TransmissionId = f.TransmissionId;
            e.FuelTypeId = f.FuelTypeId; e.ColorId = f.ColorId;
            return e;
        })
{
    readonly string _dir = Path.Combine(env.WebRootPath ?? "wwwroot", "images");

    public override async Task<CarDto> CreateAsync(CarFormDto f)
    {
        if (f.ImageFile is not null) f.ImageFile = await Save(f.ImageFile);
        return await base.CreateAsync(f);
    }
    public override async Task<bool> UpdateAsync(int id, CarFormDto f)
    {
        if (f.ImageFile is not null) f.ImageFile = await Save(f.ImageFile);
        return await base.UpdateAsync(id, f);
    }

    async Task<IFormFile> Save(IFormFile src)
    {
        Directory.CreateDirectory(_dir);
        var name = $"{Guid.NewGuid()}{Path.GetExtension(src.FileName)}";
        await using var fs = File.Create(Path.Combine(_dir, name));
        await src.CopyToAsync(fs);
        fs.Position = 0;
        return new FormFile(fs, 0, fs.Length, src.Name, name) { Headers = src.Headers, ContentType = src.ContentType };
    }
}
