
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SampleStoreCQRS.Infra.Data.Contexts.Checkout.Orders.Mappings;
using SampleStoreCQRS.Domain.Contexts.Promotions.Models;

namespace SampleStoreCQRS.Infra.Data.Contexts.Common.DataContext
{
    public class SampleStoreCQRSDataContext: DbContext
    {
        private readonly IHostingEnvironment _env;

        public SampleStoreCQRSDataContext(IHostingEnvironment env)
        {
            _env = env;
            
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CuponMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new OrderItemMap());
            modelBuilder.ApplyConfiguration(new PaymentMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            optionsBuilder
                .UseLazyLoadingProxies()
                .EnableDetailedErrors()
                .UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}
