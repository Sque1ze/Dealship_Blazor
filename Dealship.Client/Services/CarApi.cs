using Dealship.Client.Services;
using Dealship.Shared;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

public class CarApi
{
    private readonly EntityApi<CarDto, CarFormDto> _api;
    public CarApi(EntityApi<CarDto, CarFormDto> api) => _api = api;

    /* -------- Читання -------- */
    public Task<PaginatedResult<CarDto>?> GetListAsync(QueryParams qp)
        => _api.GetPagedAsync(qp);

    public Task<CarDto?> GetByIdAsync(int id)
        => _api.GetByIdAsync(id);

    /* -------- CRUD -------- */

    public Task DeleteAsync(int id) => _api.DeleteAsync(id);

    public Task CreateAsync(CarFormDto dto, IBrowserFile? file) =>
        file is null
            ? _api.CreateAsync(dto)                    // <‐‐ JSON
            : _api.CreateMultiAsync(BuildForm(dto, file));

    public Task UpdateAsync(int id, CarFormDto dto, IBrowserFile? file) =>
        file is null
            ? _api.UpdateAsync(id, dto)                // <‐‐ JSON
            : _api.UpdateMultiAsync(id, BuildForm(dto, file));

    /* -------- helper -------- */

    private static MultipartFormDataContent BuildForm(CarFormDto dto, IBrowserFile file)
    {
        var form = new MultipartFormDataContent
        {
            { JsonContent.Create(dto), "dto" }
        };

        var sc = new StreamContent(file.OpenReadStream(5_000_000));
        sc.Headers.ContentType = new(file.ContentType);
        form.Add(sc, nameof(CarFormDto.ImageFile), file.Name);

        return form;
    }
}
