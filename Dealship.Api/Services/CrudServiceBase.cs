using Dealship.Api.Data;
using Dealship.Api.Services.Interfaces;
using Dealship.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dealship.Api.Services;
public abstract class CrudServiceBase<TEntity, TDto, TForm> : ICrudService<TDto, TForm>
    where TEntity : class, new()
{
    protected readonly AppDbContext _db;
    private readonly DbSet<TEntity> _set;
    private readonly Func<TEntity, TDto> _toDto;
    private readonly Func<TForm, TEntity, TEntity> _map;

    protected CrudServiceBase(AppDbContext db,
                              Func<TEntity, TDto> toDto,
                              Func<TForm, TEntity, TEntity> map)
    {
        _db = db;
        _set = db.Set<TEntity>();
        _toDto = toDto;
        _map = map;
    }

    public virtual async Task<PaginatedResult<TDto>> GetAsync(QueryParams qp)
    {
        var q = _set.AsNoTracking();
        var total = await q.CountAsync();
        var items = await q
            .Skip((qp.Page - 1) * qp.Size)
            .Take(qp.Size)
            .Select(e => _toDto(e))
            .ToListAsync();
        return new(items, total, qp.Page, qp.Size);
    }

    public virtual async Task<TDto?> GetByIdAsync(int id)
    {
        var ent = await _set.FindAsync(id);
        return ent is null ? default : _toDto(ent);
    }

    public virtual async Task<TDto> CreateAsync(TForm f)
    {
        var ent = _map(f, new TEntity());
        _set.Add(ent);
        await _db.SaveChangesAsync();
        return _toDto(ent);
    }

    public virtual async Task<bool> UpdateAsync(int id, TForm f)
    {
        var ent = await _set.FindAsync(id);
        if (ent is null) return false;
        _map(f, ent);
        await _db.SaveChangesAsync();
        return true;
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var ent = await _set.FindAsync(id);
        if (ent is null) return false;
        _set.Remove(ent);
        await _db.SaveChangesAsync();
        return true;
    }
}
