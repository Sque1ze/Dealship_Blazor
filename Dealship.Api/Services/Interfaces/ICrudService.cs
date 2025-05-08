using Dealship.Shared;
using System.Threading.Tasks;

namespace Dealship.Api.Services.Interfaces;
public interface ICrudService<TDto, TForm>
{
    Task<PaginatedResult<TDto>> GetAsync(QueryParams qp);
    Task<TDto?> GetByIdAsync(int id);
    Task<TDto> CreateAsync(TForm dto);
    Task<bool> UpdateAsync(int id, TForm dto);
    Task<bool> DeleteAsync(int id);
}
