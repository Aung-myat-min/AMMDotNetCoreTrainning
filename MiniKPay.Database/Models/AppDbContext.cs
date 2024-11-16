﻿using System;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-KPCHONN\\SQLEXPRESS;Database=DotNetMiniKPay;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__Tbl_Hist__4D7B4ABD5945AE1B");

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
                .HasConstraintName("FK__Tbl_Histo__FromA__14270015");

            entity.HasOne(d => d.ToAccountNavigation).WithMany(p => p.TblHistoryToAccountNavigations)
                .HasForeignKey(d => d.ToAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Histo__ToAcc__151B244E");
        });

        modelBuilder.Entity<TblPerson>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Tbl_Pers__AA2FFBE55B4AD37A");

            entity.ToTable("Tbl_Person");

            entity.HasIndex(e => e.MobileNo, "UQ__Tbl_Pers__D6D73A86DE5EFE4A").IsUnique();

            entity.Property(e => e.Balance).HasDefaultValueSql("((0))");
            entity.Property(e => e.DeleteFalg).HasDefaultValueSql("((1))");
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