using Blazorcrud.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazorcrud.Server.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Store> StoresDb { get; set; }
    public virtual DbSet<Category> CategoriesDb { get; set; }
    public virtual DbSet<Product> ProductsDb { get; set; }
    public virtual DbSet<Raincheck> RainchecksDb { get; set; }
    public virtual DbSet<CartItem> CartItemsDb { get; set; }
    public virtual DbSet<Order> OrdersDb { get; set; }
    public virtual DbSet<OrderDetail> OrderDetailsDb { get; set; }
    public virtual DbSet<Person> People { get; set; }
    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<Upload> Uploads { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_dbo.Categories");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK_dbo.Products");
            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasConstraintName("FK_dbo.Products_dbo.Categories_CategoryId");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK_dbo.Stores");
        });

        modelBuilder.Entity<Raincheck>(entity =>
        {
            entity.HasKey(e => e.RaincheckId).HasName("PK_dbo.Rainchecks");
            entity.HasOne(d => d.Product).WithMany(p => p.Rainchecks).HasConstraintName("FK_dbo.Rainchecks_dbo.Products_ProductId");
            entity.HasOne(d => d.Store).WithMany(p => p.Rainchecks).HasConstraintName("FK_dbo.Rainchecks_dbo.Stores_StoreId");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK_dbo.CartItems");
            entity.HasOne(d => d.Product).WithMany(p => p.CartItems).HasConstraintName("FK_dbo.CartItems_dbo.Products_ProductId");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_dbo.Orders");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK_dbo.OrderDetails");
            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails).HasConstraintName("FK_dbo.OrderDetails_dbo.Orders_OrderId");
            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails).HasConstraintName("FK_dbo.OrderDetails_dbo.Products_ProductId");
        });

//        OnModelCreatingPartial(modelBuilder);
    }

//    public void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
