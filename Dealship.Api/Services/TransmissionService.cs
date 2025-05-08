using Dealship.Api.Data;
using Dealship.Api.Models;
using Dealship.Shared;

namespace Dealship.Api.Services;

public sealed class TransmissionService(AppDbContext db)
    : CrudServiceBase<Transmission, TransmissionDto, TransmissionDto>(
        db,
        t => new(t.Id, t.Type),
        (src, ent) => { ent.Type = src.Type; return ent; });
