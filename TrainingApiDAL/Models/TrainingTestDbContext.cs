using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrainingApiDAL.Models;

public partial class TrainingTestDbContext : DbContext
{
    public TrainingTestDbContext()
    {
    }

    public TrainingTestDbContext(DbContextOptions<TrainingTestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Localhost; Initial Catalog=TrainingDB; User Id=OshaReportableUser2; Password=123456789; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasOne(d => d.Post).WithMany(p => p.Comments).HasConstraintName("FK_Comment_Post");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Posts).HasConstraintName("FK_Post_AppUser");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
