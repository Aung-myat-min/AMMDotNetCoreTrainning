using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MiniKPay.Database.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblHistory> TblHistories { get; set; }

    public virtual DbSet<TblPerson> TblPeople { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__Tbl_Hist__4D7B4ABD9B31967C");

            entity.ToTable("Tbl_History");

            entity.Property(e => e.ActionType)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.FromAccountNavigation).WithMany(p => p.TblHistoryFromAccountNavigations)
                .HasForeignKey(d => d.FromAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Histo__FromA__7B5B524B");

            entity.HasOne(d => d.ToAccountNavigation).WithMany(p => p.TblHistoryToAccountNavigations)
                .HasForeignKey(d => d.ToAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Histo__ToAcc__7C4F7684");
        });

        modelBuilder.Entity<TblPerson>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Tbl_Pers__AA2FFBE5387E832C");

            entity.ToTable("Tbl_Person");

            entity.HasIndex(e => e.MobileNo, "UQ__Tbl_Pers__D6D73A8622DE53C6").IsUnique();

            entity.Property(e => e.Balance).HasDefaultValueSql("((0))");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Pin)
                .HasMaxLength(6)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
