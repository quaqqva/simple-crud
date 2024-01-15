using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace backend.Database;

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
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("address");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Building).HasMaxLength(45);
            entity.Property(e => e.City).HasMaxLength(45);
            entity.Property(e => e.Country).HasMaxLength(45);
            entity.Property(e => e.Street).HasMaxLength(45);
        });

        modelBuilder.Entity<Chief>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("chief");

            entity.HasIndex(e => e.Id, "ID_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FirstName).HasMaxLength(45);
            entity.Property(e => e.LastName).HasMaxLength(45);
            entity.Property(e => e.Patronymic).HasMaxLength(45);
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Number).HasName("PRIMARY");

            entity.ToTable("contract");

            entity.HasIndex(e => e.CustomerId, "fk_Contract_Customer1_idx");

            entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Contract_Customer1");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("customer");

            entity.HasIndex(e => e.AddressId, "fk_Customer_Address1_idx");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddressId).HasColumnName("Address_ID");
            entity.Property(e => e.Name).HasMaxLength(45);

            entity.HasOne(d => d.Address).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Customer_Address1");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order");

            entity.HasIndex(e => e.ContractNumber, "fk_Order_Contract1_idx");

            entity.HasIndex(e => e.ProductCode, "fk_Order_Product1_idx");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ContractNumber).HasColumnName("Contract_Number");
            entity.Property(e => e.ProductCode).HasColumnName("Product_Code");

            entity.HasOne(d => d.Contract).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ContractNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Order_Contract1");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Order_Product1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.WorkshopNumber, "fk_Product_Workshop1_idx");

            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.WorkshopNumber).HasColumnName("Workshop_Number");

            entity.HasOne(d => d.Workshop).WithMany(p => p.Products)
                .HasForeignKey(d => d.WorkshopNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Product_Workshop1");
        });

        modelBuilder.Entity<Workshop>(entity =>
        {
            entity.HasKey(e => e.Number).HasName("PRIMARY");

            entity.ToTable("workshop");

            entity.HasIndex(e => e.Number, "WorkshopNumber_UNIQUE").IsUnique();

            entity.HasIndex(e => e.ChiefId, "fk_Workshop_Chief_idx");

            entity.Property(e => e.ChiefId).HasColumnName("Chief_ID");
            entity.Property(e => e.Name).HasMaxLength(90);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);

            entity.HasOne(d => d.Chief).WithMany(p => p.Workshops)
                .HasForeignKey(d => d.ChiefId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Workshop_Chief");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
