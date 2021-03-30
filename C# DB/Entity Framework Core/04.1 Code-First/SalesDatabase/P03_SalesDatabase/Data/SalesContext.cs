using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext()
        {

        }

        public SalesContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Sales;Integrated Security=True");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(x =>
            {
                x.HasKey(p => p.ProductId);

                x.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(true);

                x.Property(p => p.Quantity)
                    .IsRequired(true);

                x.Property(p => p.Price)
                    .IsRequired(true);

                x.Property(p => p.Description)
                    .HasMaxLength(250)
                    .IsRequired(false)
                    .IsUnicode(true)
                    .HasDefaultValue("No description");
            });

            modelBuilder.Entity<Customer>(x =>
            {
                x.HasKey(c => c.CustomerId);

                x.Property(c => c.Name)
                    .HasMaxLength(100)
                    .IsRequired(true)
                    .IsUnicode(true);

                x.Property(c => c.Email)
                    .HasMaxLength(80)
                    .IsRequired(true)
                    .IsUnicode(false);

                x.Property(c => c.CreditCardNumber)
                    .IsRequired(true)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<Store>(x =>
            {
                x.HasKey(st => st.StoreId);

                x.Property(st => st.Name)
                    .HasMaxLength(80)
                    .IsRequired(true)
                    .IsUnicode(true);

            });

            modelBuilder.Entity<Sale>(x =>
            {
                x.HasKey(s => s.SaleId);

                x.Property(s => s.Date)
                    .HasDefaultValueSql("GETDATE()");
                
                x.HasOne(s => s.Product)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(s => s.ProductId);

                x.HasOne(s => s.Customer)
                    .WithMany(c => c.Sales)
                    .HasForeignKey(s => s.CustomerId);

                x.HasOne(s => s.Store)
                    .WithMany(st => st.Sales)
                    .HasForeignKey(s => s.StoreId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
