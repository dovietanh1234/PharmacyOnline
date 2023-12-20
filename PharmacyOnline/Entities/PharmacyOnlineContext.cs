using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PharmacyOnline.Entities;

public partial class PharmacyOnlineContext : DbContext
{
    public PharmacyOnlineContext()
    {
    }

    public PharmacyOnlineContext(DbContextOptions<PharmacyOnlineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<KeyToken> KeyTokens { get; set; }

    public virtual DbSet<Otp> Otps { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductDetail> ProductDetails { get; set; }

    public virtual DbSet<RefreshTokenUsed> RefreshTokenUseds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-DKL7C0F\\SQLEXPRESS;Database=pharmacyOnline;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__candidat__3213E83FCDCAD0CC");

            entity.ToTable("candidate");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IsAtive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isAtive");
            entity.Property(e => e.IsUse)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isUse");
            entity.Property(e => e.Password)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.QuantityProfile)
                .HasDefaultValueSql("((0))")
                .HasColumnName("quantityProfile");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("thumbnail");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__category__3213E83FC6A4162F");

            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CateName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cateName");
        });

        modelBuilder.Entity<KeyToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__keyToken__3213E83F6D27FE97");

            entity.ToTable("keyToken");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("createAt");
            entity.Property(e => e.IdCandidate).HasColumnName("idCandidate");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("refreshToken");

            entity.HasOne(d => d.IdCandidateNavigation).WithMany(p => p.KeyTokens)
                .HasForeignKey(d => d.IdCandidate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__keyToken__idCand__3C69FB99");
        });

        modelBuilder.Entity<Otp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__otp__3213E83FCD990875");

            entity.ToTable("otp");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.LimitTimeToSendOtp)
                .HasColumnType("datetime")
                .HasColumnName("limitTimeToSendOtp");
            entity.Property(e => e.OtpHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("otpHash");
            entity.Property(e => e.OtpSpam)
                .HasColumnType("datetime")
                .HasColumnName("otpSpam");
            entity.Property(e => e.OtpSpamNumber).HasColumnName("otpSpamNumber");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product__3213E83FAD3CEBD1");

            entity.ToTable("product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CateId).HasColumnName("cateId");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.IsAtive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isAtive");
            entity.Property(e => e.ProductDetailId).HasColumnName("productDetailId");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("productName");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("thumbnail");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Cate).WithMany(p => p.Products)
                .HasForeignKey(d => d.CateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__product__cateId__5812160E");

            entity.HasOne(d => d.ProductDetail).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__product__product__59063A47");
        });

        modelBuilder.Entity<ProductDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__productD__3213E83F42334FCD");

            entity.ToTable("productDetail");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AirPressure)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("airPressure");
            entity.Property(e => e.AirVolume)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("airVolume");
            entity.Property(e => e.CapsuleSize)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("capsuleSize");
            entity.Property(e => e.Dies)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("dies");
            entity.Property(e => e.FillingRange)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("fillingRange");
            entity.Property(e => e.FillingSpeed)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("fillingSpeed");
            entity.Property(e => e.MachineDimension)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("machineDimension");
            entity.Property(e => e.MachineSize)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("machineSize");
            entity.Property(e => e.MaxDepthOfFill)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("maxDepthOfFill");
            entity.Property(e => e.MaxDiameterOfTablet)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("maxDiameterOfTablet");
            entity.Property(e => e.MaxPressure)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("maxPressure");
            entity.Property(e => e.ModelNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("modelNumber");
            entity.Property(e => e.NetWeight)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("netWeight");
            entity.Property(e => e.Output)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("output");
            entity.Property(e => e.ProductionCapacity)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("productionCapacity");
            entity.Property(e => e.ShippingWeight)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("shippingWeight");
        });

        modelBuilder.Entity<RefreshTokenUsed>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__refreshT__3213E83FE49EDCAC");

            entity.ToTable("refreshTokenUsed");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdKeyToken).HasColumnName("idKeyToken");
            entity.Property(e => e.RefreshTokenUsed1)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("refreshTokenUsed");

            entity.HasOne(d => d.IdKeyTokenNavigation).WithMany(p => p.RefreshTokenUseds)
                .HasForeignKey(d => d.IdKeyToken)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__refreshTo__idKey__3F466844");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
