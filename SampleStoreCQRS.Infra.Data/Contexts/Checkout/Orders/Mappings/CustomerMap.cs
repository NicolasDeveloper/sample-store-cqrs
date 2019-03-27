using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;

namespace SampleStoreCQRS.Infra.Data.Contexts.Checkout.Orders.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder
                .HasKey(x => x.Id);

            builder
                .OwnsOne(x => x.Email, y =>
                {
                    y.HasIndex(z => z.Address).IsUnique();
                    y.Property(z => z.Address)
                            .HasColumnName("Email.Address");

                    y.Ignore(z => z.AggregateId);
                    y.Ignore(z => z.ValidationResult);
                    y.Ignore(z => z.Timestamp);
                    y.Ignore(z => z.MessageType);
                });

            builder
                .OwnsOne(x => x.Document, y =>
                {
                    y.HasIndex(z => z.Number).IsUnique();
                    y.Property(z => z.Number)
                            .HasColumnName("Document.Number");

                    y.Ignore(z => z.AggregateId);
                    y.Ignore(z => z.ValidationResult);
                    y.Ignore(z => z.Timestamp);
                    y.Ignore(z => z.MessageType);
                });

            builder
                .OwnsOne(x => x.Name, y =>
                {
                    y.Property(z => z.FirstName)
                           .HasColumnName("Name.FirstName");

                    y.Property(z => z.LastName)
                           .HasColumnName("Name.LastName");

                    y.Ignore(z => z.AggregateId);
                    y.Ignore(z => z.ValidationResult);
                    y.Ignore(z => z.Timestamp);
                    y.Ignore(z => z.MessageType);
                });

            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(x => x.Notifications);
            builder.Ignore(x => x.Timestamp);
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.MessageType);
        }
    }
}
