// Dealship.Client / Program.cs
using System.Net.Http;
using Blazored.Toast;
using Dealship.Client;
using Dealship.Client.Services;
using Dealship.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// --------------------------------------------------------------------
// 1) HttpClient, що звертається до back-end API
// --------------------------------------------------------------------
builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7089/")   // API-host
});

// --------------------------------------------------------------------
// 2) Toast-нотифікації
// --------------------------------------------------------------------
builder.Services.AddBlazoredToast();

// --------------------------------------------------------------------
// 3) Базовий REST-клієнт + generic EntityApi
// --------------------------------------------------------------------
builder.Services.AddScoped<ApiClient>();

// ---------- LOOKUP-довідники ---------------------------------------
// Потрібні ОБИДВА варіанти:   (DTO , DTO)  - для списку
//                             (DTO , Form) - для Create/Edit
builder.Services.AddScoped<EntityApi<ColorDto, ColorDto>>
        (sp => new(sp.GetRequiredService<ApiClient>(), "api/colors"));
builder.Services.AddScoped<EntityApi<ColorDto, ColorFormDto>>
        (sp => new(sp.GetRequiredService<ApiClient>(), "api/colors"));

builder.Services.AddScoped<EntityApi<FuelTypeDto, FuelTypeDto>>
        (sp => new(sp.GetRequiredService<ApiClient>(), "api/fueltypes"));
builder.Services.AddScoped<EntityApi<FuelTypeDto, FuelTypeFormDto>>
        (sp => new(sp.GetRequiredService<ApiClient>(), "api/fueltypes"));

builder.Services.AddScoped<EntityApi<TransmissionDto, TransmissionDto>>
        (sp => new(sp.GetRequiredService<ApiClient>(), "api/transmissions"));
builder.Services.AddScoped<EntityApi<TransmissionDto, TransmissionFormDto>>
        (sp => new(sp.GetRequiredService<ApiClient>(), "api/transmissions"));

// ---------- Інші сутності ------------------------------------------
builder.Services.AddScoped<EntityApi<EngineDto, EngineFormDto>>
        (sp => new(sp.GetRequiredService<ApiClient>(), "api/engines"));
builder.Services.AddScoped<EntityApi<CustomerDto, CustomerFormDto>>
        (sp => new(sp.GetRequiredService<ApiClient>(), "api/customers"));
builder.Services.AddScoped<EntityApi<UserDto, UserFormDto>>
        (sp => new(sp.GetRequiredService<ApiClient>(), "api/users"));
builder.Services.AddScoped<EntityApi<OrderDto, OrderFormDto>>
        (sp => new(sp.GetRequiredService<ApiClient>(), "api/orders"));

builder.Services.AddScoped<EntityApi<UserDto, UserDto>>
        (sp => new(sp.GetRequiredService<ApiClient>(), "api/users"));

builder.Services.AddScoped<EntityApi<CustomerDto, CustomerDto>>
        (sp => new(sp.GetRequiredService<ApiClient>(), "api/customers"));

builder.Services.AddScoped<EntityApi<EngineDto, EngineDto>>
        (sp => new(sp.GetRequiredService<ApiClient>(), "api/engines"));

builder.Services.AddScoped<EntityApi<OrderDto, OrderDto>>
        (sp => new(sp.GetRequiredService<ApiClient>(), "api/orders"));


// ---------- Cars ----------------------------------------------------
// потрібен EntityApi   +   спеціальний CarApi (multipart)
builder.Services.AddScoped<EntityApi<CarDto, CarFormDto>>
        (sp => new(sp.GetRequiredService<ApiClient>(), "api/cars"));
builder.Services.AddScoped<CarApi>();

// --------------------------------------------------------------------
// 4) Кеш lookup-даних
// --------------------------------------------------------------------
builder.Services.AddScoped<LookupCache>();

await builder.Build().RunAsync();
