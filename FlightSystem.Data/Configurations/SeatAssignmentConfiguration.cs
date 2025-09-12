using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlightSystem.Core.Models;

namespace FlightSystem.Data.Configurations;

public class SeatAssignmentConfiguration : IEntityTypeConfiguration<SeatAssignment>
{
    public void Configure(EntityTypeBuilder<SeatAssignment> builder)
    {
        builder.HasKey(sa => sa.Id);

        builder.Property(sa => sa.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(sa => sa.Notes)
            .HasMaxLength(500);

        builder.HasOne(sa => sa.FlightPassenger)
            .WithMany(fp => fp.SeatAssignments)
            .HasForeignKey(sa => sa.FlightPassengerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sa => sa.Seat)
            .WithMany(s => s.SeatAssignments)
            .HasForeignKey(sa => sa.SeatId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(sa => sa.AssignedByEmployee)
            .WithMany(e => e.SeatAssignments)
            .HasForeignKey(sa => sa.AssignedByEmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(sa => sa.BoardingPasses)
            .WithOne(bp => bp.SeatAssignment)
            .HasForeignKey(bp => bp.SeatAssignmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(sa => new { sa.SeatId, sa.FlightPassengerId }).IsUnique();
        builder.HasIndex(sa => sa.IsActive);
        builder.HasIndex(sa => sa.AssignedAt);
    }
}
