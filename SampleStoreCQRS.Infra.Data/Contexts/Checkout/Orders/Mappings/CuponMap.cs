using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleStoreCQRS.Domain.Contexts.Promotions.Models;

namespace SampleStoreCQRS.Infra.Data.Contexts.Checkout.Orders.Mappings
{
    public class CuponMap : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.ToTable("Cupons");

            builder
                .HasKey(x => x.Id);

            builder
                .HasIndex(x => x.Cod)
                .IsUnique();
            
            builder.OwnsOne(x => x.ValidadePeriod, y =>
            {
                y.Property(z => z.Start)
                    .HasColumnName("ValidadePeriod.Start");

                y.Property(z => z.End)
                    .HasColumnName("ValidadePeriod.End");

                y.Ignore(z => z.AggregateId);
                y.Ignore(z => z.MessageType);
                y.Ignore(z => z.ValidationResult);
                y.Ignore(z => z.Timestamp);
            });

            builder.Ignore(x => x.AggregateId);
            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(x => x.Timestamp);
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.MessageType);
        }
    }
}
