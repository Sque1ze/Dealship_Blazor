using Dealship.Shared;
using System.Net.Http.Json;
using System.Net.Http;

namespace Dealship.Client.Services;

public class EntityApi<TDto, TForm>
{
    private readonly ApiClient _api;
    private readonly string _url;

    public EntityApi(ApiClient api, string url)
    {
        _api = api;
        _url = url;
    }

    /* ---------- читання ---------- */

    public Task<PaginatedResult<TDto>?> GetPagedAsync(QueryParams qp)
        => _api.GetPagedAsync<TDto>(_url, qp);

    public Task<TDto?> GetByIdAsync(int id)
        => _api.GetAsync<TDto>(_url, id);

    /* ---------- CRUD (JSON) ------ */

    public Task CreateAsync(TForm dto)
        => _api.CreateAsync(_url, JsonContent.Create(dto));

    public Task UpdateAsync(int id, TForm dto)
        => _api.UpdateAsync(_url, id, JsonContent.Create(dto));

    public Task DeleteAsync(int id)
        => _api.DeleteAsync(_url, id);

    /* ---------- multipart (для Car) */

    public Task CreateMultiAsync(MultipartFormDataContent content)
        => _api.CreateAsync(_url, content);

    public Task UpdateMultiAsync(int id, MultipartFormDataContent content)
        => _api.UpdateAsync(_url, id, content);
}
