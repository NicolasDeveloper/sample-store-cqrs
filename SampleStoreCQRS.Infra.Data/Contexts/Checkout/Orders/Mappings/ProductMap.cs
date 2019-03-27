using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;

namespace SampleStoreCQRS.Infra.Data.Contexts.Checkout.Orders.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Description)
                .HasMaxLength(1024)
                .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired();

            builder.Property(x => x.QuantityOnHand)
                .IsRequired();

            builder.Property(x => x.Title)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(x => x.Image);

            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(x => x.Notifications);
            builder.Ignore(x => x.Timestamp);
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.MessageType);
        }
    }
}
