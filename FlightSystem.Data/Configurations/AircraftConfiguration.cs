using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlightSystem.Core.Models;

namespace FlightSystem.Data.Configurations;

public class AircraftConfiguration : IEntityTypeConfiguration<Aircraft>
{
    public void Configure(EntityTypeBuilder<Aircraft> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.AircraftCode)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.AircraftType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.TotalSeats)
            .IsRequired();

        builder.Property(a => a.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasMany(a => a.Flights)
            .WithOne(f => f.Aircraft)
            .HasForeignKey(f => f.AircraftId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.Seats)
            .WithOne(s => s.Aircraft)
            .HasForeignKey(s => s.AircraftId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(a => a.AircraftCode).IsUnique();
        builder.HasIndex(a => a.AircraftType);
        builder.HasIndex(a => a.IsActive);
    }
}
