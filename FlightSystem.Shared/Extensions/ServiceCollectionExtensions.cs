using Microsoft.Extensions.DependencyInjection;
using FlightSystem.Shared.Helpers;

namespace FlightSystem.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        services.AddSingleton<IQRCodeHelper, QRCodeHelper>();
        services.AddScoped<IValidationHelper, ValidationHelper>();
        
        return services;
    }
}
