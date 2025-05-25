using Microsoft.EntityFrameworkCore;

namespace FabricFrame.Models
{
    public class StoreDBContext : DbContext
    {
        public StoreDBContext(DbContextOptions<StoreDBContext> options)
            : base(options) { }

        public DbSet<Design> Designs { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<CartLine> CartLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartLine>().HasKey(c => c.CartItemId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.CartItems)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
