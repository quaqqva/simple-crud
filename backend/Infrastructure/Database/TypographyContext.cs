using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Database;

public partial class TypographyContext : DbContext
{
    public TypographyContext()
    {
    }

    public TypographyContext(DbContextOptions<TypographyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Chief> Chiefs { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Workshop> Workshops { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Address");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Building).HasMaxLength(45);
            entity.Property(e => e.City).HasMaxLength(45);
            entity.Property(e => e.Country).HasMaxLength(45);
            entity.Property(e => e.Street).HasMaxLength(45);
        });

        modelBuilder.Entity<Chief>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Chief");

            entity.HasIndex(e => e.Id, "ID_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FirstName).HasMaxLength(45);
            entity.Property(e => e.LastName).HasMaxLength(45);
            entity.Property(e => e.Patronymic).HasMaxLength(45);
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Contract");

            entity.HasIndex(e => e.CustomerId, "fk_Contract_Customer1_idx");

            entity.Property(e => e.Id).HasColumnName("Number");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

            entity
                .HasOne(d => d.Customer)
                .WithMany(p => p.Contracts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Contract_Customer1");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.AddressId, "fk_Customer_Address1_idx");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddressId).HasColumnName("Address_ID");
            entity.Property(e => e.Name).HasMaxLength(45);

            entity
                .HasOne(d => d.Address)
                .WithMany(p => p.Customers)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Customer_Address1");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Order");

            entity.HasIndex(e => e.ContractId, "fk_Order_Contract1_idx");

            entity.HasIndex(e => e.ProductId, "fk_Order_Product1_idx");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ContractId).HasColumnName("Contract_Number");
            entity.Property(e => e.ProductId).HasColumnName("Product_Code");

            entity
                .HasOne(d => d.Contract)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Order_Contract1");

            entity
                .HasOne(d => d.Product)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Order_Product1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Product");

            entity.HasIndex(e => e.WorkshopId, "fk_Product_Workshop1_idx");

            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.Id).HasColumnName("Code");
            entity.Property(e => e.WorkshopId).HasColumnName("Workshop_Number");

            entity
                .HasOne(d => d.Workshop)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.WorkshopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Product_Workshop1");
        });

        modelBuilder.Entity<Workshop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Workshop");

            entity.HasIndex(e => e.Id, "WorkshopId_UNIQUE").IsUnique();

            entity.HasIndex(e => e.ChiefId, "fk_Workshop_Chief_idx");

            entity.Property(e => e.Id).HasColumnName("Number");
            entity.Property(e => e.ChiefId).HasColumnName("Chief_ID");
            entity.Property(e => e.Name).HasMaxLength(90);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);

            entity
                .HasOne(d => d.Chief)
                .WithMany(p => p.Workshops)
                .HasForeignKey(d => d.ChiefId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Workshop_Chief");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}