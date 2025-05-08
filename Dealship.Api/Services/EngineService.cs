using Dealship.Api.Data;
using Dealship.Api.Models;
using Dealship.Shared;

namespace Dealship.Api.Services;

public sealed class EngineService(AppDbContext db)
    : CrudServiceBase<Engine, EngineDto, EngineFormDto>(
        db,
        e => new(e.Id, e.Type, e.Volume, e.Horsepower),
        (src, ent) =>
        {
            ent.Type = src.Type;
            ent.Volume = src.Volume;
            ent.Horsepower = src.Horsepower;
            return ent;
        });
