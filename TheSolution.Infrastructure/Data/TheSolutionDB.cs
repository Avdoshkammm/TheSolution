using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheSolution.Domain.Entities;

namespace TheSolution.Infrastructure.Data
{
    public class TheSolutionDB : IdentityDbContext<User, IdentityRole, string>
    {
        public TheSolutionDB(DbContextOptions<TheSolutionDB> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");
            builder.Entity<Order>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(uid => uid.UserID)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<OrderProduct>()
                .HasOne(o => o.Order)
                .WithMany(op => op.OrderProducts)
                .HasForeignKey(o => o.OrderID);
            builder.Entity<OrderProduct>()
                .HasOne(p => p.Product)
                .WithMany(op => op.OrderProducts)
                .HasForeignKey(p => p.ProductID);
        }
    }
}
