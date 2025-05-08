using Dealship.Api.Services;
using Dealship.Api.Services.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Dealship.Shared;


namespace Dealship.Api.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDealship(this IServiceCollection services)
    {
        // Lookup
        services.AddScoped<ColorService>();
        services.AddScoped<ICrudService<ColorDto, ColorDto>>(sp => sp.GetRequiredService<ColorService>());

        services.AddScoped<FuelTypeService>();
        services.AddScoped<ICrudService<FuelTypeDto, FuelTypeDto>>(sp => sp.GetRequiredService<FuelTypeService>());

        services.AddScoped<TransmissionService>();
        services.AddScoped<ICrudService<TransmissionDto, TransmissionDto>>(sp => sp.GetRequiredService<TransmissionService>());

        services.AddScoped<EngineService>();
        services.AddScoped<ICrudService<EngineDto, EngineFormDto>>(sp => sp.GetRequiredService<EngineService>());

        // Domain
        services.AddScoped<CarService>();

        services.AddScoped<CustomerService>();
        services.AddScoped<ICrudService<CustomerDto, CustomerFormDto>>(sp => sp.GetRequiredService<CustomerService>());

        services.AddScoped<OrderService>();
        services.AddScoped<ICrudService<OrderDto, OrderFormDto>>(sp => sp.GetRequiredService<OrderService>());

        services.AddScoped<UserService>();
        services.AddScoped<ICrudService<UserDto, UserDto>>(sp => sp.GetRequiredService<UserService>());

        return services;
    }
}
