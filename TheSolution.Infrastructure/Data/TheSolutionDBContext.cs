using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheSolution.Domain.Entities;

namespace TheSolution.Infrastructure.Data
{
    public class TheSolutionDBContext : IdentityDbContext<User, IdentityRole, string>
    {
        public TheSolutionDBContext(DbContextOptions<TheSolutionDBContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(u => u.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<OrderProduct>()
                .HasOne(o => o.Order)
                .WithMany()
                .HasForeignKey(op => op.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<OrderProduct>()
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<OrderProduct>()
                .HasIndex(op => new { op.OrderID, op.ProductID })
                .IsUnique();


            //builder.Entity<Order>()
            //    .Property(o => o.TotalAmount);
            //builder.Entity<Order>()
            //    .HasOne(u => u.User)
            //    .WithMany()
            //    .HasForeignKey(uid => uid.UserID)
            //    .OnDelete(DeleteBehavior.Cascade);
            //builder.Entity<OrderProduct>()
            //    .Property()
            //    .HasOne(o => o.Order)
            //    .WithMany()
            //    .HasForeignKey(ID => ID);
            //builder.Entity<OrderProduct>()
            //    .HasOne(o => o.Order)
            //    .WithMany(op => op.OrderProducts)
            //    .HasForeignKey(o => o.OrderID);
            //builder.Entity<OrderProduct>()
            //    .HasOne(p => p.Product)
            //    .WithMany(op => op.OrderProducts)
            //    .HasForeignKey(p => p.ProductID);
        }
    }
}
