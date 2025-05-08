using Dealship.Api.Data;
using Dealship.Api.Models;
using Dealship.Shared;

namespace Dealship.Api.Services;

public sealed class CustomerService(AppDbContext db)
    : CrudServiceBase<Customer, CustomerDto, CustomerFormDto>(
        db,
        c => new(c.Id, c.FullName, c.Email, c.PhoneNumber),
        (src, ent) =>
        {
            ent.FullName = src.FullName;
            ent.Email = src.Email;
            ent.PhoneNumber = src.PhoneNumber;
            return ent;
        });
