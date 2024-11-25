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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = "Data Source=DESKTOP-KPCHONN\\SQLEXPRESS;Initial Catalog=DotNetMiniKPay;User Id=sa;Password=sasa@123;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__Tbl_Hist__4D7B4ABDCDA5FA37");

            entity.ToTable("Tbl_History");

            entity.Property(e => e.ActionType)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AccountNavigation).WithMany(p => p.TblHistoryAccountNavigations)
                .HasForeignKey(d => d.Account)
                .HasConstraintName("FK__Tbl_Histo__Accou__282DF8C2");

            entity.HasOne(d => d.FromAccountNavigation).WithMany(p => p.TblHistoryFromAccountNavigations)
                .HasForeignKey(d => d.FromAccount)
                .HasConstraintName("FK__Tbl_Histo__FromA__29221CFB");

            entity.HasOne(d => d.ToAccountNavigation).WithMany(p => p.TblHistoryToAccountNavigations)
                .HasForeignKey(d => d.ToAccount)
                .HasConstraintName("FK__Tbl_Histo__ToAcc__2A164134");
        });

        modelBuilder.Entity<TblPerson>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Tbl_Pers__AA2FFBE52799966A");

            entity.ToTable("Tbl_Person");

            entity.HasIndex(e => e.MobileNo, "UQ__Tbl_Pers__D6D73A8662A20FB4").IsUnique();

            entity.Property(e => e.Balance).HasDefaultValueSql("((0))");
            entity.Property(e => e.DeleteFalg).HasDefaultValueSql("((0))");
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
