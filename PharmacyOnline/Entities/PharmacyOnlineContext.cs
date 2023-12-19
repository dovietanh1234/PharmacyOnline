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

    public virtual DbSet<KeyToken> KeyTokens { get; set; }

    public virtual DbSet<Otp> Otps { get; set; }

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
                .HasMaxLength(255)
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

        modelBuilder.Entity<KeyToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__keyToken__3213E83F6D27FE97");

            entity.ToTable("keyToken");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCandidate).HasColumnName("idCandidate");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(255)
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

        modelBuilder.Entity<RefreshTokenUsed>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__refreshT__3213E83FE49EDCAC");

            entity.ToTable("refreshTokenUsed");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdKeyToken).HasColumnName("idKeyToken");
            entity.Property(e => e.RefreshTokenUsed1)
                .HasMaxLength(255)
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
