using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlightSystem.Core.Models;

namespace FlightSystem.Data.Configurations;

public class BoardingPassConfiguration : IEntityTypeConfiguration<BoardingPass>
{
    public void Configure(EntityTypeBuilder<BoardingPass> builder)
    {
        builder.HasKey(bp => bp.Id);

        builder.Property(bp => bp.BoardingPassCode)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(bp => bp.QRCode)
            .HasMaxLength(1000);

        builder.Property(bp => bp.IsBoardingComplete)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(bp => bp.FlightPassenger)
            .WithMany(fp => fp.BoardingPasses)
            .HasForeignKey(bp => bp.FlightPassengerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(bp => bp.SeatAssignment)
            .WithMany(sa => sa.BoardingPasses)
            .HasForeignKey(bp => bp.SeatAssignmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(bp => bp.IssuedByEmployee)
            .WithMany(e => e.IssuedBoardingPasses)
            .HasForeignKey(bp => bp.IssuedByEmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(bp => bp.BoardingByEmployee)
            .WithMany()
            .HasForeignKey(bp => bp.BoardingByEmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(bp => bp.BoardingPassCode).IsUnique();
        builder.HasIndex(bp => bp.FlightPassengerId);
        builder.HasIndex(bp => bp.SeatAssignmentId);
        builder.HasIndex(bp => bp.IsBoardingComplete);
        builder.HasIndex(bp => bp.IssuedAt);
    }
}
