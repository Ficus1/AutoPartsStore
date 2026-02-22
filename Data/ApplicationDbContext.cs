using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoPartsStore.Data;

public class AutoPartsContext (DbContextOptions<AutoPartsContext > options) : IdentityDbContext<ApplicationUser>(options)
{
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Part> Parts { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }
    public virtual DbSet<Manufacturer> Manufacturers { get; set; }
    public virtual DbSet<CartItem> CartItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Настройка связей
        modelBuilder.Entity<Part>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Parts)
            .HasForeignKey(p => p.CategoryId);
            
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);
            
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Part)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.PartId);
            
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId);

        modelBuilder.Entity<Part>()
            .HasOne(p => p.Manufacturer)
            .WithMany(m => m.Parts)
            .HasForeignKey(p => p.ManufacturerId);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.User)
            .WithMany()
            .HasForeignKey(ci => ci.UserId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Part)
            .WithMany()
            .HasForeignKey(ci => ci.PartId)
            .OnDelete(DeleteBehavior.Restrict);
            
        // Уникальный индекс для предотвращения дублирования товаров в корзине
        modelBuilder.Entity<CartItem>()
            .HasIndex(ci => new { ci.UserId, ci.PartId })
            .IsUnique();
    }
}
