using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;

namespace SampleStoreCQRS.Infra.Data.Contexts.Checkout.Orders.Mappings
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder
                .HasKey(x => x.Id);
            
            builder
              .Property(x => x.Quantity)
              .IsRequired();

            builder
              .Property(x => x.Price)
              .IsRequired();

            builder
               .Property(x => x.Description)
               .HasMaxLength(1024)
               .IsRequired();

            builder.HasOne(x => x.Product);

            builder.Ignore(x => x.AggregateId);
            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(x => x.Timestamp);
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.MessageType);
        }
    }
}