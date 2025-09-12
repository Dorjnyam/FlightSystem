using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlightSystem.Core.Models;

namespace FlightSystem.Data.Configurations;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.FlightNumber)
            .HasMaxLength(8)
            .IsRequired();

        builder.Property(f => f.DepartureAirport)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(f => f.ArrivalAirport)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(f => f.GateNumber)
            .HasMaxLength(10);

        builder.Property(f => f.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasOne(f => f.Aircraft)
            .WithMany(a => a.Flights)
            .HasForeignKey(f => f.AircraftId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.CreatedByEmployee)
            .WithMany(e => e.CreatedFlights)
            .HasForeignKey(f => f.CreatedByEmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(f => f.FlightPassengers)
            .WithOne(fp => fp.Flight)
            .HasForeignKey(fp => fp.FlightId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(f => f.FlightNumber).IsUnique();
        builder.HasIndex(f => f.ScheduledDeparture);
        builder.HasIndex(f => f.Status);
        builder.HasIndex(f => f.AircraftId);
    }
}
