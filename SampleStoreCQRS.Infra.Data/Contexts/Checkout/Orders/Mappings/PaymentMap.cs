using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Domain.Core.Models;

namespace SampleStoreCQRS.Infra.Data.Contexts.Checkout.Orders.Mappings
{
    public class PaymentMap : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder
                .HasKey(x => x.Id);

            builder
                .OwnsOne(x => x.CreditCard, y =>
                {
                    y.Property(x => x.Cvv)
                            .HasColumnName("CreditCard.Cvv");
                    
                    y.Property(x => x.Number)
                            .HasColumnName("CreditCard.Number")
                            .HasColumnType("varchar(19)")
                            .HasMaxLength(19);

                    y.Property(x => x.Validate)
                            .HasColumnName("CreditCard.Validate")
                            .HasColumnType("varchar(19)")
                            .HasMaxLength(19);

                    y.Property(x => x.PrintName)
                        .HasColumnName("CreditCard.PrintName")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    y.Ignore(z => z.AggregateId);
                    y.Ignore(z => z.ValidationResult);
                    y.Ignore(z => z.Timestamp);
                    y.Ignore(z => z.MessageType);
                });

            builder.Ignore(x => x.AggregateId);
            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(x => x.Timestamp);
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.MessageType);
        }
    }
}
