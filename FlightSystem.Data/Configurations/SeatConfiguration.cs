using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlightSystem.Core.Models;

namespace FlightSystem.Data.Configurations;

public class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.SeatNumber)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(s => s.Row)
            .HasMaxLength(5)
            .IsRequired();

        builder.Property(s => s.Column)
            .HasMaxLength(5)
            .IsRequired();

        builder.Property(s => s.Class)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(s => s.IsWindowSeat)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(s => s.IsAisleSeat)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(s => s.IsEmergencyExit)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(s => s.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasOne(s => s.Aircraft)
            .WithMany(a => a.Seats)
            .HasForeignKey(s => s.AircraftId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.SeatAssignments)
            .WithOne(sa => sa.Seat)
            .HasForeignKey(sa => sa.SeatId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(s => new { s.AircraftId, s.SeatNumber }).IsUnique();
        builder.HasIndex(s => s.AircraftId);
        builder.HasIndex(s => s.Class);
        builder.HasIndex(s => s.IsActive);
    }
}
