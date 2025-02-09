using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Position.Infrastructure.Data.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Domain.Models.Position>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Position> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(id => id.Value, value => PositionId.Of(value))
            .ValueGeneratedNever();

        builder.Property(p => p.InstrumentId)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(p => p.Quantity)
            .HasPrecision(18, 8)
            .IsRequired();

        builder.Property(p => p.InitialRate)
            .HasPrecision(18, 8)
            .IsRequired();

        builder.Property(p => p.CurrentRate)
            .HasPrecision(18, 8)
            .IsRequired();

        builder.Property(p => p.Side)
            .IsRequired();
    }
}