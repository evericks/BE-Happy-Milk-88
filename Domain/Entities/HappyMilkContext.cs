using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class HappyMilkContext : DbContext
{
    public HappyMilkContext()
    {
    }

    public HappyMilkContext(DbContextOptions<HappyMilkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=evericks.com;Database=HappyMilk;Persist Security Info=False;User ID=sa;Password=Password@@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Admin__3214EC0729A85ED0");

            entity.ToTable("Admin");

            entity.HasIndex(e => e.Username, "UQ__Admin__536C85E4495B18B0").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Password).HasMaxLength(256);
            entity.Property(e => e.Username).HasMaxLength(256);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3214EC07F3B83F17");

            entity.ToTable("Cart");

            entity.HasIndex(e => e.CustomerId, "IX_Cart").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Customer).WithOne(p => p.Cart)
                .HasForeignKey<Cart>(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__CustomerId__6383C8BA");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CartItem__3214EC073E795BC8");

            entity.ToTable("CartItem");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK__CartItem__CartId__6477ECF3");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartItem__Produc__656C112C");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC072AE64E65");

            entity.ToTable("Category");

            entity.HasIndex(e => e.Name, "UQ__Category__737584F62F7512BD").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC0708FB23FA");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.Username, "UQ__Customer__536C85E4D1DFBCA6").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Password).HasMaxLength(256);
            entity.Property(e => e.Phone).HasMaxLength(256);
            entity.Property(e => e.Status).HasMaxLength(256);
            entity.Property(e => e.Username).HasMaxLength(256);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Feedback__3214EC07FCCC6FEF");

            entity.ToTable("Feedback");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__Custom__66603565");

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__OrderI__6754599E");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC07AD2C7A2D");

            entity.ToTable("Order");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(256);
            entity.Property(e => e.Phone).HasMaxLength(256);
            entity.Property(e => e.Receiver).HasMaxLength(256);
            entity.Property(e => e.Status).HasMaxLength(256);

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__CustomerI__68487DD7");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC072DB0107D");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Order__693CA210");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Produ__6A30C649");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC070F7CBCB4");

            entity.ToTable("Product");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Brand).HasMaxLength(256);
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            entity.Property(e => e.MadeIn).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Origin).HasMaxLength(256);
            entity.Property(e => e.Status).HasMaxLength(256);
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => new { e.CategoryId, e.ProductId }).HasName("PK__ProductC__D249F667402DF5AC");

            entity.ToTable("ProductCategory");

            entity.HasIndex(e => e.Id, "UQ__ProductC__3214EC061D89D382").IsUnique();

            entity.HasOne(d => d.Category).WithMany(p => p.ProductCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductCa__Categ__6B24EA82");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductCategories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductCa__Produ__6C190EBB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
