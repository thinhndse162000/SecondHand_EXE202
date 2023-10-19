using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Domain.Models
{
    public partial class SecondHandDbContext : DbContext
    {
        public SecondHandDbContext()
        {
        }

        public SecondHandDbContext(DbContextOptions<SecondHandDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductImage> ProductImages { get; set; } = null!;
        public virtual DbSet<Storage> Storages { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot config = builder.Build();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(config.GetConnectionString("SecondHandDb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasDefaultValueSql("(newid())").HasColumnName("CategoryId");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("categoryName");
                entity.Property(e => e.CateImage)
                   .HasMaxLength(500)
                   .HasColumnName("CateImage");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasDefaultValueSql("(newid())").HasColumnName("OrderId");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.FeedBackMessage).HasMaxLength(150);

                entity.Property(e => e.Total).HasColumnType("money");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__PaymentId__3C69FB99");

                entity.HasOne(d => d.Storage)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StorageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__StorageId__3B75D760");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__UserId__3A81B327");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentId).HasDefaultValueSql("(newid())").HasColumnName("PaymentId");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Method).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasDefaultValueSql("(newid())").HasColumnName("ProductId");

                entity.Property(e => e.OriginalImage).HasMaxLength(500);

                entity.Property(e => e.OriginalPrice).HasColumnType("money");

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.Property(e => e.Size).HasMaxLength(10);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Cond)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Categor__32E0915F");
                    

                entity.HasOne(d => d.Storage)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.StorageId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Storage__31EC6D26");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__ProductI__7516F70C1875D44E");

                entity.ToTable("ProductImage");

                entity.Property(e => e.ImageId).HasDefaultValueSql("(newid())").HasColumnName("ImageId");

                entity.Property(e => e.ImageUrl).HasMaxLength(500);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductIm__Produ__36B12243");
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storage");

                entity.Property(e => e.StorageId).HasDefaultValueSql("(newid())").HasColumnName("StorageId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Storages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Storage__UserId__2E1BDC42");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasDefaultValueSql("(newid())").HasColumnName("UserId");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Facebook).HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.Avatar).HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(25);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
