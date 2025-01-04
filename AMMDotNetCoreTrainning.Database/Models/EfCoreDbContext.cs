using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AMMDotNetCoreTrainning.Database.Models;

public partial class EfCoreDbContext : DbContext
{
    public EfCoreDbContext()
    {
    }

    public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBlog> TblBlogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = "Data Source=Aung-Myat-Min\\SQLEXPRESS;Initial Catalog=DotNetTrainning;User Id=sa;Password=sasa@123;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Tbl_Blog");

            entity.Property(e => e.BlogAuthor).HasMaxLength(50);
            entity.Property(e => e.BlogContent).HasMaxLength(50);
            entity.Property(e => e.BlogTitle).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
