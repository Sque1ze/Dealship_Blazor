using Dealship.Api.Data;
using Dealship.Api.Models;
using Dealship.Shared;

namespace Dealship.Api.Services;

public sealed class FuelTypeService(AppDbContext db)
    : CrudServiceBase<FuelType, FuelTypeDto, FuelTypeDto>(
        db,
        f => new(f.Id, f.Name),
        (src, ent) => { ent.Name = src.Name; return ent; });
