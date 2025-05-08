using Dealship.Api.Data;
using Dealship.Api.Models;
using Dealship.Shared;

namespace Dealship.Api.Services;

public sealed class UserService(AppDbContext db)
    : CrudServiceBase<User, UserDto, UserDto>(
        db,
        u => new(u.Id, u.FullName, u.Email, u.Role),
        (src, ent) =>
        {
            ent.FullName = src.FullName;
            ent.Email = src.Email;
            ent.Role = src.Role;
            return ent;
        });
