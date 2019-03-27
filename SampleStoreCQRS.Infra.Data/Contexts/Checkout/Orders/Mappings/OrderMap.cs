using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;

namespace SampleStoreCQRS.Infra.Data.Contexts.Checkout.Orders.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder
                .HasKey(x => x.Id);

            builder
               .HasIndex(x => x.Number)
               .IsUnique();

            builder
               .HasOne(x => x.Customer);

            builder
               .HasMany(x => x.Items);

            builder
                .OwnsOne(x => x.DiscountCupon, y =>
                {
                    y.Property(z => z.Cod)
                            .HasColumnName("DiscountCupon.Cod");

                    y.Property(z => z.Percentage)
                            .HasColumnName("DiscountCupon.Percentage");

                    y.OwnsOne(z => z.ValidadePeriod, a =>
                    {
                        a.Property(z => z.Start)
                            .HasColumnName("DiscountCupon.ValidadePeriod.Start");

                        a.Property(z => z.End)
                            .HasColumnName("DiscountCupon.ValidadePeriod.End");

                        a.Ignore(z => z.AggregateId);
                        a.Ignore(z => z.MessageType);
                        a.Ignore(z => z.ValidationResult);
                        a.Ignore(z => z.Timestamp);
                    });

                    y.Ignore(z => z.Expired);
                    y.Ignore(z => z.MessageType);
                    y.Ignore(z => z.AggregateId);
                    y.Ignore(z => z.ValidationResult);
                    y.Ignore(z => z.Timestamp);
                });

            builder.Ignore(x => x.Total);
            builder.Ignore(x => x.TotalWithDiscount);
            builder.Ignore(x => x.Notifications);
            builder.Ignore(x => x.MessageType);
            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(x => x.Timestamp);
            builder.Ignore(x => x.ValidationResult);
        }
    }
}
