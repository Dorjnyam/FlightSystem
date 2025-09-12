using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FlightSystem.Core.Interfaces.Repositories;
using FlightSystem.Data.Context;
using FlightSystem.Data.Repositories;

namespace FlightSystem.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
                services.AddDbContext<FlightSystemDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection") ?? "Data Source=flightsystem.db"));

                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IAircraftRepository, AircraftRepository>();
        services.AddScoped<IFlightRepository, FlightRepository>();
        services.AddScoped<IPassengerRepository, PassengerRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ISeatRepository, SeatRepository>();
        services.AddScoped<IFlightPassengerRepository, FlightPassengerRepository>();
        services.AddScoped<ISeatAssignmentRepository, SeatAssignmentRepository>();
        services.AddScoped<IBoardingPassRepository, BoardingPassRepository>();

        return services;
    }

    public static async Task EnsureDatabaseCreatedAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FlightSystemDbContext>();
        
        await context.Database.EnsureCreatedAsync();
        await Seed.DataSeeder.SeedDataAsync(context);
    }
}
