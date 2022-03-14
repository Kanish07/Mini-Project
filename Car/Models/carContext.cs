using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Car.Models
{
    public partial class carContext : DbContext
    {
        public carContext()
        {
        }

        public carContext(DbContextOptions<carContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("CarDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("cars");

                entity.Property(e => e.Carid)
                    .HasColumnName("carid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Carcity)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("carcity");

                entity.Property(e => e.Carfuel)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("carfuel");

                entity.Property(e => e.Carmake)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("carmake");

                entity.Property(e => e.Carmodelname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("carmodelname");

                entity.Property(e => e.Carshifttype)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("carshifttype");

                entity.Property(e => e.Carstatus)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("carstatus")
                    .HasDefaultValueSql("('ACTIVE')");

                entity.Property(e => e.Cartype)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cartype");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cars__userid__3F466844");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.ToTable("purchases");

                entity.Property(e => e.Purchaseid)
                    .HasColumnName("purchaseid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Carid).HasColumnName("carid");

                entity.Property(e => e.Purchasedate)
                    .HasColumnType("datetime")
                    .HasColumnName("purchasedate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.Carid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__purchases__carid__4316F928");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__purchases__useri__440B1D61");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Userphno, "UQ__users__258B3FA1070C171B")
                    .IsUnique();

                entity.HasIndex(e => e.Useremail, "UQ__users__870EAE61B2908EC9")
                    .IsUnique();

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.UserCity)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("userCity");

                entity.Property(e => e.UserRole)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("userRole")
                    .HasDefaultValueSql("('USER')");

                entity.Property(e => e.Useremail)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("useremail");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Userpassword)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("userpassword");

                entity.Property(e => e.Userphno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("userphno");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
