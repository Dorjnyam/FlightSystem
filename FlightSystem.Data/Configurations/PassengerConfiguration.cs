using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlightSystem.Core.Models;

namespace FlightSystem.Data.Configurations;

public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.PassportNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Nationality)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(100);

        builder.Property(p => p.Phone)
            .HasMaxLength(20);

        builder.Property(p => p.Type)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasMany(p => p.FlightPassengers)
            .WithOne(fp => fp.Passenger)
            .HasForeignKey(fp => fp.PassengerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.PassportNumber).IsUnique();
        builder.HasIndex(p => p.Email);
        builder.HasIndex(p => p.Type);
        builder.HasIndex(p => p.Nationality);
    }
}
