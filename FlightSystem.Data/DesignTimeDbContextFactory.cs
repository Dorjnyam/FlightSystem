using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using FlightSystem.Data.Context;

namespace FlightSystem.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FlightSystemDbContext>
{
    public FlightSystemDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FlightSystemDbContext>();
        optionsBuilder.UseSqlite("Data Source=flightsystem.db");

        return new FlightSystemDbContext(optionsBuilder.Options);
    }
}
