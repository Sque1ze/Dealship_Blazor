// Dealship.Client/Services/LookupCache.cs
using Dealship.Shared;

namespace Dealship.Client.Services;

/// <summary>
/// Пам’ятка-кеш для довідникових таблиць, які потрібні
/// у формах (двигуни, колір …).
/// </summary>
public class LookupCache
{
    private readonly EntityApi<EngineDto, EngineFormDto> _engines;
    private readonly EntityApi<TransmissionDto, TransmissionDto> _transmissions;
    private readonly EntityApi<FuelTypeDto, FuelTypeDto> _fuels;
    private readonly EntityApi<ColorDto, ColorDto> _colors;
    private readonly EntityApi<CustomerDto, CustomerFormDto> _customers;
    private readonly EntityApi<CarDto, CarFormDto> _cars;

    public LookupCache(
        EntityApi<EngineDto, EngineFormDto> engines,
        EntityApi<TransmissionDto, TransmissionDto> transmissions,
        EntityApi<FuelTypeDto, FuelTypeDto> fuels,
        EntityApi<ColorDto, ColorDto> colors,
        EntityApi<CustomerDto, CustomerFormDto> customers,
        EntityApi<CarDto, CarFormDto> cars)
    {
        _engines = engines;
        _transmissions = transmissions;
        _fuels = fuels;
        _colors = colors;
        _customers = customers;
        _cars = cars;
    }

    private PaginatedResult<EngineDto>? _e;
    private PaginatedResult<TransmissionDto>? _t;
    private PaginatedResult<FuelTypeDto>? _f;
    private PaginatedResult<ColorDto>? _c;
    private PaginatedResult<CustomerDto>? _cu;
    private PaginatedResult<CarDto>? _ca;

    // ------- API, які справді використовуються у формах -------------

    public async Task<IEnumerable<EngineDto>> Engines()
        => _e?.Items ?? (_e = await _engines.GetPagedAsync(new()))!.Items;

    public async Task<IEnumerable<TransmissionDto>> Transmissions()
        => _t?.Items ?? (_t = await _transmissions.GetPagedAsync(new()))!.Items;

    public async Task<IEnumerable<FuelTypeDto>> Fuels()
        => _f?.Items ?? (_f = await _fuels.GetPagedAsync(new()))!.Items;

    public async Task<IEnumerable<ColorDto>> Colors()
        => _c?.Items ?? (_c = await _colors.GetPagedAsync(new()))!.Items;

    public async Task<IEnumerable<CustomerDto>> Customers()
        => _cu?.Items ?? (_cu = await _customers.GetPagedAsync(new()))!.Items;

    public async Task<IEnumerable<CarDto>> Cars()
        => _ca?.Items ?? (_ca = await _cars.GetPagedAsync(new()))!.Items;
}
