
using Dealship.Api.Data;
using Dealship.Api.Models;
using Dealship.Shared;

namespace Dealship.Api.Services;

public sealed class ColorService(AppDbContext db)
    : CrudServiceBase<Color, ColorDto, ColorDto>(
        db,
        c => new(c.Id, c.Name),
        (src, ent) => { ent.Name = src.Name; return ent; });
