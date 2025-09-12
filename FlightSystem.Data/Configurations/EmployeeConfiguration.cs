using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlightSystem.Core.Models;

namespace FlightSystem.Data.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.EmployeeCode)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(e => e.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Password)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Role)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(e => e.WorkStationId)
            .HasMaxLength(20);

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasMany(e => e.CreatedFlights)
            .WithOne(f => f.CreatedByEmployee)
            .HasForeignKey(f => f.CreatedByEmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.CheckedInPassengers)
            .WithOne(fp => fp.CheckinByEmployee)
            .HasForeignKey(fp => fp.CheckinByEmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.SeatAssignments)
            .WithOne(sa => sa.AssignedByEmployee)
            .HasForeignKey(sa => sa.AssignedByEmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.IssuedBoardingPasses)
            .WithOne(bp => bp.IssuedByEmployee)
            .HasForeignKey(bp => bp.IssuedByEmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => e.EmployeeCode).IsUnique();
        builder.HasIndex(e => e.Email).IsUnique();
        builder.HasIndex(e => e.Role);
        builder.HasIndex(e => e.IsActive);
    }
}
