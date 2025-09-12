using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlightSystem.Core.Models;

namespace FlightSystem.Data.Configurations;

public class FlightPassengerConfiguration : IEntityTypeConfiguration<FlightPassenger>
{
    public void Configure(EntityTypeBuilder<FlightPassenger> builder)
    {
        builder.HasKey(fp => fp.Id);

        builder.Property(fp => fp.BookingReference)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(fp => fp.IsCheckedIn)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(fp => fp.SpecialRequests)
            .HasMaxLength(500);

        builder.Property(fp => fp.BaggageInfo)
            .HasMaxLength(200);

        builder.HasOne(fp => fp.Flight)
            .WithMany(f => f.FlightPassengers)
            .HasForeignKey(fp => fp.FlightId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(fp => fp.Passenger)
            .WithMany(p => p.FlightPassengers)
            .HasForeignKey(fp => fp.PassengerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(fp => fp.CheckinByEmployee)
            .WithMany(e => e.CheckedInPassengers)
            .HasForeignKey(fp => fp.CheckinByEmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(fp => fp.SeatAssignments)
            .WithOne(sa => sa.FlightPassenger)
            .HasForeignKey(sa => sa.FlightPassengerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(fp => fp.BoardingPasses)
            .WithOne(bp => bp.FlightPassenger)
            .HasForeignKey(bp => bp.FlightPassengerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(fp => new { fp.FlightId, fp.PassengerId }).IsUnique();
        builder.HasIndex(fp => fp.BookingReference);
        builder.HasIndex(fp => fp.IsCheckedIn);
        builder.HasIndex(fp => fp.CheckinTime);
    }
}
