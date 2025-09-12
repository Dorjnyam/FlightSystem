using Microsoft.EntityFrameworkCore;
using FlightSystem.Core.Models;
using FlightSystem.Data.Configurations;

namespace FlightSystem.Data.Context;

public class FlightSystemDbContext(DbContextOptions<FlightSystemDbContext> options) : DbContext(options)
{
    public DbSet<Aircraft> Aircraft { get; set; } = null!;
    public DbSet<Flight> Flights { get; set; } = null!;
    public DbSet<Passenger> Passengers { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Seat> Seats { get; set; } = null!;
    public DbSet<FlightPassenger> FlightPassengers { get; set; } = null!;
    public DbSet<SeatAssignment> SeatAssignments { get; set; } = null!;
    public DbSet<BoardingPass> BoardingPasses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FlightSystemDbContext).Assembly);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetColumnType("TEXT"); 
                }
            }
        }

        base.OnModelCreating(modelBuilder);
    }
}
