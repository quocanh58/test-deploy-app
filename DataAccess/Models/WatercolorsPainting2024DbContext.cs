using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Models;

public partial class WatercolorsPainting2024DbContext : DbContext
{
    public WatercolorsPainting2024DbContext()
    {
    }

    public WatercolorsPainting2024DbContext(DbContextOptions<WatercolorsPainting2024DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Style> Styles { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<WatercolorsPainting> WatercolorsPaintings { get; set; }

    //private string GetConnectionString()
    //{
    //    IConfiguration config = new ConfigurationBuilder()
    //        .SetBasePath(Directory.GetCurrentDirectory())
    //        .AddJsonFile("appsettings.json", true, true)
    //        .Build();
    //    var strConn = config["ConnectionStrings:DefaultConnection"];
    //    return strConn;
    //}
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Style>(entity =>
        {
            entity.HasKey(e => e.StyleId).HasName("PK__Style__8AD14640BF393DB9");

            entity.ToTable("Style");

            entity.Property(e => e.StyleId).HasMaxLength(30);
            entity.Property(e => e.OriginalCountry).HasMaxLength(160);
            entity.Property(e => e.StyleDescription).HasMaxLength(250);
            entity.Property(e => e.StyleName).HasMaxLength(100);
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserAccountId).HasName("PK__UserAcco__DA6C70BA6EF8B63C");

            entity.ToTable("UserAccount");

            entity.HasIndex(e => e.UserEmail, "UQ__UserAcco__08638DF84AA05228").IsUnique();

            entity.Property(e => e.UserAccountId)
                .ValueGeneratedNever()
                .HasColumnName("UserAccountID");
            entity.Property(e => e.UserEmail).HasMaxLength(90);
            entity.Property(e => e.UserFullName).HasMaxLength(70);
            entity.Property(e => e.UserPassword).HasMaxLength(50);
        });

        modelBuilder.Entity<WatercolorsPainting>(entity =>
        {
            entity.HasKey(e => e.PaintingId).HasName("PK__Watercol__CF2D90F2FEFE3704");

            entity.ToTable("WatercolorsPainting");

            entity.Property(e => e.PaintingId).HasMaxLength(45);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.PaintingAuthor).HasMaxLength(120);
            entity.Property(e => e.PaintingDescription).HasMaxLength(250);
            entity.Property(e => e.PaintingName).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StyleId).HasMaxLength(30);

            entity.HasOne(d => d.Style).WithMany(p => p.WatercolorsPaintings)
                .HasForeignKey(d => d.StyleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Watercolo__Style__29572725");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
