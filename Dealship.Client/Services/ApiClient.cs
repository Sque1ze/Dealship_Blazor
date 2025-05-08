using Dealship.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Dealship.Client.Services;

/// <summary>
/// Легковажний обгортка над HttpClient для CRUD-операцій.
/// </summary>
public class ApiClient
{
    private readonly HttpClient _http;
    public ApiClient(HttpClient http) => _http = http;

    public Task<PaginatedResult<T>?> GetPagedAsync<T>(string url, QueryParams qp)
        => _http.GetFromJsonAsync<PaginatedResult<T>>
              ($"{url}?Page={qp.Page}&Size={qp.Size}&Search={qp.Search}&Sort={qp.Sort}");

    public Task<T?> GetAsync<T>(string url, int id)
        => _http.GetFromJsonAsync<T>($"{url}/{id}");

    public async Task CreateAsync(string url, HttpContent body)
    {
        var res = await _http.PostAsync(url, body);
        res.EnsureSuccessStatusCode();
    }

    public async Task UpdateAsync(string url, int id, HttpContent body)
    {
        var res = await _http.PutAsync($"{url}/{id}", body);
        res.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(string url, int id)
    {
        var res = await _http.DeleteAsync($"{url}/{id}");
        res.EnsureSuccessStatusCode();
    }
}
